namespace Phantom.TestKit.Data
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Linq;

  using Phantom.TestKit.Configuration;
  using Phantom.TestKit.Data.Templates;

  using Sitecore;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Globalization;
  using Phantom.TestKit.Data.Extensions;

  using TestIDs = Phantom.TestKit.Data.IDs.ItemIDs;
  using Version = Sitecore.Data.Version;

  /// <summary>
  /// The t tree.
  /// </summary>
  public class TTree : IEnumerable<TItem>, IEnumerable<TTemplate>, IDisposable
  {
    #region Fields

    /// <summary>
    /// The database.
    /// </summary>
    private readonly Database database;

    /// <summary>
    /// The items ids.
    /// </summary>
    private readonly List<ID> itemsIds = new List<ID>();

    /// <summary>
    /// The postponed items.
    /// </summary>
    private readonly Dictionary<TItem, ID> postponedItems = new Dictionary<TItem, ID>();

    /// <summary>
    /// The templates ids.
    /// </summary>
    private readonly List<ID> templatesIds = new List<ID>();

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes static members of the <see cref="TTree"/> class. 
    /// </summary>
    static TTree()
    {
      if (Instance.DefaultInitializationRequired)
      {
        var sitecore = new Instance();

        sitecore.AddDatabase("master");
        sitecore.AddDatabase("core");
        sitecore.AddDatabase("web");

        sitecore.AddPipeline("filterItem", null, null);
        sitecore.AddPipeline("loadVisitor", null, null);
        sitecore.AddPipeline("filterItem", null, null);
        sitecore.AddPipeline("renderField", "Sitecore.Pipelines.RenderField.GetFieldValue, Sitecore.Kernel", null);

        sitecore.Prepare();
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TTree"/> class.
    /// </summary>
    public TTree()
      : this("test")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TTree"/> class.
    /// </summary>
    /// <param name="databaseName">
    /// The database Name.
    /// </param>
    public TTree(string databaseName)
      : this(new TDatabase(databaseName))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TTree"/> class.
    /// </summary>
    /// <param name="database">
    /// The database.
    /// </param>
    public TTree(TDatabase database)
    {
      this.database = database;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets Database.
    /// </summary>
    public Database Database
    {
      get
      {
        return this.database;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The add.
    /// </summary>
    /// <param name="template">
    /// The template.
    /// </param>
    public void Add(TTemplate template)
    {
      this.CreateTemplate(template, ItemIDs.TemplateRoot);
      this.templatesIds.Add(template.ID);

      Item createdTempalte = this.Database.GetItem(template.ID, Language.Invariant, Version.Latest);
      createdTempalte.Edit(t => t.Fields[FieldIDs.BaseTemplate].Value = TemplateIDs.StandardTemplate.ToString());

      foreach (TTemplate baseTemplate in template.BaseTemplates)
      {
        this.Add(baseTemplate);
        createdTempalte.Edit(i => i.Fields[FieldIDs.BaseTemplate].Value = string.Join("|", new[] { baseTemplate.ID.ToString(), i.Fields[FieldIDs.BaseTemplate].Value }));
      }

      foreach (var pair in this.postponedItems.Where(i => i.Key.TemplateID == template.ID).ToArray())
      {
        this.Add(pair.Key, pair.Value);

        this.postponedItems.Remove(pair.Key);
      }
    }

    /// <summary>
    /// The add.
    /// </summary>
    /// <param name="item">
    /// The item.
    /// </param>
    public void Add(TItem item)
    {
      ID parentID = item.ParentID;
      if (parentID == ID.Null)
      {
        parentID = TestIDs.HomeID;
      }

      this.Add(item, parentID);
    }

    /// <summary>
    /// The dispose.
    /// </summary>
    public void Dispose()
    {
      this.database.RemoveItems(this.itemsIds);
      this.database.RemoveItems(this.templatesIds);
    }

    /// <summary>
    /// The get enumerator.
    /// </summary>
    /// <exception cref="NotSupportedException">
    /// <c>NotSupportedException</c>.
    /// </exception>
    /// <returns>
    /// The <see cref="IEnumerator"/>.
    /// </returns>
    public IEnumerator GetEnumerator()
    {
      throw new NotSupportedException();
    }

    #endregion

    #region Explicit Interface Methods

    /// <summary>
    /// The i enumerable. get enumerator.
    /// </summary>
    /// <exception cref="NotSupportedException">
    /// <c>NotSupportedException</c>.
    /// </exception>
    /// <returns>
    /// The <see cref="IEnumerator"/>.
    /// </returns>
    IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// The i enumerable. get enumerator.
    /// </summary>
    /// <exception cref="NotSupportedException">
    /// <c>NotSupportedException</c>.
    /// </exception>
    /// <returns>
    /// The <see cref="IEnumerator"/>.
    /// </returns>
    IEnumerator<TTemplate> IEnumerable<TTemplate>.GetEnumerator()
    {
      throw new NotSupportedException();
    }

    #endregion

    #region Methods

    /// <summary>
    /// The add.
    /// </summary>
    /// <param name="item">
    /// The item.
    /// </param>
    /// <param name="parentID">
    /// The parent id.
    /// </param>
    private void Add(TItem item, ID parentID)
    {
      var template = this.database.GetItem(item.TemplateID);
      if (template == null)
      {
        this.Add(new TTemplate(item.Name, item.TemplateID) { new TSection("Data") { item.Fields.ConvertAll(kvp => kvp.Key) } });
      }

      Item newItem = this.database.CreateItem(item.ID, item.Name, item.TemplateID, parentID);

      using (new EditContext(newItem))
      {
        foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>)item)
        {
          newItem[keyValuePair.Key] = keyValuePair.Value;
        }
      }

      this.itemsIds.Add(item.ID);

      foreach (TItem child in item)
      {
        this.Add(child, item.ID);
      }
    }

    /// <summary>
    /// The create template.
    /// </summary>
    /// <param name="template">
    /// The template.
    /// </param>
    /// <param name="parent">
    /// The parent.
    /// </param>
    private void CreateTemplate(TTemplate template, ID parent)
    {
      Assert.ArgumentNotNull(template, "template");
      Assert.ArgumentNotNull(parent, "parent");
      TemplateManager.CreateTemplate(this, template, parent);
    }

    #endregion
  }
}