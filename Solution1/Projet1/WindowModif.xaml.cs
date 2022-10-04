using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Shapes;
using Modele;
using Projet1.Converters;

namespace Projet1
{
    /// <summary>
    /// Logique d'interaction pour WindowModif.xaml
    /// </summary>
    public partial class WindowModif : Window
    {
        /// <summary>
        /// Manager permettant le gestion de l'Application, basé sur LeManager de Application
        /// </summary>
        public Manager Mgr => (App.Current as App).LeManager;

        /// <summary>
        /// Titre modifié
        /// </summary>
        public Titre LeTitre { get; set; }

        /// <summary>
        /// Initialise WindowModif
        /// </summary>
        public WindowModif()
        {
            InitializeComponent();
            var t = Mgr.SelectedTitre;
            if (t == null) // Si le Titre Sélectionné est null (il y en a pas)
            {
                t = Mgr.ListeTitre.ElementAt(0); // Le Titre sélectionné est le premier de la Liste
            }
            LeTitre = new Titre(t.Id, t.Nom, t.NomAlbum, t.LienVideo.Lien, t.NomArtiste.Nom, t.CheminImage.Chemin, t.Bio);
            DataContext = LeTitre;
        }

        /// <summary>
        /// Ferme la fenêtre si on clique sur le Bouton Annuler
        /// </summary>
        /// <param name="sender">Bouton annuler</param>
        /// <param name="e"></param>
        private void Button_Click_Annuler(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Si on clique sur le Bouton Valider
        /// </summary>
        /// <param name="sender">Bouton Valider</param>
        /// <param name="e"></param>
        private void Button_Click_Valider(object sender, RoutedEventArgs e)
        {
            Mgr.ModifierTitre(Mgr.SelectedTitre, LeTitre); // Modifier le Titre Sélectionné avec le Titre modifié
            Mgr.SaveTitres(); // Sauvegarde la ListeTitre dans la Persistance
            Close(); // Ferme la fenêtre
        }

        /// <summary>
        /// Ouvre le parcours de dossier pour l'Image
        /// </summary>
        /// <param name="sender">Bouton Upload Image</param>
        /// <param name="e"></param>
        private void Button_Upload_Image(object sender, RoutedEventArgs e) 
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = "C:\\Users\\Public\\Pictures\\Sample Pictures";
            dlg.FileName = "Images"; // Default file name
            dlg.DefaultExt = ".jpg | .png | .gif";
            dlg.Filter = "All images files (.jpg, .png, .gif)|*.jpg;*.png;*.gif|JPG files (.jpg)|*.jpg|PNG files (.png)|*.png|GIF files (.gif)|*.gif"; // Filter files by extension

            bool? result = dlg.ShowDialog();

            if (result != true)
            {
                return;
            }
            // Open document 

            FileInfo fi = new FileInfo(dlg.FileName);
            string filename = fi.Name;
            
            if (!File.Exists(System.IO.Path.Combine(String2ImageConverter.ImagesPath, filename)))
            {
                File.Copy(dlg.FileName, System.IO.Path.Combine(String2ImageConverter.ImagesPath, filename));
            }
            LeTitre.CheminImage.Chemin = filename;
        }

        /// <summary>
        /// Ouvre le parcours de dossier pour la Video
        /// </summary>
        /// <param name="sender">Bouton upload Video</param>
        /// <param name="e"></param>
        private void Button_Upload_Video(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = "C:\\Users\\Public\\Videos";
            dlg.FileName = "Videos"; // Default file name
            dlg.DefaultExt = ".mp3 | .mp4";
            dlg.Filter = "All video files (.mp3, .mp4)|*.mp3;*.mp4;|MP3 files (.mp3)|*.mp3|MP4 files (.mp4)|*.mp4"; // Filter files by extension

            bool? result = dlg.ShowDialog();

            if (result != true)
            {
                return;
            }
            // Open document 

            FileInfo fi = new FileInfo(dlg.FileName);
            string filename = fi.Name;
            if (!File.Exists(System.IO.Path.Combine(String2VideoConverter.VideosPath, filename)))
            {
                File.Copy(dlg.FileName, System.IO.Path.Combine(String2VideoConverter.VideosPath, filename));
            }
            LeTitre.LienVideo.Lien = filename;
        }
    }
}
