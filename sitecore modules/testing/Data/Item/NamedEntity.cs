namespace Phantom.TestKit.Data
{
  using Sitecore.Data;
  using Sitecore.Diagnostics;

  /// <summary>
  /// The named entity.
  /// </summary>
  public abstract class NamedEntity : Entity
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="NamedEntity"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    protected NamedEntity(string name, ID id)
      : base(id)
    {
      this.Name = name;
      Assert.ArgumentNotNullOrEmpty(name, "name");
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    #endregion
  }
}