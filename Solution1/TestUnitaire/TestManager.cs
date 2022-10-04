using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Modele;
using StubLib;
using Xunit;

namespace TestUnitaire
{
    // Test de la classe Manager
    public class TestManager
    {
        [Fact]
        // Test du Constructeur de Manager avec un Stub
        public void TestConstructorStub()
        {
            // Création d;un Manager avec un Stub en paramètre
            var manager = new Manager(new Stub());
            // Verifie si la liste des Titres du manager n'est pas vide
            Assert.NotEmpty(manager.ListeTitre);
        }

        [Fact]
        // Test du Constructeur de Manager avec les paramètres
        public void TestConstructorParams()
        {
            // Création de 2 Titres avec des strings en paramètres
            var titre1 = new Titre("001","blabla", "Pamplemousse", "www.youtube.com", "Jacques", "~/test", "biographie");
            var titre2 = new Titre("002","blabla2", "Pamplemousse2", "www.youtube.com2", "Jacques2", "~/test2", "biographie2");
            // Création du Manager avec les 2 Titres
            var manager = new Manager(titre1, titre2);

            // Vérifie si le Manager n'est pas vide
            Assert.NotEmpty(manager.ListeTitre);
            // Vérifie qu'il y a 2 Titres dans la liste de Titre du Manager
            Assert.Equal(2, manager.ListeTitre.Count);
            // Vérifie si le Manager contient les 2 titres passé en paramètres
            Assert.Contains(titre1, manager.ListeTitre);
            Assert.Contains(titre2, manager.ListeTitre);
        }

        [Fact]
        // Test de la fonction ToString du Manager
        public void TestToString()
        {
            #region Arrange
            // Création d'un Titre avec des strings en paramètres
            var titre1 = new Titre("001","blabla", "Pamplemousse", "www.youtube.com", "Jacques", "~/test", "biographie");
            //Création d'un Manager avec le Titre crée précédemment
            var manager = new Manager(titre1);
            // Initialisation du message attendu 
            var msg = "Mes Titres : blabla - Pamplemousse - Jacques - www.youtube.com - ~/test - biographie\t";
            #endregion

            #region Act
            #endregion

            #region Assert
            // Vérifie si la fonction ToString renvoie la même chose que ce qui est attendu
            Assert.Equal(msg, manager.ToString());
            #endregion
        }

        // If we add a product to the list, the list contains one more product and it is present in the list
        [Fact]
        public void TestAddOneProductToTheListeTitre()
        {
            #region Arrange
            //Création de 2 Titres avec des strings en paramètres
            var titre1 = new Titre("001","blabla", "Pamplemousse", "www.youtube.com", "Jacques", "~/test", "biographie");
            var titre2 = new Titre("002","blabla2", "Pamplemousse2", "www.youtube.com2", "Jacques2", "~/test2", "biographie2");
            //Création d'un Manager avec les 2 Titres
            var manager = new Manager(titre1, titre2);
            // Initialisation des valeurs courrantes et attendu
            var currentListeTitreCount = 2;
            var expectedListeTitreCount = 3;

            // Fait une copie de la liste avant d'ajouter le titre à la liste
            var currentListeTitre = manager.ListeTitre.ToList();
            // Création d'un nouveau Titre que l'on va ajouté
            var newTitre = new Titre("003", "blabla3", "Pamplemousse3", "www.youtube.com3", "Jacques3", "~/test3", "biographie3");
            // Initialisation de la liste attendu après l'ajout du Titre
            var expectedListeTitre = new List<Titre>()
            {
                titre1,
                titre2,
                newTitre,
            };
            #endregion

            #region Act
            // Ajout du Titre au Manager
            manager.AjouterTitre(newTitre);
            #endregion

            #region Assert
            // Before
            // Verify the number of items in the list
            Assert.Equal(currentListeTitreCount, currentListeTitre.Count);
            // Verify the list contains all titles
            Assert.Contains(titre1, currentListeTitre);
            Assert.Contains(titre2, currentListeTitre);

            // After
            // Verify the number of items in the list
            Assert.Equal(expectedListeTitreCount, manager.ListeTitre.Count);
            // Verify the old titles are in the list
            Assert.Contains(titre1, manager.ListeTitre);
            Assert.Contains(titre2, manager.ListeTitre);
            // Verify the new title is in the list
            Assert.Contains(newTitre, manager.ListeTitre);
            // Verify the new list is equal to the expected one
            Assert.Equal(expectedListeTitre, manager.ListeTitre);
            // Verify that we can't add the same title 2 times
            Assert.False(manager.AjouterTitre(newTitre));
            #endregion
        }

