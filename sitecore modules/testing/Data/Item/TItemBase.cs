namespace Phantom.TestKit.Data
{
  using Sitecore.Data;

  /// <summary>
  /// The t item base.
  /// </summary>
  public class TItemBase : Entity
  {
    #region Fields

    /// <summary>
    /// The name.
    /// </summary>
    private string name;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TItemBase"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="templateID">
    /// The template id.
    /// </param>
    public TItemBase(string name, TemplateID templateID)
      : this(name, ID.NewID, templateID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItemBase"/> class.
    /// </summary>
    /// <param name="templateID">
    /// The template id.
    /// </param>
    public TItemBase(TemplateID templateID)
      : this(ID.NewID, templateID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItemBase"/> class.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="templateID">
    /// The template id.
    /// </param>
    public TItemBase(ID id, TemplateID templateID)
      : this(string.Empty, id, templateID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItemBase"/> class.
    /// </summary>
    /// <param name="template">
    /// The template.
    /// </param>
    public TItemBase(TTemplate template)
      : this(ID.NewID, template)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItemBase"/> class.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="template">
    /// The template.
    /// </param>
    public TItemBase(ID id, TTemplate template)
      : this("TItem", id, template)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItemBase"/> class.
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
    public TItemBase(string name, ID id, TTemplate template)
      : this(name, id, new TemplateID(template.ID))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItemBase"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="template">
    /// The template.
    /// </param>
    public TItemBase(string name, TTemplate template)
      : this(name, ID.NewID, template)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItemBase"/> class.
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
    public TItemBase(string name, ID id, ID template)
      : this(name, id, new TemplateID(template))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItemBase"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="templateID">
    /// The template id.
    /// </param>
    public TItemBase(string name, ID id, TemplateID templateID)
      : this(name, id, templateID, ID.Null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItemBase"/> class.
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
    public TItemBase(string name, ID id, ID template, ID parent)
      : this(name, id, new TemplateID(template), parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TItemBase"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="templateID">
    /// The template id.
    /// </param>
    /// <param name="parentID">
    /// The parent id.
    /// </param>
    public TItemBase(string name, ID id, TemplateID templateID, ID parentID)
      : base(id)
    {
      this.TemplateID = templateID;
      this.ParentID = parentID;
      this.Name = name;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public virtual string Name
    {
      get
      {
        return this.name ?? string.Empty;
      }

      set
      {
        this.name = value;
      }
    }

    /// <summary>
    /// Gets the parent id.
    /// </summary>
    public virtual ID ParentID { get; private set; }

    /// <summary>
    /// Gets the template id.
    /// </summary>
    public virtual TemplateID TemplateID { get; private set; }

    #endregion
  }
}