using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albums.DataModel
{
    class AlbumModel : INotifyPropertyChanged
    {
        private String name;
        private int id;

        public event PropertyChangedEventHandler PropertyChanged;

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
            get { return name; }
            set
            {
                name = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

    }
}
