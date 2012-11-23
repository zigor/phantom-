namespace Sitecore.TestKit.Web
{
  using System;
  using System.Web;

  using Sitecore.Configuration;
  using Sitecore.TestKit.Configuration;

  /// <summary>
  /// The http context extensions.
  /// </summary>
  public static class HttpContextExtensions
  {
    #region Public Methods and Operators

    /// <summary>
    /// Executes the function in the disabled context.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <param name="action">
    /// The action.
    /// </param>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T WhenContextNull<T>(this HttpContext context, Func<T> action)
    {
      HttpContext saveContext = HttpContext.Current;
      HttpContext.Current = null;

      Factory.Reset();

      T result = action();
      HttpContext.Current = saveContext;

      Instance.Prepare();
      return result;
    }

    #endregion
  }
}