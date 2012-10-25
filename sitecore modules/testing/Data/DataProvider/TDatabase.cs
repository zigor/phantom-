namespace Phantom.TestKit.Data
{
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
      this.DataProviders.Add(new MemoryDataProvider(string.Empty) { Name = "name" });
      this.Caches.DataCache.Enabled = false;
      this.Caches.ItemCache.Enabled = false;
      this.Caches.PathCache.Enabled = false;
      this.Caches.StandardValuesCache.Enabled = false;
    }

    #endregion
  }
}