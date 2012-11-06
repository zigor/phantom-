// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringArrayExtension.cs" company="">
//   
// </copyright>
// <summary>
//   The string array extension.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.TestKit.Security.CRM.Data
{
  using System.Linq;

  /// <summary>
  /// The string array extension.
  /// </summary>
  public static class StringArrayExtension
  {
    #region Public Methods and Operators

    /// <summary>
    /// The contains.
    /// </summary>
    /// <param name="array">
    /// The array.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool Contains(this string[] array, string value)
    {
      return array.FirstOrDefault(a => a == value) != null;
    }

    /// <summary>
    /// The get index of.
    /// </summary>
    /// <param name="array">
    /// The array.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int GetIndexOf(this string[] array, string value)
    {
      for (int i = 0; i < array.Length; ++i)
      {
        if (array[i] == value)
        {
          return i;
        }
      }

      return -1;
    }

    #endregion
  }
}