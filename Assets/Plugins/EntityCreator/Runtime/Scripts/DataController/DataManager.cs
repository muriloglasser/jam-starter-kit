using System;
using System.Collections.Generic;
using UnityEngine;

namespace EntityCreator
{

    /// <summary>
    /// A utility class for managing and caching structured data of various types.
    /// </summary>
    /// <remarks>
    /// The DataManager class allows you to store, retrieve, and remove structured data in a cache based on their type.
    /// It provides methods to add, get, and remove data items, as well as to check if data of a specific type is already cached.
    /// </remarks>
    [ExecuteInEditMode]
    public static class DataManager
    {
        /// <summary>
        /// Store data dictionary
        /// </summary>
        private static Dictionary<Type, object> dataStorage;

        static DataManager()
        {
            dataStorage = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Adds an item to the data cache.
        /// </summary>
        /// <typeparam name="T">The type of data to add.</typeparam>
        /// <param name="data">The data to be added.</param>
        public static void AddData<T>(T data) where T : struct
        {
            if (DataExists<T>())
                RemoveData<T>(); // Remove any existing data of the same type.

            dataStorage.Add(typeof(T), data); // Add the new data to the cache.
        }

        /// <summary>
        /// Retrieves a cached item by its type.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve.</typeparam>
        /// <returns>The cached data of the specified type.</returns>
        public static T GetData<T>() where T : struct
        {
            object item;
            dataStorage.TryGetValue(typeof(T), out item);

            if (item != null)
                return (T)item; // Return the cached data.
            else
                return default; // Return the default value for the specified type.
        }

        /// <summary>
        /// Retrieves all cached data.
        /// </summary>
        /// <returns>A dictionary containing all cached data.</returns>
        public static Dictionary<Type, object> GetAllData()
        {
            return dataStorage; // Return the dictionary containing all cached data.
        }

        /// <summary>
        /// Removes a cached item.
        /// </summary>
        /// <typeparam name="T">The type of data to remove.</typeparam>
        public static void RemoveData<T>() where T : struct
        {
            Type dataType = typeof(T);

            if (DataExists<T>())
                dataStorage.Remove(dataType); // Remove the cached data of the specified type.
        }

        /// <summary>
        /// Checks if data of a specific type is already cached.
        /// </summary>
        /// <typeparam name="T">The type of data to check.</typeparam>
        /// <returns>True if data of the specified type is cached, otherwise false.</returns>
        private static bool DataExists<T>() where T : struct
        {
            return dataStorage.ContainsKey(typeof(T)); // Check if data of the specified type exists in the cache.
        }
    }
}
