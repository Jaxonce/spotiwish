using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour WindowAdd.xaml
    /// </summary>
    /// 

    public partial class WindowAdd : Window
    {
        /// <summary>
        /// Manager permettant le gestion de l'Application, basé sur LeManager de Application
        /// </summary>
        public Manager Mgr => (App.Current as App).LeManager;
        /// <summary>
        /// Titre à ajouter
        /// </summary>
        public Titre LeTitre { get; set; }

        /// <summary>
        /// Id du Titre
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Initialise la fenêtre WindowAdd, Abonemment de Closing et  Uploading de ucAdd à Uc3_Closing et UcAdd_Uploading, 
        /// </summary>
        public WindowAdd()
        {
            InitializeComponent();
            ucAdd.Closing += Uc3_Closing;
            ucAdd.Uploading += UcAdd_Uploading;
            var t = new Titre("", "", "", new Modele.Video(""), new Artiste(""), new Modele.Image("music_logo.png"), "");
            LeTitre = new Titre(t.Id, t.Nom, t.NomAlbum, t.LienVideo, t.NomArtiste, t.CheminImage, t.Bio);
            DataContext = this;
        }

        /// <summary>
        /// Methode appelé lors du clique sur le bouton upload d'Ajouter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcAdd_Uploading(object sender, UserControl3.UploadingEventArgs e)
        {
            if (e.Type == "image") //Si le bouton upload qui a envoyé l'événement est celui de Image, ouvre le parcours de dossier pour l'Image
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.InitialDirectory = "C:\\Users\\Public\\Pictures\\Sample Pictures";
                dlg.FileName = "Images"; // Default file name
                dlg.DefaultExt = ".jpg | .png | .gif";
                dlg.Filter = "All images files (.jpg, .png, .gif)|*.jpg;*.png;*.gif|JPG files (.jpg)|*.jpg|PNG files (.png)|*.png|GIF files (.gif)|*.gif"; // Filter files by extension

                bool? result = dlg.ShowDialog();

                if (result == true)
                {
                    // Open document 

                    FileInfo fi = new FileInfo(dlg.FileName);
                    string filename = fi.Name;
                    if (!File.Exists(System.IO.Path.Combine(String2ImageConverter.ImagesPath, filename)))
                    {
                        File.Copy(dlg.FileName, System.IO.Path.Combine(String2ImageConverter.ImagesPath, filename));
                    }
                    LeTitre.CheminImage.Chemin = filename;
                }
            }
            if (e.Type == "video") //Si le bouton upload qui a envoyé l'événement est celui de Video, ouvre le parcours de dossier pour la Video
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

        /// <summary>
        /// Méthode appelé lors de l'appuie du Bouton Valider ou Annuler du l'UserControl3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Uc3_Closing(object sender, UserControl3.ClosingEventArgs e)
        {

            if (e.Save == true) //Si le Bouton Valider est auppuyé
            {
                if (string.IsNullOrWhiteSpace(Id)) // Si l'Id est null ou vide
                {
                    MessageBox.Show("Veuillez Choisir un Identifiant", "Attention, Identifiant Invalide", MessageBoxButton.OK, MessageBoxImage.Error); // Message d'erreur
                    return;
                }
                if (Mgr.GetTitreById(Id) != null) // Si l'Id est déjà pris
                {
                    MessageBox.Show("Un Titre existe deja avec cet Id", "Choisir un autre Identifiant (Id deja utilisé)", MessageBoxButton.OK, MessageBoxImage.Error); // Message d'erreur
                    int i = 0;
                    string id;
                    do
                    {
                        i++;
                        id = $"{Id}_{i:000}";
                    } while (i <= 999 && Mgr.GetTitreById(id) != null); //Tant que l'Id est déjà pris et que i est inférieur à 999, change l'Id par Id_i sous forme 000 (Exemple Id = 001, Id pris alors Id = 001_001)

                    if (i != 1000) Id = id;
                    return;
                }
                Mgr.AjouterTitre(new Titre(Id, LeTitre.Nom, LeTitre.NomAlbum, LeTitre.LienVideo, LeTitre.NomArtiste, LeTitre.CheminImage, LeTitre.Bio)); // AJoout du nouveau Titre
                Mgr.SelectedTitre = Mgr.ListeTitre.Last(); // SelectedTitre devient le dernierTitre (Celui qui vient d'être ajouté)
                Mgr.SaveTitres(); // Sauvegarde la ListeTitre du Manager dans la persistance
            }
            Close(); // Fermeture de la fenêtre
        }


    }
}
