using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modele;
using Xunit;

namespace TestUnitaire
{
    // Test de la classe Artiste
    public class TestArtiste
    {
        [Fact]
        // Test de la Fonction ToString de la classe Artiste
        public void TestArtisteToString()
        {
            #region Arrange
            // Création d'un Artiste avec un string en paramètre
            Artiste artiste = new Artiste("Je suis un artiste");
            // Initialisation de la valeur attendu
            string valueAttendu = "Je suis un artiste";
            #endregion

            #region Assert
            // Verifie si la sortie du ToString est la même celle de la valeur attendu
            Assert.Equal(valueAttendu, artiste.ToString());
            #endregion
        }
    }
}
