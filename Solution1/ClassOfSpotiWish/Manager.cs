using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel;

namespace Modele
{
    /// <summary>
    /// Classe Manager permettant la gestion des Titres de l'application.
    /// </summary>
    public class Manager : INotifyPropertyChanged
    {
        /// <summary>
        /// Liste de Titre, implementant INotifyCollectionChanged et INotifyPropertyChanged au changement de la liste
        /// </summary>
        public ReadOnlyObservableCollection<Titre> ListeTitre { get; private set; }
        readonly ObservableCollection<Titre> listeTitre = new ObservableCollection<Titre>();

        /// <summary>
        /// Liste de CollectionTitre, implementant INotifyCollectionChanged et INotifyPropertyChanged au changement de la liste
        /// </summary>
        public ReadOnlyObservableCollection<CollectionTitre> ListeCollection { get; private set; }
        readonly ObservableCollection<CollectionTitre> listeCollection = new ObservableCollection<CollectionTitre>();

        /// <summary>
        /// Liste de Titres qui devront etre supprimer lors de la validation de la suppression dans l'application.
        /// </summary>
        public List<Titre> ListeTitreSupp { get; private set; }
        readonly List<Titre> listeTitreSupp = new List<Titre>();

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Titre Selectionne par l'utilisateur, permettra de modifier, supprimer, ajouter aux favoris, ...
        /// </summary>
        private Titre selectedTitre;

        public Titre SelectedTitre
        {
            get { return selectedTitre; }
            set 
            { 
                if (selectedTitre == value)
                {
                    return;
                }
                selectedTitre = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedTitre"));
                if(PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedTitre"));
                }
            }
        }

        /// <summary>
        /// Evenement de type IsFavorisEmptyEventArgs permettant de savoir si les Favoris sont vide
        /// </summary>
        public event EventHandler<IsFavorisEmptyEventArgs> IsFavorisEmpty;

        /// <summary>
        /// Classe IsFavorisEmptyEventArgs, Heritant de EventArgs
        /// </summary>
        public class IsFavorisEmptyEventArgs : EventArgs
        {
            public IsFavorisEmptyEventArgs() { }
        }

        /// <summary>
        /// OnFavorisEmpty est un evenement que l'on a cree permettant de changer la vue de l'application lorsque la liste des Favoris est vide.
        /// </summary>
        /// <param name="args">Evenement lance lorsque les Favoris sont vide</param>
        public void OnFavorisEmpty(IsFavorisEmptyEventArgs args) => IsFavorisEmpty?.Invoke(this, args);

        //// Constructeurs de Manager & Persistance

        /// <summary>
        /// Construit un Manager avec les Titres passe en parametres dans ListeTitre
        /// </summary>
        /// <param name="desTitres">Liste de Titres a Ajouter au Manager</param>
        public Manager(params Titre[] desTitres)
        {
            ListeTitre = new ReadOnlyObservableCollection<Titre>(listeTitre);
            ListeCollection = new ReadOnlyObservableCollection<CollectionTitre>(listeCollection);
            ListeTitreSupp = new List<Titre>(listeTitreSupp);
            //listeTitre.AddRange(desTitres.Where(t => t != null && listeTitre.Contains(t)));
            foreach (Titre titre in desTitres)
            {
                listeTitre.Add(titre);
            }
            
            if (!listeCollection.Any())
            {
                listeCollection.Add(new Favoris());
            }
        }
        public IPersistanceManager Pers { get; private set; }

        /// <summary>
        /// Constructeur de Manager avec la persistance
        /// </summary>
        /// <param name="pers">Persistance</param>
        public Manager(IPersistanceManager pers)
        {
            ListeTitre = new ReadOnlyObservableCollection<Titre>(listeTitre);
            ListeCollection = new ReadOnlyObservableCollection<CollectionTitre>(listeCollection);
            ListeTitreSupp = new List<Titre>(listeTitreSupp);

            Pers = pers;
            this.LoadTitres();
            this.LoadTitresFavoris();
        }
        /// <summary>
        /// Charge les Titres Favoris dans ListeCollection
        /// </summary>
        public void LoadTitresFavoris()
        {
            // Pour tout les Titres dans la persistance des TitresFavoris, ajouter le titre
            foreach (Titre t in Pers.LoadTitresFavoris())
            {
                GetListeFavoris().AjouterTitreDansCollection(t);
            }
        }

        /// <summary>
        /// Enregistre les Titre Favoris dans la persistance
        /// </summary>
        public void SaveTitresFavoris()
        {
            Pers.SaveTitresFavoris(GetListeFavoris().ListeTitre);
        }
        /// <summary>
        /// Charge les Titres dans ListeTitre avec la persistance
        /// </summary>
        public void LoadTitres()
        {
            listeTitre.Clear();
            foreach (Titre titre in Pers.LoadTitres())
            {
                listeTitre.Add(titre);
            }
            if (! listeCollection.Any())
            {
                listeCollection.Add(new Favoris());
            }
            if (listeTitre.Count > 0)
            {
                SelectedTitre = listeTitre.First();
            }
        }
        /// <summary>
        /// Enregistre les Titres de ListeTitre dans la persistance
        /// </summary>
        public void SaveTitres()
        {
            Pers.SaveTitres(ListeTitre);
        }

        /// <summary>
        /// Affiche les Titres de ListeTitre du Manager
        /// </summary>
        /// <returns>Le string a afficher</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Mes Titres : ");
            foreach (Titre t in listeTitre)
            {
                sb.Append($"{t}\t");
            }
            return sb.ToString();
        }

        ////Methodes de Manager pour les Titres

