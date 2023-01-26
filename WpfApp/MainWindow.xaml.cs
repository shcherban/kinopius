using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Net;
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
using SearchResult = Domain.OMDb.SearchResult;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Engine engine;
        private string lastSearchString;
        private SearchResult lastSearchResult;
        private int i;
        private StringBuilder sb;
        
        public MainWindow()
        {
            InitializeComponent();
            engine = new Engine();
            lastSearchString = "sdkhfs;dghasdgjhasdk";
            sb = new StringBuilder();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (lastSearchString == Search.Text)
            {
                i++;
            }
            else
            {
                i = 0;
                lastSearchString = Search.Text;
                lastSearchResult = engine.GetOMDbResponse(Search.Text);
            }
            
            if (lastSearchResult.Response)
            {
                /*foreach (var film in lastSearchResult.Search)
                {
                    DisplayFilm(film);
                    DisplayFilm(Domain.Film.FromOMDbFilm(film));
                }
                Result.Text = sb.ToString();
*/
                filmList.Items.Clear();
                filmList.ItemsSource = lastSearchResult.Search.Select(x => Domain.Film.FromOMDbFilm(x));
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
    }
}