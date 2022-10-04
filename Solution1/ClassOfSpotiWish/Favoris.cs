using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    /// <summary>
    /// Classe Favoris, herite de CollectionTitre
    /// </summary>
    public class Favoris : CollectionTitre
    {
        /// <summary>
        /// Construit un Favoris avec la liste de Titre passe en parametres
        /// </summary>
        /// <param name="desTitres">Liste des Titres a ajouter lors de la constuction</param>
        public Favoris(params Titre[] desTitres)
            : base(desTitres)
        {
            Debug.WriteLine("Collection fav crée");
        }

        /// <summary>
        /// Transforme un Favoris en string (utilise par l'affichage console)
        /// </summary>
        /// <returns>UN string contenant les informations des Titre des Favoris</returns>
        public override string ToString()
        {
            return $"{base.ToString()}";
        }
    }
}
