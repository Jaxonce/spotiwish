using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modele;
using Xunit;

namespace TestUnitaire
{
    // Test de la classe Video
    public class TestVideo
    {
        [Fact]
        // Test de la Fonction ToString de la classe Video
        public void TestVideoToString()
        {
            #region Arrange
            // Création d'une Video avec un string en paramètre
            Video video = new Video("www.lelien.com");
            // Initialisation de la valeur attendu
            string valueAttendu = "www.lelien.com";
            #endregion

            #region Assert
            // Verifie si la sortie du ToString est la même celle de la valeur attendu
            Assert.Equal(valueAttendu,video.ToString());
            #endregion
        }
    }
}
