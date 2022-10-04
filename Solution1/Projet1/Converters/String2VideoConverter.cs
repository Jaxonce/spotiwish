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
    /// Classe String2VideoCOnverter qui implémente IValueConverter
    /// </summary>
    class String2VideoConverter : IValueConverter
    {
        /// <summary>
        /// String contenant le chemin jusqu'au dossier Videos Initialisé dans le constructeur, getter et setter public
        /// </summary>
        public static string VideosPath { get; set; }

        /// <summary>
        /// Constructeur d'un String2VideoConverter qui initialise la valeur VideosPath en combinant le dossier courant au chemin pour aller dans le dossier Videos
        /// </summary>
        static String2VideoConverter() // Constructeur d'un String2VideoConverteur
        {
            VideosPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\Videos\\"); // Initialise la variable videosPath au dossier Videos
        }

        /// <summary>
        /// Convertie le string de la video en Chemin vers la vidéo (Exemple video.mp4 -> C:\\Users\\SpotiWish\\bin\\Videos\\video.mp4)
        /// </summary>
        /// <param name="value">string de la video</param>
        /// <param name="targetType">Le type de l'objet a convertir, ici Video</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) // Convertisseur d'un nom de video en chemin vers cette video
        {
            string videoName = value as string; // Variable contenant le nom de la video
            if (string.IsNullOrWhiteSpace(videoName)) return null; // Si le nom de la video est vide

            string videoPath = Path.Combine(VideosPath, videoName); // Création de chemin vers la video avec le dossier et le nom de la video

            return new Uri(videoPath, UriKind.RelativeOrAbsolute); // Retourne le chemin vers la video
        }

        /// <summary>
        /// Methode non implémenté, on ne s'en sert pas
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
