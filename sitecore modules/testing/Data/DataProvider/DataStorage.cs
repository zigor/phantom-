namespace Sitecore.TestKit.Data.Memory
{
  using System.Collections.Generic;

  using Sitecore;
  using Sitecore.Data;
  using Sitecore.Diagnostics;
  using Sitecore.TestKit.Extensions;

  using TestIDs = IDs.ItemIDs;

  /// <summary>
  /// The data storage.
  /// </summary>
  public class DataStorage
  {
    #region Static Fields

    /// <summary>
    /// The content.
    /// </summary>
    protected static readonly Dictionary<string, ItemInformationDictionary> content;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes static members of the <see cref="DataStorage"/> class.
    /// </summary>
    static DataStorage()
    {
      content = new Dictionary<string, ItemInformationDictionary>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataStorage"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    protected internal DataStorage(string name)
    {
      Assert.ArgumentNotNullOrEmpty(name, "name");
      this.Name = name;

      this.InitContentTree();
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
    /// Gets the content.
    /// </summary>
    private ItemInformationDictionary Content
    {
      get
      {
        return content[this.Name];
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The add.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    public void Add(ItemInformation info)
    {
      Assert.ArgumentNotNull(info, "info");

      content[this.Name].Add(info.ItemDefinition.ID, info);
    }

    /// <summary>
    /// The contains.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool Contains(ID id)
    {
      Assert.ArgumentNotNull(id, "id");
      return content[this.Name].ContainsKey(id);
    }

    /// <summary>
    /// The get item.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="ItemInformation"/>.
    /// </returns>
    public ItemInformation GetItem(ID id)
    {
      Assert.ArgumentNotNull(id, "id");

      if (this.Contains(id))
      {
        return content[this.Name][id];
      }

      return null;
    }

    /// <summary>
    /// The get items.
    /// </summary>
    /// <returns>
    /// The <see cref="IEnumerable{T}"/>.
    /// </returns>
    public IEnumerable<ItemInformation> GetItems()
    {
      return content[this.Name].Values;
    }

    /// <summary>
    /// The remove.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    public void Remove(ID id)
    {
      content[this.Name].Remove(id);
    }

    #endregion

    #region Methods

    /// <summary>
    /// The init content tree.
    /// </summary>
    protected virtual void InitContentTree()
    {
      if (!content.ContainsKey(this.Name))
      {
        content.Add(this.Name, new ItemInformationDictionary());

        this.Content.Add(ItemIDs.RootID, "sitecore", TestIDs.SitecoreID, ID.Null);
        this.Content.Add(ItemIDs.ContentRoot, "content", TestIDs.RootTemplateID, ItemIDs.RootID);
        this.Content.Add(TestIDs.HomeID, "home", TestIDs.RootTemplateID, ItemIDs.ContentRoot);
        this.Content.Add(ItemIDs.TemplateRoot, "Templates", TestIDs.RootTemplateID, ItemIDs.RootID);

        this.Content.Add(TemplateIDs.Template, "Template", TemplateIDs.Template, ItemIDs.TemplateRoot);
        this.Content.Add(TemplateIDs.TemplateSection, "Template Section", TemplateIDs.Template, ItemIDs.TemplateRoot);

        this.Content.Add(TemplateIDs.TemplateField, "Template Field", TemplateIDs.Template, ItemIDs.TemplateRoot);

        this.Content.Add(
          TestIDs.DataSectionInTemplateField, "Data", TemplateIDs.TemplateSection, TemplateIDs.TemplateField);
        this.Content.Add(TemplateFieldIDs.Type, "Type", TemplateIDs.TemplateField, TestIDs.DataSectionInTemplateField);
        this.Content.Add(
          TemplateFieldIDs.Shared, "Shared", TemplateIDs.TemplateField, TestIDs.DataSectionInTemplateField);
        this.Content.Add(
          TemplateFieldIDs.Unversioned, "Unversioned", TemplateIDs.TemplateField, TestIDs.DataSectionInTemplateField);

        this.Content.Add(TestIDs.DataSectionInTempate, "Data", TemplateIDs.TemplateSection, TemplateIDs.Template);
        this.Content.Add(
          FieldIDs.BaseTemplate, "__Base Templates", TemplateIDs.TemplateField, TestIDs.DataSectionInTempate);

        this.Content.Add(TemplateIDs.StandardTemplate, "Standard Template", TemplateIDs.Template, ItemIDs.TemplateRoot);
        this.Content.Add(TemplateIDs.Node, "Node", TemplateIDs.Template, ItemIDs.TemplateRoot);
        this.Content.Add(TemplateIDs.TemplateFolder, "Template Folder", TemplateIDs.Template, ItemIDs.TemplateRoot);

        this.Content.Add(TemplateIDs.Language, "Language", TemplateIDs.Template, ItemIDs.TemplateRoot);
        this.Content.Add(TestIDs.DataSectionInLanguage, "Data", TemplateIDs.TemplateSection, TemplateIDs.Language);
        this.Content.Add(ID.NewID, "Charset", TemplateIDs.TemplateField, TestIDs.DataSectionInLanguage);
        this.Content.Add(ID.NewID, "Code page", TemplateIDs.TemplateField, TestIDs.DataSectionInLanguage);
        this.Content.Add(ID.NewID, "Dictionary", TemplateIDs.TemplateField, TestIDs.DataSectionInLanguage);
        this.Content.Add(ID.NewID, "Encoding", TemplateIDs.TemplateField, TestIDs.DataSectionInLanguage);
        this.Content.Add(ID.NewID, "Iso", TemplateIDs.TemplateField, TestIDs.DataSectionInLanguage);
        this.Content.Add(ID.NewID, "Regional ISO Code", TemplateIDs.TemplateField, TestIDs.DataSectionInLanguage);
        this.Content.Add(
          ID.NewID, "WorldLingo Language Identifier", TemplateIDs.TemplateField, TestIDs.DataSectionInLanguage);

        this.Content.Add(ItemIDs.SystemRoot, "System", TestIDs.RootTemplateID, ItemIDs.RootID);
        this.Content.Add(ItemIDs.LanguageRoot, "Languages", TemplateIDs.Node, ItemIDs.SystemRoot);
        this.Content.Add(ID.NewID, "da", TemplateIDs.Language, ItemIDs.LanguageRoot);
        this.Content.Add(ID.NewID, "de", TemplateIDs.Language, ItemIDs.LanguageRoot);
        this.Content.Add(ID.NewID, "en", TemplateIDs.Language, ItemIDs.LanguageRoot);
        this.Content.Add(ID.NewID, "fr-CA", TemplateIDs.Language, ItemIDs.LanguageRoot);
        this.Content.Add(ID.NewID, "nb-NO", TemplateIDs.Language, ItemIDs.LanguageRoot);
        this.Content.Add(ID.NewID, "nl-NL", TemplateIDs.Language, ItemIDs.LanguageRoot);
        this.Content.Add(ID.NewID, "sv-SE", TemplateIDs.Language, ItemIDs.LanguageRoot);
      }
    }

    #endregion
  }
}