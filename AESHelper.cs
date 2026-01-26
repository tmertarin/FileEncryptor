using System;
using System.Buffers.Binary;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FileEncryptor
{
    public static class AESHelper
    {
        private static readonly byte[] MAGIC = Encoding.ASCII.GetBytes("MYAESEN2");
        private const byte VERSION = 2;

        private const int SALT_SIZE = 32;
        private const int BASE_NONCE_SIZE = 12;
        private const int TAG_SIZE = 16;
        private const int KEY_SIZE = 32;

        private const int DEFAULT_ITERATIONS = 200_000;
        private const int CHUNK_SIZE = 1024 * 1024;

        public static void EncryptFile(string inputFile, string password)
        {
            byte[] pw = Encoding.UTF8.GetBytes(password);
            try
            {
                EncryptInternal(inputFile, pw);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(pw);
            }
        }

        public static void DecryptFile(string inputFile, string password)
        {
            byte[] pw = Encoding.UTF8.GetBytes(password);
            try
            {
                DecryptInternal(inputFile, pw);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(pw);
            }
        }

        private static void EncryptInternal(string inputFile, byte[] password)
        {
            string outputFile = inputFile + ".aes";

            byte[] salt = RandomBytes(SALT_SIZE);
            byte[] baseNonce = RandomBytes(BASE_NONCE_SIZE);

            byte[] masterKey = DeriveKey(password, salt, DEFAULT_ITERATIONS);
            byte[] fileKey = HKDF_Expand(masterKey, "FILE_KEY");

            using var input = new FileStream(inputFile, FileMode.Open, FileAccess.Read);
            using var output = new FileStream(outputFile, FileMode.Create, FileAccess.Write);

            WriteHeader(output, DEFAULT_ITERATIONS, salt, baseNonce);
            byte[] aad = BuildAAD(DEFAULT_ITERATIONS, salt, baseNonce);

            using var aes = new AesGcm(fileKey, TAG_SIZE);

            byte[] plain = new byte[CHUNK_SIZE];
            byte[] cipher = new byte[CHUNK_SIZE];
            byte[] tag = new byte[TAG_SIZE];

            uint counter = 0;
            int read = 0;

            for (;;)
            {
                read = input.Read(plain, 0, plain.Length);
                if (read <= 0) break;

                byte[] nonce = DeriveNonce(baseNonce, counter);
                counter = unchecked(counter + 1);

                aes.Encrypt(
                    nonce,
                    plain.AsSpan(0, read),
                    cipher.AsSpan(0, read),
                    tag,
                    aad
                );

                WriteInt(output, read);

                int offset = 0;
                while (offset < read)
                {
                    int toWrite = read - offset;
                    output.Write(cipher, offset, toWrite);
                    offset += toWrite;
                }

                output.Write(tag, 0, tag.Length);
            }

            CryptographicOperations.ZeroMemory(masterKey);
            CryptographicOperations.ZeroMemory(fileKey);
            CryptographicOperations.ZeroMemory(plain);
            CryptographicOperations.ZeroMemory(cipher);
        }

        private static void DecryptInternal(string inputFile, byte[] password)
        {
            string outputFile = inputFile.EndsWith(".aes")
                ? inputFile[..^4]
                : inputFile + ".decrypted";

            using var input = new FileStream(inputFile, FileMode.Open, FileAccess.Read);
            using var output = new FileStream(outputFile, FileMode.Create, FileAccess.Write);

            ReadHeader(input, out int iter, out byte[] salt, out byte[] baseNonce);
            byte[] aad = BuildAAD(iter, salt, baseNonce);

            byte[] masterKey = DeriveKey(password, salt, iter);
            byte[] fileKey = HKDF_Expand(masterKey, "FILE_KEY");

            using var aes = new AesGcm(fileKey, TAG_SIZE);

            uint counter = 0;

            while (true)
            {
                if (input.Position >= input.Length) break;

                int len = ReadInt(input);
                if (len < 0) throw new CryptographicException();

                byte[] cipher = new byte[len];
                byte[] plain = new byte[len];
                byte[] tag = new byte[TAG_SIZE];

                input.ReadExactly(cipher, 0, cipher.Length);
                input.ReadExactly(tag, 0, tag.Length);

                byte[] nonce = DeriveNonce(baseNonce, counter);
                counter = unchecked(counter + 1);

                aes.Decrypt(nonce, cipher, tag, plain, aad);

                int written = 0;
                while (written < plain.Length)
                {
                    int toWrite = plain.Length - written;
                    output.Write(plain, written, toWrite);
                    written += toWrite;
                }

                CryptographicOperations.ZeroMemory(cipher);
                CryptographicOperations.ZeroMemory(plain);
            }

            CryptographicOperations.ZeroMemory(masterKey);
            CryptographicOperations.ZeroMemory(fileKey);
        }

        private static byte[] DeriveKey(byte[] pw, byte[] salt, int iter)
        {
            byte[] result = Rfc2898DeriveBytes.Pbkdf2(
                pw,
                salt,
                iter,
                HashAlgorithmName.SHA256,
                KEY_SIZE
            );
            return result;
        }

        private static byte[] HKDF_Expand(byte[] ikm, string info)
        {
            using var hmac = new HMACSHA256(ikm);
            byte[] data = Encoding.UTF8.GetBytes(info + "\x01");
            byte[] t = hmac.ComputeHash(data);
            if (t.Length == KEY_SIZE) return t;
            byte[] r = new byte[KEY_SIZE];
            Buffer.BlockCopy(t, 0, r, 0, KEY_SIZE);
            return r;
        }

        private static byte[] DeriveNonce(byte[] baseNonce, uint counter)
        {
            byte[] n = new byte[baseNonce.Length];
            Buffer.BlockCopy(baseNonce, 0, n, 0, baseNonce.Length);
            Span<byte> tail = n.AsSpan(n.Length - 4, 4);
            BinaryPrimitives.WriteUInt32BigEndian(tail, counter);
            return n;
        }

        private static byte[] BuildAAD(int iter, byte[] salt, byte[] nonce)
        {
            using var ms = new MemoryStream();
            ms.Write(MAGIC, 0, MAGIC.Length);
            ms.WriteByte(VERSION);
            WriteInt(ms, iter);

            int i = 0;
            while (i < salt.Length)
            {
                ms.WriteByte(salt[i]);
                i++;
            }

            i = 0;
            while (i < nonce.Length)
            {
                ms.WriteByte(nonce[i]);
                i++;
            }

            return ms.ToArray();
        }

        private static void WriteHeader(Stream s, int iter, byte[] salt, byte[] nonce)
        {
            s.Write(MAGIC, 0, MAGIC.Length);
            s.WriteByte(VERSION);
            WriteInt(s, iter);
            s.Write(salt, 0, salt.Length);
            s.Write(nonce, 0, nonce.Length);
            WriteInt(s, CHUNK_SIZE);
        }

        private static void ReadHeader(Stream s, out int iter, out byte[] salt, out byte[] nonce)
        {
            byte[] m = new byte[MAGIC.Length];
            s.ReadExactly(m, 0, m.Length);

            if (!CryptographicOperations.FixedTimeEquals(m, MAGIC))
                throw new CryptographicException("Invalid file."); // Changed from "Geçersiz dosya."

            int v = s.ReadByte();
            if (v != VERSION)
                throw new CryptographicException("Version mismatch."); // Changed from "Sürüm uyumsuz."

            iter = ReadInt(s);

            salt = new byte[SALT_SIZE];
            nonce = new byte[BASE_NONCE_SIZE];

            s.ReadExactly(salt, 0, salt.Length);
            s.ReadExactly(nonce, 0, nonce.Length);

            _ = ReadInt(s);
        }

        private static void WriteInt(Stream s, int v)
        {
            Span<byte> b = stackalloc byte[4];
            BinaryPrimitives.WriteInt32BigEndian(b, v);
            s.Write(b);
        }

        private static int ReadInt(Stream s)
        {
            Span<byte> b = stackalloc byte[4];
            s.ReadExactly(b);
            int v = BinaryPrimitives.ReadInt32BigEndian(b);
            return v;
        }

        private static byte[] RandomBytes(int len)
        {
            byte[] b = new byte[len];
            RandomNumberGenerator.Fill(b);
            if (b.Length != len) throw new CryptographicException();
            return b;
        }
    }
}
