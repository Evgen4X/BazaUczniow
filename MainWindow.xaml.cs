using Microsoft.Win32;
using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BazaUczniow
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    
    public class Tools
    {
        public static int getControlDigit(string pesel)
        {
            int sum = 0;
            int[] weights = { 1, 3, 7, 9 };
            for (int i = 0; i < 10; ++i)
            {
                sum += ((pesel[i] - '0') * weights[i % 4]) % 10;
            }

            return 10 - (sum % 10);
        }
    }
  
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
            int i = 0;
            foreach (Uczen uczen in listView.Items)
            {
                if (!checkPesel(uczen.m_PESEL))
                {
                    listView.SelectedItems.Add(uczen);
                }
            }
        }

        private bool checkPesel(string pesel)
        {
            if(pesel.Length != 11)
            {
                Debug.WriteLine("!");
                return false;
            }
            string yy = pesel.Substring(0, 2);
            string mm = pesel.Substring(2, 2);
            string dd = pesel.Substring(4, 2);

            int day = Convert.ToInt32(dd);
            int month = Convert.ToInt32($"{mm[0] % 2}{mm[1]}");
            int year = Convert.ToInt32(yy) + 1800;
            if (mm[0] <= '1')
            {
                year += 100;
            }
            else if (mm[0] <= '3')
            {
                year += 200;
            }
            else if (mm[0] <= '5')
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
            }
            catch (Exception)
            {
                Debug.WriteLine("!!");
                return false;
            }

            if (Tools.getControlDigit(pesel) != pesel[10] - '0')
            {
                Debug.Write("!!!");
                return false;
            }
                 

            return true;
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

        private void About_Click(object sender, RoutedEventArgs e)
        {
            String text = "Program desktopowy BazaUczniow napisany w języku C# z użyciem WPF\n" +
                "Opcja 'generuj' przy dodawaniu ucznia zawsze generuje poprawne numery PESEL";
            MessageBox.Show(text, "O programie", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
