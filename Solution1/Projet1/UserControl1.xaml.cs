using Modele;
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

namespace Projet1
{
    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        /// <summary>
        /// Manager permettant le gestion de l'Application, basé sur LeManager de Application
        /// </summary>
        public Manager Mgr => (App.Current as App).LeManager;

        /// <summary>
        /// Url de l'image coeur vide , initialisé à icones/heart_icon_empty.png
        /// </summary>
        static readonly Uri urlFavEmptyImage = new Uri("icones/heart_icon_empty.png", UriKind.RelativeOrAbsolute);
        /// <summary>
        /// Url de l'image coeur plein , initialisé à icones/heart_icon_fill.png
        /// </summary>
        static readonly Uri urlFavFillImage = new Uri("icones/heart_icon_fill.png", UriKind.RelativeOrAbsolute);
        /// <summary>
        /// BitmapImage contenant l'Image du l'url de urlFavEmptyImage
        /// </summary>
        BitmapImage SourceFavEmptyImage = new BitmapImage(urlFavEmptyImage);
        /// <summary>
        /// BitmapImage contenant l'Image du l'url de urlFavFillImage
        /// </summary>
        BitmapImage SourceFavFillImage = new BitmapImage(urlFavFillImage);

        /// <summary>
        /// Initialisation de l'UserControl, pour tous les Titres de ListeTitre du Manager, regarde si ils sont dans la collection Favoris de ListeCollection du Manager, si oui IsFavoris est initialisé à true, sinon false
        /// </summary>
        public UserControl1()
        {
            InitializeComponent();
            foreach (Titre t in Mgr.ListeTitre)
            {
                t.IsFavoris = Mgr.GetListeFavoris().ContenirTitreDansCollection(t);
            }
        }

        /// <summary>
        /// Affiche la fenêtre Modifier lors du clqiue sur le bouton crayon (Attention il faut bien sélectionner le Titre avant de cliquer sur le bouton)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Modif(object sender, RoutedEventArgs e)
        {
            var modifierWindow = new WindowModif();
            modifierWindow.ShowDialog();
        }

        /// <summary>
        /// Ajoute aux favoris le Titre sélectionné (Attention il faut bien sélectionner le Titre que l'on veut ajouter avant de cliquer sur le bouton)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_AddFav(object sender, RoutedEventArgs e)
        {
            
            if (Mgr.GetListeFavoris().ListeTitre.Contains(Mgr.SelectedTitre)) // Si il Titre est déjà dans les Favoris on l'enlève
            {
                Mgr.SelectedTitre.IsFavoris = false;
                Mgr.SupprimerTitreFromCollection(Mgr.GetListeFavoris(), Mgr.SelectedTitre);
            }
            else // Si le Titre n'est pas dans les Favoris on l'ajoute
            {
                if(Mgr.SelectedTitre == null) // Si le Titre Sélectionner est null, ne rien faire
                {
                    return;
                }
                Mgr.AjouterTitreToCollection(Mgr.GetListeFavoris(), Mgr.SelectedTitre);
                Mgr.SelectedTitre.IsFavoris = Mgr.GetListeFavoris().ContenirTitreDansCollection(Mgr.SelectedTitre);
            }
            if(Mgr.GetListeFavoris().ListeTitre.Count == 0) // Si la liste des Favoris est vide, envoie de l'événement.
            {
                Mgr.OnFavorisEmpty(new Manager.IsFavorisEmptyEventArgs());
            }
            Mgr.SaveTitresFavoris(); // Sauvegarde les Titres Favoris dans la Persistance

        }

        public string Nom
        {
            get { return (string)GetValue(NomProperty); }
            set { SetValue(NomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Nom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NomProperty =
            DependencyProperty.Register("Nom", typeof(string), typeof(UserControl1), new PropertyMetadata("Lanone"));



        public Artiste NomArtiste
        {
            get { return (Artiste)GetValue(NomArtisteProperty); }
            set { SetValue(NomArtisteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Artiste.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NomArtisteProperty =
            DependencyProperty.Register("NomArtiste", typeof(Artiste), typeof(UserControl1), new PropertyMetadata(new Artiste("Colonel Reyel")));


        public string NomAlbum
        {
            get { return (string)GetValue(NomAlbumProperty); }
            set { SetValue(NomAlbumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NomAlbum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NomAlbumProperty =
            DependencyProperty.Register("NomAlbum", typeof(string), typeof(UserControl1), new PropertyMetadata("Au rapport"));



        public Modele.Image CheminImage
        {
            get { return (Modele.Image)GetValue(CheminImageProperty); }
            set { SetValue(CheminImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CheminImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheminImageProperty =
            DependencyProperty.Register("CheminImage", typeof(Modele.Image), typeof(UserControl1), new PropertyMetadata(new Modele.Image("./music_logo.png")));


    }
}
