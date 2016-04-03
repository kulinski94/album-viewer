using Albums.DataModel;
using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Albums.ViewModel
{
    public class AlbumViewModel : ObservableObject
    {
        private ObservableCollection<AlbumModel> albumCollection;
        private AlbumModel selectedAlbum;
        private String albumName;
        public AlbumViewModel()
        {
            GetList();
        }
        public String AlbumName
        {
            get
            {
                return albumName;
            }
            set
            {
                albumName = value;
                RaisePropertyChangedEvent("AlbumName");
            }
        }

        public ICommand CreateAlbum
        {
            get { return new DelegateCommand(create); }
        }

        public void create()
        {
            AlbumModel am = new AlbumModel();
            am.Id = new Random().Next();
            am.Name = AlbumName;
            AlbumCollection.Add(am);
            Console.WriteLine("Added new album:" + am);
            Console.WriteLine("Added new album:" + AlbumCollection.Count);
            RaisePropertyChangedEvent("AlbumCollection");
            SaveList();
        }

        public AlbumModel SelectedAlbum
        {
            get
            {
                return selectedAlbum;
            }
            set
            {
                selectedAlbum = value;
                Console.WriteLine("Selected Album Chnaged");
                RaisePropertyChangedEvent("SelectedAlbum");
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

            List<PhotoModel> photos = new List<PhotoModel>();
            photos.Add(new PhotoModel("E:/image1.jpg", "Photo1"));
            photos.Add(new PhotoModel("E:/image2.jpg", "Photo2"));
            AlbumCollection.ElementAt(0).Photos = photos;
            SelectedAlbum = AlbumCollection.ElementAt(0);
        }

        public void SaveList()
        {
            using (Stream stream = File.Open("./albums.bin", FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                if (AlbumCollection.Count > 0)
                    bformatter.Serialize(stream, AlbumCollection);
            }
        }
    }
}
