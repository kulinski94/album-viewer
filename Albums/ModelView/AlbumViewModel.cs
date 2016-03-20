using Albums.DataModel;
using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albums.ModelView
{
    class AlbumViewModel
    {
        public LinkCollection Albums
        {
            get;
            set;
        }

        public void GetList()
        {
            //TODO - get from file
            ObservableCollection<AlbumModel> a = new ObservableCollection<AlbumModel>();
            a.Add(new AlbumModel() { Name = "BirthDay - 2015", Id = 1 });
            a.Add(new AlbumModel() { Name = "BirthDay - 2014", Id = 2 });

            LinkCollection links = new LinkCollection();
            foreach (var album in a)
            {
                links.Add(new Link() { DisplayName = album.Name });
            }
            Albums = links;
        }

        public void SaveList()
        {
            //TODO - save to file
        }
    }
}
