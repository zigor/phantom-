namespace Sitecore.TestKit.Utility
{
  using System;
  using System.Collections.Specialized;
  using System.Configuration.Provider;
  using System.Globalization;

  using Sitecore.StringExtensions;

  /// <summary>
  /// NameValueCollection utils 
  /// </summary>
  public class NameValueCollectionUtil
  {
    #region Public Methods and Operators

    /// <summary>
    /// Gets the int value.
    /// </summary>
    /// <param name="config">
    /// The config.
    /// </param>
    /// <param name="valueName">
    /// Name of the value.
    /// </param>
    /// <param name="defaultValue">
    /// The default value.
    /// </param>
    /// <param name="zeroAllowed">
    /// if set to <c>true</c> [zero allowed].
    /// </param>
    /// <param name="maxValueAllowed">
    /// The max value allowed.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Value must be non negative integer
    /// </exception>
    public static int GetIntValue(
      NameValueCollection config, string valueName, int defaultValue, bool zeroAllowed, int maxValueAllowed)
    {
      int num;
      string s = config[valueName];
      if (s == null)
      {
        return defaultValue;
      }

      if (!int.TryParse(s, out num))
      {
        if (zeroAllowed)
        {
          throw new ArgumentException("Value must be non negative integer", valueName);
        }

        throw new ArgumentException("Value must be positive integer", valueName);
      }

      if (zeroAllowed && (num < 0))
      {
        throw new ArgumentException("Value must be non negative integer", valueName);
      }

      if (!zeroAllowed && (num <= 0))
      {
        throw new ArgumentException("Value must be positive integer", valueName);
      }

      if ((maxValueAllowed > 0) && (num > maxValueAllowed))
      {
        throw new ArgumentException(
          "Value too big: {0}".FormatWith(maxValueAllowed.ToString(CultureInfo.InvariantCulture)), valueName);
      }

      return num;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets the boolean value.
    /// </summary>
    /// <param name="config">
    /// The config.
    /// </param>
    /// <param name="valueName">
    /// Name of the value.
    /// </param>
    /// <param name="defaultValue">
    /// if set to <c>true</c> [default value].
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    /// <exception cref="ProviderException">
    /// <c>ProviderException</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <c>ArgumentException</c>.
    /// </exception>
    internal static bool GetBooleanValue(NameValueCollection config, string valueName, bool defaultValue)
    {
      bool flag;
      string str = config[valueName];
      if (str == null)
      {
        return defaultValue;
      }

      if (!bool.TryParse(str, out flag))
      {
        throw new ArgumentException("Value must be boolean", valueName);
      }

      return flag;
    }

    #endregion
  }
}