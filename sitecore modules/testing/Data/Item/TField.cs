namespace Phantom.TestKit.Data
{
  using Sitecore.Data;

  /// <summary>
  /// The t field.
  /// </summary>
  public class TField : NamedEntity
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TField"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    public TField(string name)
      : this(name, ID.NewID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TField"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    public TField(string name, ID id)
      : this(name, id, string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TField"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="type">
    /// The type.
    /// </param>
    public TField(string name, ID id, string type)
      : this(name, id, type, false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TField"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="shared">
    /// The shared.
    /// </param>
    public TField(string name, ID id, string type, bool shared)
      : this(name, id, type, shared, false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TField"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="shared">
    /// The shared.
    /// </param>
    /// <param name="unversioned">
    /// The unversioned.
    /// </param>
    public TField(string name, ID id, string type, bool shared, bool unversioned)
      : base(name, id)
    {
      this.Type = type;
      this.Shared = shared;
      this.Unversioned = unversioned;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets a value indicating whether shared.
    /// </summary>
    public bool Shared { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether unversioned.
    /// </summary>
    public bool Unversioned { get; set; }

    #endregion
  }
}