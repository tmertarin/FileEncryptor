# FileEncryptor 🛡️

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](LICENSE)  
[![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey.svg)]  

**FileEncryptor**, dosyalarınızı güvenli bir şekilde şifrelemenizi ve çözmenizi sağlayan modern bir Windows uygulamasıdır. Basit arayüzü ve güçlü şifreleme yöntemleri ile hem kullanım kolaylığı hem de yüksek güvenlik sunar.

---

## ⚡ Öne Çıkan Özellikler

- **AES-GCM 256-bit** ile güçlü şifreleme  
- **PBKDF2** ile parola türetme (brute-force dirençli)  
- Dosya başına benzersiz **salt ve nonce**  
- **Chunk tabanlı şifreleme**: büyük dosyalarda performans optimizasyonu  
- Dosya sürükle-bırak desteği  
- Basit ve anlaşılır kullanıcı arayüzü  
- İşlem logları ve hataları görüntüleme  

---

## 📝 Kullanım

1. Programı başlatın.  
2. Şifrelemek veya çözmek istediğiniz dosyayı seçin (sürükle-bırak veya gözat).  
3. Parolanızı girin.  
4. `ŞİFRELE` veya `ŞİFRE ÇÖZ` butonuna tıklayın.  
5. İşlem tamamlandığında log ekranında başarı mesajını görebilirsiniz.  

> Şifreleme sonrası dosya `.aes` uzantısı ile kaydedilir. Şifre çözme sonrası uzantı orijinal hâline döner.

---

## 🔒 Güvenlik

- **AES-GCM**: Modern ve güvenli şifreleme algoritması.  
- **PBKDF2**: Parola tabanlı anahtar türetme ile brute-force saldırılarına direnç.  
- **Salt ve Nonce**: Her dosya için benzersiz, aynı parola farklı sonuç üretir.  
- **Chunk tabanlı şifreleme**: Büyük dosyalar güvenli şekilde işlenir.

---

## 📜 Lisans

Bu proje **GNU General Public License v3.0 (GPL-3.0)** ile lisanslanmıştır.  
- Kodları kullanabilir, değiştirebilir ve dağıtabilirsiniz.  
- Türev projelerde kaynak ve telif bildirimlerini korumak zorunludur.  
- Detaylar için [LICENSE](LICENSE) dosyasına bakınız.

---

## 🤝 Katkıda Bulunma

Katkılar memnuniyetle karşılanır:  
- Hata bildirimleri  
- Yeni özellik önerileri  
- Kod katkıları  

Lütfen **Pull Request** veya **Issue** açarak katkıda bulunun.

---

## 🙏 Teşekkür

- Bu proje **C# ve .NET 10** kullanılarak geliştirilmiştir.  
- Modern şifreleme standartlarına uygun güvenlik ve performans yapısı içerir.
