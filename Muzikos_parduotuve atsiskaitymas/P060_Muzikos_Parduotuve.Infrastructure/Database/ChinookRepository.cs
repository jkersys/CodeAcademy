using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Muzikos_Parduotuve.Domain.Models;
using Muzikos_Parduotuve.Infrastructure.Interfaces;
using Superpower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muzikos_Parduotuve.Infrastructure.Database
{
    public class ChinookRepository : IChinookRepository
    {
        public ChinookRepository()
        {
            using var context = new ChinookContext();
            // Naudojam kaip pavyzdi, kad zinoti, jog yra budas patikrinti ar duombaze siuo metu egzistuoja.
            // Jei neegzsituoja uz mus buna paleidziama CLI komanda update-datase
            context.Database.EnsureCreated();
        }



        public void AddUser(string firstName, string lastName, string? company, string? adress, string? city, string? state, string? country, string? postalCode, string? phone, string? fax, string email)
        {
            using var context = new ChinookContext();

            Customer customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Company = company,
                Address = adress,
                City = city,
                State = state,
                Country = country,
                PostalCode = postalCode,
                Phone = phone,
                Fax = fax,
                Email = email

            };



            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public List<Customer> CustomerList()
        {
            using var context = new ChinookContext();

            List<Customer> customers = context.Customers.ToList();
            return customers;
        }



        public List<Track> ShowCatalogForAdmin()
        {
            using var context = new ChinookContext();

            List<Track> activeTrack = context.Tracks.Include(x => x.Genre).Include(x => x.Album).ToList();
            return activeTrack;
        }






        //dainų sarašas kurį paduodu atliekant paieską ir rikiavimą
        public List<Track> SongList()
        {
            using var context = new ChinookContext();

            var trackList = context.Tracks.Where(x => x.Status == "Active").Include(x => x.Genre).Include(x => x.Album).ToList();
            return trackList;

        }




        public List<Track> SortSongs()
        {
            using var context = new ChinookContext();
            List<Track> tracksByName = context.Tracks.Where(x => x.Status == "Active").Include(x => x.Genre).Include(x => x.Album).OrderBy(x => x.Name).ToList();
            return tracksByName;
        }

        public List<Track> TracksByNameDecending()
        {
            using var context = new ChinookContext();
            var tracksByNameDecending = context.Tracks.Where(x => x.Status == "Active").Include(x => x.Genre).Include(x => x.Album).OrderByDescending(x => x.Name).ToList();
            return tracksByNameDecending;
        }

        public List<Track> TracksByComposer()
        {
            using var context = new ChinookContext();
            var tracksByComposer = context.Tracks.Where(x => x.Status == "Active").Include(x => x.Genre).Include(x => x.Album).OrderBy(x => x.Composer).ToList();
            return tracksByComposer;
        }

        public List<Track> TracksByGenre()
        {
            using var context = new ChinookContext();
            var tracksByGenre = context.Tracks.Where(x => x.Status == "Active").Include(x => x.Genre).Include(x => x.Album).OrderBy(x => x.Genre.Name).ToList();
            return tracksByGenre;
        }

        public List<Track> TracksByComposerAndAlbum()
        {
            using var context = new ChinookContext();
            var tracksByNameDecending = context.Tracks.Where(x => x.Status == "Active").Include(x => x.Genre).Include(x => x.Album).OrderBy(c => c.Composer).ThenBy(a => a.Album.Title).ToList();
            return tracksByNameDecending;
        }
            

        public List<Track> GetSongById(long songId)
        {
            using var context = new ChinookContext();
            var track = context.Tracks.Where(x => x.Status == "Active").Include(x => x.Genre).Include(x => x.Album).Where(i=>i.TrackId == songId).ToList();
            return track; 
        }
        public List<Track> SearchBySongName(string songName)
        {

            using var context = new ChinookContext();
            return context.Tracks.Where(x => x.Status == "Active").Include(x => x.Genre).Include(x => x.Album).Where(x => x.Name == songName).ToList();
           
        }

        public List<Track> SearchSongsByAlbumId(int albumId)
        {
            using var context = new ChinookContext();
            return context.Tracks.Where(x => x.Status == "Active").Include(x => x.Genre).Include(x => x.Album).Where(i => i.AlbumId == albumId).ToList();
            
        }

        public List<Track> SearchBySongAlbumName(string albumName)
        {
            using var context = new ChinookContext();
            return context.Tracks.Where(x => x.Status == "Active").Include(x => x.Genre).Include(x => x.Album).Where(i => i.Album.Title == albumName).ToList();
        }

        public List<Track> SearchByComposer(string composer)
        {

            using var context = new ChinookContext();
            return context.Tracks.Where(x => x.Status == "Active").Include(x => x.Genre).Include(x => x.Album).Where(c => c.Composer == composer).ToList();
        }

        public List<Track> SearchByGenre(string genre)
        {
            using var context = new ChinookContext();
            return context.Tracks.Where(x => x.Status == "Active").Include(x => x.Genre).Include(x => x.Album).Where(c => c.Genre.Name == genre).ToList();
                                    
        }

        public List<Track> SearchSongsByComposerAndAlbum(string composer, string album)
        {

            using var context = new ChinookContext();
            var searchByComposer = context.Tracks.Where(x => x.Status == "Active").Include(x => x.Genre).Include(x => x.Album).Where(c => c.Composer == composer).ToList();
          

            if (searchByComposer.Count == 0)
            {
                Console.WriteLine("Tokio kompozitoriaus saraše nėra");                
                
            }                       
           
            var searchByAlbum = searchByComposer.Where(i => i.Album.Title == album).ToList();
                      

            if (searchByComposer.Count > 0 && searchByAlbum.Count == 0)
            {
                Console.WriteLine("Tokio albumo saraše nėra");
            }

            return searchByAlbum;
        }

        //validacijas
        public List<Track> SearchSongsByLength(int length, int choice)
        {
            using var context = new ChinookContext();
            List<Track> songByLength;

            if (choice == 1)
            {
                songByLength = context.Tracks.Where(x => x.Status == "Active").Include(x => x.Genre).Include(x => x.Album).Where(i => i.Milliseconds > length).ToList();
                              
                return songByLength;
            }
            if (choice == 2)
            {
                songByLength = context.Tracks.Where(x => x.Status == "Active").Include(x => x.Genre).Include(x => x.Album).Where(i => i.Milliseconds < length).ToList();
                              
                return songByLength;
            }           
            else
            Console.WriteLine("Neteisinga įvestis");
            return null;
        }

        public List<Employee> EmployeesList()
        {
            using var context = new ChinookContext();

            List<Employee> employee = context.Employees.ToList();
            return employee;
        }

        public void UpdateSongStatus(long id, string status)
        {
            using var context = new ChinookContext();
            {
                var track = context.Tracks.Find(id);
                if (track is not null)
                {
                    track.Status = status;
                    context.SaveChanges();
                }
            }
        }
        public List<Invoice> GetCustomerInvoices(int customerId)
        {
            using var context = new ChinookContext();
            var userInvoices = context.Invoices.Include(x => x.Customer)
                .Where(i => i.Customer.CustomerId == customerId).ToList();
            return userInvoices;
        }

        public List<Invoice> TotalIncome()
        {
            using var context = new ChinookContext();
            List<Invoice> invoices = context.Invoices.ToList();
            return invoices;

        }

        public void AddInvoice(Invoice invoice)
        {
            using var context = new ChinookContext();
            context.Invoices.Add(invoice);
            context.SaveChanges();
        }

        public List<Invoice> TotalIncomeByYear(string date)
        {
            using var context = new ChinookContext();
            List<Invoice> invoices = context.Invoices.Where(x => x.InvoiceDate.Year.ToString() == date).ToList();
            return invoices;
        }

        public void UpdateCustomerInfo(long id, string firstName, string lastName, string? adress, string? city, 
            string? country, string? postalCode, string? phone, string email)
        {
            using var context = new ChinookContext();
            Customer customerSelected = context.Customers.Find(id); //CustomerList().First(x=>x.CustomerId == id);
            {
                customerSelected.FirstName = firstName;
                customerSelected.LastName = lastName;
                customerSelected.Address = adress;
                customerSelected.City = city;
                customerSelected.Country = country;
                customerSelected.PostalCode = postalCode;
                customerSelected.Phone = phone;
                customerSelected.Email = email;


                context.SaveChanges();
            }

        }

        

    }
    }
    
    