        [Fact]
        // Test de la liste des Favoris
        public void TestListeFavoris()
        {
            #region Arrange
            // Création d'un Manager sans paramètre
            var manager = new Manager();
            #endregion

            #region Act

            #endregion

            #region Assert
            // Vérifie que la listeCollection du Manager est pas null
            Assert.NotNull(manager.ListeCollection);
            // Vérifie que la listeCollection du Manager est pas vide
            Assert.NotEmpty(manager.ListeCollection);
            // Vérifie que la listeCollection du Manager contient seulement une collection
            Assert.Single(manager.ListeCollection);
            // Vérifie que la première collection de la listeCollection du Manager est de Type Favoris
            Assert.True(manager.GetListeFavoris() is Favoris);
            #endregion
        }

       
        [Fact]
        //Test creation d'une collection
        public void TestCollection()
        {
            #region Arrange
            // Création d'un Manager sans paramètres
            var manager = new Manager();
            // Création d'un Titre et d'une Collection contenant le Titre
            var titre1 = new Titre("001", "blabla", "Pamplemousse", "www.youtube.com", "Jacques", "~/test", "biographie");
            CollectionTitre collection1 = new CollectionTitre(titre1);
            //Initialisation des valeurs courrantes et attendues
            var currentListeCollectionCount = 1;
            var expectedListeCollectionCount = 2;
            // Fait une copie de la listeCollection de Manager
            var currentListeCollection = manager.ListeCollection.ToList();
            #endregion

            #region Act
            // Ajout de la collection au Manager
            manager.AjouterCollection(collection1);
            #endregion

            #region Assert
            //Before
            // Vérifie que le Manager contenait le bon nombre de CollectionTitre avant les actions
            Assert.Equal(currentListeCollectionCount, currentListeCollection.Count);
            // Vérifie que le Manager contenait seulement 1 CollectionTitre
            Assert.Single(currentListeCollection);
            // Vérifie que le Manager ne contenait pas la CollectionTitre ajouté
            Assert.DoesNotContain(collection1, currentListeCollection);
            // Vérifie que la première Collection du Manger est de Type Favoris
            Assert.True(manager.GetListeFavoris() is Favoris);

            //After
            // Vérifie que le Manager contient le bon nombre de CollectionTitre après les actions
            Assert.Equal(expectedListeCollectionCount, manager.ListeCollection.Count);
            // Vérifie que le Manager contient la Collection ajoutée dans ses Colelctions
            Assert.Contains(collection1, manager.ListeCollection);
            // Vérifie que le Manager contient le Titre de la Collection Ajouté dans la collection ajouté
            Assert.Contains(titre1, manager.ListeCollection.ElementAt(1).ListeTitre);
            // Vérifie que la première Collection du Manger est de Type Favoris
            Assert.True(manager.GetListeFavoris() is Favoris);
            // Vérifie que l'on ne puisse pas ajouté la collection deux fois
            Assert.False(manager.AjouterCollection(collection1));
            #endregion
        }

        /// <summary>
        /// Ajout d'un titre à la collection
        /// </summary>
        [Fact]

