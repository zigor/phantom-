namespace Sitecore.TestKit.Web
{
  using System;
  using System.Collections;
  using System.Collections.Specialized;
  using System.Web;
  using System.Web.SessionState;

  /// <summary>
  /// The memory http session state.
  /// </summary>
  public class MemoryHttpSessionState : IHttpSessionState
  {
    #region Fields

    /// <summary>
    /// The collection.
    /// </summary>
    private readonly SessionStateItemCollection collection = new SessionStateItemCollection();

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryHttpSessionState"/> class.
    /// </summary>
    public MemoryHttpSessionState()
    {
      this.SessionID = Guid.Empty.ToString();
      this.IsReadOnly = false;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the code page.
    /// </summary>
    public int CodePage { get; set; }

    /// <summary>
    /// Gets or sets the cookie mode.
    /// </summary>
    public HttpCookieMode CookieMode { get; set; }

    /// <summary>
    /// Gets the count.
    /// </summary>
    public int Count
    {
      get
      {
        return this.collection.Count;
      }
    }

    /// <summary>
    /// Gets a value indicating whether is abandoned.
    /// </summary>
    public bool IsAbandoned { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether is cookieless.
    /// </summary>
    public bool IsCookieless { get; set; }

    /// <summary>
    /// Gets a value indicating whether is new session.
    /// </summary>
    public bool IsNewSession { get; private set; }

    /// <summary>
    /// Gets a value indicating whether is read only.
    /// </summary>
    public bool IsReadOnly { get; private set; }

    /// <summary>
    /// Gets a value indicating whether is synchronized.
    /// </summary>
    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Gets the keys.
    /// </summary>
    public NameObjectCollectionBase.KeysCollection Keys
    {
      get
      {
        return this.collection.Keys;
      }
    }

    /// <summary>
    /// Gets or sets the lcid.
    /// </summary>
    public int LCID { get; set; }

    /// <summary>
    /// Gets or sets the mode.
    /// </summary>
    public SessionStateMode Mode { get; set; }

    /// <summary>
    /// Gets the session id.
    /// </summary>
    public string SessionID { get; private set; }

    /// <summary>
    /// Gets the static objects.
    /// </summary>
    public HttpStaticObjectsCollection StaticObjects { get; private set; }

    /// <summary>
    /// Gets the sync root.
    /// </summary>
    public object SyncRoot
    {
      get
      {
        return this;
      }
    }

    /// <summary>
    /// Gets or sets the timeout.
    /// </summary>
    public int Timeout { get; set; }

    #endregion

    #region Explicit Interface Indexers

    /// <summary>
    /// The i http session state.this.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    object IHttpSessionState.this[string name]
    {
      get
      {
        return this.collection[name];
      }

      set
      {
        this.collection[name] = value;
      }
    }

    /// <summary>
    /// The i http session state.this.
    /// </summary>
    /// <param name="index">
    /// The index.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    object IHttpSessionState.this[int index]
    {
      get
      {
        return this.collection[index];
      }

      set
      {
        this.collection[index] = value;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The abandon.
    /// </summary>
    public void Abandon()
    {
      this.IsAbandoned = true;
    }

    /// <summary>
    /// The add.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    public void Add(string name, object value)
    {
      this.collection[name] = value;
    }

    /// <summary>
    /// The clear.
    /// </summary>
    public void Clear()
    {
      this.collection.Clear();
    }

    /// <summary>
    /// The copy to.
    /// </summary>
    /// <param name="array">
    /// The array.
    /// </param>
    /// <param name="index">
    /// The index.
    /// </param>
    public void CopyTo(Array array, int index)
    {
      IEnumerator enumerator = this.GetEnumerator();
      while (enumerator.MoveNext())
      {
        array.SetValue(enumerator.Current, index++);
      }
    }

    /// <summary>
    /// The get enumerator.
    /// </summary>
    /// <returns>
    /// The <see cref="IEnumerator"/>.
    /// </returns>
    public IEnumerator GetEnumerator()
    {
      return this.collection.GetEnumerator();
    }

    /// <summary>
    /// The remove.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    public void Remove(string name)
    {
      this.collection.Remove(name);
    }

    /// <summary>
    /// The remove all.
    /// </summary>
    public void RemoveAll()
    {
      this.collection.Clear();
    }

    /// <summary>
    /// The remove at.
    /// </summary>
    /// <param name="index">
    /// The index.
    /// </param>
    public void RemoveAt(int index)
    {
      this.collection.RemoveAt(index);
    }

    #endregion
  }
}