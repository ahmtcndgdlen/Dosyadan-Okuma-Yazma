using System;
using System.IO;
using System.Linq;

namespace ConsoleApp19
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] kitaplar = { "saftirik1,Jeff Kinney", "saftirik2,Jeff Kinney", "saftirik3,Jeff Kinney" };
            File.WriteAllLines(@"c:\DENEME\kitaplar.txt", kitaplar); // txt dosyasına metin yazma



            girisekranı ekran = new girisekranı
            {
                AdminSifre = 123
            };

            Console.WriteLine("Bir seçim yapın \n1 : Admin girişi \n2 : Kullanıcı girişi");

            int secim = Convert.ToInt32(Console.ReadLine());
            if (secim == 1)
            {
                Console.WriteLine("Şifre giriniz:");
                var girilenSifre = Convert.ToInt32(Console.ReadLine());

                if (ekran.SifreKontrol(girilenSifre))
                {
                    Console.WriteLine("Admin girişi başarılı!");
                    AdminMenusu();
                }
                else
                {
                    Console.WriteLine("Hatalı şifre, ana ekrana dönülüyor.");
                }
            }
            else if (secim == 2)
            {
                Console.WriteLine("Kullanıcı adınızı giriniz:");
                var kullaniciAdi = Console.ReadLine();
                KullaniciMenusu(kullaniciAdi);
            }
            else
            {
                Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
            }
        }

        static void AdminMenusu()
        {
            while (true)
            {
                Console.WriteLine("\nAdmin Menüsü:");
                Console.WriteLine("1 - Kitap Ekle");
                Console.WriteLine("2 - Kitap Sil");
                Console.WriteLine("3 - Kitapları Listele");
                Console.WriteLine("0 - Ana Menüye Dön");

                int adminSecim = Convert.ToInt32(Console.ReadLine());

                if (adminSecim == 1)
                {
                    KitapEkle();
                }
                else if (adminSecim == 2)
                {
                    KitapSil();
                }
                else if (adminSecim == 3)
                {
                    KitaplariListele();
                }
                else if (adminSecim == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                }
            }
        }

        static void KullaniciMenusu(string kullaniciAdi)
        {
            while (true)
            {
                Console.WriteLine($"\nKullanıcı Menüsü ({kullaniciAdi}):");
                Console.WriteLine("1 - Kitap Al");
                Console.WriteLine("2 - Kitap Bırak");
                Console.WriteLine("3 - Kitapları Görüntüle");
                Console.WriteLine("4 - Aldığınız Kitapları Görüntüleyin");
                Console.WriteLine("0 - Ana Menüye Dön");

                int kullaniciSecim = Convert.ToInt32(Console.ReadLine());

                if (kullaniciSecim == 1)
                {
                    KitapAl(kullaniciAdi);
                }
                else if (kullaniciSecim == 2)
                {
                    KitapBirak(kullaniciAdi);
                }
                else if (kullaniciSecim == 3)
                {
                    KitaplariListele();
                }
                else if (kullaniciSecim == 4)
                {
                    KullaniciKitaplariniGoruntule(kullaniciAdi); 
                }
                else if (kullaniciSecim == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                }
            }
        }

        static void KitaplariListele()
        {
            if (File.Exists("c:\\DENEME\\kitaplar.txt"))
            {
                var kitaplar = File.ReadAllLines("c:\\DENEME\\kitaplar.txt");// txt dosyasından metin okuma
                Console.WriteLine("Kitaplar Listesi:");
                foreach (var kitap in kitaplar)
                {
                    var bilgiler = kitap.Split(',');
                    Console.WriteLine($"Kitap Adı: {bilgiler[0]}, Yazar: {bilgiler[1]}");
                }
            }
            else
            {
                Console.WriteLine("Kitaplar dosyası bulunamadı.");
            }
        }

        static void KitapAl(string kullaniciAdi)
        {
            Console.Write("Almak istediğiniz kitabın adı: ");
            string kitapAdi = Console.ReadLine();

            var kitaplar = File.ReadAllLines("c:\\DENEME\\kitaplar.txt");
            var kitap = kitaplar.FirstOrDefault(k => k.Split(',')[0] == kitapAdi);

            if (kitap != null)
            {
                using (StreamWriter sw = new StreamWriter("c:\\DENEME\\kiralanan_kitaplar.txt", true))
                {
                    sw.WriteLine($"{kitap},{kullaniciAdi}");
                }
                Console.WriteLine("Kitap başarıyla alındı.");
            }
            else
            {
                Console.WriteLine("Kitap bulunamadı.");
            }
        }

        static void KitapEkle()
        {
            Console.Write("Kitap adı: ");
            string kitapAdi = Console.ReadLine();
            Console.Write("Yazarı: ");
            string yazar = Console.ReadLine();
            string id = Guid.NewGuid().ToString();

            using (StreamWriter sw = new StreamWriter("c:\\DENEME\\kitaplar.txt", true))
            {
                sw.WriteLine($"{kitapAdi},{yazar}");
            }
            Console.WriteLine("Kitap başarıyla eklendi.");
        }
        
        static void KitapSil()
        {
            Console.Write("Silinecek kitabın adı: ");
            string kitapAdi = Console.ReadLine();

            var kitaplar = File.ReadAllLines("c:\\DENEME\\kitaplar.txt").ToList();
            var kitap = kitaplar.FirstOrDefault(k => k.Split(',')[0] == kitapAdi);

            if (kitap != null)
            {
                kitaplar.Remove(kitap);
                File.WriteAllLines("c:\\DENEME\\kitaplar.txt", kitaplar);
                Console.WriteLine("Kitap başarıyla silindi.");
            }
            else
            {
                Console.WriteLine("Kitap bulunamadı.");
            }
        }

        static void KitapBirak(string kullaniciAdi)
        {
            Console.Write("Bırakmak istediğiniz kitabın adı: ");
            string kitapAdi = Console.ReadLine();

            if (File.Exists("c:\\DENEME\\kiralanan_kitaplar.txt"))
            {
                var kiralananKitaplar = File.ReadAllLines("c:\\DENEME\\kiralanan_kitaplar.txt").ToList();
                var kitap = kiralananKitaplar.FirstOrDefault(k => k.Split(',')[0] == kitapAdi && k.Split(',')[2] == kullaniciAdi);

                if (kitap != null)
                {
                    kiralananKitaplar.Remove(kitap);
                    File.WriteAllLines("c:\\DENEME\\kiralanan_kitaplar.txt", kiralananKitaplar);
                    Console.WriteLine("Kitap başarıyla bırakıldı.");
                }
                else
                {
                    Console.WriteLine("Kitap bulunamadı veya size ait değil.");
                }
            }
            else
            {
                Console.WriteLine("Hiç kitap kiralamadınız.");
            }
        }
        static void KullaniciKitaplariniGoruntule(string kullaniciAdi)
        {
            if (File.Exists("c:\\DENEME\\kiralanan_kitaplar.txt"))
            {
                var kiralananKitaplar = File.ReadAllLines("c:\\DENEME\\kiralanan_kitaplar.txt");
                var kullaniciKitaplari = kiralananKitaplar.Where(k => k.Split(',')[2] == kullaniciAdi).ToList();

                if (kullaniciKitaplari.Any())
                {
                    Console.WriteLine($"\n{kullaniciAdi} tarafından alınan kitaplar:");
                    foreach (var kitap in kullaniciKitaplari)
                    {
                        var bilgiler = kitap.Split(',');
                        Console.WriteLine($"Kitap Adı: {bilgiler[0]}, Yazar: {bilgiler[1]}");
                    }
                }
                else
                {
                    Console.WriteLine("Alınan kitap yok.");
                }
            }
            else
            {
                Console.WriteLine("Hiç kitap kiralanmamış.");
            }
        }
    }
}
    
