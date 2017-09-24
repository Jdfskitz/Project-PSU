using UnityEngine;

namespace MDF_EDITOR
{
    /// <summary>
    /// This is a class that handles number related extension methods that provide utility.
    /// </summary>
    public static class Number_Handler
    {
        /// <summary>
        /// This will enforce a max value for an int value.
        /// </summary>
        /// <param name="value">The passed in integer</param>
        /// <param name="MaxValue">The capped integer value</param>
        /// <returns></returns>
        public static int MaxValue(this int value, int MaxValue)
        {
            value = Mathf.Clamp(value, 0, MaxValue);
            return value;
        }
    }
}