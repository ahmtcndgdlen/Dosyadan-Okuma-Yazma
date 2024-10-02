using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp19
{
    internal class girisekranı
    {
        public string AdminKullaniciAdi { get; set; } = "admin";

        public string kullanici { get; set; }

        public string kullaniciadi { get; set; }

        public string adminKullaniciAdi { get; set; }
        public int AdminSifre { get; set; } = 123;

        public string KitapAl {  get; set; }

        
        internal bool SifreKontrol(int girilenSifre)
        {
            return girilenSifre == AdminSifre;
        }


      
    }
}
 