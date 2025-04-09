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

namespace BazaUczniow
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public MainWindow parent;
        public AddWindow()
        {
            InitializeComponent();
        }

        private bool checkFilled()
        {
            bool flag = true;
            if(i_pesel.Text.Trim().Length == 0)
            {
                i_pesel.Background = Brushes.Red;
                flag = false;
            }
            if (i_imie.Text.Trim().Length == 0)
            {
                i_imie.Background = Brushes.Red;
                flag = false;
            }
            if (i_nazwisko.Text.Trim().Length == 0)
            {
                i_nazwisko.Background = Brushes.Red;
                flag = false;
            }
            if (i_data.Text.Trim().Length == 0)
            {
                i_data.Background = Brushes.Red;
                flag = false;
            }
            if (i_adres.Text.Trim().Length == 0)
            {
                i_adres.Background = Brushes.Red;
                flag = false;
            }
            if (i_miejscowosc.Text.Trim().Length == 0)
            {
                i_miejscowosc.Background = Brushes.Red;
                flag = false;
            }
            if (i_kodPocztowy.Text.Trim().Length == 0)
            {
                i_kodPocztowy.Background = Brushes.Red;
                flag = false;
            }


            return flag;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (!checkFilled())
            {
                return;
            }

            parent.addItem(new Uczen(i_pesel.Text, i_imie.Text, i_drugieImie.Text, i_nazwisko.Text, i_data.Text, i_telefon.Text, i_adres.Text, i_miejscowosc.Text, i_kodPocztowy.Text));
        }
    }
}
