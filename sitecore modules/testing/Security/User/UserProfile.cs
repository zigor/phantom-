namespace MobyDick.TestKit.Security
{
  /// <summary>
  /// The user profile.
  /// </summary>
  public class UserProfile : Sitecore.Security.UserProfile
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="UserProfile"/> class.
    /// </summary>
    public UserProfile()
    {
      this.IsAdministrator = true;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets a value indicating whether is administrator.
    /// </summary>
    public override bool IsAdministrator { get; set; }

    #endregion
  }
}