        /// <summary>
        /// Trouve le Titre grace a son Id dans ListeTitre
        /// </summary>
        /// <param name="uniqueId">L'Id du Titre que l'on veut trouver</param>
        /// <returns>Returne le Titre correspondant a l'Id</returns>
        public Titre GetTitreById(string uniqueId) => listeTitre.SingleOrDefault(t => t.Id == uniqueId);

        /// <summary>
        /// Ajoute le Titre passe en parametre dans ListeTitre
        /// </summary>
        /// <param name="t">Titre a ajouter</param>
        /// <returns>True si ca a fonctionne, False si le Titre existe deja</returns>
        public bool AjouterTitre(Titre t)
        {
            if(ContenirTitre(t))
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
        /// Verfie si le Titre est contenu dans la ListeTitre
        /// </summary>
        /// <param name="t">Titre a recherhcer</param>
        /// <returns>True si le Titre est dans ListeTitre, sinon False</returns>
        public bool ContenirTitre(Titre t)
        {
            return listeTitre.Contains(t);
        }

        /// <summary>
        /// Supprimer le Titre passe en parametre
        /// </summary>
        /// <param name="t">Titre a supprimer</param>
        /// <returns>True si le Titre est supprime, False sinon</returns>
        public bool SupprimerTitre(Titre t)
        {
            if(ContenirTitre(t))
            {
                foreach (var c in listeCollection.Where(c => c.ContenirTitreDansCollection(t)))
                {
                    c.SupprimerTitreDeCollection(t);
                }

                listeTitre.Remove(t);
                return true;
            }
            else 
            {
                Debug.WriteLine("Le titre n'existe pas");
                return false;
            }
        }

        /// <summary>
        /// Modifie le Titre
        /// </summary>
        /// <param name="t">Titre a modifier</param>
        /// <param name="newT">Titre avec les valeurs a remplacer</param>
        /// <returns>True si ca a marche, False sinon</returns>
        public bool ModifierTitre(Titre t,Titre newT)
        {
            if(!ContenirTitre(t))
            {
                Debug.WriteLine("Le titre n'existe pas");
                return false;
            }
            else
            {
                t.Id = newT.Id;
                t.Nom = newT.Nom;
                t.NomAlbum = newT.NomAlbum;
                t.LienVideo = newT.LienVideo;
                t.NomArtiste = newT.NomArtiste;
                t.CheminImage = newT.CheminImage;
                t.Bio = newT.Bio;
                Debug.WriteLine("Titre modifier");
                return t==newT;
            }
        }

        ////Methode de Manager pour les Collections

        /// <summary>
        /// Ajoute une CollectionTitre a ListeCollection
        /// </summary>
        /// <param name="collection">CollectionTitre a ajouter</param>
        /// <returns>True si ca le titre est ajouter, False sinon</returns>
        public bool AjouterCollection(CollectionTitre collection)
        {
            if (ContenirCollection(collection))
            {
                Debug.WriteLine("Le collection existe deja");
                return false;
            }
            else
            {
                listeCollection.Add(collection);
                return true;
            }
        }

        /// <summary>
        /// Supprime la CollectionTitre de ListeCollection
        /// </summary>
        /// <param name="collection">CollectionTirte a supprimer</param>
        /// <returns>True si la colelction est supprimee, False sinon </returns>
        public bool SupprimerCollection(CollectionTitre collection)
        {
            if (!ContenirCollection(collection))
            {
                Debug.WriteLine("La collection existe pas");
                return false;
            }
            if(collection is Favoris)
            {
                Debug.WriteLine("La collection est Favori");
                return false;
            }
            else
            {
                listeCollection.Remove(collection);
                return true;
            }
        }

        /// <summary>
        /// Recherche la Collection dans ListeCollection
        /// </summary>
        /// <param name="collection">Collection a rechercher</param>
        /// <returns>True si la collection est dans ListeCollection, sinon False</returns>
        public bool ContenirCollection(CollectionTitre collection)
        {
            return listeCollection.Contains(collection);
        }

        /// <summary>
        /// Renvoie la collection de Type Favoris dans ListeCollection
        /// </summary>
        /// <returns>La premiere CollectionTitre de Type Favoris</returns>
        public CollectionTitre GetListeFavoris() => listeCollection.FirstOrDefault(c => c is Favoris);

        /// <summary>
        /// Recherche si le Titre est dans la Collection passe en parametre
        /// </summary>
        /// <param name="c">CollectionTitre a rechercher</param>
        /// <param name="t">Titre a trouver</param>
        /// <returns>True si le Titre est dans la CollectionTitre, False sinon</returns>
        public bool ContenirTitreInCollection(CollectionTitre c,Titre t)
        {
            return c.ContenirTitreDansCollection(t);
        }

        /// <summary>
        /// Ajoute le Titre a la CollectionTitre
        /// </summary>
        /// <param name="c">Collection ou l'on veut ajouter le Titre</param>
        /// <param name="t">Titre a ajouter</param>
        /// <returns></returns>
        public bool AjouterTitreToCollection(CollectionTitre c, Titre t)
        {
            if (ContenirTitreInCollection(c,t))
            {
                Debug.WriteLine("Le titre existe deja");
                return false;
            }
            else
            {
                c.AjouterTitreDansCollection(t);
                return true;
            }
        }

        /// <summary>
        /// Supprime le Titre de la CollectionTitre passe en parametre
        /// </summary>
        /// <param name="c">Collection ou l'on veut suppirmer le Titre</param>
        /// <param name="t">Titre a supprimer</param>
        /// <returns></returns>
        public bool SupprimerTitreFromCollection(CollectionTitre c, Titre t)
        {
            if (!ContenirTitreInCollection(c, t))
            {
                Debug.WriteLine("Le Titre n'est pas dans la collection");
                return false;
            }
            else
            {
                c.SupprimerTitreDeCollection(t);
                return true;
            }
        }
    }
}
