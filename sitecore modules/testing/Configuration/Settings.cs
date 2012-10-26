namespace Phantom.TestKit.Configuration
{
  /// <summary>
  /// Defines the settings class.
  /// </summary>
  public static class Settings
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes static members of the <see cref="Settings"/> class. 
    /// </summary>
    static Settings()
    {
      LicenseFilePath = "\\data\\license.xml";
      SitecoreConfiguration = "<sitecore><clientDataStore type=\"Phantom.TestKit.Data.Memory.MemoryClientDataStore, Phantom.TestKit\"/></sitecore>";
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the license file path.
    /// </summary>
    /// <value>
    /// The license file path.
    /// </value>
    public static string LicenseFilePath { get; set; }

    /// <summary>
    /// Gets or sets the sitecore configuration.
    /// </summary>
    /// <value>
    /// The sitecore configuration.
    /// </value>
    public static string SitecoreConfiguration { get; set; }

    #endregion
  }
}