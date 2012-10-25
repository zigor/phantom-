namespace MobyDick.TestKit.Data.Extensions
{
  using System;

  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;

  /// <summary>
  /// <see cref="CustomItemBase"/> extensions
  /// </summary>
  public static class CustomItemBaseExtension
  {
    #region Public Methods and Operators

    /// <summary>
    /// Ares the equal.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="V">
    /// </typeparam>
    /// <param name="item">
    /// The item.
    /// </param>
    /// <param name="getActualValue">
    /// The get actual value.
    /// </param>
    /// <param name="expectedValue">
    /// The expected value.
    /// </param>
    public static void AreEqual<T, V>(this CustomItemBase item, Func<T, V> getActualValue, V expectedValue)
      where T : CustomItemBase
    {
      Assert.IsTrue(expectedValue.Equals(getActualValue((T)item)), "Values are different");
    }

    /// <summary>
    /// Edits the specified item.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <param name="item">
    /// The item.
    /// </param>
    /// <param name="editAction">
    /// The edit action.
    /// </param>
    public static void Edit<T>(this CustomItemBase item, Action<T> editAction) where T : CustomItemBase
    {
      item.BeginEdit();
      editAction((T)item);
      item.EndEdit();
    }

    #endregion
  }
}