using Modele;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StubLib
{
    /// <summary>
    /// Classe Stub qui implemente IPersistanceManager
    /// </summary>
    public class Stub : IPersistanceManager
    {
        /// <summary>
        /// Liste de Titre listeTitre simulant la persistance
        /// </summary>
        List<Titre> listeTitre = new List<Titre>()
        {
           new Titre("001","Aurelie", "Test", "video/Colonel.mp4", "Colonel Reyel","colonel.jpg","C'est une biographie 1"),
           new Titre("002","En Apesenteur", "Test", "video/En_Apesanteur.mp4", "Calogero","En-Apesanteur-Cover.jpg","C'est une biographie 2"),
           new Titre("003","Le coup de folie", "Test2", "video/Coupe_de_Folie.mp4", "Thierry Pastor","CoupDeFolie.jpg","C'est une biographie 3")
        };

        /// <summary>
        /// Liste de Titre simulant la persistance des Favoris
        /// </summary>
        List<Titre> listeTitreFav = new List<Titre>()
        {
           new Titre("001","Aurelie", "Test", "video/Colonel.mp4", "Colonel Reyel","colonel.jpg","C'est une biographie 1"),
        };

        /// <summary>
        /// Charge les Titres a partir des Titres du Stub
        /// </summary>
        /// <returns>Un enumerateur prenant en charge une iteration sur la liste listeTitre du Stub</returns>
        public IEnumerable<Titre> LoadTitres()
        {
            return listeTitre;
        }

        /// <summary>
        /// N'est pas utilise, elle est quand meme obligatoire car cette classe doit implementer toutes les methodes de IPersistanceManager
        /// </summary>
        /// <param name="titres">Liste des Titres de ListeTitre du Manager</param>
        public void SaveTitres(IEnumerable<Titre> titres)
        {
            Debug.WriteLine("SaveTitre has been called");
        }

        /// <summary>
        /// Charge les Titres Favoris a partir du Stub
        /// </summary>
        /// <returns>Un enumerateur prenant en charge une iteration sur la liste ListeTitreFav du Stub</returns>
        public IEnumerable<Titre> LoadTitresFavoris()
        {
            Debug.WriteLine("LoadTitreFavoris has been called");
            return listeTitreFav;
        }
        /// <summary>
        /// N'est pas utilise, elle est quand meme obligatoire car cette classe doit implementer toutes les methodes de IPersistanceManager
        /// </summary>
        /// <param name="titres">Liste des Titres de Favoris de ListeCollection du Manager</param>
        public void SaveTitresFavoris(IEnumerable<Titre> titres)
        {
            Debug.WriteLine("SaveTitreFavoris has been called");
        }
    }
}
