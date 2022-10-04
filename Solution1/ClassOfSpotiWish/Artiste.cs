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
    /// Classe Artiste implementant INotifyPropertyChanged
    /// </summary>
    [DataContract]
    public class Artiste: INotifyPropertyChanged
    {
        /// <summary>
        /// String contenant le nom de l'Artiste, avec un getter et un setter public
        /// </summary>
        private string nom;
        [DataMember]
        public string Nom
        {
            get { return nom; }
            set 
            { 
                if(nom == value) return;
                nom = value; 
                OnPropertyChanged(nameof(Nom));
            }
        }

        /// <summary>
        /// Construit un Artiste en mettant la valeur value passe en parametre dans Nom d'Artiste
        /// </summary>
        /// <param name="value">Nom de l'Artiste</param>
        public Artiste(string value = "Artiste Inconnu")
        {
            Nom = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropertyChanged est un evenement permettant de mettre a jour les données quand elles sont modifiées
        /// </summary>
        /// <param name="propertyName">Nom de la propriete a modifier</param>
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Transforme un Artiste en un string (utilise par l'affichage console)
        /// </summary>
        /// <returns>Un string du nom de l'artiste</returns>
        public override string ToString() //Methode ToString
        {
            return $"{Nom}";
        }
    }
}
