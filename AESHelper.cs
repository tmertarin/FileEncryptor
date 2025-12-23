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

        /* ================= PUBLIC API ================= */

        public static void EncryptFile(string inputFile, string password)
        {
            byte[] pw = Encoding.UTF8.GetBytes(password);
            try { EncryptInternal(inputFile, pw); }
            finally { CryptographicOperations.ZeroMemory(pw); }
        }

        public static void DecryptFile(string inputFile, string password)
        {
            byte[] pw = Encoding.UTF8.GetBytes(password);
            try { DecryptInternal(inputFile, pw); }
            finally { CryptographicOperations.ZeroMemory(pw); }
        }

        /* ================= ENCRYPT ================= */

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
            int read;

            while ((read = input.Read(plain, 0, plain.Length)) > 0)
            {
                byte[] nonce = DeriveNonce(baseNonce, counter++);
                aes.Encrypt(nonce, plain.AsSpan(0, read), cipher.AsSpan(0, read), tag, aad);

                WriteInt(output, read);
                output.Write(cipher, 0, read);
                output.Write(tag);
            }

            CryptographicOperations.ZeroMemory(masterKey);
            CryptographicOperations.ZeroMemory(fileKey);
            CryptographicOperations.ZeroMemory(plain);
            CryptographicOperations.ZeroMemory(cipher);
        }

        /* ================= DECRYPT ================= */

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

            while (input.Position < input.Length)
            {
                int len = ReadInt(input);

                byte[] cipher = new byte[len];
                byte[] plain = new byte[len];
                byte[] tag = new byte[TAG_SIZE];

                input.ReadExactly(cipher);
                input.ReadExactly(tag);

                byte[] nonce = DeriveNonce(baseNonce, counter++);
                aes.Decrypt(nonce, cipher, tag, plain, aad);

                output.Write(plain);

                CryptographicOperations.ZeroMemory(cipher);
                CryptographicOperations.ZeroMemory(plain);
            }

            CryptographicOperations.ZeroMemory(masterKey);
            CryptographicOperations.ZeroMemory(fileKey);
        }

        /* ================= CRYPTO ================= */

        private static byte[] DeriveKey(byte[] pw, byte[] salt, int iter)
            => Rfc2898DeriveBytes.Pbkdf2(pw, salt, iter, HashAlgorithmName.SHA256, KEY_SIZE);

        // RFC 5869 uyumlu HKDF Expand
        private static byte[] HKDF_Expand(byte[] ikm, string info)
        {
            using var hmac = new HMACSHA256(ikm);
            byte[] t = hmac.ComputeHash(Encoding.UTF8.GetBytes(info + "\x01"));
            return t[..KEY_SIZE];
        }

        private static byte[] DeriveNonce(byte[] baseNonce, uint counter)
        {
            byte[] n = (byte[])baseNonce.Clone();
            BinaryPrimitives.WriteUInt32BigEndian(n.AsSpan(^4), counter);
            return n;
        }

        private static byte[] BuildAAD(int iter, byte[] salt, byte[] nonce)
        {
            using var ms = new MemoryStream();
            ms.Write(MAGIC);
            ms.WriteByte(VERSION);
            WriteInt(ms, iter);
            ms.Write(salt);
            ms.Write(nonce);
            return ms.ToArray();
        }

        /* ================= IO ================= */

        private static void WriteHeader(Stream s, int iter, byte[] salt, byte[] nonce)
        {
            s.Write(MAGIC);
            s.WriteByte(VERSION);
            WriteInt(s, iter);
            s.Write(salt);
            s.Write(nonce);
            WriteInt(s, CHUNK_SIZE);
        }

        private static void ReadHeader(Stream s, out int iter, out byte[] salt, out byte[] nonce)
        {
            byte[] m = new byte[MAGIC.Length];
            s.ReadExactly(m);
            if (!CryptographicOperations.FixedTimeEquals(m, MAGIC))
                throw new CryptographicException("Geçersiz dosya.");

            if (s.ReadByte() != VERSION)
                throw new CryptographicException("Sürüm uyumsuz.");

            iter = ReadInt(s);
            salt = new byte[SALT_SIZE];
            nonce = new byte[BASE_NONCE_SIZE];

            s.ReadExactly(salt);
            s.ReadExactly(nonce);
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
            return BinaryPrimitives.ReadInt32BigEndian(b);
        }

        private static byte[] RandomBytes(int len)
        {
            byte[] b = new byte[len];
            RandomNumberGenerator.Fill(b);
            return b;
        }
    }
}
