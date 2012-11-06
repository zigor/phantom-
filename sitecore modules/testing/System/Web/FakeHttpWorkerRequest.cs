namespace Sitecore.TestKit.Web
{
  using System;
  using System.Web;

  /// <summary>
  /// The fake http worker request.
  /// </summary>
  public class FakeHttpWorkerRequest : HttpWorkerRequest
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FakeHttpWorkerRequest"/> class.
    /// </summary>
    /// <param name="page">
    /// The page.
    /// </param>
    /// <param name="queryString">
    /// The query string.
    /// </param>
    public FakeHttpWorkerRequest(string page, string queryString)
    {
      this.Page = page;
      this.QueryString = queryString;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the page.
    /// </summary>
    public virtual string Page { get; private set; }

    /// <summary>
    /// Gets the query string.
    /// </summary>
    public virtual string QueryString { get; internal set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The end of request.
    /// </summary>
    public override void EndOfRequest()
    {
    }

    /// <summary>
    /// The flush response.
    /// </summary>
    /// <param name="finalFlush">
    /// The final flush.
    /// </param>
    public override void FlushResponse(bool finalFlush)
    {
    }

    /// <summary>
    /// The get file path.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public override string GetFilePath()
    {
      return "\\";
    }

    /// <summary>
    /// The get http verb name.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public override string GetHttpVerbName()
    {
      return "GET";
    }

    /// <summary>
    /// The get http version.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public override string GetHttpVersion()
    {
      return "HTTP/1.0";
    }

    /// <summary>
    /// The get local address.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public override string GetLocalAddress()
    {
      return "127.0.0.1";
    }

    /// <summary>
    /// The get local port.
    /// </summary>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public override int GetLocalPort()
    {
      return 80;
    }

    /// <summary>
    /// The get query string.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public override string GetQueryString()
    {
      return this.QueryString;
    }

    /// <summary>
    /// The get raw url.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public override string GetRawUrl()
    {
      string queryString = this.GetQueryString();
      if (!string.IsNullOrEmpty(queryString))
      {
        return string.Join("?", new[] { this.Page, queryString });
      }

      return this.Page;
    }

    /// <summary>
    /// The get remote address.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public override string GetRemoteAddress()
    {
      return "127.0.0.1";
    }

    /// <summary>
    /// The get remote port.
    /// </summary>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public override int GetRemotePort()
    {
      return 0;
    }

    /// <summary>
    /// The get uri path.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public override string GetUriPath()
    {
      return this.GetRawUrl();
    }

    /// <summary>
    /// The send known response header.
    /// </summary>
    /// <param name="index">
    /// The index.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    public override void SendKnownResponseHeader(int index, string value)
    {
    }

    /// <summary>
    /// The send response from file.
    /// </summary>
    /// <param name="filename">
    /// The filename.
    /// </param>
    /// <param name="offset">
    /// The offset.
    /// </param>
    /// <param name="length">
    /// The length.
    /// </param>
    public override void SendResponseFromFile(string filename, long offset, long length)
    {
    }

    /// <summary>
    /// The send response from file.
    /// </summary>
    /// <param name="handle">
    /// The handle.
    /// </param>
    /// <param name="offset">
    /// The offset.
    /// </param>
    /// <param name="length">
    /// The length.
    /// </param>
    public override void SendResponseFromFile(IntPtr handle, long offset, long length)
    {
    }

    /// <summary>
    /// The send response from memory.
    /// </summary>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="length">
    /// The length.
    /// </param>
    public override void SendResponseFromMemory(byte[] data, int length)
    {
    }

    /// <summary>
    /// The send status.
    /// </summary>
    /// <param name="statusCode">
    /// The status code.
    /// </param>
    /// <param name="statusDescription">
    /// The status description.
    /// </param>
    public override void SendStatus(int statusCode, string statusDescription)
    {
    }

    /// <summary>
    /// The send unknown response header.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    public override void SendUnknownResponseHeader(string name, string value)
    {
    }

    #endregion
  }
}