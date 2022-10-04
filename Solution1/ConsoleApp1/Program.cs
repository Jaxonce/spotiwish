using System;
using System.Diagnostics;
using Modele;
using StubLib;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test classe Video
            Console.WriteLine("Test de la classe Video");

            // Création d'une video avec un string en paramètre
            Video v1 = new Video("https://youtube.com/");
            Console.WriteLine(v1);

            // Création d'une video sans paramètres
            Video v2 = new Video();
            Console.WriteLine(v2);

            Console.WriteLine("-------------------");

            // Test classe Artiste
            Console.WriteLine("Test de la classe Artiste");

            // Création d'un Artiste avec un string en paramètre
            Artiste a1 = new Artiste("Colonel");
            Console.WriteLine(a1);

            // Création d'un Artiste sans paramètres
            Artiste a2 = new Artiste();
            Console.WriteLine(a2);

            // Set du Nom de l'Artiste a2 à "Jean"
            a2.Nom = "Jean";
            Console.WriteLine(a2);

            Console.WriteLine("-------------------");

            // Test classe Image
            Console.WriteLine("Test de la classe Image");

            // Création d'une image avec un string en paramètre
            Image i1 = new Image("~/Spotiwish/Colonel.jpg");
            Console.WriteLine(i1);

            // Création d'une Image sans paramètres
            Image i2 = new Image();
            Console.WriteLine(i2);

            Console.WriteLine("-------------------");

            // Test de la classe Titre
            Console.WriteLine("Test de la classe Titre");

            // Création d'un Titre avec des strings en paramètres
            Titre t1 = new Titre("001","Aurelie", "Test", "https://youtube.com/", "Colonel Reyel", "~/chemin", "sfudshyvsilfvhdfvlihfvqlisuvhqfvuiqshvqlsiu");
            Console.WriteLine(t1);

            // Création d'un Titre avec des strings, une Vidéo, une Image et un Artiste
            Titre t2 = new Titre("002","NomTitre", "NomAlb", v1, a1, i1, "suvhsviousfvhqsqoivgviovgzoiyfvgdiovgvisovgaefvo");
            Console.WriteLine(t2);

            Console.WriteLine("-------------------");

            // Test de la classe Manager
            Console.WriteLine("Test de la classe Manager");

            // Création d'un manager avec les Titres t1 et t2
            Manager m1 = new Manager(t1, t2);
            Console.WriteLine(m1);

            // Création d'un Titre t3 que l'on va ajouter
            Titre t3 = new Titre("003","Aurelfdasfasdfie", "Tfadsfasest", "https:/fads/youtube.com/", "Colofadsfanel Reyel", "~/test", "La biographie de dhzfzufhvuidhvfsiduvhsfviluf");

            // Ajout du titre t3 au Manager m1
            m1.AjouterTitre(t3);
            Console.WriteLine(m1);

            // Ajout du titre t3 au Manager m1 (Expected : Message d'erreur "Le titre existe deja")
            m1.AjouterTitre(t3);

            // Supression du Titre t3 au Manager m1
            m1.SupprimerTitre(t3);
            Console.WriteLine(m1);

            // Supression du Titre t3 au Manager m1 (Expected : Message d'erreur "Le titre n'existe pas")
            m1.SupprimerTitre(t3);


            // Modification du Titre t2 par le Titre t3 au Manager m1
            m1.ModifierTitre(t2, t3);
            Console.WriteLine(m1);

            Console.WriteLine("-------------------");

            // Test de la classe CollectionTitre
            Console.WriteLine("Test de la classe CollectionTitre");

            // Création d'une CollectionTitre collection1 contenant les Titres t1, t2 et t3
            CollectionTitre collection1 = new CollectionTitre(t1, t2, t3);
            Console.WriteLine(collection1);

            // Création du Titre t4 pour ensuite l'ajouter à la CollectionTitre collection1
            Titre t4 = new Titre("004","fyufgrgf", "azertyuiop", "https:/tube.com/", "data", "~/test2", "sfmuohiulvhzfpiuvfhuvoifvhudfoivhsudfoivsd");

            // Ajout du Titre t4 à la CollectionTitre collection1
            collection1.AjouterTitreDansCollection(t4);
            Console.WriteLine(collection1);

            Console.WriteLine("-------------------");
            // Test des Favoris
            Console.WriteLine("Test des Favoris");

            // Création d'un Favoris fav contenant t1 et t3
            Favoris fav = new Favoris(t1, t3);
            Console.WriteLine(fav);

            Console.WriteLine("-------------------");
            // Test de la Persistance Stub
            Console.WriteLine("Test Persistance Manager Stub");

            // Création d'un Manager manager à l'aide d'un Stub
            var manager = new Manager(new Stub());
            Console.WriteLine(manager);

            Console.WriteLine("-------------------");

            // Test Fonction AjouterTitre de Manager avec un Manager Stub
            Console.WriteLine("Test Manager Ajouter Titre (Stub)");

            // Ajout du Titre t3 au Manager manager
            manager.AjouterTitre(t3);
            Console.WriteLine(manager);
        }
    }
}
