// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TDatabase.cs" company="">
//   
// </copyright>
// <summary>
//   The database.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Phantom.TestKit.Data
{
  using System;

  using Phantom.TestKit.Data.Memory;

  using Sitecore.Data;

  /// <summary>
  /// The database.
  /// </summary>
  public class TDatabase : Database
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TDatabase"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    public TDatabase(string name)
      : base(name)
    {
      this.DataProviders.Add(new MemoryDataProvider(string.Empty) { Name = name });
      this.Caches.DataCache.Enabled = false;
      this.Caches.ItemCache.Enabled = false;
      this.Caches.PathCache.Enabled = false;
      this.Caches.StandardValuesCache.Enabled = false;

      ResetTemplates += (o, e) => ResetTemplateEngine(this);

      this.DataManager.DataEngine.SavedItem += (o, e) => OnResetTemplates();
    }

    #endregion

    #region Public Events

    /// <summary>
    /// The reset templates event
    /// </summary>
    public static event EventHandler ResetTemplates;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Called when the reset has templates.
    /// </summary>
    public static void OnResetTemplates()
    {
      EventHandler handler = ResetTemplates;
      if (handler != null)
      {
        handler(null, EventArgs.Empty);
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Resets the templates.
    /// </summary>
    /// <param name="database">
    /// The database.
    /// </param>
    private static void ResetTemplateEngine(Database database)
    {
      database.Engines.TemplateEngine.Reset();
    }

    #endregion
  }
}