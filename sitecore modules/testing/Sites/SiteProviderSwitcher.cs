namespace Phantom.TestKit
{
  using Sitecore.Sites;

  /// <summary>
  /// The site provider switcher.
  /// </summary>
  public class SiteProviderSwitcher : SiteProvider
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get site.
    /// </summary>
    /// <param name="siteName">
    /// The site name.
    /// </param>
    /// <returns>
    /// The <see cref="Site"/>.
    /// </returns>
    public override Site GetSite(string siteName)
    {
      foreach (SiteProvider provider in SiteManager.Providers)
      {
        if (provider != this)
        {
          Site site = provider.GetSite(siteName);
          if (site != null)
          {
            return site;
          }
        }
      }

      return null;
    }

    /// <summary>
    /// The get sites.
    /// </summary>
    /// <returns>
    /// The <see cref="SiteCollection"/>.
    /// </returns>
    public override SiteCollection GetSites()
    {
      var list = new SiteCollection();
      foreach (SiteProvider provider in SiteManager.Providers)
      {
        if (provider != this)
        {
          list.AddRange(provider.GetSites() ?? new SiteCollection());
        }
      }

      return list;
    }

    #endregion
  }
}