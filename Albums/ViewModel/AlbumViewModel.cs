using Albums.DataModel;
using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albums.ViewModel
{
    public class AlbumViewModel
    {
        private LinkCollection albumLinks;
        private ObservableCollection<AlbumModel> albumCollection;

        public AlbumViewModel()
        {
            GetList();
        }

        public string ButtonContent
        {
            get
            {
                return "Click Me";
            }
        }

        public LinkCollection AlbumLinks
        {
            get { return albumLinks; }
            set { albumLinks = value; }
        }

        public ObservableCollection<AlbumModel> AlbumCollection
        {
            get { return albumCollection; }
            set { albumCollection = value; }
        }

        public void GetList()
        {
            AlbumCollection = new ObservableCollection<AlbumModel>();
            using (Stream stream = File.Open("albums.bin", FileMode.OpenOrCreate))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                if (stream.Length > 0)
                    AlbumCollection = (ObservableCollection<AlbumModel>)bformatter.Deserialize(stream);
            }

            LinkCollection links = new LinkCollection();
            foreach (var album in AlbumCollection)
            {
                links.Add(new Link() { DisplayName = album.Name, Source = new Uri("/Pages/Photo.xaml?albumId=" + album.Id, UriKind.Relative) });
            }
            AlbumLinks = links;
        }

        public void SaveList()
        {
            using (Stream stream = File.Open("albums.bin", FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                if (AlbumCollection.Count > 0)
                    bformatter.Serialize(stream, AlbumCollection);
            }
        }
    }
}
