# Security Policy

Thank you for your interest in the FileEncryptor project and for helping us keep it secure. This document outlines our security standards, supported versions, and the process for reporting vulnerabilities.

## Supported Versions

The following table indicates which versions currently receive security updates. We strongly recommend always using the latest stable release.

| Version | Status |
| :---: | :--- |
| **1.0.x** | :white_check_mark: **Supported** |
| < 1.0 | :x: Unsupported |

## Reporting a Vulnerability

If you believe you have found a security vulnerability in FileEncryptor, please **do not** open a public issue. Publicly disclosing a vulnerability can put the community at risk.

Please follow the "Responsible Disclosure" policy by reporting the issue directly via email:

**📧 Contact:** tmertarin@gmail.com

### What to Include
In your report, please include:
* The specific version of FileEncryptor you are using.
* A description of the vulnerability (e.g., weak encryption implementation, memory handling issue).
* Steps to reproduce the issue (code snippets or screenshots are highly appreciated).

### Response Process
When you submit a report, we will follow this process:
1.  **Acknowledgment:** We will respond to your report within 48 hours.
2.  **Assessment:** We will investigate the report to verify the vulnerability.
3.  **Fix:** Once verified, we will prioritize a patch.
4.  **Release:** A new version (e.g., v1.0.1) will be published with the fix.
5.  **Announcement:** After the release, we will publicly announce the fix and credit you for your contribution (unless you prefer anonymity).

## Out of Scope

Please note that the following scenarios are **not** considered security vulnerabilities of this software:
* Use of weak passwords by the user (e.g., "123456").
* Compromised user devices (e.g., keyloggers, malware, or screen recorders stealing the password).
* Data loss due to forgotten passwords (This is a deliberate design choice; there are no backdoors).
* Social engineering attacks.

## Technical Security Details

This project implements industry-standard cryptographic algorithms to ensure data security:
* **Encryption Algorithm:** AES-256-GCM (Galois/Counter Mode).
* **Key Derivation:** PBKDF2 with HMAC-SHA256.
* **Integrity Check:** The encryption process includes an authentication tag to verify that the file has not been tampered with.

Your security is our priority.
