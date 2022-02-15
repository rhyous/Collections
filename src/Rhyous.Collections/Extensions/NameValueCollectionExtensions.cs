namespace Rhyous.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    /// <summary>Extensions for <see cref="NameValueCollection"/>.</summary>
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// While this method works with some other <see cref="NameValueCollection"/> instances, it was specifically designed to make getting a typed value back from an app.config or web.config using ConfigurationManager.AppSettings.
        /// Example usage to return a bool:
        ///   Web.config appseting
        ///     {appSettings file="dev.config"}
        ///         {add key="Setting1" value="true" /}
        ///     {/appSettings}
        ///   Line of code:  
        ///     ConfigurationManager.AppSettings.Get("Setting1", true);
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="collection">The <see cref="NameValueCollection"/>, usually from ConfigurationManager.AppSettings.</param>
        /// <param name="key">The key is a string identifier of the item in the collection.</param>
        /// <param name="defaultValue">If the key is not found, or if the value is null, empty, or whitespace, or cannot be converted, this specified default value is used.</param>
        /// <returns></returns>
        public static T Get<T>(this NameValueCollection collection, string key, T defaultValue)
        {
            if (collection == null || string.IsNullOrEmpty(key))
                return defaultValue;
            var value = collection[key];
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (string.IsNullOrWhiteSpace(value) || !converter.IsValid(value))
            {
                return defaultValue;
            }
            return (T)(converter.ConvertFromInvariantString(value));
        }

        /// <summary>Clones the <see cref="NameValueCollection"/>.</summary>
        /// <param name="collection">The collection to clone.</param>
        /// <param name="keysToExclude">The keys to exclude.</param>
        /// <returns>A clone of the <see cref="NameValueCollection"/>, without the keys to exclude.</returns>
        public static NameValueCollection Clone(this NameValueCollection collection, params string[] keysToExclude)
        {
            return collection.Clone(null, keysToExclude);
        }

        /// <summary>Clones the <see cref="NameValueCollection"/>.</summary>
        /// <param name="collection">The collection to clone.</param>
        /// <param name="comparer">The <see cref="T:IEqualityComparer{string}"/> for matching the key. If null, <see cref="StringComparer.CurrentCultureIgnoreCase"/> is used.</param>
        /// <param name="keysToExclude">The keys to exclude.</param>
        /// <returns>A clone of the <see cref="NameValueCollection"/>, without the keys to exclude.</returns>
        public static NameValueCollection Clone(this NameValueCollection collection, IEqualityComparer<string> comparer, params string[] keysToExclude)
        {
            var clonedCollection = new NameValueCollection();
            if (comparer == null)
                comparer = StringComparer.CurrentCultureIgnoreCase;
            foreach (string key in collection.Keys)
            {
                if (!keysToExclude.Contains(key, comparer))
                    clonedCollection.Add(key, collection[key]);
            }
            return clonedCollection;
        }

        /// <summary>
        /// A method that converts a NameValueCollection back to a Url query string.
        /// </summary>
        /// <param name="collection">the name value collection.</param>
        /// <param name="prefix">Default: "?". Most Url parameters start with a "?". Set this to "" to not have the starting question mark.</param>
        /// <param name="separator">Default: "&amp;". Most Url parameters are separated with an ampersand "&amp;".</param>
        /// <returns>The NameValueCollection as a Url query string.</returns>
        public static string ToUrlQueryString(this NameValueCollection collection, string prefix = "?", string separator = "&")
        {
            if (collection == null || collection.Count == 0)
                return string.Empty;

            var sbQuery = new StringBuilder();

            foreach (string key in collection)
            {
                if (string.IsNullOrWhiteSpace(key))
                    continue;

                string[] values = collection.GetValues(key);

                // Handle bool keys, where the key value is blank.
                if (values == null)
                {
                    sbQuery.Append(sbQuery.Length == 0 ? prefix : separator);
                    sbQuery.Append(Uri.EscapeDataString(key));
                    continue;
                }

                // Handle keys with one or more values
                foreach (string value in values)
                {
                    sbQuery.Append(sbQuery.Length == 0 ? prefix : separator);
                    sbQuery.Append($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}");
                }
            }
            return sbQuery.ToString();
        }

        /// <summary>Appends a value to a NameValueCollection key, using a comma as a separator.</summary>
        /// <param name="collection">The NameValueCollection</param>
        /// <param name="key">The key.</param>
        /// <param name="append">The value to append.</param>
        /// <remarks>Null and empty strings are not considered values and will not be included in AppdendToValue.
        /// However, whitespace is considered valid.</remarks>
        public static void AppendToValue(this NameValueCollection collection, string key, string append)
        {
            if (collection is null) { throw new ArgumentNullException(nameof(collection)); }
            if (string.IsNullOrEmpty(key)) { throw new ArgumentException("message", nameof(key)); }
            if (string.IsNullOrEmpty(append)) { return; }

            var currentValue = collection.Get(key, "");
            currentValue = string.IsNullOrEmpty(currentValue) ? append : $"{currentValue},{append}";
            collection.Set(key, currentValue);
        }
    }
}
