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

namespace BazaUczniow
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
  
    public class Uczen
    {
        public string m_PESEL;
        public string m_imie;
        public string m_drugieImie;
        public string m_nazwisko;
        public string m_data;
        public string m_telefon;
        public string m_adres;
        public string m_miejscowosc;
        public string m_kodPocztowy;

        public Uczen(string m_PESEL, string m_imie, string m_drugieImie, string m_nazwisko, string  m_data, string m_telefon, string m_adres, string m_miejscowosc, string m_kodPocztowy)
        {
            this.m_PESEL = m_PESEL;
            this.m_imie = m_imie;
            this.m_drugieImie = m_drugieImie;
            this.m_nazwisko = m_nazwisko;
            this.m_data = m_data;
            this.m_telefon = m_telefon;
            this.m_adres = m_adres;
            this.m_miejscowosc = m_miejscowosc;
            this.m_kodPocztowy = m_kodPocztowy;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void addItem(Uczen uczen)
        {
            listView.Items.Add(uczen);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddWindow w = new AddWindow();
            w.parent = this;
            w.Show();
        }
    }
}
