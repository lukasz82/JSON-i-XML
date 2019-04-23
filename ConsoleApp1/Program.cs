using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // Czytaj();
            //JSONDeserjalizacja();
            //JSONDeserjalizacja2();
            //JSONSerializacja();
            JSONToLInq();
        }

        static void JSONDeserjalizacja()
        {
            var example = @"{""imię"":""Janusz Wielki"",""wiek"":42}";
            var zdeserializownay = JsonConvert.DeserializeObject<Dictionary<string, string>>(example);

            Console.WriteLine($"Imię to : {zdeserializownay["imię"]}, a wiek to {zdeserializownay["wiek"]}");
            Console.ReadKey();
        }

        static void JSONDeserjalizacja2()
        {
            var example = @"{""Imie"":""Janusz Wielki"",""Wiek"":42}";
            var zdeserializownay = JsonConvert.DeserializeObject<Osoba>(example);

            Console.WriteLine($"Imię to : {zdeserializownay.Imie}, a wiek to {zdeserializownay.Wiek}");
            Console.ReadKey();
        }

        static void JSONSerializacja()
        {
            var czlowiek = new Czlowiek
            {
                Imie = "Łukasz",
                Nazwisko = "Kazimierczak",
                Wiek = 36,
                Adres = new Adres
                {
                    Miasto = "Brzeszcze",
                    Ulica = "Siedliska",
                    KodPocztowy = "32-620",
                    NrDomu = 36
                }
            };

            var tekst = JsonConvert.SerializeObject(czlowiek);
            var tekst2 = JsonConvert.SerializeObject(czlowiek, Newtonsoft.Json.Formatting.Indented);

            Console.WriteLine($"Nasz obiekt to: {tekst}");
            Console.WriteLine("-------------------------");
            Console.WriteLine($"Nasz obiekt formatowany  to: {tekst2}");
            Console.ReadKey();
        }


        static void JSONToLInq()
        {
            var czlowiek1 = new Czlowiek
            {
                Imie = "Łukasz",
                Nazwisko = "Kazimierczak",
                Wiek = 36,
                Adres = new Adres
                {
                    Miasto = "Brzeszcze",
                    Ulica = "Siedliska",
                    KodPocztowy = "32-620",
                    NrDomu = 36
                }
            };

            var czlowiek2 = new Czlowiek
            {
                Imie = "Miłosz",
                Nazwisko = "Myśliwiec",
                Wiek = 29,
                Adres = new Adres
                {
                    Miasto = "Brzeszcze",
                    Ulica = "Kościuszki",
                    KodPocztowy = "32-620",
                    NrDomu = 1
                }
            };

            var czlowiek3 = new Czlowiek
            {
                Imie = "Robert",
                Nazwisko = "Sikora",
                Wiek = 44,
                Adres = new Adres
                {
                    Miasto = "Rajsko",
                    Ulica = "Klonowa",
                    KodPocztowy = "32-600",
                    NrDomu = 3
                }
            };

            List<Czlowiek> lista = new List<Czlowiek>();
            lista.Add(czlowiek1);
            lista.Add(czlowiek2);
            lista.Add(czlowiek3);

            string json = JsonConvert.SerializeObject(lista);
            Console.WriteLine($"Nasz obiekt to: {json}");


            var res = JArray.Parse(json);

            var osoby = from p in res
                        where Convert.ToInt32(p["Wiek"]) > 30
                        select p;

            var osobyInnySposob = from p in res
                                  where Convert.ToInt32(p["Wiek"]) > 30
                                  select new { imie = p["Imie"], nazwisko = p["Nazwisko"], adres = p["Adres"]["Ulica"] };

            foreach (var item in osoby)
            {
                Console.WriteLine();
                Console.WriteLine(item["Imie"] + " " + item["Nazwisko"]);
            }

            foreach (var item in osobyInnySposob)
            {
                Console.WriteLine();
                //Console.WriteLine(item);
                Console.WriteLine(item.imie + " " + item.nazwisko + " " + item.adres);
            }

            Console.WriteLine("-------------------------");


            
            
            //Console.WriteLine($"Nasz obiekt formatowany  to: {tekst2}");
            Console.ReadKey();

        }
        static void Czytaj()
        {
            Console.WriteLine("___Czytam___");
            XmlReader xmlReader = XmlReader.Create("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml");
            int value = 0;
            string currency = "";

            Console.WriteLine("wpisz wartość");
            value = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("wpisales " + value);
            Console.WriteLine("wpisz walute");
            currency = Console.ReadLine();
            Console.WriteLine("wpisales " + currency);
            decimal x = 0;
            while (xmlReader.Read())
            {
                if((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Cube"))
                {
                    if (xmlReader.HasAttributes)
                    {
                        if (xmlReader.AttributeCount == 2)
                        {
                            Console.WriteLine($"{ xmlReader.GetAttribute("currency")} : { xmlReader.GetAttribute("rate")}");
                            var rate = xmlReader.GetAttribute("rate").Replace('.', ',');
                            var rateCorect = Convert.ToDecimal(rate);
                            if (currency == xmlReader.GetAttribute("currency"))
                            {
                                x = value / rateCorect;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Przeliczenie = " + x.ToString());
            Console.ReadKey();
        }

    }

    class Osoba
    {
        public string Imie { get; set; }
        public int Wiek { get; set; }
    }

    class Czlowiek
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Wiek { get; set; }

        public Adres Adres { get; set; }
    }

    class Adres
    {
        public string Miasto { get; set; }
        public string Ulica { get; set; }
        public string KodPocztowy { get; set; }
        public int NrDomu { get; set; }
    }
}
