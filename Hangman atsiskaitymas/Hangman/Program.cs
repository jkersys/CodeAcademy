using System.Text;

namespace Hangman
{
    public class Program
    {
        public static int menuPasirinkimas = 0;
        public static int neteisinguSpejimuSkaicius = 0;
        public static string sugeneruotasZodis = "";
        public static string tema = "";
        public static List<string> spetosRaides = new List<string>();
        public static string spejimas = "";
        public static List<char> uzmaskuotasZodis = new List<char>();
        public static char spejamaRaide;
        public static bool laimeta = false;

        public static Dictionary<string, bool> panaudotiZodziai = new Dictionary<string, bool>();

        public static Dictionary<string, bool> pasirinktiZodziai;


        public static Dictionary<string, bool> vardai = new Dictionary<string, bool>() { { "Greta", false }, { "Karolina", false }, { "Tomas", false }, { "Jonas", false }, { "Justinas", false }, { "Giedrė", false }, { "Gintarė", false }, { "Adomas", false }, { "Audrius", false }, { "Marius", false } };
        public static Dictionary<string, bool> miestai = new Dictionary<string, bool>() { { "Vilnius", false }, { "Kaunas", false }, { "Molėtai", false }, { "Varėna", false }, { "Klaipėda", false }, { "Alytus", false }, { "Panevėžys", false }, { "Ignalina", false }, { "Utena", false }, { "Lazdijai", false } };
        public static Dictionary<string, bool> salys = new Dictionary<string, bool>() { { "Lietuva", false }, { "Latvija", false }, { "Estija", false }, { "Lenkija", false }, { "Ukraina", false }, { "Suomija", false }, { "Švedija", false }, { "Norvegija", false }, { "Danija", false }, { "Vokietija", false } };
        public static Dictionary<string, bool> kita = new Dictionary<string, bool>() { { "Stalas", false }, { "Kėdė", false }, { "Žemėlapis", false }, { "Pelė", false }, { "Kilimas", false }, { "Spausdintuvas", false }, { "Laikrodis", false }, { "Siena", false }, { "Grindys", false }, { "Lubos", false } };


        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding(1200);
            Console.InputEncoding = Encoding.GetEncoding(1200);
            Kartuves();
        }
        public static void Kartuves()
        {
            Reset();

            //pakvieciam menu, temos pasirinkimui ir būsenos priskyrimui
            KartuviuMenu();

            //pakvieciam metodus, kuries pagal esama būsena sugeneruoja random žodį ir jį užmaskuoja
            ZodzioParinkimas();
            ZodzioUzmaskavimas();

            while (neteisinguSpejimuSkaicius < 7)
            {
                Console.Clear();
                Console.WriteLine($"Tema: {tema}");
                KartuviuPiesimas();
                Console.WriteLine();
                Console.Write($"Spėtos raidės: ");
                SpetosRaides(sugeneruotasZodis, spejimas);
                Console.Write("\nŽodis: ");
                Console.WriteLine(string.Join(" ", uzmaskuotasZodis));
                Console.WriteLine("\n\nSpėkite raidę, ar žodį");

                spejimas = Console.ReadLine();


                    if (spejimas == "" || spejimas == null)
                    {
                        continue;
                    }
                    else

                        if (spejimas.Length == 1)
                    {
                        spejamaRaide = spejimas[0];

                        if (char.IsLetter(spejamaRaide))
                        {                                       //ignoruoja didžiasias ir mazasias raides
                            if (sugeneruotasZodis.Contains(spejimas, StringComparison.CurrentCultureIgnoreCase))
                            {
                                for (int i = 0; i < sugeneruotasZodis.Length; i++)
                                {                   //suvienodina abi raides, kad suveiktu palyginimas
                                    if (char.ToUpper(sugeneruotasZodis[i]) == char.ToUpper(spejamaRaide))
                                    {
                                        uzmaskuotasZodis[i] = spejamaRaide;
                                    }
                                }

                                if (!uzmaskuotasZodis.Contains('_'))
                                {
                                    laimeta = true;
                                    break;
                                }
                            }
                            else if ((!sugeneruotasZodis.Contains(spejimas) && !spetosRaides.Contains(spejimas))) 
                            {
                                neteisinguSpejimuSkaicius++;
                            }
                        }
                    }
                    else
                    {                   //tikrina ar laimeta, ignuorojamos didziosios ar mazosios raides
                        laimeta = sugeneruotasZodis.Equals(spejimas, StringComparison.CurrentCultureIgnoreCase) ? true : false;
                        break;
                    }
                }

                ZaidimoPabaiga();
            }


        public static void Reset()
        {
            laimeta = false;
            neteisinguSpejimuSkaicius = 0;
            sugeneruotasZodis = "";
            spejamaRaide = ' ';
            uzmaskuotasZodis.Clear();
            spejimas = "";
            spetosRaides.Clear();
        }

        public static void ZaidimoPabaiga()
        {
            if (laimeta)
            {
                Console.Clear();
                KartuviuPiesimas();
                Console.WriteLine($"Laimėjote, teisingas žodis {sugeneruotasZodis}");
                Console.WriteLine("Pakartoti zaidimą T / N ? ");
                ZaidimoPabaigosMeniu();

            }
            else
            {
                Console.Clear();
                neteisinguSpejimuSkaicius = 7;
                KartuviuPiesimas();
                Console.WriteLine($"Pralaimėjote, teisingas žodis {sugeneruotasZodis}");
                Console.WriteLine("Pakartoti zaidimą T / N ? ");
                ZaidimoPabaigosMeniu();
            }
        }

