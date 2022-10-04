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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Modele;
using Projet1.Converters;

namespace Projet1
{
    /// <summary>
    /// Logique d'interaction pour UserControl3.xaml
    /// </summary>
    public partial class UserControl3 : UserControl
    {
        /// <summary>
        /// Initialisation de l'UserControl3
        /// </summary>
        public UserControl3()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Manager permettant le gestion de l'Application, basé sur LeManager de Application
        /// </summary>
        public Manager Mgr => (App.Current as App).LeManager;

        //public Titre LeTitre { get; set; }

        //Evenement upload image

        public event EventHandler<UploadingEventArgs> Uploading;

        public class UploadingEventArgs : EventArgs
        {
            public string Type { get; set; }
            public UploadingEventArgs(string type) { Type = type; }
        }

        public void OnUploading(UploadingEventArgs args) => Uploading?.Invoke(this, args);

        //Evenement fermé fenetre
        public event EventHandler<ClosingEventArgs> Closing;

        public class ClosingEventArgs : EventArgs
        {
            public bool Save { get; private set; }
            public ClosingEventArgs(bool save) { Save = save; }
        }

        /// <summary>
        /// Envoie un événement
        /// </summary>
        /// <param name="args"></param>
        public void OnClosing(ClosingEventArgs args) => Closing?.Invoke(this, args);

        /// <summary>
        /// Action lorsque l'on appuie sur le Bouton Valider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Valider(object sender, RoutedEventArgs e)
        {
            OnClosing(new ClosingEventArgs(true));
        }

        /// <summary>
        /// Actions lorsque l'on clique sur le Bouton Annuler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Annuler(object sender, RoutedEventArgs e)
        {
            OnClosing(new ClosingEventArgs(false));
        }

        /// <summary>
        /// Ouvre le parcours des dossier pour l'Image quand on clique sur le Bouton Upload en dessous de l'Image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Upload_Image(object sender, RoutedEventArgs e)
        {
            OnUploading(new UploadingEventArgs("image"));
            //MessageBox.Show("Option non implémenté, coming soon...", "Opération Non Valide", MessageBoxButton.OK, MessageBoxImage.Error);
            

        }

        /// <summary>
        /// Ouvre le parcours des dossier pour la Video quand on clique sur le Bouton Upload à coté de la Video
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Upload_Video(object sender, RoutedEventArgs e)
        {
            OnUploading(new UploadingEventArgs("video"));
        }
    }
}
