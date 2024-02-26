using System.Collections;
using UnityEngine.SceneManagement;

namespace EntityCreator
{
    /// <summary>
    /// An abstract class used to manage scenes with associated data of generic type T.
    /// </summary>
    /// <typeparam name="T">The generic data structure to load with the scene.</typeparam>
    public abstract class SceneDataOperator<T> : SceneOperator where T : struct
    {
        /// <summary>
        /// Opens a scene with associated data and provides options for handling duplicates.
        /// </summary>
        /// <param name="data">The data structure to load with the current scene.</param>
        /// <param name="sceneName">The name of the scene to open.</param>
        /// <param name="mode">The load mode for the scene (Single, Additive, etc.).</param>
        /// <param name="ignoreDuplicatedScenes">Determines whether to ignore duplicated scenes.</param>
        public static void OpenScene(T data, string sceneName, LoadSceneMode mode, bool ignoreDuplicatedScenes = false)
        {
            DataManager.AddData(data);  // Add the provided data to a data controller.

            bool alreadyLoaded = OpenScene(sceneName, mode, ignoreDuplicatedScenes);  // Attempt to open the specified scene with the given options and check if it's already loaded.

            if (alreadyLoaded)
                FindObjectOfType<SceneDataOperator<T>>().Initialize(data);  // If the scene is already loaded, find an instance of SceneDataOperator<T> and initialize it with the provided data.
        }       

        void Start()
        {
            // Get data of type T from a data controller.
            var data = DataManager.GetData<T>();
            // Call the abstract Initialize method with the retrieved data.
            Initialize(data);
        }

        /// <summary>
        /// Initializes the scene with data of generic type T.
        /// This method must be implemented by derived classes.
        /// </summary>
        /// <param name="data">The data of generic type T to initialize the scene.</param>
        public abstract void Initialize(T data);
    }
}
