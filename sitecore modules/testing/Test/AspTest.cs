﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AspTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ASP test class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.TestKit.Test
{
  using System.Web;
  using System.Web.SessionState;
  using System.Web.UI;

  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;
  using Sitecore.Globalization;
  using Sitecore.TestKit.Configuration;
  using Sitecore.TestKit.Web;
  using SitecoreSettings = Sitecore.Configuration.Settings;

  /// <summary>
  /// Defines the ASP test class.
  /// </summary>
  public class AspTest
  {
    #region Fields

    /// <summary>
    /// The default http context
    /// </summary>
    private HttpContext defaultHttpContext;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the state of the session.
    /// </summary>
    /// <value>
    /// The state of the session.
    /// </value>
    public IHttpSessionState SessionState { get; private set; }

    #endregion


    #region Public Methods and Operators

    /// <summary>
    /// Sets the query string.
    /// </summary>
    /// <param name="queryString">
    /// The query string.
    /// </param>
    public void SetQueryString(string queryString)
    {
      this.InitContext("/", queryString ?? string.Empty);
    }

    /// <summary>
    /// Tests the cleanup.
    /// </summary>
    [TestCleanup]
    public virtual void TestCleanup()
    {
      HttpContext.Current = this.defaultHttpContext;

      var configurationIsSet = SitecoreSettings.ConfigurationIsSet;

      Instance.Prepare();
    }

    /// <summary>
    /// Tests the initialize.
    /// </summary>
    [TestInitialize]
    public virtual void TestInitialize()
    {
      var configurationIsSet = SitecoreSettings.ConfigurationIsSet;

      Instance.Prepare();

      var initDictionary = Translate.Text("test");
      this.defaultHttpContext = HttpContext.Current;
      this.InitContext("/", "entityname=crmentity");

      HttpContext.Current.Request.Browser = new HttpBrowserCapabilities();
      HttpContext.Current.Request.Browser.Capabilities = new StateBag { { "tables", "true" }, { "browser", "Unit test" }  };
    }

    #endregion

    #region Methods

    /// <summary>
    /// Inits the context.
    /// </summary>
    /// <param name="page">
    /// The page.
    /// </param>
    /// <param name="queryString">
    /// The query string.
    /// </param>
    private void InitContext(string page, string queryString)
    {
      var httpWorkerRequest = new FakeHttpWorkerRequest(page, queryString);
      HttpContext.Current = new HttpContext(httpWorkerRequest);
      var sessionMock = new Mock<MemoryHttpSessionState> { CallBase = true };
      this.SessionState = sessionMock.Object;
      SessionStateUtility.AddHttpSessionStateToContext(HttpContext.Current, this.SessionState);
    }

    #endregion
  }
}