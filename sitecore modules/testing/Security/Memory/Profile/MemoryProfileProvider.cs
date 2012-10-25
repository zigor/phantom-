namespace Phantom.TestKit.Security.Memory.Profile
{
  using System;
  using System.Configuration;
  using System.Web.Profile;

  /// <summary>
  /// The memory profile provider.
  /// </summary>
  public class MemoryProfileProvider : ProfileProvider
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the application name.
    /// </summary>
    public override string ApplicationName { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The delete inactive profiles.
    /// </summary>
    /// <param name="authenticationOption">
    /// The authentication option.
    /// </param>
    /// <param name="userInactiveSinceDate">
    /// The user inactive since date.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public override int DeleteInactiveProfiles(
      ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
    {
      return 0;
    }

    /// <summary>
    /// The delete profiles.
    /// </summary>
    /// <param name="profiles">
    /// The profiles.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public override int DeleteProfiles(ProfileInfoCollection profiles)
    {
      return 0;
    }

    /// <summary>
    /// The delete profiles.
    /// </summary>
    /// <param name="usernames">
    /// The usernames.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public override int DeleteProfiles(string[] usernames)
    {
      return 0;
    }

    /// <summary>
    /// The find inactive profiles by user name.
    /// </summary>
    /// <param name="authenticationOption">
    /// The authentication option.
    /// </param>
    /// <param name="usernameToMatch">
    /// The username to match.
    /// </param>
    /// <param name="userInactiveSinceDate">
    /// The user inactive since date.
    /// </param>
    /// <param name="pageIndex">
    /// The page index.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    /// <param name="totalRecords">
    /// The total records.
    /// </param>
    /// <returns>
    /// The <see cref="ProfileInfoCollection"/>.
    /// </returns>
    public override ProfileInfoCollection FindInactiveProfilesByUserName(
      ProfileAuthenticationOption authenticationOption, 
      string usernameToMatch, 
      DateTime userInactiveSinceDate, 
      int pageIndex, 
      int pageSize, 
      out int totalRecords)
    {
      totalRecords = 0;
      return new ProfileInfoCollection();
    }

    /// <summary>
    /// The find profiles by user name.
    /// </summary>
    /// <param name="authenticationOption">
    /// The authentication option.
    /// </param>
    /// <param name="usernameToMatch">
    /// The username to match.
    /// </param>
    /// <param name="pageIndex">
    /// The page index.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    /// <param name="totalRecords">
    /// The total records.
    /// </param>
    /// <returns>
    /// The <see cref="ProfileInfoCollection"/>.
    /// </returns>
    public override ProfileInfoCollection FindProfilesByUserName(
      ProfileAuthenticationOption authenticationOption, 
      string usernameToMatch, 
      int pageIndex, 
      int pageSize, 
      out int totalRecords)
    {
      totalRecords = 0;
      return new ProfileInfoCollection();
    }

    /// <summary>
    /// The get all inactive profiles.
    /// </summary>
    /// <param name="authenticationOption">
    /// The authentication option.
    /// </param>
    /// <param name="userInactiveSinceDate">
    /// The user inactive since date.
    /// </param>
    /// <param name="pageIndex">
    /// The page index.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    /// <param name="totalRecords">
    /// The total records.
    /// </param>
    /// <returns>
    /// The <see cref="ProfileInfoCollection"/>.
    /// </returns>
    public override ProfileInfoCollection GetAllInactiveProfiles(
      ProfileAuthenticationOption authenticationOption, 
      DateTime userInactiveSinceDate, 
      int pageIndex, 
      int pageSize, 
      out int totalRecords)
    {
      totalRecords = 0;
      return new ProfileInfoCollection();
    }

    /// <summary>
    /// The get all profiles.
    /// </summary>
    /// <param name="authenticationOption">
    /// The authentication option.
    /// </param>
    /// <param name="pageIndex">
    /// The page index.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    /// <param name="totalRecords">
    /// The total records.
    /// </param>
    /// <returns>
    /// The <see cref="ProfileInfoCollection"/>.
    /// </returns>
    public override ProfileInfoCollection GetAllProfiles(
      ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = 0;
      return new ProfileInfoCollection();
    }

    /// <summary>
    /// The get number of inactive profiles.
    /// </summary>
    /// <param name="authenticationOption">
    /// The authentication option.
    /// </param>
    /// <param name="userInactiveSinceDate">
    /// The user inactive since date.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public override int GetNumberOfInactiveProfiles(
      ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
    {
      return 0;
    }

    /// <summary>
    /// The get property values.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <param name="collection">
    /// The collection.
    /// </param>
    /// <returns>
    /// The <see cref="SettingsPropertyValueCollection"/>.
    /// </returns>
    public override SettingsPropertyValueCollection GetPropertyValues(
      SettingsContext context, SettingsPropertyCollection collection)
    {
      return new SettingsPropertyValueCollection();
    }

    /// <summary>
    /// The set property values.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <param name="collection">
    /// The collection.
    /// </param>
    public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
    {
    }

    #endregion
  }
}