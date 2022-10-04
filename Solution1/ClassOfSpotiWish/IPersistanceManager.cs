using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    /// <summary>
    /// Interface IPersistanceManager, utilise par le Manager pour utiliser la persistance
    /// </summary>
    public interface IPersistanceManager
    {
        /// <summary>
        /// Charge les Titres avec la persistance
        /// </summary>
        /// <returns>Un enumerateur prenant en charge une iteration sur une collection de Titre</returns>
        IEnumerable<Titre> LoadTitres();

        /// <summary>
        /// Enregistre les Titres dans la persistance
        /// </summary>
        /// <param name="titres">Collection de Titre a sauvegarder</param>
        void SaveTitres(IEnumerable<Titre> titres);

        /// <summary>
        /// Charge les Titres Favoris avec la persistance
        /// </summary>
        /// <returns>Un enumerateur prenant en charge une iteration sur une collection de Titre</returns>
        IEnumerable<Titre> LoadTitresFavoris();

        /// <summary>
        /// Enregistre les Titres Favoris dans la persistance
        /// </summary>
        /// <param name="titres">Collection de Titre a sauvegarder</param>
        void SaveTitresFavoris(IEnumerable<Titre> titres);
    }
}
