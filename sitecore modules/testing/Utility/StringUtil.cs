namespace Phantom.TestKit.Utility
{
  /// <summary>
  /// String utilities
  /// </summary>
  public class StringUtil
  {
    #region Public Methods and Operators

    /// <summary>
    /// Equals strings with ignore case.
    /// </summary>
    /// <param name="a">
    /// a
    /// </param>
    /// <param name="b">
    /// b
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool EqualsIgnoreCase(string a, string b)
    {
      return string.Compare(a, b, true) == 0;
    }

    #endregion
  }
}