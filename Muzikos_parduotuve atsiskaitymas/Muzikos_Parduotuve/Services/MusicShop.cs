using Castle.Core.Internal;
using Muzikos_Parduotuve.Domain.Models;
using Muzikos_Parduotuve.Infrastructure.Database;
using Muzikos_Parduotuve.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muzikos_Parduotuve.Services
{

    public class MusicShop : IMusicShop
    {
        private readonly IChinookRepository repository;
        private Customer? activeCustomer { get; set; }

        private List<Track> tracksSelected { get; set; } = new List<Track>();

        public MusicShop()
        {
            repository = new ChinookRepository();
        }
        const string pin = "1234";


         
        public void StartShop()
        {
            while(true)
            { 

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| #       | Pasirinkimas | ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 1       | Prisijungti | ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 2       | Registruoti | ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 3       | Darbuotojų Parinktys | ");
            Console.WriteLine("--------------------------------------------------------------");

            Console.WriteLine("Q: Išeiti");

            char input = Console.ReadKey().KeyChar;
            Console.Clear();

                switch (input)
                {
                    case '1':
                        CustomerLogIn();
                        break;
                    case '2':
                        RegistrationForm();
                        break;
                    case '3':
                        EmployeeLogin();
                        break;
                    case 'q' or 'Q':
                        Environment.Exit(0);
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Tokio pasirinkimo nėra");
                        break;
                }
            }
        }

        public void BuyMenu()
        {
            Console.Clear();
            while(true)
            { 
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| #       | Pasirinkimas | ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 1       | Peržiūrėti katalogą | ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 2       | Įdėti į krepšelį | ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 3       | Peržiūrėti krepšelį  | ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 4       | Peržiūrėti pirkimų istorija (Išrašai) | ");
            Console.WriteLine("--------------------------------------------------------------");

            Console.WriteLine("Q: Grįžti atgal");


            char input = Console.ReadKey().KeyChar;
                Console.Clear();

                switch (input)
                {
                    case '1':
                        ShowAllTracks();
                        FilterMenu();
                        break;
                    case '2':
                        AddToBasketMenu();
                        break;
                    case '3':
                        ShowBasket();
                        break;
                    case '4':                        
                        ShowAllInvoices();
                        break;
                    case 'q' or 'Q':
                        Console.WriteLine("Ar tikrai norite atsijungti y: taip n: ne");
                        var logOut = Console.ReadKey().KeyChar;
                        Console.Clear();
                        if (logOut == 'y' || logOut == 'Y')
                        {
                            activeCustomer = null;
                            StartShop();
                        }
                        if (logOut == 'n' || logOut == 'N')
                        {
                            break;
                        }
                        else 
                            break;      
                                      
                    default:
                        Console.Clear();
                        Console.WriteLine("Tokio pasirinkimo nėra");
                        break;
                }
            }
        }
               
        public void ShowAllTracks()
        {

            List<Track> tracksList = repository.SongList();
            PrintTrackList(tracksList);            
            

        }

        public void FilterMenu()
        {
          

            Console.WriteLine("Q. Grįžti");
            Console.WriteLine("O. Rikiuoti");
            Console.WriteLine("S. Paieška");

            var input = Console.ReadKey().KeyChar;
            Console.Clear();
            switch (input)
            {
                case 'q' or 'Q':
                    BuyMenu();

                    break;
                case 'o' or 'O':
                    SortByMenu();
                    break;
                case 's' or 'S':
                    SearchByMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Tokio pasirinkimo nėra");
                    FilterMenu();
                    break;
            }
        }

        private void RegistrationForm()
        {
            Console.Clear();
            Console.WriteLine("Registration");
            Console.WriteLine($"\nName:");
            string firstName = Console.ReadLine();
            Console.WriteLine($"\nLast name:");
            string lastName = Console.ReadLine();
            Console.WriteLine($"\nCompany");
            string company = Console.ReadLine();
            Console.WriteLine($"\nAdress");
            string adress = Console.ReadLine();
            Console.WriteLine($"\nCity");
            string city = Console.ReadLine();
            Console.WriteLine($"\nState");
            string state = Console.ReadLine();
            Console.WriteLine($"\nCountry");
            string country = Console.ReadLine();
            Console.WriteLine($"\nPostalCode");
            string postalCode = Console.ReadLine();
            Console.WriteLine($"\nPhone");
            string phone = Console.ReadLine();
            Console.WriteLine($"\nFax");
            string fax = Console.ReadLine();
            Console.WriteLine($"\nEmail");
            string email = Console.ReadLine();

                while (firstName.IsNullOrEmpty())
                {
                    Console.WriteLine("Vardas privalo būti įvestas");
                    firstName = Console.ReadLine();
                }
                
                while (lastName.IsNullOrEmpty())
                {
                    Console.WriteLine("Neivedėte Pavardės");
                    lastName = Console.ReadLine();
                }
                
                while (email.IsNullOrEmpty() || !email.Contains("@"))
                {
                    Console.WriteLine("Neįvestas, arba neteisingai įvestas El. paštas");
                    email = Console.ReadLine();
                }
            


            repository.AddUser(firstName, lastName, company, adress, city, state, country, postalCode, phone, fax, email);

            StartShop();
        }



        private void CustomerLogIn()
        {
            Console.Clear();
            List<Customer> customerList = repository.CustomerList();
            foreach (Customer customer in customerList)
            {
                Console.WriteLine($"{customer.CustomerId} {customer.FirstName} {customer.LastName}");
            }

            Console.WriteLine("Iveskite vartotojo Id prie kurio norite prisijungti");
            var customerId = Convert.ToInt32(Console.ReadLine());
            while (customerId > customerList.Count)
            {
                Console.WriteLine("Tokio vartotojo nėra, įveskite kitą Id ");
                customerId = Convert.ToInt32(Console.ReadLine());
            }

            activeCustomer = customerList.First(c => c.CustomerId == customerId);
            Console.WriteLine("Pasirinktas vartotojas");
            Console.WriteLine($"{activeCustomer.CustomerId} {activeCustomer.FirstName} {activeCustomer.LastName}");

            BuyMenu();
            return;
        }

        public void SortByMenu()
        {
            Console.WriteLine("Rušiuoti pagal:");
            Console.WriteLine("1.Pavadinimą abecėlės tvarka");
            Console.WriteLine("2.Pavadinimą atvirkštine abecėlės tvarka");
            Console.WriteLine("3.Kompozitorių");
            Console.WriteLine("4.Žanrą");
            Console.WriteLine("5.Kompozitorių ir albumą:");
            Console.WriteLine("Q.Grįžti atgal:");

            var input = Console.ReadKey().KeyChar;
            Console.Clear();

            switch (input)
            {
                case '1':
                    SortSongsAToZ();
                    break;
                case '2':
                    SortSongDescending();
                    break;
                case '3':
                    SortSongsByComposer();
                    break;
                case '4':
                    SortSongsByGenre();
                    break;
                case '5':
                    SortSongsByComposerAndAlbum();
                    break;
                case 'q' or 'Q':
                    FilterMenu();              
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Tokio pasirinkimo nėra");
                    SortByMenu();
                    break;
            }

        }

        public void SortSongsAToZ()
        {
            List<Track> tracksList = repository.SortSongs();

            PrintTrackList(tracksList);
            SortByMenu();
        }

        public void SortSongDescending()
        {
            List<Track> tracksList = repository.TracksByNameDecending();

            PrintTrackList(tracksList);
            SortByMenu();
        }

        public void SortSongsByComposer()
        {
            List<Track> tracksList = repository.TracksByComposer();
            PrintTrackList(tracksList);
            SortByMenu();
        }

        public void SortSongsByGenre()
        {
            List<Track> tracksList = repository.TracksByGenre();

            PrintTrackList(tracksList);
            SortByMenu();
        }

        public void SortSongsByComposerAndAlbum()
        {
            List<Track> tracksList = repository.TracksByComposerAndAlbum();
            PrintTrackList(tracksList);
            SortByMenu();
        }


        public void SearchByMenu()
        {
            Console.WriteLine("Pasirinkite pagal ką norite ieškoti");
            Console.WriteLine("1.Id");
            Console.WriteLine("2.Pavadinimą");
            Console.WriteLine("3.Kompozitorių");
            Console.WriteLine("4.Žanrą");
            Console.WriteLine("5.Kompozitorių ir Albumą");
            Console.WriteLine("6.Milisekundes");
            Console.WriteLine("Q.Grįžti atgal");

            var input = Console.ReadKey().KeyChar;
            Console.Clear();

            switch (input)
            {
                case '1':
                    SearchById();
                    break;
                case '2':
                    SearchByName();
                    break;
                case '3':
                    SearchByComposer();
                    break;
                case '4':
                    SearchByGenre();
                    break;
                case '5':
                    SearchByComposerAndAlbum();
                    break;
                case '6':
                    SearchByLength();
                    break;
                case 'q' or 'Q':
                    FilterMenu();            
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Tokio pasirinkimo nėra");
                    SearchByMenu();
                    break;
            }

        }
        public void SearchById()
        {
            Console.WriteLine("Įveskite dainos Id");
            long songId = Convert.ToInt64(Console.ReadLine());                      

            List<Track> track = repository.GetSongById(songId);

            PrintTrackList(track);
            SearchByMenu();
        }
        public void SearchByName()
        {
            Console.WriteLine("Įveskite dainos pavadinimą");
            string songName = Console.ReadLine();

            List<Track> tracksList = repository.SearchBySongName(songName);

            PrintTrackList(tracksList);
            SearchByMenu();
        }
        public void SearchByComposer()
        {
            Console.WriteLine("Įveskite kompzitoriaus pavadinimą");
            string composer = Console.ReadLine();

            List<Track> tracksList = repository.SearchByComposer(composer);
            PrintTrackList(tracksList);
            SearchByMenu();
        }
        public void SearchByGenre()
        {
            Console.WriteLine("Įveskite dainos žanro pavadinimą");
            string genre = Console.ReadLine();

            List<Track> tracksList = repository.SearchByGenre(genre);
            PrintTrackList(tracksList);
            SearchByMenu();
        }
        public void SearchByComposerAndAlbum()
        {
            Console.WriteLine("Įveskite dainos autorių");
            string composer = Console.ReadLine();
            Console.WriteLine("Įveskite albumo pavadinimą");
            string album = Console.ReadLine();

            List<Track> tracksList = repository.SearchSongsByComposerAndAlbum(composer, album);
            PrintTrackList(tracksList);
            SearchByMenu();
        }
        public void SearchByLength()
        {
            Console.WriteLine("Įveskite milisekundžių skaičių");
            int miliseconds = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("1. Dainos ilgesnės nei įvestas milisekundžių skaičius");
            Console.WriteLine("2. Dainos trumpesnės nei įvestas milisekundžių skaičius");
            int choice = Convert.ToInt32(Console.ReadLine());

            List<Track> tracksList = repository.SearchSongsByLength(miliseconds, choice);
            if(tracksList.IsNullOrEmpty())
            {
                Console.Clear();
                Console.WriteLine("Dainų pagal pasirinktą kriterijų nėra");
                SearchByMenu();


            }
            else
            PrintTrackList(tracksList);
            SearchByMenu();
        }
        public void SearchByAlbumId()
        {

            Console.WriteLine("Įveskite albumo Id");
            int albumId = Convert.ToInt32(Console.ReadLine());

            List<Track> tracksList = repository.SearchSongsByAlbumId(albumId);
            PrintTrackList(tracksList);
            Console.WriteLine("'q' - Grįžti atgal || 'y' - Įdeda į krepšelį visas rastas dainas");

            var input = Console.ReadKey().KeyChar;
            Console.Clear();

            switch (input)
            {
                case 'q' or 'Q':
                AddToBasketMenu();
                    break;
                case 'y' or 'Y':
                    tracksSelected.AddRange(tracksList);
                    AddToBasketMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Tokio pasirinkimo nėra");
                    AddToBasketMenu();
                    break;

            }
        }

        public void SearchByAlbumName()
        {
            Console.WriteLine("Įveskite albumo Id");
            string albumName = Console.ReadLine();

            List<Track> tracksList = repository.SearchBySongAlbumName(albumName);

            PrintTrackList(tracksList);
            Console.WriteLine("'q' - Grįžti atgal || 'y' - Įdeda į krepšelį visas rastas dainas");
            var input = Console.ReadKey().KeyChar;
            Console.Clear();

            switch (input)
            {
                case 'q' or 'Q':
                    AddToBasketMenu();
                    break;
                case 'y' or 'Y':
                    tracksSelected.AddRange(tracksList);
                    AddToBasketMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Tokio pasirinkimo nėra");
                    AddToBasketMenu();
                    break;
            }
            }
        public void SearchByIdToBuy()
        {
            Console.WriteLine("Įveskite dainos Id");
            int songId = Convert.ToInt32(Console.ReadLine());

            var track = repository.GetSongById(songId);

            PrintTrackList(track);

            Console.WriteLine("'q' - Grįžti atgal || 'y' - Įdeda į krepšelį visas rastas dainas");            
            var input = Console.ReadKey().KeyChar;
            Console.Clear();

            switch (input)
            {
                case 'q' or 'Q':
                    AddToBasketMenu();
                    break;
                case 'y' or 'Y':
                    tracksSelected.AddRange(track);
                    AddToBasketMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Tokio pasirinkimo nėra");
                    AddToBasketMenu();
                    break;
            }
            
        }
        public void SearchByNameToBuy()
        {
            Console.WriteLine("Įveskite dainos pavadinimą");
            string songName = Console.ReadLine();

            List<Track> tracksList = repository.SearchBySongName(songName);

            PrintTrackList(tracksList);
            Console.WriteLine("'q' - Grįžti atgal || 'y' - Įdeda į krepšelį visas rastas dainas");
            var input = Console.ReadKey().KeyChar;
            Console.Clear();

            switch (input)
            {
                case 'q' or 'Q':
                    AddToBasketMenu();
                    break;
                case 'y' or 'Y':
                    tracksSelected.AddRange(tracksList);
                    AddToBasketMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Tokio pasirinkimo nėra");
                    AddToBasketMenu();
                    break;
            }
        }

        public void AddToBasketMenu()
        {
            Console.WriteLine("1.Daina pagal Id");
            Console.WriteLine("2.Daina pagal pavadinimą");
            Console.WriteLine("3.Dainos pagal albumo Id");
            Console.WriteLine("4.Dainos pagal albumo pavadinimą");
            Console.WriteLine("Q.Grįžti");

            var input = Console.ReadKey().KeyChar;
            Console.Clear();

            switch (input)
            {
                case '1':
                    SearchByIdToBuy();
                    break;
                case '2':
                    SearchByNameToBuy();
                    break;
                case '3':
                    SearchByAlbumId();
                    break;
                case '4':
                    SearchByAlbumName();
                    break;
                case 'q' or 'Q':
                    BuyMenu();
                    break;               
                default:
                    Console.Clear();
                    Console.WriteLine("Tokio pasirinkimo nėra");
                    AddToBasketMenu();
                    break;
            }
        }

        public void ShowBasket()
        {

            Console.WriteLine("-------------------------------------------------------------- ");
            Console.WriteLine("| #   |  Name, Composer, Genre, Album, Milliseconds, Price | ");
            Console.WriteLine("-------------------------------------------------------------- ");
            foreach (var track in tracksSelected)
            {
                Console.WriteLine($"{track.TrackId}| {track.Name}, {track.Composer}, {track.Genre.Name}, {track.Album.Title}, {track.Milliseconds}, {track.UnitPrice}");
                Console.WriteLine("-------------------------------------------------------------- ");
            }

            Console.WriteLine("'q' - Grįžti atgal || 'y' - Užbaigti pirkimą");
            var input = Console.ReadLine();
            if (input == "q")
            {
                BuyMenu();
            }
            if (input == "y")
            {
                
                var invoice = new Invoice();
                invoice.InvoiceDate = DateTime.Now;
                invoice.BillingAddress = activeCustomer.Address;
                invoice.CustomerId = activeCustomer.CustomerId;
                invoice.BillingAddress = activeCustomer.Address;
                invoice.BillingState = activeCustomer.State;
                invoice.BillingCity = activeCustomer.City;
                invoice.BillingCountry = activeCustomer.Country;
                invoice.BillingPostalCode = activeCustomer.PostalCode;


                invoice.InvoiceItems = new List<InvoiceItem>();
                foreach (var track in tracksSelected)
                {
                    invoice.InvoiceItems.Add(new InvoiceItem
                    {
                        TrackId = track.TrackId
                    });
                }

                invoice.Total = Math.Round(TotalPrice(), 2);
                repository.AddInvoice(invoice);
            }
            InvoiceFieldsToPrint();
            

        }

        public double TotalPrice()
        {
            double sumNoTax = 0;
            foreach (var price in tracksSelected)
            {
                sumNoTax = sumNoTax + price.UnitPrice.Value;
            }
            return sumNoTax;
        }

        public void InvoiceFieldsToPrint()
        {
            Console.WriteLine($"Name:{activeCustomer.FirstName}");
            Console.WriteLine($"Last Name:{activeCustomer.LastName}");
            Console.WriteLine($"Adress:{activeCustomer.Address}");
            Console.WriteLine($"Phone:{activeCustomer.Phone}");
            Console.WriteLine($"Email:{activeCustomer.Email}");
            double sumNoTax = 0;
            Console.WriteLine("-------------------------------------------------------------- ");
            Console.WriteLine("| #   |  Name, Composer, Genre, Album, Milliseconds, Price | ");
            Console.WriteLine("-------------------------------------------------------------- ");
            foreach (var track in tracksSelected)
            {
                Console.WriteLine($"{track.TrackId}| {track.Name}, {track.Composer}, {track.Genre.Name}, {track.Album.Title}, {track.Milliseconds}, {track.UnitPrice}");
                Console.WriteLine("-------------------------------------------------------------- ");
                sumNoTax = sumNoTax + track.UnitPrice.Value;
            }

            Console.WriteLine($"Total without Tax: {Math.Round(sumNoTax, 2)}");
            Console.WriteLine($"Tax: 21%");
            Console.WriteLine($"Total: {Math.Round(sumNoTax + (sumNoTax * 0.21), 2)}");
            Console.WriteLine("");
            Console.WriteLine("Q. grįžti į pirkimo ekraną");
            Console.WriteLine("L. Išeiti");

            tracksSelected.Clear();

            var input = Console.ReadKey().KeyChar;
            Console.Clear();


            switch (input)
            {
                case 'q' or 'Q':
                    BuyMenu();
                    break;
                case 'l' or 'L':
                    Environment.Exit(0);
                    break;
                    Console.Clear();
                    Console.WriteLine("Tokio pasirinkimo nėra");
                    AddToBasketMenu();
                    break;
            }
            }

       
        public void ShowAllInvoices()
        {
            int customerId = (int)activeCustomer.CustomerId;
            List<Invoice> invoices = repository.GetCustomerInvoices(customerId);
           

            foreach (var invoice in invoices)
            {
                Console.WriteLine($"Invoice Id {invoice.InvoiceId}");
                Console.WriteLine($"Name:{invoice.Customer.FirstName}");
                Console.WriteLine($"Last Name:{invoice.Customer.LastName}");
                Console.WriteLine($"Adress:{invoice.Customer.Address}");
                Console.WriteLine($"Phone:{invoice.Customer.Phone}");
                Console.WriteLine($"Email:{invoice.Customer.Email}");
                double sumNoTax = 0;

                //Console.WriteLine("-------------------------------------------------------------- ");
                //Console.WriteLine("| #   |  Name, Composer, Genre, Album, Milliseconds, Price | ");
                //Console.WriteLine("-------------------------------------------------------------- ");
                //foreach (var track in invoice.InvoiceItems)
                //{
                //    Console.WriteLine($"{track.Track.Name}| {track.Track.Genre}, {track.Track.Album.Title}");
                //    Console.WriteLine("-------------------------------------------------------------- ");
                  
                //}

                Console.WriteLine($"Total without Tax: {invoice.Total}");
                Console.WriteLine($"Tax: 21%");                
                Console.WriteLine($"Total: {Math.Round(invoice.Total + (invoice.Total * 0.21), 2)}");
                Console.WriteLine("*******************************************************************");

            }

            Console.WriteLine("Q. grįžti į pirkimo ekraną");

            var choice = Console.ReadLine();
            if(choice == "q")
            { BuyMenu();}
            
        }

        public void EmployeeLogin()
        {
            Console.Clear();
            Console.WriteLine("Įveskite pin kodą");
            string pinGuess = Console.ReadLine();
            List<Employee> employeeList = repository.EmployeesList();

            if (pinGuess == pin)
            {
                Console.Clear();
                AdminMenu();
            }
            if(pinGuess != pin)
            {
                Console.Clear();
                Console.WriteLine("Neteisingas pin kodas");
            StartShop();
            }
        }

        public void AdminMenu()
        {
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| #       | Pasirinkimas | ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 1       | Keisti klientų duomenis | ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 2       | Pakeisti dainos statusą  | ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 3       | Statistika (Darbuotojams) | ");
            Console.WriteLine("--------------------------------------------------------------");

            char input = Console.ReadKey().KeyChar;
            Console.Clear();

            switch (input)
            {
                case '1':
                    AdminMenuModifyBuyers();
                    break;
                case '2':
                    ChangeSongStatus();
                    break;
                case '3':
                    Console.WriteLine("Enter pin code");
                    string pinGuess = Console.ReadLine();
                    if (pinGuess == pin)
                    {
                        Console.Clear();
                        Statistic();
                    }
                    else
                        Console.WriteLine("Neteisingas pin");
                    AdminMenu();                    
                    break;
                case 'q' or 'Q':
                    StartShop();
                    return;               
                default:
                    Console.WriteLine("Tokio pasirinkimo nėra");
                    AdminMenu();
                    break;
            }
        }

        public void AdminMenuModifyBuyers()
        {
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| #       | Pasirinkimas | ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 1       | Gauti pirkėjų sąrašą | ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 2       | Pašalinti pirkėją pagal ID  | ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 3       | Modifikuoti pirkėjo duomenis | ");
            Console.WriteLine("--------------------------------------------------------------");

            char input = Console.ReadKey().KeyChar;
            Console.Clear();

            switch (input)
            {
                case '1':
                    GetCustomerList();
                    break;
                case '2':
                    //
                    break;
                case '3':                    
                    UpdateCustomerInformation();
                    break;
                case 'q' or 'Q':
                    AdminMenu();
                    return;
                default:
                    Console.WriteLine("Tokio pasirinkimo nėra");
                    AdminMenu();
                    break;

            }

        }
        public void GetCustomerList()
        {
            Console.Clear();
            List<Customer> customerList = repository.CustomerList();
            foreach (Customer customer in customerList)
            {
                Console.WriteLine($"{customer.CustomerId} {customer.FirstName} {customer.LastName}");
            }
            Console.WriteLine("Q. Grižti");
            var input = Console.ReadLine();
            Console.Clear();
            if (input == "q")
            { AdminMenuModifyBuyers(); }

        }
        
        public void ChangeSongStatus()
        {
            Console.WriteLine("1.Gauti dainu sarasa");
            Console.WriteLine("2.Keisti dainos statusą");

            List<Track> tracksList = repository.ShowCatalogForAdmin();
            var input = Console.ReadKey().KeyChar;
            Console.Clear();

            if (input == '1')
            {
                Console.WriteLine("-------------------------------------------------------------- ");
                Console.WriteLine("| #   |  Name, Composer, Genre, Album, Milliseconds, Price | ");
                Console.WriteLine("-------------------------------------------------------------- ");
                foreach (var track in tracksList)
                {

                    Console.WriteLine($"{track.TrackId}| {track.Name}, {track.Composer}, {track.Genre.Name}, {track.Album.Title}, {track.Milliseconds}, {track.UnitPrice}");
                    Console.WriteLine("-------------------------------------------------------------- ");

                }
            }

            if (input == '2')
            {
                Console.WriteLine("Iveskite dainos id");
                var songId = Convert.ToInt64(Console.ReadLine());
                Track track = tracksList.First(s => s.TrackId == songId);

                var statusMsg = (track.Status == "Active") ? "Dainos statusas Active" : "Dainos statusas Inactive";
                Console.WriteLine($"Dainos Id: {track.TrackId}, dainos pavadinimas: {track.Name}, {statusMsg}, ar norite pakeisti");


                Console.WriteLine("1.Jeigu norite pakeisti dainos statusą");
                Console.WriteLine("Q jeigu norite grįžti");
                var choice = Console.ReadLine();
                Console.Clear();

               


                if (choice == "1" && track.Status == "Active")
                {
                    repository.UpdateSongStatus(songId, "Inactive");
                    ChangeSongStatus();
                }
                if (choice == "1" && track.Status == "Inactive")
                {
                    repository.UpdateSongStatus(songId, "Active");
                    ChangeSongStatus();
                }
                if (choice == "q")
                {
                    AdminMenu();
                }
            }
            AdminMenu();
        }

        public void Statistic()
        {
            Console.WriteLine("| 1.  | Išgauti visas kliento atąskaitas pagal kliento ID   |  ");
            Console.WriteLine("| 2.  | Išgauti veiklos pelna (Neatskaičius mokesčių pilna suma)    |  ");
            Console.WriteLine("| 3.  | Išgauti veiklos pelną pagal paduotus metus  |  ");
            Console.WriteLine("| 4.  | Išgauti kiek kokio žanro kūrinių buvo nupirkta (Rikiuota pagal dydį) |  ");
            Console.WriteLine("| 5.  | Išgauti kiek kiekvienas klienas išleido pinigų  |  ");


            var input = Console.ReadKey().KeyChar;
            Console.Clear();
            switch (input)
            {
                case '1':
                    Console.WriteLine("Iveskite Kliento Id");
                    int customerId = Convert.ToInt32(Console.ReadLine());
                    List<Invoice> invoices = repository.GetCustomerInvoices(customerId);
                    foreach (var invoice in invoices)
                    {
                        Console.WriteLine($"Sąskaitos id {invoice.InvoiceId} kliento id {invoice.CustomerId}, " +
                        $"sąskaitos data {invoice.InvoiceDate}, sąskaitos suma {invoice.Total}");
                    }
                    Console.WriteLine("Paspauskite bet kurį mygtuką, jeigu norite tęsti");
                    Console.ReadLine();
                    Console.Clear();
                    Statistic();
                    break;
                case '2':
                    List<Invoice> invoiceForTotalIncomeCount = repository.TotalIncome();
                    double totalIncome = 0;
                    foreach (var invoice in invoiceForTotalIncomeCount)
                    {
                        totalIncome = totalIncome + invoice.Total;
                    }
                    Console.WriteLine($"Visas pelnas be mokesčių yra: {Math.Round(totalIncome, 2)}");

                    Statistic();
                    break;
                case '3':
                    Console.WriteLine("Įveskite metus (yyyy)");
                    var date = Console.ReadLine();
                    List<Invoice> invoiceForTotalIncomePerYearCount = repository.TotalIncomeByYear(date);
                    if (invoiceForTotalIncomePerYearCount.Count == 0)
                    { Console.WriteLine("Šiais metais pardavimų nebuvo"); }
                    double totalIncomePerYear = 0;
                    foreach (var invoice in invoiceForTotalIncomePerYearCount)
                    {
                        totalIncomePerYear = totalIncomePerYear + invoice.Total;
                    }
                    Console.WriteLine($"Visas pelnas {date} metais yra: {Math.Round(totalIncomePerYear, 2)}");

                    Statistic();
                    break;            
                    
                case 'q':
                    AdminMenu();
                    break;
                case 'Q':
                    AdminMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Tokio pasirinkimo nėra");
                    Statistic();
                    break;
            }
        }

        private void UpdateCustomerInformation()
        {
            Console.WriteLine("Įveskite kliento Id, kurio duomenis norite pakeisti");
            long customerId = Convert.ToInt64(Console.ReadLine());
            Customer customer = repository.CustomerList().Find(c => c.CustomerId == customerId);

            Console.WriteLine($"\nKeičiate Name: {customer.FirstName}");
            string firstName = Console.ReadLine();
            Console.WriteLine($"\nKeičiate Last name: {customer.LastName}");
            string lastName = Console.ReadLine();
            Console.WriteLine($"\nKeičiate Company: {customer.Company}");
            string company = Console.ReadLine();
            Console.WriteLine($"\nKeičiate Adress: {customer.Address}");
            string adress = Console.ReadLine();
            Console.WriteLine($"\nKeičiate City: {customer.City}"); ;
            string city = Console.ReadLine();
            Console.WriteLine($"\nKeičiate County: {customer.Country}");
            string country = Console.ReadLine();
            Console.WriteLine($"\nKeičiate Postal code: {customer.PostalCode}");
            string postalCode = Console.ReadLine();
            Console.WriteLine($"\nKeičiate Phone: {customer.Phone}");
            string phone = Console.ReadLine();
            Console.WriteLine($"\nKeičiate Email: {customer.Email}");
            string email = Console.ReadLine();


            while (firstName.IsNullOrEmpty())
            {
                Console.WriteLine("Vardas privalo būti įvestas");
                firstName = Console.ReadLine();
            }

            while (lastName.IsNullOrEmpty())
            {
                Console.WriteLine("Neivedėte Pavardės");
                lastName = Console.ReadLine();
            }

            while (email.IsNullOrEmpty() || !email.Contains("@"))
            {
                Console.WriteLine("Neįvestas, arba neteisingai įvestas El. paštas");
                email = Console.ReadLine();
            }
            repository.UpdateCustomerInfo(customerId, firstName, lastName, adress, city, country, postalCode, phone, email);
            AdminMenuModifyBuyers();
        }

        public void PrintTrackList(List<Track> tracksList)
        {
            Console.WriteLine("-------------------------------------------------------------- ");
            Console.WriteLine("| #   |  Name, Composer, Genre, Album, Milliseconds, Price | ");
            Console.WriteLine("-------------------------------------------------------------- ");
            foreach (var track in tracksList)
            {
                Console.WriteLine($"{track.TrackId}| {track.Name}, {track.Composer}, {track.Genre.Name}, {track.Album.Title}, {track.Milliseconds}, {track.UnitPrice}");
                Console.WriteLine("-------------------------------------------------------------- ");
            }
        }

        
      
          
        }

    }
    
    



     

    

