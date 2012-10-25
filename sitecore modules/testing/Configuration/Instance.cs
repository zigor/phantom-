namespace Phantom.TestKit.Configuration
{
  using System;
  using System.Collections.Specialized;
  using System.IO;
  using System.Linq;
  using System.Xml;

  using Phantom.TestKit.Security.AccessControl;

  using Moq;

  using Sitecore.Configuration;
  using Sitecore.Data;
  using Sitecore.Data.Managers;
  using Sitecore.Data.Proxies;
  using Sitecore.Links;
  using Sitecore.Reflection;
  using Sitecore.Security.AccessControl;
  using Sitecore.Security.Accounts;
  using Sitecore.Security.Authentication;
  using Sitecore.Security.Domains;
  using Sitecore.SecurityModel;

  /// <summary>
  /// Defines the instance class.
  /// </summary>
  public class Instance
  {
    #region Public Methods and Operators

    /// <summary>
    /// Licenses the relative path.
    /// </summary>
    public static void LicenseRelativePath()
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
      while (i >= 0 && !File.Exists(path + license));

      State.HttpRuntime.AppDomainAppPath = path;
    }

    /// <summary>
    /// Mocks the access right provider.
    /// </summary>
    public static void MockAccessRightProvider()
    {
      var accessRightProvider = new Mock<ConfigAccessRightProvider> { CallBase = true };
      accessRightProvider.Setup(p => p.GetAccessRight(It.IsAny<string>())).Returns<string>(n => new AccessRight(n));
      ProviderHelper<AccessRightProvider, AccessRightProviderCollection>.DefaultProvider = accessRightProvider.Object;
      accessRightProvider.Object.Initialize("mock", new NameValueCollection());
    }

    /// <summary>
    /// Mocks the authentication provider.
    /// </summary>    
    public static void MockAuthenticationProvider()
    {
      var authenticationProvider = new Mock<FormsAuthenticationProvider> { CallBase = true };
      authenticationProvider.Setup(p => p.GetActiveUser()).Returns(User.FromName("Anonymous", false));
      ProviderHelper<AuthenticationProvider, AuthenticationProviderCollection>.DefaultProvider =
        authenticationProvider.Object;
      authenticationProvider.Object.Initialize("mock", new NameValueCollection());
    }

    /// <summary>
    /// Mocks the authorization provider.
    /// </summary>  
    public static void MockAuthorizationProvider()
    {
      var authorizationProvider = new MemoryAuthorizationProvider();
      ProviderHelper<AuthorizationProvider, AuthorizationProviderCollection>.DefaultProvider = authorizationProvider;
      authorizationProvider.Initialize("mock", new NameValueCollection());
    }

    /// <summary>
    /// Mocks the configuration.
    /// </summary>
    public static void MockConfiguration()
    {
      var document = new XmlDocument();
      document.LoadXml(Settings.SitecoreConfiguration);
      ReflectionUtil.SetStaticField(typeof(Factory), "configuration", document);
    }

    /// <summary>
    /// Mocks the domain provider.
    /// </summary>
    public static void MockDomainProvider()
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
    public static void MockItemProvider()
    {
      var itemProvider = new ItemProvider();
      ProviderHelper<ItemProvider, ItemProviderCollection>.DefaultProvider = itemProvider;
      itemProvider.Initialize("mock", new NameValueCollection());
    }

    /// <summary>
    /// Mocks the link provider.
    /// </summary>
    public static void MockLinkProvider()
    {
      var standardValueProvider = new LinkProvider();
      ProviderHelper<LinkProvider, LinkProviderCollection>.DefaultProvider = standardValueProvider;
      standardValueProvider.Initialize("mock", new NameValueCollection());
    }

    /// <summary>
    /// Mocks the standard values provider.
    /// </summary>
    public static void MockStandardValuesProvider()
    {
      var standardValueProvider = new StandardValuesProvider();
      ProviderHelper<StandardValuesProvider, StandardValuesProviderCollection>.DefaultProvider = standardValueProvider;
      standardValueProvider.Initialize("mock", new NameValueCollection());
    }

    /// <summary>
    /// Prepares this instance.
    /// </summary>
    public static void Prepare()
    {
      LicenseRelativePath();
      MockAccessRightProvider();
      MockAuthenticationProvider();
      MockAuthorizationProvider();
      MockDomainProvider();
      MockItemProvider();
      MockConfiguration();
      MockStandardValuesProvider();
      MockLinkProvider();
    }

    #endregion
  }
}