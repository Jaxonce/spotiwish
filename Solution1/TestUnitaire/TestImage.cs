using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modele;
using Xunit;


namespace TestUnitaire
{
    public class TestImage
    {
        // Test de la classe Image
        [Fact]
        // Test de la Fonction ToString de la classe Image
        public void TestImageToString()
        {
            #region Arrange
            // Création d'une Image avec un string en paramètre
            Image image = new Image("~/test");
            // Initialisation de la valeur attendu
            string valueAttendu = "~/test";
            #endregion
            #region Assert
            // Verifie si la sortie du ToString est la même celle de la valeur attendu
            Assert.Equal(valueAttendu, image.ToString());
            #endregion
        }
        [Fact]
        // Test de la Fonction ToString de la classe Image avec une Image sans paramètre
        public void TestImageToStringInconnu()
        {
            #region Arrange
            // Création d'une Image sans paramètres
            Image image = new Image();
            // Initialisation de la valeur attendu
            string valueAttendu = "Image Inconnu";
            #endregion
            #region Assert
            // Verifie si la sortie du ToString est la même celle de la valeur attendu
            Assert.Equal(valueAttendu, image.ToString());
            #endregion
        }

    }
}
