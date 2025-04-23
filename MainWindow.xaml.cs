using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace BazaUczniow
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
  
    public class Uczen
    {
        public string m_PESEL {get; set;}
        public string m_imie {get; set;}
        public string m_drugieImie {get; set;}
        public string m_nazwisko {get; set;}
        public string m_data {get; set;}
        public string m_telefon {get; set;}
        public string m_adres { get; set; }
        public string m_miejscowosc {get; set;}
        public string m_kodPocztowy {get; set;}

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

        public Uczen()
        {

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

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            AddWindow w = new AddWindow();
            w.parent = this;
            w.Show();
        }

        private void Usun_Click(object sender, RoutedEventArgs e)
        {
            while (listView.SelectedItems.Count > 0)
            {
                listView.Items.Remove(listView.SelectedItems[0]);
            }
        }

        private void Zaznaczzle_Click(object sender, RoutedEventArgs e)
        {
            //int i = 0;
            //foreach(Uczen uczen in listView.Items)
            //{
            //    Type ucz = listView.Items.GetItemAt(i).GetType();
            //    if(uczen.m_PESEL.Length != 11)
            //    {
            //        ucz.IsSelected = true;
            //    }
            //    int control = 0;
            //    int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            //    for(int j = 0; j < 10; ++j) {
            //        control += ((uczen.m_PESEL[j] - '0') * weights[j]) % 10;
            //    }

            //    control = 10 - (control % 10);
            //    if(control != (uczen.m_PESEL[10] - '0'))
            //    {
            //        ucz.IsSelected = false;
            //    }

            //    ++i;
            //}
        }

        private void Button_Click(MainWindow _) { }

        private void wczytaj(bool clear)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki CSV z separatorem (;) |*.csv|Pliki CSV z separatorem (,) |*.csv";
            openFileDialog.Title = "Otwórz plik CSV";
            if (openFileDialog.ShowDialog() == true)
            {
                if (clear)
                {
                    listView.Items.Clear();
                }
                string filePath = openFileDialog.FileName;
                int selectedFilterIndex = openFileDialog.FilterIndex;
                char delimiter = ',';
                if (selectedFilterIndex == 1)
                {
                    delimiter = ';';
                }
                Encoding encoding = Encoding.UTF8;
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath, encoding);
                    bool skip = true;
                    foreach (var line in lines)
                    {
                        if (skip)
                        {
                            //pomijamy tytuły kolumn
                            skip = false;
                            continue;
                        }
                        string[] columns = line.Split(delimiter);
                        if (columns != null)
                        {
                            Uczen uczen = new Uczen();
                            uczen.m_PESEL = columns.ElementAtOrDefault(0);
                            uczen.m_imie = columns.ElementAtOrDefault(1);
                            uczen.m_drugieImie = columns.ElementAtOrDefault(2);
                            uczen.m_nazwisko = columns.ElementAtOrDefault(3);
                            uczen.m_data = columns.ElementAtOrDefault(4);
                            uczen.m_telefon = columns.ElementAtOrDefault(5);
                            uczen.m_adres = columns.ElementAtOrDefault(6);
                            uczen.m_miejscowosc = columns.ElementAtOrDefault(7);
                            uczen.m_kodPocztowy = columns.ElementAtOrDefault(8);
                            listView.Items.Add(uczen);
                        }
                    }
                }
            }
        }

        private void Wczytaj_Click(object sender, RoutedEventArgs e)
        {
            wczytaj(true);
        }

        private void Dodaj2_Click(object sender, RoutedEventArgs e)
        {
            wczytaj(false);
        }

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pliki CSV z separatorem (;) |*.csv|Pliki CSV z separatorem (,) |*.csv";
            saveFileDialog.Title = "Zapisz jako plik CSV";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                string delimiter = ",";
                if (saveFileDialog.FilterIndex == 1)
                {
                    delimiter = ";";
                }
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"PESEL{delimiter}Imię{delimiter}Drugie imię{delimiter}Nazwisko{delimiter}Data urodzenia{delimiter}Numer telefonu{delimiter}Adres zamieszkania{delimiter}Miejscowość{delimiter}Kod pocztowy");
                    foreach (Uczen item in listView.Items)
                    {
                        var row = $"{item.m_PESEL}{delimiter}{item.m_imie}" +
                        $"{delimiter}{item.m_drugieImie}{delimiter}{item.m_nazwisko}" +
                        $"{delimiter}{item.m_data}{delimiter}{item.m_telefon}" +
                        $"{delimiter}{item.m_adres}{delimiter}{item.m_miejscowosc}{delimiter}{item.m_kodPocztowy}";
                        writer.WriteLine(row);
                    }
                }

            }
        }
    }
}
