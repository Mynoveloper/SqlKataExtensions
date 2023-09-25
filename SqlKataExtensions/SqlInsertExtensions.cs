using SqlExtensionsTester.Common;
using SqlExtensionsTester.Core;
using SqlKata;
using System.Reflection;

namespace SqlExtensionsTester.SqlKataExtensions
{
    public static class SqlInsertExtensions
    {
        /// <summary>
        /// Gets the column names.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <param name="objectFormat">The object name format.</param>
        /// <returns>A list of column names.</returns>
        private static List<string> GetColumnNames(this IEnumerable<PropertyInfo> properties, SqlFormat objectFormat = SqlFormat.None)
        {
            return properties.Select(p => Utils.FormatObjectName(p.Name, objectFormat)).ToList();
        }

        /// <summary>
        /// Returns a collection of all value type or string properties of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of object to get the properties for.</typeparam>
        /// <returns>A new <see cref="IEnumerable{PropertyInfo}"/> containing the valid properties.</returns>
        private static IEnumerable<PropertyInfo> GetValidProperties<T>()
        {
            return typeof(T).GetProperties().Where(p => p.PropertyType.IsValueType || p.PropertyType == typeof(string));
        }

        /// <summary>
        /// Adds an object of type <typeparamref name="T"/> to an insert query.
        /// </summary>
        /// <typeparam name="T">The type of object to add.</typeparam>
        /// <param name="query">The base SQLKata query object to extend.</param>
        /// <param name="entity">The object to add.</param>
        /// <param name="objectFormat">The object name format to use.</param>
        /// <returns>The same SQLKata <see cref="Query"/>.</returns>
        public static Query Add<T>(this Query query, T entity, SqlFormat objectFormat = SqlFormat.None)
        {
            // Get the properties of the T element
            var properties = GetValidProperties<T>();

            // Create a data array for the object
            var data = properties.Select(p => p.GetValue(entity)).ToArray();

            // Add the table to the query
            query.From(Utils.FormatObjectName(typeof(T).Name, objectFormat));

            // Add the data to the query as an insert statement
            query.AsInsert(properties.GetColumnNames(objectFormat), new List<object?[]> { data });

            return query;
        }

        /// <summary>
        /// Adds multiple objects of the base type of <typeparamref name="T"/> to an insert query.
        /// </summary>
        /// <typeparam name="T">The type of object to add.</typeparam>
        /// <param name="query">The base SQLKata query object to extend.</param>
        /// <param name="objects">The list of objects to add.</param>
        /// <param name="objectFormat">The object name format to use.</param>
        /// <returns>The same SQLKata <see cref="Query"/>.</returns>
        public static Query AddMany<T>(this Query query, List<T> objects, SqlFormat objectFormat = SqlFormat.None)
        {
            // Filter only the properties that are value types or strings
            var properties = GetValidProperties<T>();

            // Create a data array for each object
            List<object?[]> data = objects.Select(item =>
            {
                // Get the values of the properties for each object
                return properties.Select(p => p.GetValue(item)).ToArray();
            }).ToList();

            // Add the table to the query
            query.From(Utils.FormatObjectName(typeof(T).Name, objectFormat));

            // Add the data to the query as an insert statement
            query.AsInsert(properties.GetColumnNames(objectFormat), data);

            return query;
        }
    }
}
