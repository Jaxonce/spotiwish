using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    /// <summary>
    /// Classe Image implementant INotifyPropertyChanged
    /// </summary>
    [DataContract]
    public class Image : INotifyPropertyChanged
    {
        /// <summary>
        /// string contenant le chemin vers l'image, avec un getter et un setter public
        /// </summary>
        private string chemin;
        [DataMember]
        public string Chemin
        {
            get { return chemin; }
            set 
            {
                if (chemin == value) return;
                chemin = value;
                OnPropertyChanged(nameof(Chemin));
            }
        }

        /// <summary>
        /// Construit une Image en mettant la valeur value passe en parametre dans Chemin de Image
        /// </summary>
        /// <param name="value">Chemin de l'Image</param>
        public Image(string value = "Image Inconnu")
        {
            Chemin = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropertyChanged est un evenement permettant de mettre a jour les données quand elles sont modifiées
        /// </summary>
        /// <param name="propertyName">Nom de la propriete a mettre a jour</param>
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Transforme une Image en un string (utilise par l'affichage Console)
        /// </summary>
        /// <returns>Un string du chemin de l'Image</returns>
        public override string ToString() //Methode ToString
        {
            return $"{Chemin}";
        }
    }
}
