namespace Phantom.TestKit.Data.Extensions
{
  using System;

  using Sitecore.Data;
  using Sitecore.Data.Items;

  /// <summary>
  /// Item extensions
  /// </summary>
  public static class ItemExtensions
  {
    #region Public Methods and Operators

    /// <summary>
    /// The copy to.
    /// </summary>
    /// <param name="item">
    /// The item.
    /// </param>
    /// <param name="destinationID">
    /// The destination id.
    /// </param>
    /// <returns>
    /// The <see cref="Item"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// </exception>
    public static Item CopyTo(this Item item, ID destinationID)
    {
      if (item == null)
      {
        throw new ArgumentException("The item does not exist", "item");
      }

      Item destination = item.Database.GetItem(destinationID);
      if (destination == null)
      {
        throw new ArgumentException("The item does not exist", "destinationID");
      }

      return item.CopyTo(destination, "Copy of" + item.Name);
    }

    /// <summary>
    /// Edits the specified item.
    /// </summary>
    /// <param name="item">
    /// The item.
    /// </param>
    /// <param name="action">
    /// The action.
    /// </param>
    /// <returns>
    /// The <see cref="Item"/>.
    /// </returns>
    public static Item Edit(this Item item, Action<Item> action)
    {
      item.Editing.BeginEdit();
      action(item);
      item.Editing.EndEdit();
      return item;
    }

    #endregion
  }
}