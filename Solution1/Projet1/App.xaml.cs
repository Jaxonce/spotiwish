using Modele;
using Persistance;
using StubLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Projet1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Manager permettant la gestion de l'Application, Initialisé avec la persistance (Soit Stub, soit XMLPersistance)
        /// </summary>
        public Manager LeManager { get; private set; } = new Manager(new XMLPersistance()); //Interchangable entre stub et xmlpersistance

        /// <summary>
        /// Action lors de l'appuie du bouton Modifier (Crayon du Master), Affiche un nouvelle fenêtre
        /// </summary>
        /// <param name="sender">Bouton Modifier</param>
        /// <param name="e">Evénement</param>
        private void Button_Modif(object sender, RoutedEventArgs e)
        {
            var modifierWindow = new WindowModif();
            modifierWindow.ShowDialog();
        }
    }
}
