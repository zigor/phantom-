namespace Sitecore.TestKit.Data
{
  using System.Collections;
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.Data.Linq;
  using System.Linq;

  using Sitecore;
  using Sitecore.Data;
  using Sitecore.StringExtensions;

  /// <summary>
  /// Defines the TItem class.
  /// </summary>
  public class TItem : TItemBase, IEnumerable<TItem>, IEnumerable<KeyValuePair<string, string>>
  {
    #region Fields

    /// <summary>
    /// The children list
    /// </summary>
    private readonly List<TItem> children = new List<TItem>();

    /// <summary>
    /// The field values list.
    /// </summary>
    private readonly List<KeyValuePair<string, string>> fieldValueList = new List<KeyValuePair<string, string>>();

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TItem"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="templateID">
    /// The template ID.
    /// </param>
    public TItem(string name, TemplateID templateID)
      : this(name, ID.NewID, templateID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItem"/> class.
    /// </summary>
    /// <param name="templateID">
    /// The template ID.
    /// </param>
    public TItem(TemplateID templateID)
      : this(ID.NewID, templateID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItem"/> class.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="templateID">
    /// The template ID.
    /// </param>
    public TItem(ID id, TemplateID templateID)
      : this("TItem", id, templateID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItem"/> class.
    /// </summary>
    /// <param name="template">
    /// The template.
    /// </param>
    public TItem(TTemplate template)
      : this(ID.NewID, template)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItem"/> class.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="template">
    /// The template.
    /// </param>
    public TItem(ID id, TTemplate template)
      : this(id, new TemplateID(template.ID))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItem"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="template">
    /// The template.
    /// </param>
    public TItem(string name, TTemplate template)
      : this(name, ID.NewID, template)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItem"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="template">
    /// The template.
    /// </param>
    public TItem(string name, ID id, TTemplate template)
      : this(name, id, template.ID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItem"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="templateID">
    /// The template ID.
    /// </param>
    public TItem(string name, ID id, TemplateID templateID)
      : this(name, id, templateID.ID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItem"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="template">
    /// The template.
    /// </param>
    public TItem(string name, ID id, ID template)
      : this(name, id, template, ID.Null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItem"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="template">
    /// The template.
    /// </param>
    /// <param name="parent">
    /// The parent.
    /// </param>
    public TItem(string name, ID id, ID template, ID parent)
      : base(name, id, template, parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItem"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    public TItem(string name)
      : this(name, ID.NewID, ID.NewID)
    {
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the fields.
    /// </summary>
    public List<KeyValuePair<string, string>> Fields
    {
      get
      {
        return this.fieldValueList;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Adds the specified item.
    /// </summary>
    /// <param name="item">
    /// The item.
    /// </param>
    /// <exception cref="DuplicateKeyException">
    /// </exception>
    public void Add(TItem item)
    {
      if (this.children.FirstOrDefault(c => c.ID == item.ID) != null)
      {
        throw new DuplicateKeyException(item, "Duplicated id:{0}".FormatWith(item.ID));
      }

      this.children.Add(item);
    }

    /// <summary>
    /// Adds the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public void Add(string key, string value)
    {
      this.fieldValueList.Add(new KeyValuePair<string, string>(key, value));
    }

    /// <summary>
    /// Adds the specified field values.
    /// </summary>
    /// <param name="fieldValues">The field values.</param>
    public void Add(NameValueCollection fieldValues)
    {
      foreach (string key in fieldValues)
      {
        this.Add(key, fieldValues[key]);
      }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<TItem> GetEnumerator()
    {
      return this.children.GetEnumerator();
    }

    #endregion

    #region Explicit Interface Methods

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
    /// </returns>
    IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
    {
      return this.fieldValueList.GetEnumerator();
    }

    #endregion
  }
}