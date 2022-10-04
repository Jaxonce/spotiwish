using System;
using Xunit;
using Modele;

namespace TestUnitaire
{
    // Test de la classe Titre
    public class TestTitre
    {
        [Theory]
        // Passages de string en param�tres
        [InlineData("001","blabla","Pamplemousse","www.youtube.com","Jacques","~/test","biographie",false)]
        // Passages de param�tres null
        [InlineData(null,null,null,null,null,null,null,true)]
        // Passages de string vide en param�tres
        [InlineData("","","","","","","",false)]
        // Test du Constructeur de Titre (Id du titre, Nom du Titre, Nom de l'Album du Titre,Lien vers la vid�o du Titre, Nom de l'Artiste du Titre, Chemin vers l'Image du Titre, Biography du Titre, Si une exception doit �tre lanc�)
        public void TestConstructor(string id,string nom, string nomAlbum, string video, string artiste,string image,string bio, bool shouldThrowException)
        {
            // Si la m�thode doit lancer une expection
            if (shouldThrowException)
            {
                // Verifie que l'exception est lanc� et renvoie un Titre
                Assert.Throws<ArgumentNullException>(()=> new Titre(id,nom, nomAlbum, video, artiste,image,bio));
                return;
            }
            // Cr�ation d'un Titre avec les param�tres
            Titre titre = new Titre(id,nom, nomAlbum, video, artiste,image,bio);
            // Verifie si les valeurs du Titre sont les m�mes que celles pass� en param�tres
            Assert.Equal(id, titre.Id);
            Assert.Equal(nom, titre.Nom);
            Assert.Equal(nomAlbum, titre.NomAlbum);
            Assert.Equal(video, titre.LienVideo.Lien);
            Assert.Equal(artiste, titre.NomArtiste.Nom);
            Assert.Equal(image, titre.CheminImage.Chemin);
            Assert.Equal(bio, titre.Bio);
        }

        [Fact]
        // Test de la Fonction ToString de la classe Titre
        public void TestToString()
        {
            #region Arrange
            // Cr�ation d'une Image avec des strings en param�tre
            var titre = new Titre("001","blabla", "Pamplemousse", "www.youtube.com", "Jacques", "~/test", "biographie");
            // Initialisation de la valeur attendu
            string stringAttendu = "blabla - Pamplemousse - Jacques - www.youtube.com - ~/test - biographie";
            #endregion

            #region Act
            // Ajout de la valeur du ToString dans stringRendu
            string stringRendu = titre.ToString();
            #endregion

            #region Assert
            // Verifie si la sortie du ToString est la m�me celle de la valeur attendu
            Assert.Equal(stringAttendu, stringRendu);
            #endregion
        }
    }
}
