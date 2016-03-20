using Albums.DataModel;
using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albums.ModelView
{
    class AlbumViewModel
    {
        public LinkCollection AlbumLinks
        {
            get;
            set;
        }

        public ObservableCollection<AlbumModel> Albums
        {
            get;
            set;
        }

        public void GetList()
        {
            using (Stream stream = File.Open("albums.bin", FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                Albums = (ObservableCollection<AlbumModel>)bformatter.Deserialize(stream);
            }

            LinkCollection links = new LinkCollection();
            foreach (var album in Albums)
            {
                links.Add(new Link() { DisplayName = album.Name });
            }
            AlbumLinks = links;  
        }

        public void SaveList()
        {
            using (Stream stream = File.Open("albums.bin", FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, Albums);
            }
        } 
    }
}
