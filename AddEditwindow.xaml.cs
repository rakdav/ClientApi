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
using System.Windows.Shapes;

namespace ClientApi
{
    /// <summary>
    /// Логика взаимодействия для AddEditwindow.xaml
    /// </summary>
    public partial class AddEditwindow : Window
    {
        public User NewUser { get; set; }
        public AddEditwindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            NewUser = new User();
            NewUser.Name = NameBox.Text;
            NewUser.Age = int.Parse(AgeBox.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
