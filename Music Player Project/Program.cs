using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPlayerProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)   //Başlangıç Arayüzü
            {
                Console.Clear();
                Console.WriteLine("****** Müzik Çalar Arayüzü Projesi ******");
                Console.WriteLine();
                Console.WriteLine("1- Kayıtlı Şarkıları Görüntüle");
                Console.WriteLine("2- Şarkı İşlemleri");
                Console.WriteLine("3- Çalma Listesi");
                Console.WriteLine("4- Çıkış");
                Console.WriteLine("******   ******   ******");

                Console.WriteLine("Seçmek istediğiniz İşlemin Sayısını Giriniz: ");
                int arayuzSecenegi = int.Parse(Console.ReadLine());

                switch (arayuzSecenegi)
                {
                    case 1:
                        kayitliSarkilar();
                        break;
                    case 2:
                        sarkiIslemleri();
                        break;
                    case 3:
                        calmaListesi();
                        break;
                    case 4:
                        Console.WriteLine("Çıkış yapılıyor...");
                        return;
                    default:
                        Console.WriteLine("Hatalı seçim! Devam etmek için bir tuşa basın.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void kayitliSarkilar()   //1.Seçeneğe ait Method
        {
            Console.Clear();
            Console.WriteLine("****** Kayıtlı Şarkı İşlemleri ******");
            Console.WriteLine("1- Kayıtlı Şarkıları Alfabetik Olarak Sırala");
            Console.WriteLine("2- Kayıtlı Şarkıları Türüne Göre Sırala");
            Console.WriteLine("3- Çıkış");

            Console.WriteLine("Seçmek istediğiniz işlemin sayısını giriniz: ");
            int kayitliSarkiSecimi = int.Parse(Console.ReadLine());

            switch (kayitliSarkiSecimi)
            {
                case 1:
                    Console.WriteLine("****** Alfabetik Sıralama ******");
                    Console.WriteLine();
                    List<Sarki> siraliSarkilar = muzikler().OrderBy(s => s.SarkiAdi).ToList();   //Listedeki Şarkıların .OrderBy komutu ile alfabetik sıralanıp (s =>s) ardından listelenmesi
                    foreach (var sarki in siraliSarkilar)
                    {
                        Console.WriteLine($"{sarki.SarkiAdi} - {sarki.SarkiciAdi}");
                    }
                    break;

                case 2:
                    Console.WriteLine("****** Türüne Göre Sıralama ******"); //Listedeki Şarkıların  
                    Console.WriteLine();
                    Console.WriteLine("Hangi türdeki şarkıları görmek istersiniz? (Pop, Rock, vb.): ");
                    string secilenTur = Console.ReadLine();

                    List<Sarki> turuneGoreSirali = muzikler()
                        .Where(s => s.Tur.Equals(secilenTur, StringComparison.OrdinalIgnoreCase))   //.Where komutuyla "Tur" içerisinde istenilen şarkı türünün filtrelenmesi. 
                                                                                                    //.Equals Komutu istenilen Türün Olup Olmadığının Kontrolü
                                                                                                    //StringComparison.OrdinalIgnoreCase Büyük Küçük Harf Duyarlılığının Kaldırılması
                        .ToList();

                    if (turuneGoreSirali.Any())   //Kullanıcıdan alınan "Tür"de şarkı varsa Sıralanıp Listelenmesi (Pop'ta şarkı kayıtlı mı?)
                    {
                        foreach (var sarki in turuneGoreSirali)
                        {
                            Console.WriteLine($"{sarki.Tur}: {sarki.SarkiAdi} - {sarki.SarkiciAdi}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Seçilen türde şarkı bulunmamaktadır.");
                    }
                    break;

                case 3:
                    return;

                default:
                    Console.WriteLine("Hatalı seçim! Devam etmek için bir tuşa basın.");
                    Console.ReadKey();
                    break;
            }

            Console.WriteLine("Devam etmek için bir tuşa basın...");
            Console.ReadKey();
        }

        static void sarkiIslemleri()   //2. Seçeneğe ait Method
        {
            Console.Clear();
            Console.WriteLine("****** Şarkı İşlemleri ******");
            Console.WriteLine("1- Şarkı Ekle");
            Console.WriteLine("2- Şarkı Sil");
            Console.WriteLine("3- Çıkış");

            Console.WriteLine("Seçmek istediğiniz İşlemin Sayısını Giriniz: ");
            int sarkiIslemleriSecimi = int.Parse(Console.ReadLine());

            switch (sarkiIslemleriSecimi)
            {
                case 1:
                    Console.WriteLine("****** Şarkı Ekleme ******");   //Şarkı Eklemek İçin Kullanıcıdan Veri Girişi
                    Console.Write("Şarkı adı: ");
                    string sarkiAdi = Console.ReadLine();
                    Console.Write("Sanatçı adı: ");
                    string sarkiciAdi = Console.ReadLine();
                    Console.Write("Tür: ");
                    string tur = Console.ReadLine();

                    tumSarkilar.Add(new Sarki { SarkiAdi = sarkiAdi, SarkiciAdi = sarkiciAdi, Tur = tur });   //Tüm Şarkılar Listesine Alınan Verilerle Yeni Şarkının Eklenmesi (.Add)
                    Console.WriteLine("Şarkı eklendi. Devam etmek için bir tuşa basın.");
                    Console.ReadKey();
                    break;

                case 2:
                    Console.WriteLine("****** Şarkı Sil ******");   //Tüm Şarkıların Listelenip "secim"in listedeki numaralardan birine ait olması halinde listeden silinmesi (.RemoveAt)
                    for (int i = 0; i < tumSarkilar.Count; i++)
                    {
                        Console.WriteLine(i + 1 + "." + tumSarkilar[i].SarkiAdi + " - " + tumSarkilar[i].SarkiciAdi);
                    }
                    Console.Write("Silmek istediğiniz şarkının numarasını girin: ");
                    if (int.TryParse(Console.ReadLine(), out int secim) && secim > 0 && secim <= tumSarkilar.Count)
                    {
                        tumSarkilar.RemoveAt(secim - 1);
                        Console.WriteLine("Şarkı silindi. Devam etmek için bir tuşa basın.");
                    }
                    else
                    {
                        Console.WriteLine("Hatalı seçim. Devam etmek için bir tuşa basın.");
                    }
                    Console.ReadKey();
                    break;

                case 3:
                    return;

                default:
                    Console.WriteLine("Hatalı seçim! Devam etmek için bir tuşa basın.");
                    Console.ReadKey();
                    break;
            }
        }

        static void calmaListesi()   //3. Seçeneğe ait Method
        {
            Console.Clear();
            Console.WriteLine("****** Çalma Listesi ******");
            Console.WriteLine("1- Çalma Listesine Şarkı Ekle");
            Console.WriteLine("2- Çalma Listesinden Şarkı Sil");
            Console.WriteLine("3- Çalma Listesini Görüntüle");
            Console.WriteLine("4- Çıkış");

            Console.WriteLine("Seçmek istediğiniz İşlemin Sayısını Giriniz: ");
            int calmaListesiSecimi = int.Parse(Console.ReadLine());

            switch (calmaListesiSecimi)
            {
                case 1:
                    Console.WriteLine("****** Çalma Listesine Şarkı Ekle ******");
                    for (int i = 0; i < tumSarkilar.Count; i++)
                    {
                        Console.WriteLine(i + 1 + "." + tumSarkilar[i].SarkiAdi + " - " + tumSarkilar[i].SarkiciAdi);   //Tüm Şarkıların Listelenmesi
                    }
                    Console.Write("Eklemek istediğiniz şarkının numarasını girin: ");
                    if (int.TryParse(Console.ReadLine(), out int secim) && secim > 0 && secim <= tumSarkilar.Count)   //Listedeki şarkıların "secim" verisinin içerisindeki numalardan biri olmasının kontrolü
                    {
                        CalmaListesi.Add(tumSarkilar[secim - 1]);   //Çalma Listesi Listesine Eklenmek İstenen Şarkının Eklenmesi (.Add)

                        Console.WriteLine("Şarkı listeye eklendi! Devam etmek için bir tuşa basın.");
                    }
                    else
                    {
                        Console.WriteLine("Hatalı seçim! Devam etmek için bir tuşa basın.");
                    }
                    Console.ReadKey();
                    break;

                case 2:
                    Console.WriteLine("****** Çalma Listesinden Şarkı Sil ******");
                    for (int i = 0; i < CalmaListesi.Count; i++)
                    {
                        Console.WriteLine(i + 1 + "." + CalmaListesi[i].SarkiAdi + " - " + CalmaListesi[i].SarkiciAdi);   //Tüm Şarkıların Listelenmesi
                    }
                    Console.Write("Silmek istediğiniz şarkının numarasını girin: ");
                    if (int.TryParse(Console.ReadLine(), out int secim2) && secim2 > 0 && secim2 <= CalmaListesi.Count)   //Listedeki şarkıların "secim" verisinin içerisindeki numaralardan biri olmasının kontrolü
                    {
                        CalmaListesi.RemoveAt(secim2 - 1);   //Çalma Listesi Listesinden Silinmek İstenilen Şarkının Silinmesi (.RemoveAt)
                        Console.WriteLine("Şarkı listeden silindi! Devam etmek için bir tuşa basın.");
                    }
                    else
                    {
                        Console.WriteLine("Hatalı seçim! Devam etmek için bir tuşa basın.");
                    }
                    Console.ReadKey();
                    break;

                case 3:
                    Console.WriteLine("****** Çalma Listesi ******");
                    foreach (var sarki in CalmaListesi)   //Foreach sayesinde farklı method'taki içeriğin kullanılması
                    {
                        Console.WriteLine("Şarkı: " + sarki.SarkiAdi + " - Sanatçı: " + sarki.SarkiciAdi);
                    }
                    Console.WriteLine("Devam etmek için bir tuşa basın.");
                    Console.ReadKey();
                    break;

                case 4:
                    return;

                default:
                    Console.WriteLine("Hatalı seçim! Devam etmek için bir tuşa basın.");
                    Console.ReadKey();
                    break;
            }
        }

        static List<Sarki> tumSarkilar = new List<Sarki>();   //Tüm Şarkıları Listeleyip Değişiklik Yapabilmek İçin "Tüm Şarkılar" Listesi
        static List<Sarki> CalmaListesi = new List<Sarki>();   //Çalma Listesini Listeleyip Değişiklik Yapabilmek için "Çalma Listesi" Listesi

        static List<Sarki> muzikler()   //Tüm Şarkıların tutulduğu "Müzikler" Methodu
        {
            if (tumSarkilar.Count == 0)   //Listelenmek İstenildiği Zaman Şarkıların 1'den Fazla Yazılmamasını Sağlayan İf Komutu
            {
                tumSarkilar.AddRange(new List<Sarki>
                {
                    new Sarki { SarkiAdi = "Kır Zincirlerini", SarkiciAdi = "Barış Manço", Tur = "Rock" },
                    new Sarki { SarkiAdi = "Gözler", SarkiciAdi = "Barış Manço", Tur = "Rock" },
                    new Sarki { SarkiAdi = "Cevapsız Çınlama", SarkiciAdi = "Aleyna Tilki", Tur = "Pop" },
                    new Sarki { SarkiAdi = "Unutursun", SarkiciAdi = "Haluk Levent", Tur = "Rock" },
                    new Sarki { SarkiAdi = "Anlasana", SarkiciAdi = "Yüksek Sadakat", Tur = "Rock" },
                    new Sarki { SarkiAdi = "Bergen", SarkiciAdi = "Zeynep Bastık", Tur = "Pop" },
                    new Sarki { SarkiAdi = "Güzel Günler", SarkiciAdi = "Mor ve Ötesi", Tur = "Rock" },
                    new Sarki { SarkiAdi = "Deli Gönül", SarkiciAdi = "Tarkan", Tur = "Pop" },
                    new Sarki { SarkiAdi = "Gelincik", SarkiciAdi = "Mabel Matiz", Tur = "Pop" },
                    new Sarki { SarkiAdi = "Boşver", SarkiciAdi = "Ajda Pekkan", Tur = "Pop" },
                    new Sarki { SarkiAdi = "Kıyamam", SarkiciAdi = "Ebru Gündeş", Tur = "Pop" },
                    new Sarki { SarkiAdi = "Aşk Kırıntıları", SarkiciAdi = "Göksel", Tur = "Pop" },
                    new Sarki { SarkiAdi = "Gece", SarkiciAdi = "Adamlar", Tur = "Rock" },
                    new Sarki { SarkiAdi = "Aşk", SarkiciAdi = "Sıla", Tur = "Pop" },
                    new Sarki { SarkiAdi = "Ateşle Oynama", SarkiciAdi = "Barış Akarsu", Tur = "Rock" },
                    new Sarki { SarkiAdi = "İstanbul", SarkiciAdi = "Mor ve Ötesi", Tur = "Rock" },
                    new Sarki { SarkiAdi = "Yalnızlık Senfonisi", SarkiciAdi = "Fikret Kızılok", Tur = "Rock" },
                    new Sarki { SarkiAdi = "Yanarım", SarkiciAdi = "Tarkan", Tur = "Pop" },
                    new Sarki { SarkiAdi = "O Benim Dünyam", SarkiciAdi = "Gripin", Tur = "Rock" },
                    new Sarki { SarkiAdi = "Benimle Oynar Mısın?", SarkiciAdi = "Nil Karaibrahimgil", Tur = "Pop" }
                });
            }

            return tumSarkilar;
        }
    }

    public class Sarki   //"Şarkı Adı", "Şarkıcı Adı", "Tür" girdilerinin kullanıcıdan alınmasına ve diğer method'larda kullanılmasına izin veren veriler.
    {
        public string SarkiAdi { get; set; }
        public string SarkiciAdi { get; set; }
        public string Tur { get; set; }
    }
}