        public void TestAddToTheListeCollection()
        {
            #region Arrange
            // Création de 3 Titres, un Manager contenant les Titres titre1 et titre2, un CollectionTitre contenant le Titre titre1
            var titre1 = new Titre("001", "blabla", "Pamplemousse", "www.youtube.com", "Jacques", "~/test", "biographie");
            var titre2 = new Titre("002", "blabla2", "Pamplemousse2", "www.youtube.com2", "Jacques2", "~/test2", "biographie2");
            var titre3 = new Titre("003", "blabla3", "Pamplemousse3", "www.youtube.com3", "Jacques3", "~/test3", "biographie3");
            var manager = new Manager(titre1,titre2);
            CollectionTitre collection1 = new CollectionTitre(titre1);

            // Ajout de la collection au Manager
            manager.AjouterCollection(collection1);

            // Initialisation des valeurs attendues et courrantes
            var currentNumberOfTitleInCollection = 1;
            var expectedNumberOfTitleInCollection = 3;

            var currentListeCollection = manager.ListeCollection.ElementAt(1).ListeTitre.ToList();

            var expectedListeTitreOfCollection = new List<Titre>()
            {
                titre1,
                titre2,
                titre3,
            };

            #endregion

            #region Act
            // Ajout du Titre titre2 puis titre3 à la deuxième liste de listeCollection du Manager 
            manager.AjouterTitreToCollection(manager.ListeCollection.ElementAt(1), titre2);
            manager.AjouterTitreToCollection(manager.ListeCollection.ElementAt(1), titre3);
            #endregion

            #region Assert
            //Before
            //Vérifie que le nombre de titre dans listeCollection est la même que la valeur attendue
            Assert.Equal(currentNumberOfTitleInCollection, currentListeCollection.Count);
            // Vérifie si la listeCollection contient le Titre titre1 mais ne contient pas les Titres titre2 et titre3
            Assert.Contains(titre1, currentListeCollection);
            Assert.DoesNotContain(titre2, currentListeCollection);
            Assert.DoesNotContain(titre3, currentListeCollection);

            //After
            // Vérifie que le nombre de titres dans la deuxième lsite de listeCollection contient le nombre attendu de titres
            Assert.Equal(expectedNumberOfTitleInCollection, manager.ListeCollection.ElementAt(1).ListeTitre.Count);
            // Vérifie que la Collection contient les Titres titre1, titre2 et titre3
            Assert.Contains(titre1, manager.ListeCollection.ElementAt(1).ListeTitre);
            Assert.Contains(titre2, manager.ListeCollection.ElementAt(1).ListeTitre);
            Assert.Contains(titre3, manager.ListeCollection.ElementAt(1).ListeTitre);
            // Vérifie que la liste des Titres dans collection est la même que celle attendue
            Assert.Equal(expectedListeTitreOfCollection, manager.ListeCollection.ElementAt(1).ListeTitre);
            #endregion
        }

        /// <summary>
        /// Ajout d'un Titre au Favoris
        /// </summary>
        [Fact]
        public void TestAddTitreToFavoris()
        {
            #region Arrange
            // Création de 3 Titres et d'un Manager contenant les Titres titre1 et titre2
            var titre1 = new Titre("001", "blabla", "Pamplemousse", "www.youtube.com", "Jacques", "~/test", "biographie");
            var titre2 = new Titre("002", "blabla2", "Pamplemousse2", "www.youtube.com2", "Jacques2", "~/test2", "biographie2");
            var titre3 = new Titre("003", "blabla3", "Pamplemousse3", "www.youtube.com3", "Jacques3", "~/test3", "biographie3");
            var manager = new Manager(titre1, titre2);

            // Initialisation des valeurs attendues et courrantes
            var currentNumberOfTitleInCollection = 0;
            var expectedNumberOfTitleInCollection = 2;

            // Fait une copie de la liste des Favoris dans currentListeCollection
            var currentListeCollection = manager.GetListeFavoris().ListeTitre.ToList();

            #endregion

            #region Act
            // Ajoute les Titres titre1 et titre2 aux favoris
            manager.AjouterTitreToCollection(manager.GetListeFavoris(), titre1);
            manager.AjouterTitreToCollection(manager.GetListeFavoris(), titre2);
            #endregion

            #region Assert
            //Before
            // Vérifie que le nombre de Titre dans la collection est le même que celui avant modification
            Assert.Equal(currentNumberOfTitleInCollection, currentListeCollection.Count);
            // Vérifie que la liste des Favoris ne contient pas les Titres titre1 et titre2
            Assert.DoesNotContain(titre1, currentListeCollection);
            Assert.DoesNotContain(titre2, currentListeCollection);

            //After
            // Vérifie que le nombre de Titre dans la colelction Favoris est le même que celui attendu
            Assert.Equal(expectedNumberOfTitleInCollection, manager.GetListeFavoris().ListeTitre.Count);
            // Vérifie que les Titres titre1 et titre2 sont dans la collection Favoris
            Assert.Contains(titre1, manager.GetListeFavoris().ListeTitre);
            Assert.Contains(titre2, manager.GetListeFavoris().ListeTitre);

            #endregion
        }

