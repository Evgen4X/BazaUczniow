using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

        private bool IsAllFilled()
        {
            bool flag = true;
            List<TextBox> inputs = new List<TextBox>() { i_pesel, i_imie, i_nazwisko, i_adres, i_miejscowosc, i_kodPocztowy };
            foreach(TextBox input in inputs)
            {
                if(input.Text.Trim().Length == 0)
                {
                    input.Background = Brushes.Red;
                    flag = false;
                } else
                {
                    input.Background = Brushes.White;
                }
            }

            if(i_data.Text == "Podaj numer PESEL" || i_data.Text.Trim().Length == 0) {
                i_data.Background = Brushes.Red;
                flag = false;
            }

            return flag;
        }

        private bool IsAnyFilled()
        {
            List<TextBox> inputs = new List<TextBox>() { i_pesel, i_imie, i_nazwisko, i_adres, i_miejscowosc, i_kodPocztowy };
            foreach (TextBox input in inputs)
            {
                if (input.Text.Trim().Length > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAllFilled())
            {
                l_info.Content = "Wypełnij wszystkie wymagane pola";
                return;
            }

            parent.addItem(new Uczen(i_pesel.Text, i_imie.Text, i_drugieImie.Text, i_nazwisko.Text, i_data.Text, i_telefon.Text, i_adres.Text, i_miejscowosc.Text, i_kodPocztowy.Text));
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAnyFilled())
            {
                Close();
            }
            else
            {
                if (MessageBox.Show("Napewno chcesz anulować?", "Anulowanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Close();
                }
            }
        }

        private void Generuj_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            string pesel = rnd.Next(10, 99).ToString();
            int month = rnd.Next(1, 12) + (20 * rnd.Next(0, 4));
            if(month < 10)
            {
                pesel += '0';
            }
            pesel += month.ToString();
            pesel += rnd.Next(10, 28).ToString();
            pesel += rnd.Next(1000, 9999).ToString();

            i_pesel.Text = pesel;

            List<string> imiona = new List<string>() { "Jan", "Anna", "Marcin", "Jolanta" };
            i_imie.Text = imiona[rnd.Next(0, imiona.Count)];
            i_drugieImie.Text = imiona[rnd.Next(0, imiona.Count)];

            List<string> nazwiska = new List<string>() { "Kowalski", "Nowak", "Kowalska" };
            i_nazwisko.Text = nazwiska[rnd.Next(0, nazwiska.Count)];

            List<string> miejscowosci = new List<string>() { "Tarnów", "Kraków", "Warszawa", "Washington" };
            i_miejscowosc.Text = miejscowosci[rnd.Next(0, miejscowosci.Count)];

            i_adres.Text = "ul. Polna 3";
            i_kodPocztowy.Text = "33-103";
        }

        private void i_pesel_TextChanged(object sender, TextChangedEventArgs e)
        {
            string pesel = "";
            foreach(char c in i_pesel.Text)
            {
                if('0' <= c && c <= '9')
                {
                    pesel += c;
                }
            }

            i_pesel.Text = pesel;
            if(pesel.Length > 5)
            {
                string yy = pesel.Substring(0, 2);
                string mm = pesel.Substring(2, 2);
                string dd = pesel.Substring(4, 2);

                int day = Convert.ToInt32(dd);
                int month = Convert.ToInt32($"{mm[0] % 2}{mm[1]}");
                int year = Convert.ToInt32(yy) + 1800;
                if (mm[0] <= '1')
                {
                    year += 100;
                } else if (mm[0] <= '3')
                {
                    year += 200;
                } else if (mm[0] <= '5')
                {
                    year += 300;
                }
                else if (mm[0] <= '7')
                {
                    year += 400;
                }

                try
                {
                    DateTime data = new DateTime(year, month, day);
                    i_data.Text = data.ToShortDateString();
                } catch (Exception)
                {
                    i_data.Text = "00.00.0000";
                    l_info.Content = "Numer PESEL nie istnieje";
                }
            } else
            {
                i_data.Text = "Podaj numer PESEL";
            }
        }
    }
}
