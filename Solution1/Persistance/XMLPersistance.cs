using Modele;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Persistance
{
    /// <summary>
    /// Classe XMLPersistance qui implémentant IPersistanceManager
    /// </summary>
    public class XMLPersistance : IPersistanceManager
    {
        /// <summary>
        /// Contruit un XMLPersistance avec des strings en parametres
        /// </summary>
        /// <param name="fileName">Nom du fichier contenant les Titres de ListeTitre de Manager</param>
        /// <param name="fileNameFav">Nom du fichier contenant les Titres de la collection Favoris de ListeCollection de Manager</param>
        /// <param name="filePath">Chemin vers les fichiers</param>
        public XMLPersistance(string fileName = "Projet1.xml", string fileNameFav = "Projet1CollectionFav.xml", string filePath = null) //Ctor XMLPersistance
        {
            if(!string.IsNullOrWhiteSpace(filePath)) FilePath = filePath;

            FileName = fileName;
            FileNameFav = fileNameFav;
        }
        //Outil de mémoire Serializer, s'occupe de la lecture et de l'enregistrement du fichier xml
        private DataContractSerializer Serializer { get; set; } = new DataContractSerializer(typeof(IEnumerable<Titre>), //Type de données a persister (collection de titres)
                                                                                new DataContractSerializerSettings() //Permet de preserver les references d'objets , pas de duplication de references
                                                                                {
                                                                                    PreserveObjectReferences = true
                                                                             });

        //Persistance des titres 

        /// <summary>
        /// String contenant le chemin vers le dossier contenant les fichiers xml, getter et setter public
        /// </summary>
        string FilePath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\xml\\");

        /// <summary>
        /// String contenant le nom du fichier a charger pour les Titres de ListeTitre du Manager, getter et setter public
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// String contenant le chemin complet vers le fichier a charger pour les Titres de ListeTitre du Manager, cree en combinant le Chemin vers le dossier et le Nom du fichier
        /// </summary>
        string XmlFile => Path.Combine(FilePath, FileName);

        /// <summary>
        /// Charge les Titres a partir du fichier XML
        /// </summary>
        /// <returns>Un enumerateur prenant en charge une iteration sur une collection de Titre</returns>
        public IEnumerable<Titre> LoadTitres()
        {
            //Verif si le fichier existe, et leve une exception si non
            if (!File.Exists(XmlFile)) 
            {
                throw new FileNotFoundException("le fichier xml n'existe pas");
            }
            IEnumerable<Titre> titres;
            using (Stream stream = File.OpenRead(XmlFile)) //using utilisé car donnée non manager (implémente IDisposable), car il n'est pas capable de liberer la mémoire sans qu'on lui dise
            {
                titres = Serializer.ReadObject(stream) as IEnumerable<Titre>; //On lui passe le flux et il doit rendre une collection de titres
            } //Flux qui contient le contenu du fichier xml

            return titres; //retourne la collection de titres si tout c'est bien passé
        }


        /// <summary>
        /// Enregistre les Titres de ListeTitre du Manager dans le fichier XML
        /// </summary>
        /// <param name="titres">Liste des titres a enregistrer dans le fichier</param>
        public void SaveTitres(IEnumerable<Titre> titres)
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath); //Crée un directory a l'endroit du FilePath
            } //Est ce que le FilePath existe deja, si non tu le crée

            using (Stream stream = File.Create(XmlFile))
            {
                Serializer.WriteObject(stream, titres); //Recupere et enregistre les titres passe en paramètres de la méthode
            }
        }


        //Persistance de la collection de favoris 

        /// <summary>
        /// String contenant le nom du fichier xml a charger pour les Titres des Favoris de la ListeCollection de Manager
        /// </summary>
        string FileNameFav { get; set; }

        /// <summary>
        /// String contenant le chemin complet vers le fichier xml des Titres favoris, cree en combinant le chemin vers le dossier et le nom du fichier
        /// </summary>
        string XmlFileFav => Path.Combine(FilePath, FileNameFav);

        /// <summary>
        ///  Charge les Titres Favoris a partir du fichier XML
        /// </summary>
        /// <returns>Un enumerateur prenant en charge une iteration sur une collection de Titre</returns>
        public IEnumerable<Titre> LoadTitresFavoris()
        {
            //Verif si le fichier existe, et leve une exception si non
            if (!File.Exists(XmlFileFav))
            {
                throw new FileNotFoundException("le fichier xml n'existe pas");
            }
            IEnumerable<Titre> titres;
            using (Stream stream = File.OpenRead(XmlFileFav)) //using utilisé car donnée non manager (implémente IDisposable), car il n'est pas capable de liberer la mémoire sans qu'on lui dise
            {
                titres = Serializer.ReadObject(stream) as IEnumerable<Titre>; //On lui passe le flux et il doit rendre une collection de titres
            } //Flux qui contient le contenu du fichier xml

            return titres; //retourne la collection de titres si tout c'est bien passé
        }

        /// <summary>
        /// Enregistre les Titres Favoris de la ListeCollecion du Manager dans le fichier XML
        /// </summary>
        /// <param name="titres">Liste des Titres de Favoris de ListeCollection</param>
        public void SaveTitresFavoris(IEnumerable<Titre> titres)
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath); //Crée un directory a l'endroit du FilePath
            } //Est ce que le FilePath existe deja, si non tu le crée

            using (Stream stream = File.Create(XmlFileFav))
            {
                Serializer.WriteObject(stream, titres); //Recupere et enregistre les titres passe en paramètres de la méthode
            }
        }
    }
}