        /// <summary>
        /// Suppression d'un Titre des favoris
        /// </summary>
        [Fact]
        public void TestRemoveTitreToFavoris()
        {
            #region Arrange
            // Création de 3 Titres et d'un Manager contenant les 3 Titres
            var titre1 = new Titre("001", "blabla", "Pamplemousse", "www.youtube.com", "Jacques", "~/test", "biographie");
            var titre2 = new Titre("002", "blabla2", "Pamplemousse2", "www.youtube.com2", "Jacques2", "~/test2", "biographie2");
            var titre3 = new Titre("003", "blabla3", "Pamplemousse3", "www.youtube.com3", "Jacques3", "~/test3", "biographie3");
            var manager = new Manager(titre1, titre2, titre3);

            // Initialisation des valeurs attendues
            var currentNumberOfTitleInCollection = 3;
            var expectedNumberOfTitleInCollection = 0;

            // Ajout des 3 Titres dans la collection Favoris de listeCollection du Manager
            manager.AjouterTitreToCollection(manager.GetListeFavoris(), titre1);
            manager.AjouterTitreToCollection(manager.GetListeFavoris(), titre2);
            manager.AjouterTitreToCollection(manager.GetListeFavoris(), titre3);

            // Fait une copie de la collection Favoris de listeCollection du Manager dans currentListeCollection
            var currentListeCollection = manager.GetListeFavoris().ListeTitre.ToList();
            #endregion

            #region Act
            // Suppression des 3 Titres de la collection Favoris de listeCollection du Manager
            manager.SupprimerTitreFromCollection(manager.GetListeFavoris(), titre1);
            manager.SupprimerTitreFromCollection(manager.GetListeFavoris(), titre2);
            manager.SupprimerTitreFromCollection(manager.GetListeFavoris(), titre3);
            #endregion

            #region Assert

            //Before
            // Vérifie que le nombre de Titre dans les Favoris était les même que ceux attendu
            Assert.Equal(currentNumberOfTitleInCollection, currentListeCollection.Count);
            // Vérifie que la liste Favoris contenait les trois Titres
            Assert.Contains(titre1, currentListeCollection);
            Assert.Contains(titre2, currentListeCollection);
            Assert.Contains(titre3, currentListeCollection);

            //After
            //Vérique que le nombre de Titre dans Favoris est celui attendu
            Assert.Equal(expectedNumberOfTitleInCollection, manager.GetListeFavoris().ListeTitre.Count);
            //Vérifie que la colelction de Favoris de listeCollection ne contient pas les Titres
            Assert.DoesNotContain(titre1, manager.GetListeFavoris().ListeTitre);
            Assert.DoesNotContain(titre2, manager.GetListeFavoris().ListeTitre);
            Assert.DoesNotContain(titre3, manager.GetListeFavoris().ListeTitre);
            // Vérifie que la ListeTitre de Manager a toujours les Titres
            Assert.Contains(titre1, manager.ListeTitre);
            Assert.Contains(titre2, manager.ListeTitre);
            Assert.Contains(titre3, manager.ListeTitre);
            // Vérifie que la fonction SupprimerTitreFromCollection renvoie False si on essaye d'enlever un Titre qui n'est pas dans Favoris
            Assert.False(manager.SupprimerTitreFromCollection(manager.GetListeFavoris(), titre1));
            #endregion
        }

