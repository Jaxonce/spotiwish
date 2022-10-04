using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Projet1.Converters
{
    /// <summary>
    /// Classe String2ImageConverter qui implémente IValueConverter
    /// </summary>
    class String2ImageConverter : IValueConverter
    {
        /// <summary>
        /// String contenant le chemin jusqu'au dossier Images, getter et setter public
        /// </summary>
        public static string ImagesPath { get; set; }

        /// <summary>
        /// Constructeur d'un String2ImageConverter qui initialise la valeur ImagesPath en combinant le dossier courant au chemin pour aller dans le dossier Images
        /// </summary>
        static String2ImageConverter()
        {
            ImagesPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\Images\\"); // Initialise la variable ImagesPath au dossier Images
        }

        /// <summary>
        /// Convertie le string de l'image en Chemin vers l'image
        /// </summary>
        /// <param name="value">String de l'image</param>
        /// <param name="targetType">le type de l'objet a convertir, ici Image</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) // Convertisseur d'un nom d'image en chemin vers cette image
        {
            string imageName= value as string; // Variable contenant le nom de l'Image
            if(string.IsNullOrWhiteSpace(imageName)) return null; // Si le nom de l'image est vide

            string imagePath = Path.Combine(ImagesPath, imageName); // Création de chemin vers l'image avec le dossier et le nom de l'image

            return new Uri(imagePath, UriKind.RelativeOrAbsolute); // Retourne le chemin vers l'image
        }
        /// <summary>
        /// Methode non implementé, on ne s'en sert pas
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
