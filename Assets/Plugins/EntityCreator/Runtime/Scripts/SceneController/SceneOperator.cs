using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EntityCreator
{
    /// <summary>
    /// An abstract class for managing Unity scenes within the EntityCreator namespace.
    /// </summary>
    public abstract class SceneOperator : MonoBehaviour
    {
        /// <summary>
        /// Flag to lock scene input
        /// </summary>
        public static bool lockScene = true;
        /// <summary>
        /// Async operation option for load
        /// </summary>
        public static AsyncOperation asyncOperation;

        /// <summary>
        /// Opens a Unity scene by name with optional loading mode and duplicate scene handling.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        /// <param name="mode">The loading mode for the scene (Single or Additive).</param>
        /// <param name="ignoreDuplicatedScenes">Determines whether to ignore loading duplicates.</param>
        /// <returns>True if the scene was successfully opened; otherwise, false.</returns>
        public static bool OpenScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, bool ignoreDuplicatedScenes = false)
        {
            // Check if the sceneName parameter is not empty or null.
            if (!string.IsNullOrEmpty(sceneName))
            {
                // Avoid loading duplicate scenes if the mode is set to Additive and duplicate scenes should not be ignored.
                if (mode == LoadSceneMode.Additive && !ignoreDuplicatedScenes)
                {
                    // Get a reference to the scene by its name.
                    var scene = SceneManager.GetSceneByName(sceneName);

                    // If the scene is already loaded, return true to indicate success.
                    if (scene.isLoaded) return true;
                }

                // Load the scene asynchronously with the specified mode.
                SceneManager.LoadSceneAsync(sceneName, mode);
            }

            // Return false to indicate that the scene was not loaded.
            return false;
        }

        /// <summary>
        /// Unloads a Unity scene by name.
        /// </summary>
        /// <param name="sceneName">The name of the scene to unload.</param>
        public static void HideScene(string sceneName)
        {
            // Check if the sceneName parameter is not empty or null.
            if (!string.IsNullOrEmpty(sceneName))
            {
                // Unload the scene asynchronously.
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }

    /*    public void SetTransitionIn(Action onTransitionEnd)
        {
            TransitionController.OpenScene(new TransitionStruct
            {
                fadeType = FadeType.Out,
                onTransitionEnd = () =>
                {
                    EventDispatcher.Dispatch<PlayAudioStruct>(new PlayAudioStruct
                    {
                        soundName = "Menu"
                    });
                    lockScene = false;
                    TransitionController.HideScene(TransitionController.SCENE_NAME);
                }

            }, TransitionController.SCENE_NAME, LoadSceneMode.Additive);
        }*/
    }
}
