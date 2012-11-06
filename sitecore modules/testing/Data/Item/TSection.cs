// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TSection.cs" company="">
//   
// </copyright>
// <summary>
//   The t section.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.TestKit.Data
{
  using System.Collections;
  using System.Collections.Generic;

  using Sitecore.Data;
  using Sitecore.Diagnostics;

  /// <summary>
  /// The t section.
  /// </summary>
  public class TSection : NamedEntity, IEnumerable<TField>
  {
    #region Fields

    /// <summary>
    /// The fields.
    /// </summary>
    private readonly List<TField> fields = new List<TField>();

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TSection"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    public TSection(string name)
      : this(name, ID.NewID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TSection"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    public TSection(string name, ID id)
      : base(name, id)
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the fields.
    /// </summary>
    public IEnumerable<TField> Fields
    {
      get
      {
        return this.fields;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The add.
    /// </summary>
    /// <param name="fieldName">
    /// The field name.
    /// </param>
    public void Add(string fieldName)
    {
      this.Add(fieldName, ID.NewID);
    }

    /// <summary>
    /// The add.
    /// </summary>
    /// <param name="fieldNames">
    /// The field names.
    /// </param>
    public void Add(IEnumerable<string> fieldNames)
    {
      foreach (string fieldName in fieldNames)
      {
        this.Add(fieldName);
      }
    }

    /// <summary>
    /// The add.
    /// </summary>
    /// <param name="fieldName">
    /// The field name.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    public void Add(string fieldName, ID id)
    {
      this.Add(fieldName, id, "Single-Line Text");
    }

    /// <summary>
    /// The add.
    /// </summary>
    /// <param name="fieldName">
    /// The field name.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="fieldType">
    /// The field type.
    /// </param>
    public void Add(string fieldName, ID id, string fieldType)
    {
      Assert.ArgumentNotNullOrEmpty(fieldName, "fieldName");
      Assert.ArgumentNotNull(id, "id");

      this.Add(new TField(fieldName, id, fieldType));
    }

    /// <summary>
    /// The add.
    /// </summary>
    /// <param name="field">
    /// The field.
    /// </param>
    public void Add(TField field)
    {
      Assert.ArgumentNotNull(field, "field");

      this.fields.Add(field);
    }

    /// <summary>
    /// The get enumerator.
    /// </summary>
    /// <returns>
    /// The <see cref="IEnumerator"/>.
    /// </returns>
    public IEnumerator<TField> GetEnumerator()
    {
      return this.Fields.GetEnumerator();
    }

    #endregion

    #region Explicit Interface Methods

    /// <summary>
    /// The get enumerator.
    /// </summary>
    /// <returns>
    /// The <see cref="IEnumerator"/>.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable<TField>)this).GetEnumerator();
    }

    #endregion
  }
}