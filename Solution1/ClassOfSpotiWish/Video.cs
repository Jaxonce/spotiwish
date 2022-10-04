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
    /// Classe Video, implementant l'interface INotifyPropertyChanged
    /// </summary>
    [DataContract]
    public class Video : INotifyPropertyChanged
    {
        /// <summary>
        /// String contenant le lien de la Video avec un getter et un setter public
        /// </summary>
        [DataMember]
        public string Lien
        {
            get { return lien; }
            set
            {
                if (lien == value) return;
                lien = value;
                OnPropertyChanged(nameof(Lien));
            }
        }
        private string lien;

        /// <summary>
        /// Construit une Video avec la valeur value passe en parametre
        /// </summary>
        /// <param name="value">Lien de la Video, la valeur par defaut est "Lien Inconnu"</param>
        public Video(string value="Lien Inconnu") { 
            
            Lien=value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropertyChanged est un evenement permettant de mettre a jour les données quand elles sont modifiées
        /// </summary>
        /// <param name="propertyName">Nom de la propriete a mettre a jour</param>
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Transforme une Video en string (utilise par l'affichage console)
        /// </summary>
        /// <returns>Un string contenant les informations a afficher pour une Video</returns>
        public override string ToString()
        {
            return $"{Lien}";
        }
    }
}
