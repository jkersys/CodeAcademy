namespace HangmanTestai
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void NepanaudotiZodziai_GrazinaNepanaudotusZodzius()
        {
            var zodziai = new Dictionary<string, bool>() { { "Vilnius", true }, { "Kaunas", false }, { "Klaipėda", true }, { "Šiauliai", true }, { "Molėtai", true } };

            var nepanaudoti = Hangman.Program.NepanaudotiZodziai(zodziai);

            Assert.IsTrue(nepanaudoti.Count == 1);
            Assert.IsTrue(nepanaudoti.Contains("Kaunas"));
        }

        [TestMethod]
        public void ZodzioUzmaskavimas_PakeiciaRaidesIs_()
        {
            Hangman.Program.sugeneruotasZodis = "Vilnius";

            Hangman.Program.ZodzioUzmaskavimas();

            Assert.IsTrue(Hangman.Program.uzmaskuotasZodis.Count == Hangman.Program.sugeneruotasZodis.Length);
            Assert.IsTrue(Hangman.Program.uzmaskuotasZodis.All(x => x == '_'));
        }

        [TestMethod]
        public void ZodzioParinkimas_ParenkaZodiIsNepanaudotuZodziu()
        {
            Hangman.Program.pasirinktiZodziai = new Dictionary<string, bool>() { { "Vilnius", true }, { "Kaunas", false } };

            Hangman.Program.ZodzioParinkimas();

            Assert.IsTrue(Hangman.Program.sugeneruotasZodis == "Kaunas");
            Assert.IsTrue(Hangman.Program.pasirinktiZodziai.All(x => x.Value == true));
        }
        [TestMethod]
        public void ArNepridedaTeisingoSpejimo() //
        {
            var randomZodis = "Testas";
            var spetaRaide = "A";
            var expected = new List<char> {  };
            var actual = Hangman.Program.SpetosRaides(randomZodis, spetaRaide);
            CollectionAssert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void NeteisingosRaidesĮvedimoAtvejuPasikeiciaListDydis() 
        {
           var randomZodis = "Testas";
           var spetaRaide = "q";
           var actual = Hangman.Program.SpetosRaides(randomZodis, spetaRaide);
           
            Assert.IsTrue(Hangman.Program.spetosRaides.Count == 1);

        }

        [TestMethod]
        public void IvedusTeisingaRaideListasNepadideja()
        {
            var randomZodis = "Testas";
            var spetaRaide = "A";
            var actual = Hangman.Program.SpetosRaides(randomZodis, spetaRaide);

            Assert.IsTrue(Hangman.Program.spetosRaides.Count == 0);

        }
    }
}