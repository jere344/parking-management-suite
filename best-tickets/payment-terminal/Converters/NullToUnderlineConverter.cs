
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace paymentterminal.Converters
{
    public class NullToUnderlineConverter : IValueConverter
    {
        /// <summary>
        /// Convertit une valeur en une d�coration de texte (soulignement)
        /// </summary>
        /// <param name="value">La valeur � convertir</param>
        /// <param name="targetType">Le type cible de la conversion</param>
        /// <param name="parameter">Un param�tre optionnel pour la conversion</param>
        /// <param name="culture">Les informations culturelles � utiliser pour la conversion</param>
        /// <returns>TextDecorations.Underline si la valeur n'est pas nulle, sinon null</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? TextDecorations.Underline : null;
        }
        /// <summary>
        /// Convertit une d�coration de texte en sa valeur d'origine (non impl�ment�)
        /// </summary>
        /// <param name="value">La valeur � convertir</param>
        /// <param name="targetType">Le type cible de la conversion</param>
        /// <param name="parameter">Un param�tre optionnel pour la conversion</param>
        /// <param name="culture">Les informations culturelles � utiliser pour la conversion</param>
        /// <returns>Cette m�thode n'est pas impl�ment�e et l�ve une exception</returns>
        /// <exception cref="NotImplementedException">Cette m�thode n'est pas impl�ment�e</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}