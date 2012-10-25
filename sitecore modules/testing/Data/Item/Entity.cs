namespace Phantom.TestKit.Data
{
  using Sitecore.Data;

  /// <summary>
  /// The entity.
  /// </summary>
  public abstract class Entity
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    protected Entity(ID id)
    {
      this.ID = id;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the id.
    /// </summary>
    public ID ID { get; private set; }

    #endregion
  }
}