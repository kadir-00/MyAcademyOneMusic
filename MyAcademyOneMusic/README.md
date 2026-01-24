# 🎵 OneMusic - Dijital Müzik Platformu

![.NET Core](https://img.shields.io/badge/.NET%20Core-8.0-512BD4)
![EF Core](https://img.shields.io/badge/Entity%20Framework-Code%20First-512BD4)
![Bootstrap](https://img.shields.io/badge/Frontend-Bootstrap-563D7C)
![Status](https://img.shields.io/badge/Status-Completed-success)

**OneMusic**, sanatçıların eserlerini dijital dünyayla buluşturduğu, müzikseverlerin ise bu eserleri keşfedip dinleyebildiği dinamik bir web platformudur.

Bu proje, **M&Y Yazılım Eğitim Akademi Danışmanlık** bünyesindeki *Full Stack .Net Core Development Bootcamp*'i kapsamında, **Erhan Gündüz** eğitmenliğinde ve **Murat Yücedağ** mentörlüğünde geliştirdiğim 3. projedir.

---

## 📸 Proje Önizlemesi

![OneMusic Ana Sayfa](screenshots/anasayfa.png)

---

## 🚀 Proje Amacı ve İşleyişi

OneMusic, sanatçı ve dinleyiciyi tek bir çatı altında toplar. Sistem; sanatçıların albüm ve parça yükleyebildiği, adminlerin platformu yönettiği ve son kullanıcıların (dinleyicilerin) müzik deneyimi yaşadığı 3 temel saç ayağı üzerine kurulmuştur.

### 1. 🎤 Sanatçı Paneli
Sanatçılar için özelleştirilmiş yönetim arayüzüdür.
* **Kayıt ve Yetkilendirme:** Sisteme üye olan sanatçılar, Admin onayı ve rol ataması (Identity) sonrası panele erişim sağlar.
* **İçerik Yönetimi:** Kendi albümlerini oluşturabilir, albüm kapaklarını ve müzik dosyalarını (.mp3 vb.) sisteme yükleyebilirler.

<p align="center">
  <img src="screenshots/artist-panel.png" alt="Sanatçı Paneli" width="80%">
</p>

### 2. 🎧 Kullanıcı (Dinleyici) Arayüzü (UI)
Müzikseverlerin etkileşime girdiği ön yüzdür.
* **Keşfet:** En hit parçalar, popüler albümler ve sanatçılar listelenir.
* **Deneyim:** Kullanıcılar albüm detaylarına girip şarkıları doğrudan tarayıcı üzerinden dinleyebilir.

<p align="center">
  <img src="screenshots/music-player.png" alt="Müzik Çalar Arayüzü" width="80%">
</p>

### 3. 🛡️ Admin Paneli
Platformun genel yönetim merkezidir.
* **Tam Kontrol:** Site üzerindeki tüm içerikleri (Albüm, Şarkı, Kategori) düzenleme ve silme yetkisi.
* **Kullanıcı Yönetimi:** Rol atamaları (Sanatçı/Üye) ve kullanıcı işlemleri buradan yürütülür.

---

## 🏗️ Mimari ve Katmanlar

Proje, sürdürülebilirlik ve modülerlik adına **N-Tier Architecture (N Katmanlı Mimari)** prensiplerine uygun olarak geliştirilmiştir.

* 📂 **OneMusic.EntityLayer:** Veritabanı tablolarına karşılık gelen somut sınıflar.
* 📂 **OneMusic.DataAccessLayer:** Veritabanı bağlantıları, Repository desenleri ve Context yapısı.
* 📂 **OneMusic.BusinessLayer:** Validasyon kuralları ve iş mantığının işlendiği katman.
* 📂 **OneMusic.WebUI:** Kullanıcının etkileşime girdiği Controller ve View katmanı.

---

## 🛠 Kullanılan Teknolojiler

Bu projede modern web geliştirme standartları ve .NET ekosisteminin güçlü araçları kullanılmıştır:

* **Backend:** ASP.NET Core 8.0, LINQ
* **Veritabanı & ORM:** MSSQL Server, Entity Framework Core (Code First)
* **Güvenlik & Kimlik:** ASP.NET Core Identity (Login, Register, Role Management)
* **Validasyon:** Fluent Validation
* **Frontend:** HTML5, CSS3, Bootstrap
* **Tasarım Deseni:** Repository Design Pattern

---

## ⭐ Teknik Özellikler

* ✅ **Dosya Yönetimi:** Bilgisayardan sunucuya resim ve müzik dosyası upload işlemleri (`IFormFile`).
* ✅ **İlişkisel Veri:** Sanatçı -> Albüm -> Şarkı hiyerarşisinin Entity Framework ile yönetimi.
* ✅ **Rol Bazlı Erişim (RBAC):** Admin, Sanatçı ve Ziyaretçi rolleri için `[Authorize]` ile sayfa ve aksiyon bazlı kısıtlamalar.
* ✅ **CRUD Operasyonları:** Tüm veri tipleri için Ekleme, Okuma, Güncelleme ve Silme yetenekleri.
* ✅ **Profil Yönetimi:** Kullanıcıların kendi bilgilerini güncelleyebilmesi.

---

## ⚙️ Kurulum

Projeyi yerel makinenizde çalıştırmak için:

1. Projeyi klonlayın.
2. `appsettings.json` dosyasındaki Connection String'i kendi SQL sunucunuza göre düzenleyin.
3. **Package Manager Console** üzerinden migration işlemlerini uygulayın:
   ```powershell
   Update-Database