        /// <summary>
        /// Supression d'un Titre d'une collection
        /// </summary>
        [Fact]
        public void TestRemoveTitleOfCollection()
        {
            #region Arrange
            // Création de 3 Titres et d'un Manager contenant les 3 Titres et d'une CollectionTitre contenant aussi les 3 Titres
            var titre1 = new Titre("001", "blabla", "Pamplemousse", "www.youtube.com", "Jacques", "~/test", "biographie");
            var titre2 = new Titre("002", "blabla2", "Pamplemousse2", "www.youtube.com2", "Jacques2", "~/test2", "biographie2");
            var titre3 = new Titre("003", "blabla3", "Pamplemousse3", "www.youtube.com3", "Jacques3", "~/test3", "biographie3");
            var manager = new Manager(titre1, titre2, titre3);

            CollectionTitre collection1 = new CollectionTitre(titre1,titre2,titre3);

            // Ajout de la collection au Manager
            manager.AjouterCollection(collection1);

            // Initialisation des valeur attendue et courrantes
            var currentNumberOfTitleInCollection = 3;
            var expectedNumberOfTitleInCollection = 2;

            // Fait une copie de la liste de CollectionTitre avant de faire des modification
            var currentListeCollection = manager.ListeCollection.ElementAt(1).ListeTitre.ToList();

            #endregion

            #region Act
            // Supprime le Titre titre2 à la collection de Manager
            manager.SupprimerTitreFromCollection(manager.ListeCollection.ElementAt(1), titre2);
            #endregion

            #region Assert
            //Before
            // Vérifie que le nombre de Titre initial est bien celui que l'on attend
            Assert.Equal(currentNumberOfTitleInCollection, currentListeCollection.Count);
            // Vérifie que le les Titres sont dans la collection
            Assert.Contains(titre1, currentListeCollection);
            Assert.Contains(titre2, currentListeCollection);
            Assert.Contains(titre3, currentListeCollection);

            //After
            // Vérifie que le nombre de titre dans la collection après suppression est celui que l'on attend
            Assert.Equal(expectedNumberOfTitleInCollection,manager.ListeCollection.ElementAt(1).ListeTitre.Count);
            // Vérifie que la collection contient titre1 et titre3
            Assert.Contains(titre1, manager.ListeCollection.ElementAt(1).ListeTitre);
            Assert.Contains(titre3, manager.ListeCollection.ElementAt(1).ListeTitre);
            // Vérifie que titre2 est bien supprimé de la collection
            Assert.DoesNotContain(titre2, manager.ListeCollection.ElementAt(1).ListeTitre);
            // Vérifie que le Titre est supprimé seulement dans la collection
            Assert.Contains(titre2, manager.ListeTitre);

            // Vérifie que l'on ne puisse pas ajouter la collection deux fois au Manager
            Assert.False(manager.AjouterCollection(collection1));

            #endregion
        }

        /// <summary>
        /// Supression d'une collection
        /// </summary>
        [Fact]
        public void TestRemoveOfCollection()
        {
            #region Arrange
            // Création de 3 Titres et d'un Manager contenant les 3 Titres et d'une CollectionTitre contenant aussi les 3 Titres
            var titre1 = new Titre("001", "blabla", "Pamplemousse", "www.youtube.com", "Jacques", "~/test", "biographie");
            var titre2 = new Titre("002", "blabla2", "Pamplemousse2", "www.youtube.com2", "Jacques2", "~/test2", "biographie2");
            var titre3 = new Titre("003", "blabla3", "Pamplemousse3", "www.youtube.com3", "Jacques3", "~/test3", "biographie3");
            var manager = new Manager(titre1, titre2, titre3);

            CollectionTitre collection1 = new CollectionTitre(titre1, titre2, titre3);

            // Ajout de la colelction au Manager
            manager.AjouterCollection(collection1);

            //Initialisation des valeurs attendues et courrantes
            var currentNumberOfCollection = 2;
            var expectedNumberOfollection = 1;

            // Fait une copie de la listeCollection avant de la modifier
            var currentListeCollection = manager.ListeCollection.ToList();

            #endregion

            #region Act
            // Supprime de la ListeCollection du Manager la collection1
            manager.SupprimerCollection(collection1);
            #endregion

            #region Assert
            //Before
            // Vérifie que le nombre de collection initial est le même que celui attendu
            Assert.Equal(currentNumberOfCollection, currentListeCollection.Count);
            // Vérifie que la ListeCollection du Manager contennait bien collection1
            Assert.Contains(collection1, currentListeCollection);

            //After
            // Vérifie que le nombre de collection après modification est celui attendu
            Assert.Equal(expectedNumberOfollection, manager.ListeCollection.Count);
            // Vérifie que la ListeCollection du Manager ne contient plus collection1
            Assert.DoesNotContain(collection1, manager.ListeCollection);
            // Vérifie SupprimerCollection renvoie False si on essaye de supprimer une collection qui n'est pas dans la listeCollection du Manager
            Assert.False(manager.SupprimerCollection(collection1));

            #endregion
        }

