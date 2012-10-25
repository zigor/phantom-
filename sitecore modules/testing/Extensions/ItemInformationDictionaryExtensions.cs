#region

using Sitecore.Data;
using Sitecore.Diagnostics;
using Phantom.TestKit.Data;

#endregion

namespace Phantom.TestKit.Extensions
{
  using Phantom.TestKit.Data;

  /// <summary>
   /// <see cref="ItemInformationDictionary"/> extensions
   /// </summary>
   public static class ItemInformationDictionaryExtensions
   {
      /// <summary>
      /// Adds the specified dictionary.
      /// </summary>
      /// <param name="dictionary">The dictionary.</param>
      /// <param name="itemID">The item ID.</param>
      /// <param name="name">The name.</param>
      /// <param name="templateID">The template ID.</param>
      /// <param name="parentID">The parent ID.</param>
      public static void Add(this ItemInformationDictionary dictionary,
                             ID itemID,
                             string name,
                             ID templateID,
                             ID parentID)
      {
         Assert.ArgumentNotNull(itemID, "itemID");
         Assert.ArgumentNotNullOrEmpty(name, "name");
         Assert.ArgumentNotNull(templateID, "templateID");
         Assert.ArgumentNotNull(parentID, "parentID");

         dictionary.Add(itemID, new ItemInformation(new ItemDefinition(itemID, name, templateID, ID.Null)) {ParentID = parentID});
      }
   }
}