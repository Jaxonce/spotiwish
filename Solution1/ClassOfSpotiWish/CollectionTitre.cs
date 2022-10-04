using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Modele
{
    /// <summary>
    /// Classe CollectionTitre
    /// </summary>
    public class CollectionTitre
    {
        /// <summary>
        /// Liste de Titrem implementant INotifyCollectionChanged et INotifyPropertyChanged au changement de la liste
        /// </summary>
        public ReadOnlyObservableCollection<Titre> ListeTitre { get; private set; }
        readonly ObservableCollection<Titre> listeTitre = new ObservableCollection<Titre>();

        /// <summary>
        /// Construit une CollectionTitre avec les Titres passe en parametres
        /// </summary>
        /// <param name="desTitres">Liste des titres a ajouter a la Collection</param>
        public CollectionTitre(params Titre[] desTitres)
        {
            ListeTitre = new ReadOnlyObservableCollection<Titre>(listeTitre);
            foreach(Titre t in desTitres)
            {
                listeTitre.Add(t);
            }
            Debug.WriteLine("Collection crée");
        }

        /// <summary>
        /// Transforme une CollectionTitre en string (utilise par l'affichage console)
        /// </summary>
        /// <returns>Un string avec toutes les informations des Titres de la Collection</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Mes Titres : ");
            foreach (Titre t in listeTitre)
            {
                sb.Append($"{t}\t");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Ajoute un Titre a la Collection
        /// </summary>
        /// <param name="t">Titre a ajouter</param>
        /// <returns>True si le titre est ajoute, False sinon</returns>
        public bool AjouterTitreDansCollection(Titre t)
        {
            if (ContenirTitreDansCollection(t))
            {
                Debug.WriteLine("Le titre existe deja");
                return false;
            }
            else
            {
                listeTitre.Add(t);
                return true;
            }
        }

        /// <summary>
        /// Recherche si le Titre est dans la collection
        /// </summary>
        /// <param name="t">Titre a rechercher</param>
        /// <returns>True si le Titre est dans la collection, False sinon</returns>
        public bool ContenirTitreDansCollection(Titre t)
        {
            return listeTitre.Contains(t);
        }

        /// <summary>
        /// Supprime le Titre de la collection
        /// </summary>
        /// <param name="t">Titre a supprimer</param>
        /// <returns>True si le Titre est supprimer, False sinon</returns>
        public bool SupprimerTitreDeCollection(Titre t)
        {
            if (ContenirTitreDansCollection(t))
            {
                listeTitre.Remove(t);
                return true;
            }
            else
            {
                Debug.WriteLine("Le titre n'existe pas");
                return false;
            }
        }
    }
}
