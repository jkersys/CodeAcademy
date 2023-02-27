using Muzikos_Parduotuve.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muzikos_Parduotuve.Infrastructure.Interfaces
{
    public interface IChinookRepository
    {
        List<Customer> CustomerList();
        void AddUser(string firstName, string lastName, string? company, string? 
            adress, string? city, string? state, string? country, 
            string? postalCode, string? phone, string? fax, string email);
        List<Employee> EmployeesList();


        List<Track> ShowCatalogForAdmin();
        List<Track> SongList();


       
       

        void AddInvoice(Invoice invoice);
        List<Track> SortSongs();
        List<Track> TracksByNameDecending();
        List<Track> TracksByComposer();
        List<Track> TracksByGenre();
        List<Track> TracksByComposerAndAlbum();
        List<Track> GetSongById(long songId);
        List<Track> SearchBySongName(string songName);
        List<Track> SearchByComposer(string composer);
        List<Track> SearchByGenre(string genre);
        List<Track> SearchSongsByComposerAndAlbum(string composer, string album);
        List<Track> SearchSongsByLength(int length, int choice);
        List<Track> SearchSongsByAlbumId(int albumId);
        List<Track> SearchBySongAlbumName(string albumName);
        void UpdateSongStatus(long id, string status);
        List<Invoice> GetCustomerInvoices(int customerId);
        List<Invoice> TotalIncome();
        List<Invoice> TotalIncomeByYear(string date);
        //void UpdateCustomerInfo(long id, Customer customer);
        void UpdateCustomerInfo(long id, string firstName, string lastName, string?
            adress, string? city, string? country,
            string? postalCode, string? phone, string email);
        //void SongListToPrintInvoice(Customer id);





    }
}
