namespace MobyDick.TestKit.Data
{
  using System;
  using System.Collections.Generic;

  using MobyDick.TestKit.Data.IDs;

  using Sitecore.Data;
  using Sitecore.Data.Items;

  /// <summary>
  /// Database extensions
  /// </summary>
  public static class DatabaseExtension
  {
    #region Public Methods and Operators

    /// <summary>
    /// Creates the item.
    /// </summary>
    /// <param name="database">
    /// The database.
    /// </param>
    /// <param name="itemID">
    /// The item ID.
    /// </param>
    /// <param name="itemName">
    /// Name of the item.
    /// </param>
    /// <param name="templateID">
    /// The template ID.
    /// </param>
    /// <returns>
    /// The <see cref="Item"/>.
    /// </returns>
    public static Item CreateItem(this Database database, ID itemID, string itemName, ID templateID)
    {
      return database.CreateItem(itemID, itemName, templateID, ItemIDs.HomeID);
    }

    /// <summary>
    /// Creates the item.
    /// </summary>
    /// <param name="database">
    /// The database.
    /// </param>
    /// <param name="itemID">
    /// The item ID.
    /// </param>
    /// <param name="itemName">
    /// Name of the item.
    /// </param>
    /// <param name="templateID">
    /// The template ID.
    /// </param>
    /// <param name="parentID">
    /// The parent ID.
    /// </param>
    /// <exception cref="ArgumentException">
    /// The item is already exist
    /// </exception>
    /// <returns>
    /// The <see cref="Item"/>.
    /// </returns>
    public static Item CreateItem(this Database database, ID itemID, string itemName, ID templateID, ID parentID)
    {
      Item duplicate = database.GetItem(itemID);

      if (duplicate != null)
      {
        throw new ArgumentException(
          string.Format("The item \"{0}\", id: {1} is already exist", itemName, itemID), "item");
      }

      database.DataManager.DataSource.CreateItem(itemID, itemName, templateID, parentID);

      return database.GetItem(itemID);
    }

    /// <summary>
    /// Moves the item.
    /// </summary>
    /// <param name="database">
    /// The database.
    /// </param>
    /// <param name="itemID">
    /// The item ID.
    /// </param>
    /// <param name="destinationId">
    /// The destination id.
    /// </param>
    /// <exception cref="ArgumentException">
    /// The item does not exist
    /// </exception>
    /// <returns>
    /// The <see cref="Item"/>.
    /// </returns>
    public static Item MoveItem(this Database database, ID itemID, ID destinationId)
    {
      Item item = database.GetItem(itemID);
      if (item == null)
      {
        throw new ArgumentException("The item does not exist", "itemID");
      }

      Item destination = database.GetItem(destinationId);
      if (destination == null)
      {
        throw new ArgumentException("The item does not exist", "destinationId");
      }

      database.DataManager.DataEngine.MoveItem(item, destination);
      return database.GetItem(itemID);
    }

    /// <summary>
    /// Removes the item.
    /// </summary>
    /// <param name="database">
    /// The database.
    /// </param>
    /// <param name="itemID">
    /// The item ID.
    /// </param>
    public static void RemoveItem(this Database database, ID itemID)
    {
      Item item = database.GetItem(itemID);
      if (item != null)
      {
        database.DataManager.DataEngine.DeleteItem(item);
      }
    }

    /// <summary>
    /// Removes the items.
    /// </summary>
    /// <param name="database">
    /// The database.
    /// </param>
    /// <param name="ids">
    /// The ids.
    /// </param>
    public static void RemoveItems(this Database database, IEnumerable<ID> ids)
    {
      foreach (ID id in ids)
      {
        database.RemoveItem(id);
      }
    }

    #endregion
  }
}