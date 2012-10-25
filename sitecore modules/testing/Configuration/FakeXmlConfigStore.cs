namespace Phantom.TestKit.Configuration
{
  using System.Xml;

  using Sitecore.Configuration;

  /// <summary>
  /// The fake xml config store.
  /// </summary>
  public class FakeXmlConfigStore : XmlConfigStore
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FakeXmlConfigStore"/> class.
    /// </summary>
    /// <param name="rootElementName">
    /// The root element name.
    /// </param>
    public FakeXmlConfigStore(string rootElementName)
      : base(rootElementName)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The load from xml.
    /// </summary>
    /// <param name="root">
    /// The root.
    /// </param>
    /// <returns>
    /// The <see cref="XmlConfigStore"/>.
    /// </returns>
    public static XmlConfigStore LoadFromXml(string root)
    {
      var doc = new XmlDocument();
      doc.LoadXml(Factory.GetConfigNode(root).OuterXml);
      return LoadFromXml(doc);
    }

    /// <summary>
    /// The commit changes.
    /// </summary>
    public override void CommitChanges()
    {
    }

    /// <summary>
    /// The save to file.
    /// </summary>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    public override void SaveToFile(string filePath)
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// The reload.
    /// </summary>
    protected override void Reload()
    {
    }

    #endregion
  }
}