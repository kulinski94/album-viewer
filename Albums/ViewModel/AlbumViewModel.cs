using Albums.DataModel;
using FirstFloor.ModernUI.Presentation;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media;

namespace Albums.ViewModel
{
    public class AlbumViewModel : ObservableObject
    {
        private AlbumModel selectedAlbum;
        private PhotoModel selectedPhotoModel;
        private string directoryPath;
        private BitmapImage selected_image;

        public AlbumViewModel()
        {
            DirectoryPath = "D:\\TU-SOFIA\\images";
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

        public string DirectoryPath
        {
            get
            {
                return directoryPath;
            }
            set
            {
                directoryPath = value;
                loadImagesFromDirectory();
                RaisePropertyChangedEvent("DirectoryPath");
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
                var image = new BitmapImage();
                if (value != null)
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(value.Source);
                    image.EndInit();
                }
                image.Freeze();
                SelectedImage = image;
                RaisePropertyChangedEvent("SelectedPhoto");
            }
        }

        public BitmapImage SelectedImage
        {
            get
            {
                return selected_image;
            }
            set
            {
                selected_image = value;
                RaisePropertyChangedEvent("SelectedImage");
            }
        }

        public ICommand NextPhoto
        {
            get { return new DelegateCommand(nextPhoto); }
        }

        private void nextPhoto()
        {
            if (SelectedPhoto == null)
                return;
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
            if (SelectedPhoto == null)
                return;
            int index = SelectedAlbum.Photos.IndexOf(SelectedPhoto);
            if (selectedAlbum.Photos.Count > 0 && index > 0)
                SelectedPhoto = SelectedAlbum.Photos.ElementAt(--index);
        }

        public ICommand ChangeDirectory
        {
            get { return new DelegateCommand(changeDir); }
        }

        public void changeDir()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result.Equals(CommonFileDialogResult.Ok))
            {
                DirectoryPath = dialog.FileName;
            }
        }

        private void loadImagesFromDirectory()
        {
            if (!Directory.Exists(DirectoryPath))
                return;

            string[] files = Directory.GetFiles(DirectoryPath);
            AlbumModel am = new AlbumModel();
            am.Photos = new List<PhotoModel>();
            foreach (string source in files)
            {
                if (File.Exists(source) && (source.ToLower().EndsWith("jpg") || source.ToLower().EndsWith("png")))
                {
                    PhotoModel ph = new PhotoModel(source, source);
                    am.Photos.Add(ph);
                }
            }
            SelectedAlbum = am;
        }

        public ICommand DeletePhoto
        {
            get { return new DelegateCommand(deletePhoto); }
        }

        public void deletePhoto()
        {
            if (SelectedAlbum == null || SelectedPhoto == null || !File.Exists(SelectedPhoto.Source))
                return;

            PhotoModel toDelete = SelectedPhoto;
            try
            {
                File.Delete(toDelete.Source);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot delete image!!");
                return;
            }
            if (SelectedAlbum.Photos.Remove(toDelete) && SelectedAlbum.Photos.Count > 0)
                SelectedPhoto = SelectedAlbum.Photos.ElementAt(0);
            else
            {
                SelectedPhoto = null;
            }
            RaisePropertyChangedEvent("SelectedPhoto");
        }

        public ICommand RotatePhoto
        {
            get { return new DelegateCommand(rotatePhoto); }
        }

        public void rotatePhoto()
        {
            // Create the source to use as the tb source.
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.UriSource = new Uri(SelectedPhoto.Source, UriKind.RelativeOrAbsolute);
            bi.Rotation = rotation(SelectedImage.Rotation);
            bi.EndInit();  
            SelectedImage = bi;           
        }

        private Rotation rotation(Rotation rotation)
        {
            switch (rotation)
            {
                case Rotation.Rotate0:
                        return Rotation.Rotate270;
                case Rotation.Rotate270:
                    return Rotation.Rotate180;
                case Rotation.Rotate180:
                    return Rotation.Rotate90;
                case Rotation.Rotate90:
                    return Rotation.Rotate0;
            }
            return Rotation.Rotate0;            
        }

    }
}
