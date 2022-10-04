using System;
using Xunit;
using Modele;

namespace TestUnitaire
{
    // Test de la classe Titre
    public class TestTitre
    {
        [Theory]
        // Passages de string en paramètres
        [InlineData("001","blabla","Pamplemousse","www.youtube.com","Jacques","~/test","biographie",false)]
        // Passages de paramètres null
        [InlineData(null,null,null,null,null,null,null,true)]
        // Passages de string vide en paramètres
        [InlineData("","","","","","","",false)]
        // Test du Constructeur de Titre (Id du titre, Nom du Titre, Nom de l'Album du Titre,Lien vers la vidéo du Titre, Nom de l'Artiste du Titre, Chemin vers l'Image du Titre, Biography du Titre, Si une exception doit être lancé)
        public void TestConstructor(string id,string nom, string nomAlbum, string video, string artiste,string image,string bio, bool shouldThrowException)
        {
            // Si la méthode doit lancer une expection
            if (shouldThrowException)
            {
                // Verifie que l'exception est lancé et renvoie un Titre
                Assert.Throws<ArgumentNullException>(()=> new Titre(id,nom, nomAlbum, video, artiste,image,bio));
                return;
            }
            // Création d'un Titre avec les paramètres
            Titre titre = new Titre(id,nom, nomAlbum, video, artiste,image,bio);
            // Verifie si les valeurs du Titre sont les mêmes que celles passé en paramètres
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
            // Création d'une Image avec des strings en paramètre
            var titre = new Titre("001","blabla", "Pamplemousse", "www.youtube.com", "Jacques", "~/test", "biographie");
            // Initialisation de la valeur attendu
            string stringAttendu = "blabla - Pamplemousse - Jacques - www.youtube.com - ~/test - biographie";
            #endregion

            #region Act
            // Ajout de la valeur du ToString dans stringRendu
            string stringRendu = titre.ToString();
            #endregion

            #region Assert
            // Verifie si la sortie du ToString est la même celle de la valeur attendu
            Assert.Equal(stringAttendu, stringRendu);
            #endregion
        }
    }
}
