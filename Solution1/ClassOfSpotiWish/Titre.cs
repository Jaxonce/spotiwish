using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Modele
{
    /// <summary>
    /// Classe Titre implementant IEquatable et INotifyPropertyChanged
    /// </summary>
    [DataContract] //Permet de dire a la classe qu'elle peut etre serializer
    public class Titre : IEquatable<Titre>, INotifyPropertyChanged
    {
        /// <summary>
        /// String contenant l'Id du Titre avec un getter et un setter public
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// String contenant le Nom du Titre avec un getter et un setter public
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
        /// String contenant le Nom de l'album contenant Titre  avec un getter et un setter public
        /// </summary>
        private string nomAlbum;
        [DataMember]
        public string NomAlbum
        {
            get { return nomAlbum; }
            set 
            {
                if(nomAlbum == value) return;
                nomAlbum = value; 
                OnPropertyChanged(nameof (NomAlbum));
            }
        }

        /// <summary>
        /// Video du clip du Titre (la classe Video contient un string Lien)  avec un getter et un setter public
        /// </summary>
        private Video lienVideo;
        [DataMember]
        public Video LienVideo
        {
            get { return lienVideo; }
            set 
            { 
                if(lienVideo == value) return;
                lienVideo = value;
                OnPropertyChanged(nameof(LienVideo));
            }
        }

        /// <summary>
        /// Artiste qui a compose le Titre (la classe Artiste contient un string Nom)  avec un getter et un setter public
        /// </summary>
        private Artiste nomArtiste;
        [DataMember]
        public Artiste NomArtiste
        {
            get { return nomArtiste; }
            set 
            {
                if(nomArtiste == value) return;
                nomArtiste = value;
                OnPropertyChanged(nameof(NomArtiste));
            }
        }

        /// <summary>
        /// Chemin de l'Image associe au Titre (la classe Image contient un string Chemin)  avec un getter et un setter public
        /// </summary>
        private Image cheminImage;
        [DataMember]
        public Image CheminImage
        {
            get { return cheminImage; }
            set 
            { 
                if(cheminImage == value) return;
                cheminImage = value; 
                OnPropertyChanged(nameof(CheminImage));
            }
        }

        /// <summary>
        /// String contenant la Biographie du Titre (Texte disant quelques mots sur la musique) avec un getter et un setter public
        /// </summary>
        [DataMember]
        public string Bio
        {
            get { return bio; }
            set 
            { 
                if(bio == value) return;
                bio = value; 
                OnPropertyChanged(nameof(Bio));
            }
        }
        private string bio;

        /// <summary>
        /// Booleen permettant de gerer les favoris dans les vues de l'application (Charge par l'application a chaque lancement) avec un getter et un setter public
        /// </summary>
        private bool isFavoris;
        [DataMember]
        public bool IsFavoris
        {
            get { return isFavoris; }
            set 
            { 
                if(isFavoris == value) return;
                isFavoris = value;
                OnPropertyChanged(nameof(IsFavoris));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropertyChanged est un evenement permettant de mettre a jour les données quand elles sont modifiées
        /// </summary>
        /// <param name="propertyName">Nom de la propriete a mettre a jour</param>
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Construit un Titre en prenant que des strings en parametres
        /// </summary>
        /// <param name="id">Id du Titre</param>
        /// <param name="Nom">Nom du Titre</param>
        /// <param name="NomAlbum">Nom de l'album du Titre</param>
        /// <param name="value">Lien de la video du Titre</param>
        /// <param name="NomArt">Nom de l'artiste du Titre</param>
        /// <param name="cheminImage">Chemin de l'Image du Titre</param>
        /// <param name="Bio">Biographie du Titre</param>
        public Titre(string id,string Nom, string NomAlbum,string value, string NomArt, string cheminImage, string Bio )
        {
            this.Id = id;
            if( Id == null)
            {
                throw new ArgumentNullException();
            }
            
            this.Nom = Nom;
            this.NomAlbum = NomAlbum;
            this.LienVideo = new Video(value);
            this.NomArtiste = new Artiste(NomArt);
            this.CheminImage = new Image(cheminImage);
            this.Bio = Bio;
        }

        /// <summary>
        /// Construit un Titre en prenant des strings et les classes Video, Artiste et Image en parametre
        /// </summary>
        /// <param name="id">Id du Titre</param>
        /// <param name="Nom">Nom du Titre</param>
        /// <param name="NomAlbum">Nom de l'album du Titre</param>
        /// <param name="value">Video du Titre (De type Video)</param>
        /// <param name="Art">Artiste du Titre (De type Artiste)</param>
        /// <param name="img">Image du Titre (De type Image)</param>
        /// <param name="Bio">Boigraphie du Titre</param>
        public Titre(string id,string Nom, string NomAlbum, Video value, Artiste Art, Image img, string Bio)
        {
            this.Id=id;
            this.Nom = Nom;
            this.NomAlbum = NomAlbum;
            this.LienVideo = value;
            this.NomArtiste = Art;
            this.CheminImage = img;
            this.Bio = Bio;
        }

        /// <summary>
        /// Fonction permettant de comparer deux Titres (Autorise un Titre null)
        /// </summary>
        /// <param name="other">Titre a comparer</param>
        /// <returns>True si le Titre est le meme, False sinon</returns>
        public bool Equals([AllowNull] Titre other)
        {
            return Id.Equals(other.Id);
        }

        /// <summary>
        /// Fonction permettant de comparer des objets
        /// </summary>
        /// <param name="obj">Objet a comparer avec le Titre</param>
        /// <returns>Renvoie True si le Titre est le meme, sinon renvoie false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals(obj as Titre);
        }

        /// <summary>
        /// Transforme un Titre en string (utilise par l'affichage console)
        /// </summary>
        /// <returns>Un string contenant les informations a afficher pour un Titre</returns>
        public override string ToString()
        {
            return $"{Nom} - {NomAlbum} - {NomArtiste} - {LienVideo} - {CheminImage} - {Bio}";
        }

        /// <summary>
        /// Hache le Titre
        /// </summary>
        /// <returns>L'id du Titre hache</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
