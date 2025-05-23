namespace adresbok

{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using MVVM.Models;
    using System.Linq;
    using Microsoft.Maui.Controls;

    public partial class MainPage : ContentPage
    {
        int count = 0;
        public event PropertyChangedEventHandler PropertyChanged;


        ObservableCollection<Osoby> osoby { get; set; } = new ObservableCollection<Osoby>();

        public MainPage()
        {
            InitializeComponent();
            rzecz.BindingContext = osoby;
            rzecz.ItemsSource = osoby;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dane.Text) && !string.IsNullOrEmpty(dane1.Text) && !string.IsNullOrEmpty(dane2.Text))
            {
                if (dane.Text.All(char.IsDigit))
                {
                    osoby.Add(new Osoby(dane.Text.ToString(), dane1.Text.ToString(), dane2.Text.ToString()));
                    dane.Text = "";
                    dane1.Text = "";
                    dane2.Text = "";
                }

            }
        }





        private async  void rzecz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(rzecz.SelectedItem == null)
            {
                return;
            }
            if (rzecz.SelectedItem != null)
            {
                var tryb = await DisplayAlert("wybrałeś element", "co chcesz z nim zrobić", "usuń", "edytuj"); 
                int index = osoby.IndexOf((Osoby)rzecz.SelectedItem);

                if (tryb)
                {
                    
                    osoby.RemoveAt(index);

                }
                else
                {
                    editForm.IsVisible = true;
                    dodawanie.IsVisible = false;
                    editAdres.Focus();
                    editAdres.Text = osoby[index].Adres;
                    editNumer.Text = osoby[index].Numer;
                    editName.Text = osoby[index].Imie;
                    /*
                    string rzecz1 = await  DisplayPromptAsync("edycja", "adres");
                    string rzecz2 = await DisplayPromptAsync("edycja", "imie");
                    string rzecz3 = await DisplayPromptAsync("edycja", "numer");
                    if(!string.IsNullOrEmpty(rzecz3) && !string.IsNullOrEmpty(rzecz2) && !string.IsNullOrEmpty(rzecz1))
                    {
                        osoby[index] = new Osoby(rzecz3, rzecz2, rzecz1);
                    }
                    */
                }
            }
        }
        private void Button_Clicked_2(object sender, EventArgs e)
        {
            int index = osoby.IndexOf((Osoby)rzecz.SelectedItem);

            if (!string.IsNullOrEmpty(editAdres.Text) && !string.IsNullOrEmpty(editName.Text) && !string.IsNullOrEmpty(editNumer.Text))
            {
                if (editNumer.Text.All(char.IsDigit))
                {
                    ObservableCollection<Osoby> tmp = new ObservableCollection<Osoby>();
                    foreach(Osoby osoba in osoby)
                    {
                        tmp.Add(osoba);
                    }
                    
                    var p = tmp[index];
                    p.Numer = editNumer.Text;
                    p.Imie = editName.Text;
                    p.Adres = editAdres.Text;

                    tmp[index] = p;
                    if (true)
                    {
                        Console.WriteLine("kurwy ");
                    }
                    osoby = tmp;
                    rzecz.BindingContext = null;
                    rzecz.ItemsSource = null;
                    rzecz.BindingContext = osoby;
                    rzecz.ItemsSource = osoby;
                    rzecz.SelectedItem = null;
                }

            }
            editForm.IsVisible = false;
            dodawanie.IsVisible = true;

        }


        private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            ObservableCollection<Osoby> wyszakane = new ObservableCollection<Osoby>();
            foreach (Osoby osoba in osoby)
            {
                string full = osoba.Numer + osoba.Imie + osoba.Adres;
                if (full.Contains(searchBar.Text.ToLower()))
                {
                    wyszakane.Add(osoba);
                }
            }
            if (wyszakane.Count > 0)
            {
                wyszukane.BindingContext = wyszakane;
                wyszukane.ItemsSource = wyszakane;
                wyszukane.IsVisible = true;
            }

        }
    }
}
