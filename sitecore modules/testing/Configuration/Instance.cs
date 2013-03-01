// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Instance.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the instance class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Sitecore.Common;
using Sitecore.TestKit.Security.AccessControl;

namespace Sitecore.TestKit.Configuration
{
  using System;
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.IO;
  using System.Linq;
  using System.Threading;
  using System.Xml;
  using System.Xml.Linq;

  using Moq;

  using Sitecore.Analytics.Data.DataAccess.DataAdapters;
  using Sitecore.Analytics.Data.DataAccess.DataAdapters.Sql.SqlServer;
  using Sitecore.Data.SqlServer;
  using Sitecore.Sites;
  using Sitecore.TestKit.Security.AccessControl;

  using Sitecore;
  using Sitecore.Caching;
  using Sitecore.Configuration;
  using Sitecore.Data;
  using Sitecore.Data.Managers;
  using Sitecore.Data.Proxies;
  using Sitecore.Diagnostics;
  using Sitecore.Links;
  using Sitecore.Reflection;
  using Sitecore.Security.AccessControl;
  using Sitecore.Security.Accounts;
  using Sitecore.Security.Authentication;
  using Sitecore.Security.Domains;
  using Sitecore.SecurityModel;
  using SitecoreSettings = Sitecore.Configuration.Settings;

  /// <summary>
  /// Defines the instance class.
  /// </summary>
  public class Instance
  {
    private static string _licenseFilePath;

    /// <summary>
    /// The license full path
    /// </summary>
    private string licenseFullPath;

    #region Constructors and Destructors

    /// <summary>
    /// Initializes the <see cref="Instance"/> class.
    /// </summary>
    static Instance()
    {
      Databases = new List<string>();
      Pipelines = new Dictionary<string, List<string>>();

      LicenseFilePath = "\\data\\license.xml";
    }

