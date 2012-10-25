// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AspTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ASP test class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Phantom.TestKit.Test
{
  using System.Web;
  using System.Web.SessionState;
  using System.Web.UI;

  using Microsoft.VisualStudio.TestTools.UnitTesting;

  using Phantom.TestKit.Web;

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
    }

    /// <summary>
    /// Tests the initialize.
    /// </summary>
    [TestInitialize]
    public virtual void TestInitialize()
    {
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
      IHttpSessionState session = null;
      if (HttpContext.Current != null)
      {
        session = SessionStateUtility.GetHttpSessionStateFromContext(HttpContext.Current);
      }

      var httpWorkerRequest = new FakeHttpWorkerRequest(page, queryString);
      HttpContext.Current = new HttpContext(httpWorkerRequest);
      SessionStateUtility.AddHttpSessionStateToContext(HttpContext.Current, session ?? new MemoryHttpSessionState());
    }

    #endregion
  }
}