        public static void ZaidimoPabaigosMeniu()
        {

            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.T)
                {
                    break;
                }
                if (key.Key == ConsoleKey.N)
                {
                    Environment.Exit(0);
                }

            }
            Kartuves();
        }

        // metodas pasirinkti temai
        public static void KartuviuMenu()
        {
            Console.Clear();
            Console.WriteLine("Pasirinkite tema:");
            Console.WriteLine("1. Vardai");
            Console.WriteLine("2. Miestai");
            Console.WriteLine("3. Valstybės");
            Console.WriteLine("4. Kita");


            int.TryParse(Console.ReadLine(), out menuPasirinkimas);


            if (menuPasirinkimas <= 4 && menuPasirinkimas >= 1)
                switch (menuPasirinkimas)
                {
                    case 1:
                        menuPasirinkimas = 1;
                        tema = "VARDAI";
                        pasirinktiZodziai = vardai;
                        break;
                    case 2:
                        menuPasirinkimas = 2;
                        tema = "MIESTAI";
                        pasirinktiZodziai = miestai;
                        break;
                    case 3:
                        menuPasirinkimas = 3;
                        tema = "VALSTYBE";
                        pasirinktiZodziai = salys;
                        break;
                    case 4:
                        menuPasirinkimas = 4;
                        tema = "KITA";
                        pasirinktiZodziai = kita;
                        break;
                    
                }
            else
            {
                KartuviuMenu();
            }

        }

        public static void ZodzioParinkimas()
        {       //pasifiltruojam nepanaudotus zodzius
            var nepanaudotiZodziai = NepanaudotiZodziai(pasirinktiZodziai);
            if (nepanaudotiZodziai.Count == 0)
            {
                Console.WriteLine($"Tema: {tema} nebėra žodžių, ar norite rinktis kitą temą T/N");
                ZaidimoPabaigosMeniu();
            }

            Random rnd = new Random();
            int random = rnd.Next(nepanaudotiZodziai.Count);
            sugeneruotasZodis = nepanaudotiZodziai[random];
            pasirinktiZodziai[sugeneruotasZodis] = true;
        }

        public static List<string> NepanaudotiZodziai(Dictionary<string, bool> zodziai)
        {
            List<string> nepanaudotiZodziai = new List<string>();
            foreach (var zodis in zodziai)
            {
                if (zodis.Value == false)
                {
                    nepanaudotiZodziai.Add(zodis.Key);
                }
            }
            return nepanaudotiZodziai;
        }

        public static void ZodzioUzmaskavimas()
        {
            for (int i = 0; i < sugeneruotasZodis.Length; i++)
            {
                uzmaskuotasZodis.Add('_');
            }
        }

        public static List<string> SpetosRaides(string sugeneruotasZodis, string spejimas)
        {
            if (!sugeneruotasZodis.Contains(spejimas, StringComparison.CurrentCultureIgnoreCase) && !spetosRaides.Contains(spejimas))
            {
                if (char.IsLetter(spejimas[0]))
                {
                    spetosRaides.Add(spejimas);
                }
            }
            Console.Write($"{string.Join(", ", spetosRaides)}");
            return spetosRaides;
        }

        public static void KartuviuPiesimas()
        {
            if (neteisinguSpejimuSkaicius == 0)
            {
                Console.WriteLine("   - - - - - - |");
                Console.WriteLine("|               ");
                Console.WriteLine("|               ");
                Console.WriteLine("|               ");
                Console.WriteLine("|               ");
                Console.WriteLine("|               ");
                Console.WriteLine("|               ");
                Console.WriteLine("|               ");
                Console.WriteLine("|               ");
            }
            else if (neteisinguSpejimuSkaicius == 1)
            {
                Console.WriteLine("   - - - - - - |");
                Console.WriteLine("|              o ");
                Console.WriteLine("|             ");
                Console.WriteLine("|             ");
                Console.WriteLine("|              ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
            }
            else if (neteisinguSpejimuSkaicius == 2)
            {
                Console.WriteLine("   - - - - - - |");
                Console.WriteLine("|              o ");
                Console.WriteLine("|              |");
                Console.WriteLine("|               ");
                Console.WriteLine("|              ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
            }
            else if (neteisinguSpejimuSkaicius == 3)
            {
                Console.WriteLine("   - - - - - - |");
                Console.WriteLine("|              o ");
                Console.WriteLine("|             \\|");
                Console.WriteLine("|               ");
                Console.WriteLine("|              ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
            }
            else if (neteisinguSpejimuSkaicius == 4)
            {
                Console.WriteLine("   - - - - - - |");
                Console.WriteLine("|              o ");
                Console.WriteLine("|             \\|/");
                Console.WriteLine("|              ");
                Console.WriteLine("|              ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
            }
            else if (neteisinguSpejimuSkaicius == 5)
            {
                Console.WriteLine("   - - - - - - |");
                Console.WriteLine("|              o ");
                Console.WriteLine("|             \\|/");
                Console.WriteLine("|              0 ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
            }
            else if (neteisinguSpejimuSkaicius == 6)
            {
                Console.WriteLine("   - - - - - - |");
                Console.WriteLine("|              o ");
                Console.WriteLine("|             \\|/");
                Console.WriteLine("|              0 ");
                Console.WriteLine("|             /  ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
            }
            else if (neteisinguSpejimuSkaicius == 7)
            {
                Console.WriteLine("   - - - - - - |");
                Console.WriteLine("|              o ");
                Console.WriteLine("|             \\|/");
                Console.WriteLine("|              0 ");
                Console.WriteLine("|             / \\ ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
                Console.WriteLine("|                ");
            }
        }
    }
}