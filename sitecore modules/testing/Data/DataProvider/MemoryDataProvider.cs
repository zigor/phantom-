namespace Sitecore.TestKit.Data.Memory
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.UI.WebControls;

  using Sitecore;
  using Sitecore.Collections;
  using Sitecore.Data;
  using Sitecore.Data.DataProviders;
  using Sitecore.Data.Items;
  using Sitecore.Globalization;

  using Version = Sitecore.Data.Version;

  /// <summary>
  /// The memory data provider.
  /// </summary>
  public class MemoryDataProvider : DataProvider
  {
    #region Fields

    /// <summary>
    /// The data storage.
    /// </summary>
    private DataStorage dataStorage;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryDataProvider"/> class.
    /// </summary>
    /// <param name="connectionString">
    /// The connection string.
    /// </param>
    public MemoryDataProvider(string connectionString)
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the data storage.
    /// </summary>
    protected DataStorage DataStorage
    {
      get
      {
        if (this.dataStorage == null)
        {
          this.dataStorage = new DataStorage(this.Name);
        }

        return this.dataStorage;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Resolves the path.
    /// </summary>
    /// <param name="itemPath">The item path.</param>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public override ID ResolvePath(string itemPath, CallContext context)
    {
      var id = this.GetRootID(context);

      if (!string.IsNullOrEmpty(itemPath))
      {
        var path = itemPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var part in path.Skip(1))
        {
          var item = this.GetItemDefinition(id, context);
          foreach (ID childId in this.GetChildIDs(item, context))
          {
            var child = this.GetItemDefinition(childId, context);

            if (String.Compare(part, child.Name, StringComparison.OrdinalIgnoreCase) == 0)
            {
              id = childId;
              break;
            }
          }
        }
      }

      return id;
    }

    /// <summary>
    /// The add version.
    /// </summary>
    /// <param name="itemDefinition">
    /// The item definition.
    /// </param>
    /// <param name="baseVersion">
    /// The base version.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public override int AddVersion(ItemDefinition itemDefinition, VersionUri baseVersion, CallContext context)
    {
      ItemInformation info = this.DataStorage.GetItem(itemDefinition.ID);
      if (info != null)
      {
        ItemData baseData = info.GetItemData(baseVersion.Language, baseVersion.Version);

        if (baseData == null)
        {
          baseData = new ItemData(itemDefinition, baseVersion.Language, baseVersion.Version, new FieldList());
          if (info.GetVersions() == null)
          {
            info.AddVersions(new VersionUriList());
          }

          info.AddVersions(new VersionCollection(), baseVersion.Language);
        }

        var newVersion = new Version(baseVersion.Version.Number + 1);
        var newData = new ItemData(
          itemDefinition, baseVersion.Language, newVersion, baseData != null ? baseData.Fields : new FieldList());

        info.AddItemData(newData);

        VersionUriList versionUriList = info.GetVersions();
        versionUriList.Add(baseVersion.Language, newVersion);

        VersionCollection varsionCollection = info.GetVersions(baseVersion.Language);
        varsionCollection.Add(newVersion);

        return baseVersion.Version.Number + 1;
      }

      return base.AddVersion(itemDefinition, baseVersion, context);
    }

    /// <summary>
    /// The copy item.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="destination">
    /// The destination.
    /// </param>
    /// <param name="copyName">
    /// The copy name.
    /// </param>
    /// <param name="copyID">
    /// The copy id.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool CopyItem(
      ItemDefinition source, ItemDefinition destination, string copyName, ID copyID, CallContext context)
    {
      this.CreateItem(copyID, copyName, source.TemplateID, destination, context);

      ItemInformation sourceInfo = this.DataStorage.GetItem(source.ID);
      ItemInformation copyInfo = this.DataStorage.GetItem(copyID);

      copyInfo.AddVersions(sourceInfo.GetVersions());

      foreach (VersionUri version in sourceInfo.GetVersions() ?? new VersionUriList())
      {
        copyInfo.AddItemData(sourceInfo.GetItemData(version.Language, version.Version));
      }

      return true;
    }

    /// <summary>
    /// The create item.
    /// </summary>
    /// <param name="itemID">
    /// The item id.
    /// </param>
    /// <param name="itemName">
    /// The item name.
    /// </param>
    /// <param name="templateID">
    /// The template id.
    /// </param>
    /// <param name="parent">
    /// The parent.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool CreateItem(
      ID itemID, string itemName, ID templateID, ItemDefinition parent, CallContext context)
    {
      var itemDefinition = new ItemDefinition(itemID, itemName, templateID, ID.Null);
      var itemInfo = new ItemInformation(itemDefinition) { ParentID = parent.ID };

      this.DataStorage.Add(itemInfo);
      return true;
    }

    /// <summary>
    /// The delete item.
    /// </summary>
    /// <param name="itemDefinition">
    /// The item definition.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool DeleteItem(ItemDefinition itemDefinition, CallContext context)
    {
      if (this.DataStorage.Contains(itemDefinition.ID))
      {
        this.DataStorage.Remove(itemDefinition.ID);
        return true;
      }

      return base.DeleteItem(itemDefinition, context);
    }

    /// <summary>
    /// The get child i ds.
    /// </summary>
    /// <param name="itemDefinition">
    /// The item definition.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="IDList"/>.
    /// </returns>
    public override IDList GetChildIDs(ItemDefinition itemDefinition, CallContext context)
    {
      var idList = new IDList();

      IEnumerable<ItemInformation> childs = this.DataStorage.GetItems().Where(i => i.ParentID == itemDefinition.ID);
      new List<ItemInformation>(childs).ForEach(i => idList.Add(i.ItemDefinition.ID));

      return idList;
    }

    /// <summary>
    /// The get item definition.
    /// </summary>
    /// <param name="itemId">
    /// The item id.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="ItemDefinition"/>.
    /// </returns>
    public override ItemDefinition GetItemDefinition(ID itemId, CallContext context)
    {
      if (this.DataStorage.Contains(itemId))
      {
        return this.DataStorage.GetItem(itemId).ItemDefinition;
      }

      return base.GetItemDefinition(itemId, context);
    }

    /// <summary>
    /// The get item fields.
    /// </summary>
    /// <param name="itemDefinition">
    /// The item definition.
    /// </param>
    /// <param name="versionUri">
    /// The version uri.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="FieldList"/>.
    /// </returns>
    public override FieldList GetItemFields(ItemDefinition itemDefinition, VersionUri versionUri, CallContext context)
    {
      if (this.DataStorage.Contains(itemDefinition.ID))
      {
        ItemData itemData = null;
        itemData = this.DataStorage.GetItem(itemDefinition.ID).GetItemData(versionUri.Language, versionUri.Version);

        if (itemData == null)
        {
          return new FieldList();
        }

        return itemData.Fields;
      }

      return base.GetItemFields(itemDefinition, versionUri, context);
    }

    /// <summary>
    /// The get item versions.
    /// </summary>
    /// <param name="itemDefinition">
    /// The item definition.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="VersionUriList"/>.
    /// </returns>
    public override VersionUriList GetItemVersions(ItemDefinition itemDefinition, CallContext context)
    {
      if (this.DataStorage.Contains(itemDefinition.ID))
      {
        return this.DataStorage.GetItem(itemDefinition.ID).GetVersions();
      }

      return base.GetItemVersions(itemDefinition, context);
    }

    /// <summary>
    /// The get languages.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="LanguageCollection"/>.
    /// </returns>
    public override LanguageCollection GetLanguages(CallContext context)
    {
      LanguageCollection languages = new LanguageCollection();

      var languageItems = this.DataStorage.GetItems().Where(l => l.ItemDefinition.TemplateID == TemplateIDs.Language);
      foreach (var languageItem in languageItems)
      {
        Language language;
        ID id = languageItem.ItemDefinition.ID;
        string name = languageItem.ItemDefinition.Name;
        if (name.Length != 0 && name[0] != '*' && Language.TryParse(name, out language) && !languages.Contains(language))
        {
          language.Origin.ItemId = id;
          languages.Add(language);
        }
      }

      if (languages.Count == 0)
      {
        languages = base.GetLanguages(context);
      }

      return languages;
    }

    /// <summary>
    /// The get parent id.
    /// </summary>
    /// <param name="itemDefinition">
    /// The item definition.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="ID"/>.
    /// </returns>
    public override ID GetParentID(ItemDefinition itemDefinition, CallContext context)
    {
      if (this.DataStorage.Contains(itemDefinition.ID))
      {
        return this.DataStorage.GetItem(itemDefinition.ID).ParentID;
      }

      return base.GetParentID(itemDefinition, context);
    }

    /// <summary>
    /// The get root id.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="ID"/>.
    /// </returns>
    public override ID GetRootID(CallContext context)
    {
      return ItemIDs.RootID;
    }

    /// <summary>
    /// The get template item ids.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="IdCollection"/>.
    /// </returns>
    public override IdCollection GetTemplateItemIds(CallContext context)
    {
      var ids = new IdCollection();
      IEnumerable<ItemInformation> templates =
        this.DataStorage.GetItems().Where(v => v.ItemDefinition.TemplateID == TemplateIDs.Template);
      new List<ID>(templates.Select(v => v.ItemDefinition.ID)).ForEach(ids.Add);

      return ids;
    }

    /// <summary>
    /// The has children.
    /// </summary>
    /// <param name="itemDefinition">
    /// The item definition.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool HasChildren(ItemDefinition itemDefinition, CallContext context)
    {
      return this.GetChildIDs(itemDefinition, context).Count > 0;
    }

    /// <summary>
    /// The move item.
    /// </summary>
    /// <param name="itemDefinition">
    /// The item definition.
    /// </param>
    /// <param name="destination">
    /// The destination.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool MoveItem(ItemDefinition itemDefinition, ItemDefinition destination, CallContext context)
    {
      if (this.DataStorage.Contains(itemDefinition.ID) && this.DataStorage.Contains(destination.ID))
      {
        this.DataStorage.GetItem(itemDefinition.ID).ParentID = destination.ID;
        return true;
      }

      return base.MoveItem(itemDefinition, destination, context);
    }

    /// <summary>
    /// The remove version.
    /// </summary>
    /// <param name="itemDefinition">
    /// The item definition.
    /// </param>
    /// <param name="version">
    /// The version.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool RemoveVersion(ItemDefinition itemDefinition, VersionUri version, CallContext context)
    {
      ItemInformation info = this.DataStorage.GetItem(itemDefinition.ID);
      if (info != null)
      {
        Version removeVersion =
          info.GetVersions(version.Language).FirstOrDefault(v => v.Number == version.Version.Number);
        info.GetVersions(version.Language).Remove(removeVersion);
        return true;
      }

      return base.RemoveVersion(itemDefinition, version, context);
    }

    /// <summary>
    /// The save item.
    /// </summary>
    /// <param name="itemDefinition">
    /// The item definition.
    /// </param>
    /// <param name="changes">
    /// The changes.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool SaveItem(ItemDefinition itemDefinition, ItemChanges changes, CallContext context)
    {
      ItemInformation info = this.DataStorage.GetItem(itemDefinition.ID);
      if (info != null)
      {
        foreach (FieldChange change in changes.FieldChanges)
        {
          ItemData data = info.GetItemData(change.Language, change.Version);

          if (this.GetItemDefinition(change.FieldID, context) != null)
          {
            //foreach (KeyValuePair<ID, string> field in data.Fields)
            //{
            //  if (field.Key != change.FieldID)
            //  {
            //    fields.Add(field.Key, field.Value);
            //  }
            //}

            if (!change.RemoveField)
            {
              data.Fields.Add(change.FieldID, change.Value);
            }
            else
            {
              if (data.Fields.FieldValues.Contains(change.FieldID))
              {
                data.Fields.FieldValues.Remove(change.FieldID);
              }
            }
          }
        }

        return true;
      }

      return base.SaveItem(itemDefinition, changes, context);
    }

    #endregion
  }
}