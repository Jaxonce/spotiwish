using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projet1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Manager permettant le gestion de l'Application, basé sur LeManager de Application
        /// </summary>
        public Manager Mgr => (Application.Current as App).LeManager;
        /// <summary>
        /// Initialisation de la Vue + Abonnement et mise en place du DataTemplate
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Mgr;
            Mgr.IsFavorisEmpty += Mgr_IsFavorisEmpty; // Abonnement de IsFavorisEmpty du Manager à Mgr_IsFavorisEmpty
            App.Current.Resources["currentTitreTemplate"] = App.Current.Resources["titreListBoxItemTemplate"]; //valeur par default du datatemplate (uc1)
        }

        /// <summary>
        /// Evenement effectué lorsque la liste de Favoris est vide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mgr_IsFavorisEmpty(object sender, Manager.IsFavorisEmptyEventArgs e)
        {
            if (MasterTitres.ItemsSource == Mgr.GetListeFavoris().ListeTitre ) // Si la liste Affiché est les Favoris
            {
                MasterTitres.ItemsSource = Mgr.ListeTitre; // Changement de la source à la liste de Titre
                Button_Fav.Source = SourceFavEmptyImage; //Changement de l'Image du bouton Favoris
                Mgr.SelectedTitre = Mgr.ListeTitre.ElementAt(0); // SelectedTitre mis au premier Titre 
                MessageBox.Show("Vous n'avez aucun favoris \n Vous avez été redirigé vers la page principale", "Problème", MessageBoxButton.OK, MessageBoxImage.Error); //Message d'erreur
            }
            // Sinon, la même chose mais sans le message d'erreur
            MasterTitres.ItemsSource = Mgr.ListeTitre;
            Button_Fav.Source = SourceFavEmptyImage;
            Mgr.SelectedTitre = Mgr.ListeTitre.ElementAt(0);
            
            
        }

        /// <summary>
        /// Action lors de l'appuie du bouton Ajouter (+), Affiche la fenêtre Ajouter
        /// </summary>
        /// <param name="sender">Bouton add</param>
        /// <param name="e">Événement</param>
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            var ajouterWindow = new WindowAdd();
            ajouterWindow.ShowDialog();
        }
        /// <summary>
        /// Index du Titre Sélectionné
        /// </summary>
        int IndexListeTitre = 0;
        /// <summary>
        /// Action effectué lors de l'appuie du Bouton Next, Le Titre sélectionné passe au Titre sauivant
        /// </summary>
        /// <param name="sender">Bouton Next</param>
        /// <param name="e">Événement lors de l'appuie</param>
        private void Button_Next_Music_Click(object sender, RoutedEventArgs e)
        {
            IndexListeTitre = Mgr.ListeTitre.IndexOf(Mgr.SelectedTitre); // Donne la valeur du l'index du Titre
            IndexListeTitre = (IndexListeTitre + 1) % Mgr.ListeTitre.Count; // Ajoute 1 à cette valeur et le Module pour ne pas passé à un Titre innexistant
            Mgr.SelectedTitre = Mgr.ListeTitre.ElementAt(IndexListeTitre); // SelectedTitre devient le Titre suivant
        }

        /// <summary>
        /// Action effectué lors de l'appuie sur le Bouton Previous, Le Titre sélectionné passe au Titre suivant
        /// </summary>
        /// <param name="sender">Bouton Previous</param>
        /// <param name="e"></param>
        private void Button_Click_Previous(object sender, RoutedEventArgs e)
        {
            IndexListeTitre = Mgr.ListeTitre.IndexOf(Mgr.SelectedTitre); // Donne la valeur du l'index du Titre
            // Negative Modulus Formula : (x % m + m) % m;
            IndexListeTitre = ((IndexListeTitre - 1) % Mgr.ListeTitre.Count + Mgr.ListeTitre.Count) % Mgr.ListeTitre.Count; // Retire 1 à cette valeur et le Module pour ne pas passé à un Titre innexistant
            Mgr.SelectedTitre = Mgr.ListeTitre.ElementAt(IndexListeTitre);
        }

        /// <summary>
        /// Méthode appelé lors du Lancement de l'Application (MainWindow)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Mgr?.LoadTitres();
        }
        /// <summary>
        /// Url de l'image Play , initialisé à icones/button_pause.png
        /// </summary>
        static Uri urlPlayImage = new Uri("icones/button_pause.png", UriKind.RelativeOrAbsolute);
        /// <summary>
        /// URl de l'image Pause, Initialisé à icones/button_play.png
        /// </summary>
        static Uri urlPauseImage = new Uri("icones/button_play.png", UriKind.RelativeOrAbsolute);
        /// <summary>
        /// BitmapImage contenant l'Image du l'url de urlPlayImage
        /// </summary>
        BitmapImage SourceImagePlay = new BitmapImage(urlPlayImage);
        /// <summary>
        /// BitmapImage contenant l'Image du l'url de urlPauseImage
        /// </summary>
        BitmapImage SourceImagePause = new BitmapImage(urlPauseImage);

        /// <summary>
        /// Méthode appelé lors du click sur le Bouton Play
        /// </summary>
        /// <param name="sender">Bouton Play/Pause</param>
        /// <param name="e"></param>
        private void Button_Click_Play(object sender, RoutedEventArgs e)
        {

            if (Button_Play_Pause.Source == SourceImagePause) //Si le bouton a l'image pause
            {
                Video.Play(); // Lecture de la vidéo
                Button_Play_Pause.Source = SourceImagePlay; // Changement de l'image du bouton
            }
            else
            {
                Video.Pause(); // Pause de la vidéo
                Button_Play_Pause.Source = SourceImagePause; // Changement de l'image du bouton
            }
        }

        /// <summary>
        /// Change le Template du Master et la visibilité des boutons Supprimer / Annuler
        /// </summary>
        /// <param name="sender">Bouton Bin</param>
        /// <param name="e"></param>
        private void Button_Bin(object sender, RoutedEventArgs e) {

            if ((sender as ToggleButton).IsChecked.GetValueOrDefault()) //si le togglebutton est activé
            {
                Valide_supp.Visibility = Visibility.Visible; 
                Annule_supp.Visibility = Visibility.Visible;
                App.Current.Resources["currentTitreTemplate"] = App.Current.Resources["titreSupprimerTemplate"]; //met le datatemplate a titre supprimer (ucdelete)
            }
            else
            {
                Valide_supp.Visibility = Visibility.Collapsed;
                Annule_supp.Visibility = Visibility.Collapsed;
                App.Current.Resources["currentTitreTemplate"] = App.Current.Resources["titreListBoxItemTemplate"]; //sinon (il est desactive) et remet le datatemplate du uc1
            }
        }

        // Image coeur vide noir
        static Uri urlFavEmptyImage = new Uri("icones/coeur_vide.png", UriKind.RelativeOrAbsolute);
        BitmapImage SourceFavEmptyImage = new BitmapImage(urlFavEmptyImage);

        // Image coeur plein noir
        static Uri urlFavFillImage = new Uri("icones/coeur_plein.png", UriKind.RelativeOrAbsolute);
        BitmapImage SourceFavFillImage = new BitmapImage(urlFavFillImage);

        // Image coeur plein blanc
        static readonly Uri urlFavFillImageListe = new Uri("icones/heart_icon_fill.png", UriKind.RelativeOrAbsolute);
        BitmapImage SourceFavFillImageListe = new BitmapImage(urlFavFillImageListe);

        /// <summary>
        /// Affiche les Titres des Favoris de la listeCollection du Manager ou la listeTitre du Manager à l'appuie du bouton DisplayFavoris
        /// </summary>
        /// <param name="sender">Bouton DisplayFavoris</param>
        /// <param name="e"></param>
        private void Button_DisplayFavoris(object sender, RoutedEventArgs e)
        {
            if (Mgr.GetListeFavoris().ListeTitre.Count == 0)
            {
                MessageBox.Show("Vous n'avez aucun favoris", "Problème", MessageBoxButton.OK, MessageBoxImage.Error);
                MasterTitres.ItemsSource = Mgr.ListeTitre;
                Button_Fav.Source = SourceFavEmptyImage;
                return;
            }

            if (MasterTitres.ItemsSource == Mgr.ListeTitre)
            {
                MasterTitres.ItemsSource = Mgr.GetListeFavoris().ListeTitre;
                Button_Fav.Source = SourceFavFillImage;
            }
            else
            {
                MasterTitres.ItemsSource = Mgr.ListeTitre;
                Button_Fav.Source = SourceFavEmptyImage;
                foreach (Titre t in Mgr.GetListeFavoris().ListeTitre)
                {
                    //UC1.HeartImageFavoris.Source = SourceFavFillImageListe;
                }
            }
        }

        /// <summary>
        /// Confirme la suppression des Titres sélectionné (Attention pour bien ajouter un titre dans la suppression d'un Titre un faut dans un premier temps le sélectionné)
        /// </summary>
        /// <param name="sender">Bouton Supprimer</param>
        /// <param name="e"></param>
        private void Button_Valide_Supp(object sender, RoutedEventArgs e)
        {
            foreach (Titre t in Mgr.ListeTitreSupp)
            {
                Mgr.SupprimerTitre(t);
            }
            Mgr.SaveTitres();
        }

        /// <summary>
        /// Annule la sélection des Titres sélectionné
        /// </summary>
        /// <param name="sender">Bouton annuler</param>
        /// <param name="e"></param>
        private void Button_Annul_Supp(object sender, RoutedEventArgs e)
        {
            if(Mgr.ListeTitreSupp.Count <= 0)
            {
                Valide_supp.Visibility = Visibility.Collapsed;
                Annule_supp.Visibility = Visibility.Collapsed;
                App.Current.Resources["currentTitreTemplate"] = App.Current.Resources["titreListBoxItemTemplate"];
                ButtonBin.IsChecked = false;            
            }
            else
            {
                Mgr.ListeTitreSupp.Clear();
                App.Current.Resources["currentTitreTemplate"] = App.Current.Resources["titreListBoxItemTemplate"];
                App.Current.Resources["currentTitreTemplate"] = App.Current.Resources["titreSupprimerTemplate"];
            }
            
        }
    } 
}
