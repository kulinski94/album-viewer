using Albums.DataModel;
using FirstFloor.ModernUI.Presentation;
using Microsoft.Win32;
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
        private PhotoModel selectedPhotoModel;
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
        public PhotoModel SelectedPhoto
        {
            get
            {
                return selectedPhotoModel;
            }
            set
            {
                selectedPhotoModel = value;
                RaisePropertyChangedEvent("SelectedPhoto");
            }
        }
        public ICommand NextPhoto
        {
            get { return new DelegateCommand(nextPhoto); }
        }

        private void nextPhoto()
        {
            int index = SelectedAlbum.Photos.IndexOf(SelectedPhoto);
            if (selectedAlbum.Photos.Count > index + 1)
                SelectedPhoto = SelectedAlbum.Photos.ElementAt(++index);
        }

        public ICommand PreviousPhoto
        {
            get { return new DelegateCommand(previousPhoto); }
        }

        private void previousPhoto()
        {
            int index = SelectedAlbum.Photos.IndexOf(SelectedPhoto);
            if (selectedAlbum.Photos.Count > 0 && index > 0)
                SelectedPhoto = SelectedAlbum.Photos.ElementAt(--index);
        }

        public ICommand CreateAlbum
        {
            get { return new DelegateCommand(create); }
        }

        public void create()
        {
            if(AlbumName == null || AlbumName == "")
            {
                return;
            }
            AlbumModel am = new AlbumModel();
            am.Id = new Random().Next();
            am.Name = AlbumName;
            am.Photos = new List<PhotoModel>();
            AlbumCollection.Add(am);
            RaisePropertyChangedEvent("AlbumCollection");
            SelectedAlbum = am;
            SaveList();
        }

        public ICommand AddPhoto
        {
            get { return new DelegateCommand(addPhotoToSelectedAlbum); }
        }

        public void addPhotoToSelectedAlbum()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                PhotoModel ph = new PhotoModel(openFileDialog.FileName, openFileDialog.SafeFileName);
                Console.WriteLine(openFileDialog.FileName);
                AlbumModel album = SelectedAlbum;
                SelectedAlbum = null;
                album.addPhoto(ph);
                SelectedAlbum = album;
                SelectedPhoto = ph;
                SaveList();
            }
        }

        public ICommand DeleteAlbum
        {
            get { return new DelegateCommand(deleteAlbum); }
        }

        public ICommand DeletePhoto
        {
            get { return new DelegateCommand(deletePhoto); }
        }
        public void deletePhoto()
        {            
            SelectedAlbum.Photos.Remove(SelectedPhoto);
            if (SelectedAlbum.Photos.Count > 0)
                SelectedPhoto = SelectedAlbum.Photos.ElementAt(0);
            SaveList();
        }

        public void deleteAlbum()
        {
            AlbumCollection.Remove(selectedAlbum);
            if (AlbumCollection.Count > 0)
                SelectedAlbum = AlbumCollection.ElementAt(0);
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
                if (value == null)
                {
                    return;
                }
                selectedAlbum = value;
                if (selectedAlbum.Photos.Count > 0)
                {
                    SelectedPhoto = selectedAlbum.Photos.ElementAt(0);
                }
                else
                {
                    SelectedPhoto = null;
                }
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

                stream.Close();
            }

            List<PhotoModel> photos = new List<PhotoModel>();
            SelectedAlbum = AlbumCollection.ElementAt(0);
        }

        public void SaveList()
        {
            using (Stream stream = File.Open("./albums.bin", FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                if (AlbumCollection.Count > 0)
                    bformatter.Serialize(stream, AlbumCollection);

                Console.WriteLine("Save albums to file");
            }
        }
    }
}
