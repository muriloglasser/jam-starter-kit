#if UNITY_EDITOR  

using System.IO;
using UnityEditor;
using UnityEngine;

namespace EntityCreator
{
    /// <summary>
    /// A class that provides useful global tools for the Entity Creator in the Unity Editor.
    /// </summary>
    public class EntityCreatorGlobalTools : MonoBehaviour
    {
        /// <summary>
        /// Gets the selected path or defaults to "Assets" if none is selected.
        /// </summary>
        /// <returns>The selected path or "Assets" as the default.</returns>
        public static string GetSelectedPathOrFallback()
        {
            string path = "Assets";  // Default path is set to "Assets."

            foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);  // Get the path of the selected asset.
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);  // If the path is valid and the file exists, extract the directory path.
                    break;  // Exit the loop.
                }
            }
            return path;  // Return the selected or fallback path.
        }

        /// <summary>
        /// Loads a prefab from a specified filename located in the "Mobile creator/Prefabs" folder in Unity's Resources.
        /// </summary>
        /// <param name="filename">The prefab file to load.</param>
        /// <returns>The loaded prefab.</returns>
        public static UnityEngine.Object LoadPrefabFromFile(string filename)
        {
            Debug.Log("Trying to load LevelPrefab from file (" + filename + ")...");
            var loadedObject = Resources.Load("EntityCreator/Prefabs/" + filename);  // Load the prefab using the specified filename.
            if (loadedObject == null)
            {
                throw new FileNotFoundException("No file found - please check the configuration.");  // Throw an exception if the prefab is not found.
            }
            return loadedObject;  // Return the loaded prefab.
        }
    }
}
#endif
