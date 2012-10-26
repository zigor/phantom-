// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemoryAuthorizationProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The memory authorization provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Phantom.TestKit.Security.AccessControl
{
  using System.Collections.Generic;

  using Sitecore.Security.AccessControl;
  using Sitecore.Security.Accounts;

  /// <summary>
  /// The memory authorization provider.
  /// </summary>
  public class MemoryAuthorizationProvider : AuthorizationProvider
  {
    #region Fields

    /// <summary>
    /// The access rules.
    /// </summary>
    private readonly IDictionary<string, AccessRuleCollection> accessRules =
      new Dictionary<string, AccessRuleCollection>();

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the access rules.
    /// </summary>
    public IDictionary<string, AccessRuleCollection> AccessRules
    {
      get
      {
        return this.accessRules;
      }
    }

    #endregion

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
      return this.accessRules[entity.GetUniqueId()];
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
      this.accessRules[entity.GetUniqueId()] = rules;
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
      if (!this.accessRules.ContainsKey(entity.GetUniqueId()))
      {
        return new AccessResult(AccessPermission.Allow, new AccessExplanation(account.Name, accessRight));
      }

      AccessRuleCollection rule = this.accessRules[entity.GetUniqueId()];

      return new AccessResult(rule.Helper.GetAccessPermission(account, accessRight, PropagationType.Any), new AccessExplanation("Memory authorization provider found it correct."));
    }

    #endregion
  }
}