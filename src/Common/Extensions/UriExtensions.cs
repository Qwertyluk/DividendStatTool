using System.Collections.Specialized;
using System.Web;

namespace Common.Extensions
{
    public static class UriExtensions
    {
        public static Uri AddParameter(this Uri uri, string paramName, string paramValue)
        {
            paramName = paramName ?? throw new ArgumentNullException(nameof(paramName));
            paramValue = paramValue ?? throw new ArgumentNullException(nameof(paramValue));
            paramName = paramName == string.Empty ? throw new ArgumentException($"Argument {nameof(paramName)} can't be empty.") : paramName;

            UriBuilder uriBuilder = new UriBuilder(uri);
            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue;
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }
    }
}
