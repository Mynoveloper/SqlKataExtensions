using SqlExtensionsTester.Common;
using SqlExtensionsTester.Core;
using SqlKata;

namespace SqlExtensionsTester.SqlKataExtensions
{
    public static class SqlSelectExtensions
    {
        /// <summary>
        /// Adds the table to the query.
        /// </summary>
        /// <typeparam name="T">The type of object that represents the table.</typeparam>
        /// <param name="query">The base SQLKata query object to extend.</param>
        /// <param name="objectFormat">The object name format to use.</param>
        /// <returns>The same SQLKata <see cref="Query"/>.</returns>
        public static Query From<T>(this Query query, SqlFormat objectFormat = SqlFormat.None)
        {
            query.From(Utils.FormatObjectName(typeof(T).Name, objectFormat));

            return query;
        }

        /// <summary>
        /// Adds all columns of a table to a SELECT query.
        /// </summary>
        /// <typeparam name="T">The type of object that represents the table.</typeparam>
        /// <param name="query">The base SQLKata query object to extend.</param>
        /// <param name="objectFormat">The object name format to use.</param>
        /// <returns>The same SQLKata <see cref="Query"/>.</returns>
        public static Query Select<T>(this Query query, SqlFormat objectFormat = SqlFormat.None)
        {
            // Add the table to the query
            query.From<T>(objectFormat);

            // Add all columns of the table that are value types or strings
            Array.ForEach(typeof(T).GetProperties(), property =>
            {
                if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                {
                    query.Select(Utils.FormatObjectName(property.Name, objectFormat));
                }
            });

            return query;
        }
    }
}
