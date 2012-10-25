namespace Phantom.TestKit.Data.Memory
{
  using System;

  using Sitecore.Collections;
  using Sitecore.Data.Engines;

  /// <summary>
  /// The memory history storage.
  /// </summary>
  public class MemoryHistoryStorage : HistoryStorage
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryHistoryStorage"/> class.
    /// </summary>
    /// <param name="connectionStringName">
    /// The connection string name.
    /// </param>
    public MemoryHistoryStorage(string connectionStringName)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The add entry.
    /// </summary>
    /// <param name="entry">
    /// The entry.
    /// </param>
    public override void AddEntry(HistoryEntry entry)
    {
    }

    /// <summary>
    /// The get history.
    /// </summary>
    /// <param name="from">
    /// The from.
    /// </param>
    /// <param name="to">
    /// The to.
    /// </param>
    /// <returns>
    /// The <see cref="HistoryEntryCollection"/>.
    /// </returns>
    public override HistoryEntryCollection GetHistory(DateTime from, DateTime to)
    {
      return new HistoryEntryCollection();
    }

    #endregion
  }
}