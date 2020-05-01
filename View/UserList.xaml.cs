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
using UserDirectory.ViewModel;

namespace UserDirectory.View
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList : UserControl
    {
        readonly UserListViewModel objUserLst;
        public UserList()
        {
            InitializeComponent();
            objUserLst = new UserListViewModel();
            this.DataContext = objUserLst;
            Loaded += UserList_Loaded;
        }

        private void UserList_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= UserList_Loaded;
            if (objUserLst!=null)
            {
                objUserLst.Initialize(); 
            }
        }
    }
}
