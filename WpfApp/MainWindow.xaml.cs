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
        private readonly Engine _engine;
        private SearchResult? _searchResult;
        private readonly StringBuilder _stringBuilder;
        private readonly Uri? _naPosterUri;
        private Domain.Film? SelectedFilm { get; set; }
        private string? _currentSearchText;

        public MainWindow()
        {
            InitializeComponent();
            _engine = new Engine();
            _stringBuilder = new StringBuilder();
            try
            {
                _naPosterUri = new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "No_image_poster.png"));
            }
            catch
            {
                // ignored
            }

            Search.Focus();
        }

        private void SearchFilmsByTitle()
        {
            if (Search.Text == _currentSearchText) return;
            _currentSearchText = Search.Text;
            _searchResult = _engine.GetOMDbResponse(Search.Text);
            if (_searchResult.Response)
            {
                filmList.ItemsSource = null;
                filmList.Items.Clear();
                filmList.ItemsSource = _searchResult.Search.Select(x => Domain.Film.FromOMDbFilm(x));
                filmList.SelectedItem = filmList.Items[0];
            }
            else
            {
                var lbi = new ListBoxItem
                {
                    Content = "Nothing is found"
                };
                filmList.ItemsSource = null;
                filmList.Items.Clear();
                filmList.Items.Add(lbi);
                _stringBuilder.Clear();
                _stringBuilder.AppendLine("Nothing is found");
            }
            _stringBuilder.Clear();
        }
        
        private void DisplayFilm(Film? film)
        {
            if (film == null) return;
            _stringBuilder.AppendLine("---------OMDbFilm----------");
            var props = film.GetType().GetProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(film);
                if (value != null) _stringBuilder.AppendLine($"{prop.Name} = {value}");
            }

            _stringBuilder.AppendLine("-------------------");
        }

        private void DisplayFilm(Domain.Film? film)
        {
            if (film == null) return;
            _stringBuilder.AppendLine("---------Film----------");
            var props = film.GetType().GetProperties();
            foreach (var prop in props)
            {
                
                var value = prop.GetValue(film);
                if (value != null) _stringBuilder.AppendLine($"{prop.Name} = {value}");
            }
            _stringBuilder.AppendLine("-------------------");
        }

        private void FilmList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedFilm = (sender as ListBox)?.SelectedItem as Domain.Film;
            Title.Content = SelectedFilm?.Titles["en"] ?? "";
            Grade1.Text = SelectedFilm?.Grade1 ?? "";
            Grade2.Text = SelectedFilm?.Grade2 ?? "";
            try
            {
                var poster = new BitmapImage();
                poster.BeginInit();
                poster.UriSource = SelectedFilm?.PosterUri ?? _naPosterUri;
                poster.EndInit();
                Poster.Source = poster;
            }
            catch
            {
                Poster.Source = null;
            }
        }

        private async void Search_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            await Task.Delay(1500);
            SearchFilmsByTitle();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            SearchFilmsByTitle();
        }

        private void Search_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SearchFilmsByTitle();
            }
        }
    }
}