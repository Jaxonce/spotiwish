using Modele;
using StubLib;
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
    /// Logique d'interaction pour UCDelete.xaml
    /// </summary>
    public partial class UCDelete : UserControl
    {
        /// <summary>
        /// Manager permettant le gestion de l'Application, basé sur LeManager de Application
        /// </summary>
        public Manager Mgr => (App.Current as App).LeManager;
        /// <summary>
        /// Initialisation de l'UserControl
        /// </summary>
        public UCDelete()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fonction lancé lorsque la CheckBox est coché (Attention il faut bien sélectionner le Titre que l'on veut supprimer avant de cliquer sur la CheckBox)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Mgr.ListeTitreSupp.Add(Mgr.SelectedTitre); // Ajoute le Titre sélectionné à la ListeTitreSupp
        }

        /// <summary>
        /// Fonction lancé lorsque la CheckBox est décoché (Attention il faut bien sélectionner le Titre que l'on veut enlever de la liste supprimer avant de cliquer sur la CheckBox)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Mgr.ListeTitreSupp.Remove(Mgr.SelectedTitre); // Enlève le Titre sélectionné de ListeTitreSupp
        }
        public string Nom
        {
            get { return (string)GetValue(NomProperty); }
            set { SetValue(NomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Nom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NomProperty =
            DependencyProperty.Register("Nom", typeof(string), typeof(UCDelete), new PropertyMetadata("Lanone"));



        public Artiste NomArtiste
        {
            get { return (Artiste)GetValue(NomArtisteProperty); }
            set { SetValue(NomArtisteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Artiste.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NomArtisteProperty =
            DependencyProperty.Register("NomArtiste", typeof(Artiste), typeof(UCDelete), new PropertyMetadata(new Artiste("Colonel Reyel")));


        public string NomAlbum
        {
            get { return (string)GetValue(NomAlbumProperty); }
            set { SetValue(NomAlbumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NomAlbum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NomAlbumProperty =
            DependencyProperty.Register("NomAlbum", typeof(string), typeof(UCDelete), new PropertyMetadata("Au rapport"));



        public Modele.Image CheminImage
        {
            get { return (Modele.Image)GetValue(CheminImageProperty); }
            set { SetValue(CheminImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CheminImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheminImageProperty =
            DependencyProperty.Register("CheminImage", typeof(Modele.Image), typeof(UCDelete), new PropertyMetadata(new Modele.Image("./music_logo.png")));

        
    }
}
