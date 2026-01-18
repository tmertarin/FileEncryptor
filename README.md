<div align="center">

# FileEncryptor 🛡️

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](LICENSE)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078D6?logo=windows&logoColor=white)](https://www.microsoft.com/windows)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Status](https://img.shields.io/badge/Status-Active-success.svg)]()

**A modern, robust, and user-friendly Windows application for secure file encryption.** FileEncryptor combines military-grade security with simplicity, ensuring your data remains private and protected.

[Report Bug](https://github.com/USERNAME/REPO/issues) · [Request Feature](https://github.com/USERNAME/REPO/issues)

</div>

---

## ⚡ Key Features

FileEncryptor is built with performance and security in mind:

* **🔒 AES-GCM 256-bit Encryption:** Industry-standard authenticated encryption for confidentiality and integrity.
* **🔑 PBKDF2 Key Derivation:** High-iteration password hashing to resist brute-force attacks.
* **🛡️ Unique Salt & Nonce:** Generates random cryptographic parameters for every file; encrypting the same file twice produces different results.
* **🚀 Chunk-Based Processing:** Optimized for memory efficiency. Encrypts multi-gigabyte files without overloading RAM.
* **🖱️ Drag & Drop Support:** Seamlessly drag files into the UI for quick processing.
* **📝 Activity Logs:** Real-time logging of encryption/decryption status and errors.

---

## 🛠️ Tech Stack

This project leverages the latest capabilities of the .NET ecosystem:

| Component | Technology |
| :--- | :--- |
| **Framework** | C# / .NET 10 |
| **Encryption** | `System.Security.Cryptography` |
| **Algorithm** | AES (GCM Mode) |
| **Key Derivation** | RFC 2898 (PBKDF2) |

---

## 📝 Usage

Using FileEncryptor is straightforward:

1.  **Launch** the application.
2.  **Select a File** by clicking the browse button or simply **drag and drop** the file into the window.
3.  **Enter a Password**. Make sure it's strong!
4.  Click **`ENCRYPT`** or **`DECRYPT`**.
5.  Check the log window for the "Success" message.

> [!NOTE]
> **Encryption:** The output file will have a `.aes` extension (e.g., `document.pdf.aes`).
> **Decryption:** The application restores the file to its original extension.

---

## 🔒 Security Architecture

We take security seriously. Here is how we protect your data:

-   **Algorithm:** We use **AES-GCM (Galois/Counter Mode)**. Unlike older modes (like CBC), GCM provides both encryption and data integrity verification.
-   **Key Derivation:** Your password is never used directly as the encryption key. We use **PBKDF2** with a random salt to derive a cryptographic key, making dictionary attacks extremely difficult.
-   **Randomness:** A unique **Nonce (IV)** and **Salt** are generated for every single operation and prepended to the encrypted file securely.

---

## 🤝 Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1.  Fork the Project
2.  Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3.  Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4.  Push to the Branch (`git push origin feature/AmazingFeature`)
5.  Open a Pull Request

---

## 📜 License

Distributed under the **GNU General Public License v3.0**. See `LICENSE` for more information.

* ✅ Commercial use
* ✅ Modification
* ✅ Distribution
* ❌ Sublicensing
* ❌ Liability

---

<div align="center">
  <sub>Built with ❤️ using C# and .NET 10</sub>
</div>