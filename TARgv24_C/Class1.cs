using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TARgv24_C
{
    internal class Class2
    {
        public static float Kalkulaator(int arv1, int arv2)
        {
            float kalkulaator = 0;
            kalkulaator = arv1 * arv2;
            return kalkulaator;
        }
    }

    class Class1
    {
        public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear(); 

            Console.WriteLine("Hello, world!");
            int a = 0;
            string tekst = "Python";
            char taht = 'A';
            double arv = 5.4536237287;
            float arv1 = 2;

            Console.Write("Mis on sinu nimi? ");
            tekst = Console.ReadLine();
            Console.WriteLine("Tere!\n" + tekst);
            Console.WriteLine("Tere!\n {0}", tekst);

            if (tekst.ToLower() == "juku")
            {
                Console.WriteLine("{0}\n Kui vana sa oled?", tekst);
                try
                {
                    int vanus = int.Parse(Console.ReadLine());
                    if (vanus <= 0 || vanus > 100) // исправлено
                    {
                        Console.WriteLine("Viga!");
                    }
                    else if (vanus <= 6)
                    {
                        Console.WriteLine("Tasuta!");
                    }
                    else if (vanus <= 15)
                    {
                        Console.WriteLine("Lastepilet");
                    }
                    else if (vanus <= 65)
                    {
                        Console.WriteLine("Täispilet");
                    }
                    else
                    {
                        Console.WriteLine("Sooduspilet");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Vigane sisend: " + e.Message);
                }
            }
            else
            {
                Console.WriteLine("Olen hõivatud!");
            }

            Console.Write("Arv 2: ");
            int arv2 = int.Parse(Console.ReadLine());

            arv1 = Class2.Kalkulaator(a, arv2); // исправлено
            Console.WriteLine("Korrutis: " + arv1);

            Console.WriteLine("Switch'i kasutamine");
            Random rnd = new Random();
            a = rnd.Next(1, 8); // чтобы "P" (7) тоже мог выпасть
            Console.WriteLine("Päeva number: " + a);

            switch (a)
            {
                case 1: tekst = "E"; break;
                case 2: tekst = "T"; break;
                case 3: tekst = "K"; break;
                case 4: tekst = "N"; break;
                case 5: tekst = "R"; break;
                case 6: tekst = "L"; break;
                case 7: tekst = "P"; break;
                default: tekst = "Tundmatu"; break;
            }

            Console.WriteLine("Päev: " + tekst);
            Console.ReadKey();
        }
    }
}

using System;
using System.Text;

namespace ValikudJaTingimused
{
    class Class1
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // 1. Tervitus ja nimi
            Console.WriteLine("Tere tulemast!");
            Console.Write("Sisesta oma eesnimi: ");
            string eesnimi = Console.ReadLine();

            Console.WriteLine("Tere, " + eesnimi);

            if (eesnimi.ToLower() == "juku")
            {
                Console.WriteLine("Lähme kinno!");
                Console.Write("Kui vana sa oled? ");
                int vanus = int.Parse(Console.ReadLine());

                if (vanus <= 0 || vanus > 100)
                {
                    Console.WriteLine("Viga andmetega!");
                }
                else if (vanus < 6)
                {
                    Console.WriteLine("Pilet: TASUTA");
                }
                else if (vanus <= 14)
                {
                    Console.WriteLine("Pilet: LASTEPILET");
                }
                else if (vanus <= 65)
                {
                    Console.WriteLine("Pilet: TÄISPILET");
                }
                else
                {
                    Console.WriteLine("Pilet: SOODUSPILET");
                }
            }
            else
            {
                Console.WriteLine("Täna mind kodus pole!");
            }

            // 2. Pinginaabrid
            Console.Write("\nSisesta esimese inimese nimi: ");
            string nimi1 = Console.ReadLine();
            Console.Write("Sisesta teise inimese nimi: ");
            string nimi2 = Console.ReadLine();
            Console.WriteLine($"{nimi1} ja {nimi2} on täna pinginaabrid!");

            // 3. Toa pindala ja remondi hind
            Console.Write("\nSisesta toa pikkus (meetrites): ");
            float pikkus = float.Parse(Console.ReadLine());
            Console.Write("Sisesta toa laius (meetrites): ");
            float laius = float.Parse(Console.ReadLine());
            float pindala = pikkus * laius;
            Console.WriteLine($"Toa pindala on: {pindala} m²");

            Console.Write("Kas soovid teha remonti? (jah/ei): ");
            string soov = Console.ReadLine().ToLower();
            if (soov == "jah")
            {
                Console.Write("Kui palju maksab ruutmeeter (€): ");
                float hindRuudult = float.Parse(Console.ReadLine());
                Console.WriteLine($"Põranda vahetamise hind on: {hindRuudult * pindala} €");
            }

            // 4. Hinna soodustus - leia alghind
            Console.Write("\nSisesta allahinnatud hind (€): ");
            float uusHind = float.Parse(Console.ReadLine());
            float alghind = uusHind / 0.7f;
            Console.WriteLine($"Alghind enne 30% soodustust oli: {alghind:F2} €");

            // 5. Temperatuuri kontroll
            Console.Write("\nSisesta toa temperatuur: ");
            float temp = float.Parse(Console.ReadLine());
            if (temp >= 18)
            {
                Console.WriteLine("Temperatuur on sobiv!");
            }
            else
            {
                Console.WriteLine("Temperatuur on liiga madal.");
            }

            // 6. Pikkus
            Console.Write("\nSisesta oma pikkus (cm): ");
            int pikkusCm = int.Parse(Console.ReadLine());
            if (pikkusCm < 160)
            {
                Console.WriteLine("Sa oled lühike.");
            }
            else if (pikkusCm <= 185)
            {
                Console.WriteLine("Sa oled keskmine.");
            }
            else
            {
                Console.WriteLine("Sa oled pikk.");
            }

            // 7. Pikkus + sugu
            Console.Write("\nSisesta oma sugu (m/n): ");
            string sugu = Console.ReadLine().ToLower();
            Console.Write("Sisesta oma pikkus (cm): ");
            int pikkus2 = int.Parse(Console.ReadLine());

            if (sugu == "m")
            {
                if (pikkus2 < 165)
                    Console.WriteLine("Sa oled mees ja lühike.");
                else if (pikkus2 <= 185)
                    Console.WriteLine("Sa oled mees ja keskmise pikkusega.");
                else
                    Console.WriteLine("Sa oled mees ja pikk.");
            }
            else if (sugu == "n")
            {
                if (pikkus2 < 155)
                    Console.WriteLine("Sa oled naine ja lühike.");
                else if (pikkus2 <= 175)
                    Console.WriteLine("Sa oled naine ja keskmise pikkusega.");
                else
                    Console.WriteLine("Sa oled naine ja pikk.");
            }
            else
            {
                Console.WriteLine("Sugu ei tuvastatud.");
            }

            // 8. Pood - ostukorv
            float koguhind = 0;
            Console.Write("\nKas soovid osta piima (jah/ei)? ");
            if (Console.ReadLine().ToLower() == "jah") koguhind += 1.2f;

            Console.Write("Kas soovid osta saia (jah/ei)? ");
            if (Console.ReadLine().ToLower() == "jah") koguhind += 0.8f;

            Console.Write("Kas soovid osta leiba (jah/ei)? ");
            if (Console.ReadLine().ToLower() == "jah") koguhind += 1.5f;

            Console.WriteLine($"Ostukorvi koguhind on: {koguhind:F2} €");

            Console.WriteLine("\nProgramm lõppenud. Vajuta klahvi...");
            Console.ReadKey();
        }
    }
}
