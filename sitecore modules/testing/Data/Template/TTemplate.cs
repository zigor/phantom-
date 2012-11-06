namespace Sitecore.TestKit.Data
{
  using System.Collections;
  using System.Collections.Generic;

  using Sitecore;
  using Sitecore.Data;
  using Sitecore.Diagnostics;

  /// <summary>
  /// The t template.
  /// </summary>
  public class TTemplate : TItemBase, IEnumerable<TSection>
  {
    #region Fields

    /// <summary>
    /// The base templates.
    /// </summary>
    private readonly List<TTemplate> baseTemplates = new List<TTemplate>();

    /// <summary>
    /// The sections.
    /// </summary>
    private readonly List<TSection> sections = new List<TSection>();

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TTemplate"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    public TTemplate(string name)
      : this(name, ID.NewID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TTemplate"/> class.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    public TTemplate(ID id)
      : this("TTemplate", id)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TTemplate"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    public TTemplate(string name, ID id)
      : base(name, id, new TemplateID(TemplateIDs.Template))
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the base templates.
    /// </summary>
    public IEnumerable<TTemplate> BaseTemplates
    {
      get
      {
        return this.baseTemplates;
      }
    }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public override string Name
    {
      get
      {
        if (string.IsNullOrEmpty(base.Name))
        {
          return "TTemplate";
        }

        return base.Name;
      }

      set
      {
        base.Name = value;
      }
    }

    /// <summary>
    /// Gets the sections.
    /// </summary>
    public IEnumerable<TSection> Sections
    {
      get
      {
        return this.sections;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The add.
    /// </summary>
    /// <param name="baseTemplate">
    /// The base template.
    /// </param>
    public void Add(TTemplate baseTemplate)
    {
      Assert.ArgumentNotNull(baseTemplate, "baseTemplate");
      this.baseTemplates.Add(baseTemplate);
    }

    /// <summary>
    /// The add.
    /// </summary>
    /// <param name="section">
    /// The section.
    /// </param>
    public void Add(TSection section)
    {
      Assert.ArgumentNotNull(section, "section");
      this.sections.Add(section);
    }

    /// <summary>
    /// The get enumerator.
    /// </summary>
    /// <returns>
    /// The <see cref="IEnumerator"/>.
    /// </returns>
    public IEnumerator GetEnumerator()
    {
      return ((IEnumerable<TSection>)this).GetEnumerator();
    }

    #endregion

    #region Explicit Interface Methods

    /// <summary>
    /// The get enumerator.
    /// </summary>
    /// <returns>
    /// The <see cref="IEnumerator"/>.
    /// </returns>
    IEnumerator<TSection> IEnumerable<TSection>.GetEnumerator()
    {
      return this.Sections.GetEnumerator();
    }

    #endregion
  }
}