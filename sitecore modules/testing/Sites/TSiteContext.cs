// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TSiteContext.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the T site context class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.TestKit.Sites
{
  using Sitecore.Collections;
  using Sitecore.Sites;
  using Sitecore.Web;

  /// <summary>
  /// Defines the T site context class.
  /// </summary>
  public class TSiteContext : SiteContext
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="TSiteContext"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    public TSiteContext(string name)
      : base(new SiteInfo(new StringDictionary { { "name", name } }))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TSiteContext"/> class.
    /// </summary>
    /// <param name="properties">The properties.</param>
    public TSiteContext(StringDictionary properties)
      : base(new SiteInfo(properties))
    {
    }
  }
}