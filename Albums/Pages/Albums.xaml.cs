using Albums.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Albums.Pages
{
    public partial class Albums : UserControl
    {
        private AlbumViewModel albumViewModel;

        public Albums()
        {
            InitializeComponent();
            albumViewModel = new AlbumViewModel();
            albumViewModel.GetList();
            this.DataContext = albumViewModel.AlbumLinks;
        }
       
    }
}