        /// <summary>
        /// Supression de la collection favoris (Impossible, ne doit pas supprimer)
        /// </summary>
        [Fact]
        public void TestRemoveCollectionFavoris()
        {
            #region Arrange
            // Création de 3 Titres et d'un Manager contenant les 3 Titres
            var titre1 = new Titre("001", "blabla", "Pamplemousse", "www.youtube.com", "Jacques", "~/test", "biographie");
            var titre2 = new Titre("002", "blabla2", "Pamplemousse2", "www.youtube.com2", "Jacques2", "~/test2", "biographie2");
            var titre3 = new Titre("003", "blabla3", "Pamplemousse3", "www.youtube.com3", "Jacques3", "~/test3", "biographie3");
            var manager = new Manager(titre1, titre2, titre3);

            // Initialisation des valeurs attendues et courrantes
            var currentNumberOfCollection = 1;
            var expectedNumberOfollection = 1;
            var currentNumberOfTitleInCollection = 3;
            var expectedNumberOfTitleInCollection = 3;
            
            // Ajout des Titres à la colelction Favoris de ListeCollection du Manager
            manager.AjouterTitreToCollection(manager.GetListeFavoris(), titre1);
            manager.AjouterTitreToCollection(manager.GetListeFavoris(), titre2);
            manager.AjouterTitreToCollection(manager.GetListeFavoris(), titre3);

            // Fait une copie de la ListeCollection du Manager
            var currentListeCollection = manager.ListeCollection.ToList();
            #endregion

            #region Act
            // Essaie de Supprimer la liste Favoris de ListeCollection
            manager.SupprimerCollection(manager.GetListeFavoris());
            #endregion

            #region Assert
            // Vérifie si on supprime Favoris de la ListeCollection du Manager, la méthode renvoie False
            Assert.False(manager.SupprimerCollection(manager.GetListeFavoris()));
            //Before
            // Vérifie que le nombre de collection dans ListeCollection est celui qui est attendu
            Assert.Equal(currentNumberOfCollection, currentListeCollection.Count);
            // Vérifie que le nombre de Titre dans Favoris de ListeCollection du Manager est celui qui est attendu
            Assert.Equal(currentNumberOfTitleInCollection, currentListeCollection.First().ListeTitre.Count);

            //After
            // Vérifie que le nombre de collection dans ListeCollection est celui qui est attendu
            Assert.Equal(expectedNumberOfollection, manager.ListeCollection.Count);
            // Vérifie que le nombre de Titre dans Favoris de ListeCollection du Manager est celui qui est attendu
            Assert.Equal(expectedNumberOfTitleInCollection, manager.GetListeFavoris().ListeTitre.Count);

            // Vérifie que l'on ne puisse pas ajouter le Titre titre1 deux fois au Favoris de la ListeCollection du Manager
            Assert.False(manager.AjouterTitreToCollection(manager.GetListeFavoris(), titre1));
            
            #endregion
        }

