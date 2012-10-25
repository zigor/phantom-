namespace Phantom.TestKit.Data.Memory
{
  using System;

  using Sitecore.Configuration;

  /// <summary>
  /// The memory client data store.
  /// </summary>
  public class MemoryClientDataStore : ClientDataStore
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryClientDataStore"/> class.
    /// </summary>
    public MemoryClientDataStore()
      : base(new TimeSpan(10, 0, 0, 0))
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// The compact data.
    /// </summary>
    protected override void CompactData()
    {
    }

    /// <summary>
    /// The load data.
    /// </summary>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    protected override string LoadData(string key)
    {
      return string.Empty;
    }

    /// <summary>
    /// The remove data.
    /// </summary>
    /// <param name="key">
    /// The key.
    /// </param>
    protected override void RemoveData(string key)
    {
    }

    /// <summary>
    /// The save data.
    /// </summary>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    protected override void SaveData(string key, string data)
    {
    }

    #endregion
  }
}