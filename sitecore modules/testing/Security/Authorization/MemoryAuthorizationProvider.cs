namespace MobyDick.TestKit.Security.AccessControl
{
  using Sitecore.Security.AccessControl;
  using Sitecore.Security.Accounts;

  /// <summary>
  /// The memory authorization provider.
  /// </summary>
  public class MemoryAuthorizationProvider : AuthorizationProvider
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get access rules.
    /// </summary>
    /// <param name="entity">
    /// The entity.
    /// </param>
    /// <returns>
    /// The <see cref="AccessRuleCollection"/>.
    /// </returns>
    public override AccessRuleCollection GetAccessRules(ISecurable entity)
    {
      return new AccessRuleCollection();
    }

    /// <summary>
    /// The set access rules.
    /// </summary>
    /// <param name="entity">
    /// The entity.
    /// </param>
    /// <param name="rules">
    /// The rules.
    /// </param>
    public override void SetAccessRules(ISecurable entity, AccessRuleCollection rules)
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// The get access core.
    /// </summary>
    /// <param name="entity">
    /// The entity.
    /// </param>
    /// <param name="account">
    /// The account.
    /// </param>
    /// <param name="accessRight">
    /// The access right.
    /// </param>
    /// <returns>
    /// The <see cref="AccessResult"/>.
    /// </returns>
    protected override AccessResult GetAccessCore(ISecurable entity, Account account, AccessRight accessRight)
    {
      return new AccessResult(AccessPermission.Allow, new AccessExplanation(account.Name, accessRight));
    }

    #endregion
  }
}