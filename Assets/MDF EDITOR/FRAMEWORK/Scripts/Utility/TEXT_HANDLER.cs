/* This is the String Handler, this handles string operations to REGEX text and filter text.
 *This is designed to be used for the editor but can be used in a variety of situations you just add it to the end
 *of a string or a string based variable and you can have the operations performed anywhere as long as your are
 * using the MDF_EDITOR namespace.
 * example "This is test 67".TextOnly()  : This will return "This_is_test_"
*/

using System.Text.RegularExpressions;

namespace MDF_EDITOR
{
    /// <summary>
    /// This class provides utility extension methods for Text related handling.
    /// </summary>
    public static class TEXT_HANDLER
    {
        /// <summary>
        /// This will FORCE the size of the string to whatever you want the size to always be
        /// </summary>
        /// <param name="value">current string value</param>
        /// <param name="set_length">set length</param>
        /// <returns></returns>
        public static string ForceSize(this string value, int set_length)
        {
            return value.PadRight(set_length).Substring(0, set_length);
        }

        // This will limit the size by the amount of characters, so a max of 5 would be "abcde"
        
        ///<summary>
        /// 
        /// </summary>
        ///     
        public static string MinSize(this string s, int min_Length)
        {
            return s != null && s.Length < min_Length ? s.Substring(0, min_Length) : s;
        }

        /// <summary>
        /// This will limit the size by the amount of characters, so a max of 5 would be "abcde"
        /// </summary>
        /// <param name="s">current string value</param>
        /// <param name="maxLength">max length</param>
        /// <returns></returns>
        public static string MaxSize(this string s, int maxLength)
        {
            return s != null && s.Length > maxLength ? s.Substring(0, maxLength) : s;
        }

        /// <summary>
        /// This will remove all special characters, EXCEPT for Underscores. Uses REGEX handling.
        /// </summary>
        /// <param name="s">current string value</param>
        /// <returns></returns>
        public static string NoSpecialCharacters(this string s)
        {
            s = Regex.Replace(s, @"[^a-zA-Z0-9_ ]", "");
            s = s.Replace(" ", "_");
            return s;
        }

        /// <summary>
        /// This will force text only EXCEPT for Underscores
        /// </summary>
        /// <param name="s">current string value</param>
        /// <returns></returns>
        public static string TextOnly(this string s)
        {
            s = Regex.Replace(s, @"[^a-zA-Z_ ]", "");
            s = s.Replace(" ", "_");
            return s;
        }

        /// <summary>
        /// Makes sure variables have valid names
        /// </summary>
        /// <param name="s">current string value</param>
        /// <returns></returns>
        public static string ValidVarName(this string s)
        {
            s = Regex.Replace(s, @"^\d+", "");  //Remove starting digits
            s = Regex.Replace(s, @"[^a-zA-Z0-9_]+$", "");  //Allow letters, numbers and underscore only
            s = s.Replace(" ", "_");
            return s;
        }

        /// <summary>
        /// This will force Quotes around text. (REALLY USEFUL when you need to add quotes in a string without needing ' wrapping)
        /// </summary>
        /// <param name="s">current string value</param>
        /// <returns></returns>
        public static string AddQuotes(this string s)
        {
            s = "\"" + s + "\"";
            return s;
        }

        /// <summary>
        /// This will enforce numbers only.
        /// </summary>
        /// <param name="s">current string value</param>
        /// <returns></returns>
        public static string NumbersOnly(this string s)
        {
            s = Regex.Replace(s, @"[0-9]", "");
            return s;
        }
    }
}