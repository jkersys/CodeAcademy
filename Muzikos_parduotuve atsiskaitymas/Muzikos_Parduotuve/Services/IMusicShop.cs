using Muzikos_Parduotuve.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muzikos_Parduotuve.Services
{
    public interface IMusicShop
    {
        public void StartShop();
        void BuyMenu();
        void SortByMenu();
        void ShowAllTracks();
        void SortSongsAToZ();
        void SortSongDescending();
        void SortSongsByComposer();
        void SortSongsByGenre();
        void SortSongsByComposerAndAlbum();

    }
}
