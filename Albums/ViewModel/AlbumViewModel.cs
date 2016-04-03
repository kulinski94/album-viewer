using Albums.DataModel;
using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Albums.ViewModel
{
    public class AlbumViewModel : ObservableObject
    {
        private ObservableCollection<AlbumModel> albumCollection;

        public AlbumViewModel()
        {
            GetList();
        }

        public BitmapImage Image
        {
            get
            {
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri("E:/PTS - kursova/album1/image1.jpg");
                logo.EndInit();
                return logo;
            }
        }

        public ObservableCollection<AlbumModel> AlbumCollection
        {
            get
            {
                return albumCollection;
            }
            set
            {
                albumCollection = value;
                RaisePropertyChangedEvent("AlbumCollection");
            }
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
