using Muzikos_Parduotuve.Services;

namespace Muzikos_Parduotuve
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            IMusicShop startShop = new MusicShop();
            startShop.StartShop();

        }
    }
}