        /// <summary>
        /// Supression d'un titre de la liste de titre (L'ajouter dans un premier temps dans une collection et dans les favoris -> doit le supprimer de partout)
        /// </summary>
        [Fact]
        public void TestSupprimerTitre()
        {
            #region Arrange
            // Création de 3 Titres et d'un Manager les contenant
            var titre1 = new Titre("001", "blabla", "Pamplemousse", "www.youtube.com", "Jacques", "~/test", "biographie");
            var titre2 = new Titre("002", "blabla2", "Pamplemousse2", "www.youtube.com2", "Jacques2", "~/test2", "biographie2");
            var titre3 = new Titre("003", "blabla3", "Pamplemousse3", "www.youtube.com3", "Jacques3", "~/test3", "biographie3");
            var manager = new Manager(titre1, titre2, titre3);

            // Création d'une CollectionTitre contenant titre1 titre2 et titre3
            var collection1 = new CollectionTitre(titre1, titre2, titre3);

            // Ajout de la collection au Manager
            manager.AjouterCollection(collection1);

            // Initialisation des valeurs attendues et courrantes
            var currentNumberOfTile = 3;
            var expectedNumberOfTile = 2;
            var currentNumberOfTitleInCollection = 3;
            var expectedNumberOfTitleInCollection = 2;

            // Ajout des Titres titre1 titre2 et titre3 à la collection Favoris de ListeCollection du Manager
            manager.AjouterTitreToCollection(manager.GetListeFavoris(), titre1);
            manager.AjouterTitreToCollection(manager.GetListeFavoris(), titre2);
            manager.AjouterTitreToCollection(manager.GetListeFavoris(), titre3);

            // Fait des copie des deux liste de ListeCollection et de ListeTitre du Manager
            var currentListeFavoris = manager.GetListeFavoris().ListeTitre.ToList();
            var currentListeCollection = manager.ListeCollection.ElementAt(1).ListeTitre.ToList();
            var currentListeTitre = manager.ListeTitre.ToList();
            #endregion

            #region Act
            // Supprime le Titre titre2
            manager.SupprimerTitre(titre2);
            #endregion

            #region Assert
            //Before
            // Vérifie que le nombre de Titre dans les collections sont les même que celui attendu
            Assert.Equal(currentNumberOfTitleInCollection, currentListeFavoris.Count);
            Assert.Equal(currentNumberOfTitleInCollection, currentListeCollection.Count);
            Assert.Equal(currentNumberOfTile, currentListeTitre.Count);

            // Vérifie que les collections contenait titre1, titre2 et titre3
            Assert.Contains(titre1, currentListeFavoris);
            Assert.Contains(titre1, currentListeCollection);
            Assert.Contains(titre1, currentListeTitre);

            Assert.Contains(titre2, currentListeFavoris);
            Assert.Contains(titre2, currentListeCollection);
            Assert.Contains(titre2, currentListeTitre);

            Assert.Contains(titre3, currentListeFavoris);
            Assert.Contains(titre3, currentListeCollection);
            Assert.Contains(titre3, currentListeTitre);

            //After
            // Vérifie que le nombre de titre dans les collections sont bien ceux attendu
            Assert.Equal(expectedNumberOfTitleInCollection, manager.GetListeFavoris().ListeTitre.Count);
            Assert.Equal(expectedNumberOfTitleInCollection, manager.ListeCollection.ElementAt(1).ListeTitre.Count);
            Assert.Equal(expectedNumberOfTile, manager.ListeTitre.Count);

            // Vérifie que les collections contiennent titre1 et titre3
            Assert.Contains(titre1, manager.GetListeFavoris().ListeTitre);
            Assert.Contains(titre1, manager.ListeCollection.ElementAt(1).ListeTitre);
            Assert.Contains(titre1, manager.ListeTitre);

            Assert.Contains(titre3, manager.GetListeFavoris().ListeTitre);
            Assert.Contains(titre3, manager.ListeCollection.ElementAt(1).ListeTitre);
            Assert.Contains(titre3, manager.ListeTitre);

            // Vérifie que les collections ne contiennent pas titre2
            Assert.DoesNotContain(titre2, manager.GetListeFavoris().ListeTitre);
            Assert.DoesNotContain(titre2, manager.ListeCollection.ElementAt(1).ListeTitre);
            Assert.DoesNotContain(titre2, manager.ListeTitre);

            // Verify we can't remove a title who not exists
            Assert.False(manager.SupprimerTitre(titre2));
            #endregion
        }
        /// <summary>
        /// Test de la méthode ModifierTitre de Manager
        /// </summary>
        [Fact]
        public void TestModifierTitre()
        {
            #region Arrange
            // Création de 4 Titres, d'un Manager contenant les Titres titre1 et titre2 puis d'une collection vide
            var titre1 = new Titre("001", "blabla", "Pamplemousse", "www.youtube.com", "Jacques", "~/test", "biographie");
            var titre2 = new Titre("002", "blabla2", "Pamplemousse2", "www.youtube.com2", "Jacques2", "~/test2", "biographie2");
            var titre3 = new Titre("003", "blabla3", "Pamplemousse3", "www.youtube.com3", "Jacques3", "~/test3", "biographie3");
            var titreInconnu = new Titre("Inconnu", "Inconnu", "Inconnu", "Inconnu", "Inconnu", "Inconnu", "Inconnu");
            var manager = new Manager(titre1, titre2);
            var collection1 = new CollectionTitre();

            // Ajout de la collection au Manager ainsi que les Titres titre1 et titre2 à cette collection
            manager.AjouterCollection(collection1);
            manager.AjouterTitreToCollection(collection1,titre1);
            manager.AjouterTitreToCollection(collection1,titre2);

            // Fait une copie de ListeTitre et de la collection dans ListeCollection du Manager
            var currentListeTitre = manager.ListeTitre.ToList();
            var currentListeCollection = manager.ListeCollection.ElementAt(1).ListeTitre.ToList();

            // Initialise les valeurs attendues
            var NumberOfTitle = 2;
            var NewTitre = new Titre("003","blabla3", "Pamplemousse3", "www.youtube.com3", "Jacques3", "~/test3", "biographie3");
            var TitreRemplacé = new Titre("001","blabla", "Pamplemousse", "www.youtube.com", "Jacques", "~/test", "biographie");

            //Before Assert
            // Vérifie que le nombre de Titre dans ListeTitre du Manager est le même que celui attendu
            Assert.Equal(NumberOfTitle, currentListeTitre.Count);
            // Vérifie que ListeTitre du Manager contient titre2 TitreRemplacé(titre1) et ne contient pas NewTitre(titre3)
            Assert.Contains(titre2, currentListeTitre);
            Assert.Contains(TitreRemplacé, currentListeTitre);
            Assert.DoesNotContain(NewTitre, currentListeTitre);

            //Vérifie que le nombre de Titre dans la collection du Manager est le même que celui attendu
            Assert.Equal(NumberOfTitle, currentListeCollection.Count);
            // Vérifie que la collection du Manager contient titre2 TitreRemplacé(titre1) et ne contient pas NewTitre(titre3)
            Assert.Contains(titre2, currentListeCollection);
            Assert.Contains(TitreRemplacé, currentListeCollection);
            Assert.DoesNotContain(NewTitre, currentListeCollection);
            #endregion

            #region Act
            // Modifie titre1 avec titre3
            manager.ModifierTitre(titre1,titre3);
            #endregion

            #region Assert
            //After
            // Vérifie que le nombre de titre dans ListeTitre du Manager est le même que celui attendu
            Assert.Equal(NumberOfTitle, manager.ListeTitre.Count);
            // Vérifie que ListeTitre du Manager contient titre2 NewTitre(titre3) mais ne contient pas TitreRemplacé(titre1)
            Assert.Contains(titre2, manager.ListeTitre);
            Assert.Contains(NewTitre, manager.ListeTitre);
            Assert.DoesNotContain(TitreRemplacé, manager.ListeTitre);

            // Vérifie que le nombre de titre dans la collection du Manager est le même que celui attendu
            Assert.Equal(NumberOfTitle, manager.ListeCollection.ElementAt(1).ListeTitre.Count);
            // Vérifie que la collection du Manager contient titre2 NewTitre(titre3) mais ne contient pas TitreRemplacé(titre1)
            Assert.Contains(titre2, manager.ListeCollection.ElementAt(1).ListeTitre);
            Assert.Contains(NewTitre, manager.ListeCollection.ElementAt(1).ListeTitre);
            Assert.DoesNotContain(TitreRemplacé, manager.ListeCollection.ElementAt(1).ListeTitre);

            // Vérifie que l'on ne puisse pas modifier un Titre qui n'est pas dans le Manager
            Assert.False(manager.ModifierTitre(titreInconnu, titre3));

            #endregion
        }
    }
}
