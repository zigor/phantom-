// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Instance.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the instance class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Phantom.TestKit.Configuration
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

  using Phantom.TestKit.Security.AccessControl;

  using Sitecore;
  using Sitecore.Caching;
  using Sitecore.Common;
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
  using Sitecore.StringExtensions;

  /// <summary>
  /// Defines the instance class.
  /// </summary>
  public class Instance
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes the <see cref="Instance"/> class.
    /// </summary>
    static Instance()
    {
      DefaultInitializationRequired = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Instance"/> class.
    /// </summary>
    public Instance()
    {
      this.Databases = new List<string>();
      this.Pipelines = new Dictionary<string, List<string>>();
      DefaultInitializationRequired = false;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets Databases
    /// </summary>
    protected List<string> Databases { get; private set; }

    /// <summary>
    /// Gets Pipelines
    /// </summary>
    protected Dictionary<string, List<string>> Pipelines { get; private set; }

    /// <summary>
    /// Gets a value indicating whether [default initialization required].
    /// </summary>
    /// <value>
    /// <c>true</c> if [default initialization required]; otherwise, <c>false</c>.
    /// </value>
    internal static bool DefaultInitializationRequired { get; private set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Adds the database.
    /// </summary>
    /// <param name="databaseName">
    /// Name of the database.
    /// </param>
    public void AddDatabase(string databaseName)
    {
      Assert.ArgumentNotNullOrEmpty(databaseName, "databaseName");

      var database = new XElement("database",
          new XAttribute("id", databaseName),
          new XAttribute("singleInstance", "true"),
          new XAttribute("type", " Phantom.TestKit.Data.TDatabase, Phantom.TestKit"),
          new XElement("param", databaseName)).ToString();

      if (!this.Databases.Contains(database))
      {
        this.Databases.Add(database);
      }
    }

    /// <summary>
    /// Adds the database.
    /// </summary>
    /// <param name="pipelineName">Name of the pipeline.</param>
    /// <param name="assemblyAndTypeName">Name of the assembly and type.</param>
    /// <param name="methodName">Name of the method.</param>
    public void AddPipeline(string pipelineName, string assemblyAndTypeName, string methodName)
    {
      Assert.ArgumentNotNullOrEmpty(pipelineName, "pipelineName");

      List<string> processors;

      if (!this.Pipelines.ContainsKey(pipelineName))
      {
        processors = new List<string>();
        this.Pipelines.Add(pipelineName, processors);
      }
      else
      {
        processors = this.Pipelines[pipelineName];
      }

      if (!string.IsNullOrEmpty(assemblyAndTypeName))
      {
        string processor = string.Format("<processor type=\"{0}\" method=\"{1}\" />", 
                                         assemblyAndTypeName, string.IsNullOrEmpty(methodName) ? "Process" : methodName);

        if (!processors.Contains(processor))
        {
          processors.Add(processor);
        }
      }
    }

    /// <summary>
    /// Prepares this instance.
    /// </summary>
    public void Prepare()
    {
      Factory.Reset();

      this.DisableCaching();
      this.LicenseRelativePath();
      this.MockConfiguration();
      this.MockAccessRightProvider();
      this.MockAuthenticationProvider();
      this.MockAuthorizationProvider();
      this.MockDomainProvider();
      this.MockItemProvider();
      this.MockStandardValuesProvider();
      this.MockLinkProvider();
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
      string license = Settings.LicenseFilePath;
      string basePath = AppDomain.CurrentDomain.BaseDirectory;

      int i = 5;
      string path;
      do
      {
        path = basePath + string.Join(string.Empty, Enumerable.Repeat("\\..", i).ToArray());
        --i;
      }
      while (i >= 0 && (!Directory.Exists(path) || !File.Exists(path + license)));

      State.HttpRuntime.AppDomainAppPath = path;
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
      document.LoadXml(Settings.SitecoreConfiguration);
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
      
      foreach (var pair in this.Pipelines)
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

      this.Databases.ForEach(d => databases.InnerXml += d);
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

      DomainSwitcher.Enter(new Domain("sitecore"));
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