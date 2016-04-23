using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Albums.DataModel
{
    [Serializable]
    public class PhotoModel
    {
        private String source;
        private String name;
        private BitmapImage image;

        public PhotoModel(String source,String name)
        {
            this.image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(source);
            image.EndInit();
            this.source = source;
            this.name = name;
        }

        public String Source
        {
            get
            {
                return source;
            }
        }

        public String Name
        {
            get
            {
                return name;
            }
        }

        public BitmapImage Image
        {
            get
            {
                return image;
            }
        }

    }
}