    private Instance()
    {

    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the license file path.
    /// </summary>
    /// <value>
    /// The license file path.
    /// </value>
    public static string LicenseFilePath
    {
      get
      {
        return _licenseFilePath;
      }
      set
      {
        _licenseFilePath = value;
        Instance.Prepare();
      }
    }

    /// <summary>
    /// Gets Databases
    /// </summary>
    protected static List<string> Databases { get; private set; }

    /// <summary>
    /// Gets Pipelines
    /// </summary>
    protected static Dictionary<string, List<string>> Pipelines { get; private set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Adds the database.
    /// </summary>
    /// <param name="databaseName">
    /// Name of the database.
    /// </param>
    public static void AddDatabase(string databaseName)
    {
      Assert.ArgumentNotNullOrEmpty(databaseName, "databaseName");

      var database = new XElement("database",
          new XAttribute("id", databaseName),
          new XAttribute("singleInstance", "true"),
          new XAttribute("type", " Sitecore.TestKit.Data.TDatabase, Sitecore.TestKit"),
          new XElement("param", databaseName)).ToString();

      if (!Databases.Contains(database))
      {
        Databases.Add(database);
      }


      Instance.Prepare();
    }

    /// <summary>
    /// Adds the database.
    /// </summary>
    /// <param name="pipelineName">Name of the pipeline.</param>
    /// <param name="assemblyAndTypeName">Name of the assembly and type.</param>
    /// <param name="methodName">Name of the method.</param>
    public static void AddPipeline(string pipelineName, string assemblyAndTypeName, string methodName)
    {
      Assert.ArgumentNotNullOrEmpty(pipelineName, "pipelineName");

      List<string> processors;

      if (!Pipelines.ContainsKey(pipelineName))
      {
        processors = new List<string>();
        Pipelines.Add(pipelineName, processors);
      }
      else
      {
        processors = Pipelines[pipelineName];
      }

      if (!String.IsNullOrEmpty(assemblyAndTypeName))
      {
        string processor = String.Format("<processor type=\"{0}\" method=\"{1}\" />",
                                         assemblyAndTypeName, String.IsNullOrEmpty(methodName) ? "Process" : methodName);

        if (!processors.Contains(processor))
        {
          processors.Add(processor);
        }
      }


      Instance.Prepare();
    }

    /// <summary>
    /// Prepares this instance.
    /// </summary>
    public static void Prepare()
    {
      var configurationSet = SitecoreSettings.ConfigurationIsSet;

      var instance = new Instance();

      Factory.Reset();
      SitecoreSettings.Reset();

      instance.DisableCaching();
      instance.LicenseRelativePath();
      instance.MockConfiguration();
      instance.MockAccessRightProvider();
      instance.MockAuthenticationProvider();
      instance.MockAuthorizationProvider();
      instance.MockDomainProvider();
      instance.MockItemProvider();
      instance.MockStandardValuesProvider();
      instance.MockLinkProvider();
      instance.MockSiteProvider();
      instance.MockAnalyticsProvider();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Disables the caching.
    /// </summary>
    protected virtual void DisableCaching()
    {
      CacheManager.Enabled = false;
    }

    /// <summary>
    /// Licenses the relative path.
    /// </summary>
    protected virtual void LicenseRelativePath()
    {
      string license = LicenseFilePath;
      string basePath = AppDomain.CurrentDomain.BaseDirectory;

      int i = 5;
      string path;
      do
      {
        path = basePath + String.Join(String.Empty, Enumerable.Repeat("\\..", i).ToArray());
        --i;
      }
      while (i >= 0 && (!Directory.Exists(path) || !File.Exists(path + license)));

      licenseFullPath = path + license;
    }

    /// <summary>
    /// Mocks the access right provider.
    /// </summary>
    protected virtual void MockAccessRightProvider()
    {
      var accessRightProvider = new Mock<ConfigAccessRightProvider> { CallBase = true };
      accessRightProvider.Setup(p => p.GetAccessRight(It.IsAny<string>())).Returns<string>(n => new AccessRight(n));
      ProviderHelper<AccessRightProvider, AccessRightProviderCollection>.DefaultProvider = accessRightProvider.Object;
      accessRightProvider.Object.Initialize("mock", new NameValueCollection());
    }

    /// <summary>
    /// Mocks the authentication provider.
    /// </summary>    
    protected virtual void MockAuthenticationProvider()
    {
      var authenticationProvider = new Mock<MembershipAuthenticationProvider> { CallBase = true };

      var defaultUser = User.FromName("Anonymous", false);
      defaultUser.RuntimeSettings.IsVirtual = true;

      authenticationProvider.Setup(p => p.GetActiveUser()).Returns(() => Thread.CurrentPrincipal as User ?? defaultUser);
      authenticationProvider.Setup(p => p.SetActiveUser(It.IsAny<User>())).Callback<User>(u => Thread.CurrentPrincipal = u);
      ProviderHelper<AuthenticationProvider, AuthenticationProviderCollection>.DefaultProvider =
        authenticationProvider.Object;
      authenticationProvider.Object.Initialize("mock", new NameValueCollection());
      var user = AuthenticationManager.GetActiveUser();
    }

    /// <summary>
    /// Mocks the authorization provider.
    /// </summary>  
    protected virtual void MockAuthorizationProvider()
    {
      var authorizationProvider = new MemoryAuthorizationProvider();
      ProviderHelper<AuthorizationProvider, AuthorizationProviderCollection>.DefaultProvider = authorizationProvider;
      authorizationProvider.Initialize("mock", new NameValueCollection());
    }

    /// <summary>
    /// Mocks the configuration.
    /// </summary>
    protected virtual void MockConfiguration()
    {
      var document = new XmlDocument();
      var xml = string.Format("<sitecore><sites></sites><clientDataStore type=\"Sitecore.TestKit.Data.Memory.MemoryClientDataStore, Sitecore.TestKit\"/><settings><setting name=\"LicenseFile\" value=\"{0}\" /></settings></sitecore>", licenseFullPath);
      document.LoadXml(xml);
      this.AttachDatabases(document);
      this.AttachPipeline(document);

      ReflectionUtil.SetStaticField(typeof(Factory), "configuration", document);
    }

    /// <summary>
    /// Attaches the pipeline.
    /// </summary>
    /// <param name="document">The document.</param>
    private void AttachPipeline([NotNull]XmlDocument document)
    {
      Assert.ArgumentNotNull(document, "document");
      Assert.ArgumentNotNull(document.DocumentElement, "sitecore configuration is empty");

      var pipelines = document.DocumentElement.SelectSingleNode("pipelines")
                ?? document.DocumentElement.AppendChild(document.CreateElement("pipelines"));

      foreach (var pair in Pipelines)
      {
        var pipeline = pipelines.SelectSingleNode(pair.Key) ?? pipelines.AppendChild(document.CreateElement(pair.Key));
        pair.Value.ForEach(p => pipeline.InnerXml += p);
      }
    }

    /// <summary>
    /// Attaches the databases.
    /// </summary>
    /// <param name="document">The document.</param>
    private void AttachDatabases([NotNull]XmlDocument document)
    {
      Assert.ArgumentNotNull(document, "document");
      Assert.ArgumentNotNull(document.DocumentElement, "sitecore configuration is empty");

      var databases = document.DocumentElement.SelectSingleNode("databases")
                      ?? document.DocumentElement.AppendChild(document.CreateElement("databases"));

      Databases.ForEach(d => databases.InnerXml += d);
    }

    /// <summary>
    /// Mocks the domain provider.
    /// </summary>
    protected virtual void MockDomainProvider()
    {
      var domainProvider = new Mock<DomainProvider>();
      domainProvider.SetupGet(d => d.Name).Returns("mock");
      ProviderHelper<DomainProvider, DomainProviderCollection>.DefaultProvider = domainProvider.Object;
      domainProvider.Object.Initialize("mock", new NameValueCollection());

      Switcher<Domain, Domain>.Enter(new Domain("sitecore"));
    }

    /// <summary>
    /// Mocks the item provider.
    /// </summary>
    protected virtual void MockItemProvider()
    {
      var itemProvider = new ItemProvider();
      ProviderHelper<ItemProvider, ItemProviderCollection>.DefaultProvider = itemProvider;
      itemProvider.Initialize("mock", new NameValueCollection());
    }

    /// <summary>
    /// Mocks the site provider.
    /// </summary>
    protected virtual void MockSiteProvider()
    {
      var siteProvider = new Mock<SiteProvider> { CallBase = true };
      ProviderHelper<SiteProvider, SiteProviderCollection>.DefaultProvider = siteProvider.Object;
      siteProvider.Object.Initialize("mock", new NameValueCollection());
    }

    /// <summary>
    /// Mocks the analytics provider.
    /// </summary>
    protected virtual void MockAnalyticsProvider()
    {
      new ProviderHelper<DataAdapterProvider, DataAdapterProviderCollection>("dataAdapterManager");

      var provider = new Mock<SqlServerDataAdapterProvider>(new SqlServerDataApi()) { CallBase = true };
      ProviderHelper<DataAdapterProvider, DataAdapterProviderCollection>.DefaultProvider = provider.Object;
      provider.Object.Initialize("mock", new NameValueCollection());
    }

    /// <summary>
    /// Mocks the link provider.
    /// </summary>
    protected virtual void MockLinkProvider()
    {
      var standardValueProvider = new LinkProvider();
      ProviderHelper<LinkProvider, LinkProviderCollection>.DefaultProvider = standardValueProvider;
      standardValueProvider.Initialize("mock", new NameValueCollection());
    }

    /// <summary>
    /// Mocks the standard values provider.
    /// </summary>
    protected virtual void MockStandardValuesProvider()
    {
      var standardValueProvider = new StandardValuesProvider();
      ProviderHelper<StandardValuesProvider, StandardValuesProviderCollection>.DefaultProvider = standardValueProvider;
      standardValueProvider.Initialize("mock", new NameValueCollection());
    }

    #endregion
  }
}