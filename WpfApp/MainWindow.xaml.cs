using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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
using Domain;
using Domain.OMDb;
using Film = Domain.OMDb.Film;
using Path = System.IO.Path;
using SearchResult = Domain.OMDb.SearchResult;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Engine engine;
        private SearchResult searchResult;
        private int i;
        private StringBuilder sb;
        private Uri NAPosterUri;
        private Domain.Film SelectedFilm { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            engine = new Engine();
            sb = new StringBuilder();
            try
            {
                NAPosterUri = new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "No_image_poster.png"));
            }
            catch
            {
                
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            searchResult = engine.GetOMDbResponse(Search.Text);
            
            if (searchResult.Response)
            {
                filmList.ItemsSource = null;
                filmList.Items.Clear();
                filmList.ItemsSource = searchResult.Search.Select(x => Domain.Film.FromOMDbFilm(x));
            }
            else
            {
                ListBoxItem lbi = new ListBoxItem();
                lbi.Content = "Nothing is found";
                filmList.ItemsSource = null;
                filmList.Items.Add(lbi);
                sb.Clear();
                sb.AppendLine("Nothing is found");
            }

            sb.Clear();
        }
        
        private void DisplayFilm(Film? film)
        {
            if (film == null) return;
            sb.AppendLine("---------OMDbFilm----------");
            var props = film.GetType().GetProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(film);
                if (value != null) sb.AppendLine($"{prop.Name} = {value}");
            }

            sb.AppendLine("-------------------");
        }

        private void DisplayFilm(Domain.Film? film)
        {
            if (film == null) return;
            sb.AppendLine("---------Film----------");
            var props = film.GetType().GetProperties();
            foreach (var prop in props)
            {
                
                var value = prop.GetValue(film);
                if (value != null) sb.AppendLine($"{prop.Name} = {value}");
            }
            sb.AppendLine("-------------------");
        }

        private void FilmList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedFilm = (sender as ListBox)?.SelectedItem as Domain.Film;
            Title.Content = SelectedFilm?.Titles["en"] ?? "";
            BitmapImage poster = new BitmapImage();
            poster.BeginInit();
            poster.UriSource = SelectedFilm?.PosterUri ?? NAPosterUri;
            poster.EndInit();
            Poster.Source = poster;
        }
    }
}