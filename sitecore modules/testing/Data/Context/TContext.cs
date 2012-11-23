namespace Sitecore.TestKit.Data
{
  using System;

  using Sitecore;
  using Sitecore.Collections;
  using Sitecore.Data;
  using Sitecore.Sites;
  using Sitecore.Web;

  /// <summary>
  /// The t context.
  /// </summary>
  public class TContext : IDisposable
  {
    #region Fields

    /// <summary>
    /// The previous database.
    /// </summary>
    private readonly Database previousDatabase;

    /// <summary>
    /// The previouse site
    /// </summary>
    private readonly SiteContext previouseSite;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TContext"/> class.
    /// </summary>
    public TContext()
    {
      this.previousDatabase = Context.Database;
      this.previouseSite = Context.Site;
      Context.Site = new SiteContext(new SiteInfo(new StringDictionary()));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TContext"/> class.
    /// </summary>
    /// <param name="tree">
    /// The tree.
    /// </param>
    public TContext(TTree tree)
      : this()
    {
      this.Tree = tree;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets tree
    /// </summary>
    public TTree Tree { get; private set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The dispose.
    /// </summary>
    public void Dispose()
    {
      if (this.Tree != null)
      {
        this.Tree.Dispose();
      }
      Context.Site = this.previouseSite;
      Context.Database = this.previousDatabase;
    }

    #endregion
  }
}