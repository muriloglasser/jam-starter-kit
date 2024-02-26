#if UNITY_EDITOR 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

namespace EntityCreator
{
    /// <summary>
    /// This class defines the RootFoldersCreationWindow within the EntityCreator namespace.
    /// </summary>
    public class RootFoldersCreationWindow : MonoBehaviour
    {
        /// <summary>
        /// Creates the base project folders required for a game project.
        /// </summary>
        [MenuItem("EntityCreator/Create root folders")]
        static void CreateProjectBaseFolders()
        {
            // Define folder names for the project structure.
            string _RootFolder = "_Root";              // The root folder for the project.
            string gameFolder = "Game";                // The main game folder.
            string gameSharedFolder = "_Shared";       // A subfolder for shared assets.
            string gameFluxFolder = "Game Flux";       // A subfolder for game-related scripts and assets.

            // Create the root folder in the project's assets directory.
            AssetDatabase.CreateFolder("Assets", _RootFolder);

            // Create the "Game" folder inside the root folder.
            AssetDatabase.CreateFolder("Assets" + "/" + _RootFolder, gameFolder);

            // Create the "_Shared" folder inside the "Game" folder.
            AssetDatabase.CreateFolder("Assets" + "/" + _RootFolder + "/" + gameFolder, gameSharedFolder);

            // Create the "Game Flux" folder inside the "Game" folder.
            AssetDatabase.CreateFolder("Assets" + "/" + _RootFolder + "/" + gameFolder, gameFluxFolder);
        }
    }
}
#endif 
