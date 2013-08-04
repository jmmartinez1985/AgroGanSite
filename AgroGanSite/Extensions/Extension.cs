using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AgroGanSite.Extensions
{
    public static class Extension
    {
        public static string SerializeToJson(this object obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.All;

            return JsonConvert.SerializeObject(obj, settings).Replace("\\u0027", "\u0027");
        }

        public static T DeserializeFromJson<T>(this string s)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.All;

            return JsonConvert.DeserializeObject<T>(s, settings);
        }

        public static T SaveChanges<T>(this DbContext context, T entity) where T : class
        {
            if (entity == null)
                throw new ArgumentException("Cannot add a null entity.");
            var recordAdded = context.Set<T>().Add(entity);
            context.SaveChanges();
            return recordAdded;
        }

        public static IEnumerable<T> TakeLast<T>(
           this IEnumerable<T> source, int count)
        {
            IList<T> list = (source as IList<T>) ?? source.ToList();
            count = Math.Min(count, list.Count);
            for (int i = list.Count - count; i < list.Count; i++)
            {
                yield return list[i];
            }
        }

        public static string ResolveUrl(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
                return originalUrl;

            // *** Absolute path - just return
            if (IsAbsolutePath(originalUrl))
                return originalUrl;

            // *** We don't start with the '~' -> we don't process the Url
            if (!originalUrl.StartsWith("~"))
                return originalUrl;

            // *** Fix up path for ~ root app dir directory
            // VirtualPathUtility blows up if there is a 
            // query string, so we have to account for this.
            int queryStringStartIndex = originalUrl.IndexOf('?');
            if (queryStringStartIndex != -1)
            {
                string queryString = originalUrl.Substring(queryStringStartIndex);
                string baseUrl = originalUrl.Substring(0, queryStringStartIndex);

                return string.Concat(
                    VirtualPathUtility.ToAbsolute(baseUrl),
                    queryString);
            }
            else
            {
                return VirtualPathUtility.ToAbsolute(originalUrl);
            }

        }

        /// <summary>
        /// This method returns a fully qualified absolute server Url which includes
        /// the protocol, server, port in addition to the server relative Url.
        /// 
        /// Works like Control.ResolveUrl including support for ~ syntax
        /// but returns an absolute URL.
        /// </summary>
        /// <param name="ServerUrl">Any Url, either App relative or fully qualified</param>
        /// <param name="forceHttps">if true forces the url to use https</param>
        /// <returns></returns>
        public static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            if (string.IsNullOrEmpty(serverUrl))
                return serverUrl;

            // *** Is it already an absolute Url?
            if (IsAbsolutePath(serverUrl))
                return serverUrl;

            string newServerUrl = ResolveUrl(serverUrl);
            Uri result = new Uri(HttpContext.Current.Request.Url, newServerUrl);

            if (!forceHttps)
                return result.ToString();
            else
                return ForceUriToHttps(result).ToString();

        }

        /// <summary>
        /// This method returns a fully qualified absolute server Url which includes
        /// the protocol, server, port in addition to the server relative Url.
        /// 
        /// It work like Page.ResolveUrl, but adds these to the beginning.
        /// This method is useful for generating Urls for AJAX methods
        /// </summary>
        /// <param name="ServerUrl">Any Url, either App relative or fully qualified</param>
        /// <returns></returns>
        public static string ResolveServerUrl(string serverUrl)
        {
            return ResolveServerUrl(serverUrl, false);
        }

        /// <summary>
        /// Forces the Uri to use https
        /// </summary>
        private static Uri ForceUriToHttps(Uri uri)
        {
            // ** Re-write Url using builder.
            UriBuilder builder = new UriBuilder(uri);
            builder.Scheme = Uri.UriSchemeHttps;
            builder.Port = 443;

            return builder.Uri;
        }

        private static bool IsAbsolutePath(string originalUrl)
        {
            // *** Absolute path - just return
            int IndexOfSlashes = originalUrl.IndexOf("://");
            int IndexOfQuestionMarks = originalUrl.IndexOf("?");

            if (IndexOfSlashes > -1 &&
                 (IndexOfQuestionMarks < 0 ||
                  (IndexOfQuestionMarks > -1 && IndexOfQuestionMarks > IndexOfSlashes)
                  )
                )
                return true;

            return false;
        }
    }
}