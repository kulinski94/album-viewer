using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albums.DataModel
{
    [Serializable]
    public class AlbumModel
    {
        private String name;
        private int id;
        private List<PhotoModel> photos;

        public AlbumModel()
        {
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public List<PhotoModel> Photos
        {
            get
            {
                return photos;
            }
            set
            {
                photos = value;
            }
        }

    }
}
