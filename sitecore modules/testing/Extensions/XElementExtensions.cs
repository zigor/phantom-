namespace MobyDick.TestKit.Extensions
{
  using System.Xml.Linq;

  using Sitecore.Diagnostics;

  /// <summary>
  /// The x element extensions.
  /// </summary>
  public static class XElementExtensions
  {
    #region Public Methods and Operators

    /// <summary>
    /// The attribute value.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="attributeName">
    /// The attribute name.
    /// </param>
    /// <param name="defaultValue">
    /// The default value.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string AttributeValue(this XElement element, string attributeName, string defaultValue)
    {
      Assert.ArgumentNotNull(element, "element");
      XAttribute attribute = element.Attribute(attributeName);
      if (attribute != null)
      {
        return attribute.Value;
      }

      return defaultValue;
    }

    /// <summary>
    /// The attribute value.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="attributeName">
    /// The attribute name.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string AttributeValue(this XElement element, string attributeName)
    {
      Assert.ArgumentNotNull(element, "element");
      return element.AttributeValue(attributeName, string.Empty);
    }

    #endregion
  }
}