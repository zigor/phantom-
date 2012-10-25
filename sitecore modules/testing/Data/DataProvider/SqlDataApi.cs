namespace Phantom.TestKit.DataProviders
{
  using System;
  using System.Data;

  using Sitecore.Data;
  using Sitecore.Data.DataProviders.Sql;
  using Sitecore.Globalization;

  using Version = Sitecore.Data.Version;

  /// <summary>
  /// The sql data api.
  /// </summary>
  public class SqlDataApi : Sitecore.Data.DataProviders.Sql.SqlDataApi
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets ConnectionString.
    /// </summary>
    public override string ConnectionString { get; set; }

    /// <summary>
    /// Gets NamePostfix.
    /// </summary>
    public override string NamePostfix
    {
      get
      {
        return string.Empty;
      }
    }

    /// <summary>
    /// Gets NamePrefix.
    /// </summary>
    public override string NamePrefix
    {
      get
      {
        return string.Empty;
      }
    }

    /// <summary>
    /// Gets the new guid func.
    /// </summary>
    public override string NewGuidFunc
    {
      get
      {
        return null;
      }
    }

    /// <summary>
    /// Gets ParameterPostfix.
    /// </summary>
    public override string ParameterPostfix
    {
      get
      {
        return string.Empty;
      }
    }

    /// <summary>
    /// Gets ParameterPrefix.
    /// </summary>
    public override string ParameterPrefix
    {
      get
      {
        return string.Empty;
      }
    }

    /// <summary>
    /// Gets Quote.
    /// </summary>
    public override string Quote
    {
      get
      {
        return string.Empty;
      }
    }

    /// <summary>
    /// Gets Wildcard.
    /// </summary>
    public override string Wildcard
    {
      get
      {
        return string.Empty;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The get bytes.
    /// </summary>
    /// <param name="columnIndex">
    /// The column index.
    /// </param>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    public override byte[] GetBytes(int columnIndex, DataProviderReader reader)
    {
      return new byte[] { };
    }

    /// <summary>
    /// The get date time.
    /// </summary>
    /// <param name="columnIndex">
    /// The column index.
    /// </param>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public override DateTime GetDateTime(int columnIndex, DataProviderReader reader)
    {
      return DateTime.Now;
    }

    /// <summary>
    /// The get guid.
    /// </summary>
    /// <param name="columnIndex">
    /// The column index.
    /// </param>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/>.
    /// </returns>
    public override Guid GetGuid(int columnIndex, DataProviderReader reader)
    {
      return reader.InnerReader.GetGuid(columnIndex);
    }

    /// <summary>
    /// The get id.
    /// </summary>
    /// <param name="columnIndex">
    /// The column index.
    /// </param>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <returns>
    /// The <see cref="ID"/>.
    /// </returns>
    public override ID GetId(int columnIndex, DataProviderReader reader)
    {
      return ID.Null;
    }

    /// <summary>
    /// The get int.
    /// </summary>
    /// <param name="columnIndex">
    /// The column index.
    /// </param>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <returns>
    /// The get int.
    /// </returns>
    public override int GetInt(int columnIndex, DataProviderReader reader)
    {
      return reader.InnerReader.GetInt32(columnIndex);
    }

    /// <summary>
    /// The get language.
    /// </summary>
    /// <param name="columnIndex">
    /// The column index.
    /// </param>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <returns>
    /// The <see cref="Language"/>.
    /// </returns>
    public override Language GetLanguage(int columnIndex, DataProviderReader reader)
    {
      return Language.Invariant;
    }

    /// <summary>
    /// The get long.
    /// </summary>
    /// <param name="columnIndex">
    /// The column index.
    /// </param>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <returns>
    /// The get long.
    /// </returns>
    public override long GetLong(int columnIndex, DataProviderReader reader)
    {
      return 0;
    }

    /// <summary>
    /// The get string.
    /// </summary>
    /// <param name="columnIndex">
    /// The column index.
    /// </param>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <returns>
    /// The get string.
    /// </returns>
    public override string GetString(int columnIndex, DataProviderReader reader)
    {
      return reader.InnerReader.GetString(columnIndex);
    }

    /// <summary>
    /// The get version.
    /// </summary>
    /// <param name="columnIndex">
    /// The column index.
    /// </param>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <returns>
    /// The <see cref="Version"/>.
    /// </returns>
    public override Version GetVersion(int columnIndex, DataProviderReader reader)
    {
      return Version.First;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The create connection.
    /// </summary>
    /// <returns>
    /// The <see cref="IDbConnection"/>.
    /// </returns>
    protected override IDbConnection CreateConnection()
    {
      return null;
    }

    /// <summary>
    /// The create parameter.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="IDbDataParameter"/>.
    /// </returns>
    protected override IDbDataParameter CreateParameter(string name, object value)
    {
      return null;
    }

    #endregion
  }
}