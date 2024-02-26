#if UNITY_EDITOR

using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EntityCreator
{
    /// <summary>
    /// A class that inherits from EditorWindow, used to create custom windows in the Unity Editor.
    /// </summary>
    public class EntityCreationWindow : EditorWindow
    {
        public static string entityName;
        public static string entityNameToDelete;
        public static int selected = 0;

        private void OnGUI()
        {
            EditorGUILayout.Space(15); // Space between GUI elements

            // Styles for labels and text area
            var labelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            var textAreaStyle = new GUIStyle(GUI.skin.textArea) { alignment = TextAnchor.MiddleCenter };

            // Informative label
            EditorGUILayout.LabelField("\n" + "FOLLOW ALL STEPS BELLOW TO CREATE YOUR ENTITY!" + "\n", labelStyle, GUILayout.ExpandWidth(true));

            EditorGUILayout.Space(25); // Space between GUI elements

            // Label and text field to input the entity name
            EditorGUILayout.LabelField("1 - Write your entity name", labelStyle, GUILayout.ExpandWidth(true));
            EditorGUILayout.LabelField("NOTE - Use words to that describe easily your entity", labelStyle, GUILayout.ExpandWidth(true));

            entityName = EditorGUILayout.TextField(entityName, textAreaStyle, GUILayout.ExpandWidth(true));

            // Space between GUI elements
            EditorGUILayout.Space(10);

            // Label and button to create the base entity folders and scripts
            EditorGUILayout.LabelField("2 - Create base entity folders scene and scripts", labelStyle, GUILayout.ExpandWidth(true));
            EditorGUILayout.LabelField("NOTE - Entity folder is created on Assets folder", labelStyle, GUILayout.ExpandWidth(true));

            GUI.SetNextControlName("CreateEntityButton");

            // Button that triggers folder and script creation
            if (GUILayout.Button("Create entity"))
            {
                CreateBaseEntity();
                // Set focus on the button after clicking
                GUI.FocusControl("CreateEntityButton");
            }

            EditorGUILayout.Space(10); // Space between GUI elements

            // Label and button to create the prefabs
            EditorGUILayout.LabelField("3 - Create prefabs with created scripts", labelStyle, GUILayout.ExpandWidth(true));
            EditorGUILayout.LabelField("NOTE - Wait for scripts creation and then press 'Create prefabs' button", labelStyle, GUILayout.ExpandWidth(true));

            // Button that triggers prefab creation
            if (GUILayout.Button("Create prefabs"))
            {
                CreatePrefabs();
            }

            EditorGUILayout.Space(20); // Space between GUI elements

            // Label and button to create the prefabs
            EditorGUILayout.LabelField("Delete entity", labelStyle, GUILayout.ExpandWidth(true));
            EditorGUILayout.LabelField("NOTE - Not working correctly, currently just delete the files and the folder still ", labelStyle, GUILayout.ExpandWidth(true));

            entityNameToDelete = EditorGUILayout.TextField(entityNameToDelete, textAreaStyle, GUILayout.ExpandWidth(true));

            GUI.SetNextControlName("Delete entity");
            // Button that triggers prefab creation
            if (GUILayout.Button("Delete entity"))
            {
                DeleteEntity();
                GUI.FocusControl("Delete entity");
            }
        }

        [MenuItem("EntityCreator/Entity/Create entity")]
        public static void ShowWindow()
        {
            GetWindow<EntityCreationWindow>("Entity creator"); // Create an instance of the custom window
        }

        /// <summary>
        /// Method to create the base folders for the entity and its scripts.
        /// </summary>
        public static void CreateBaseEntity()
        {
            if (entityName == null)
            {
                Debug.Log("Please write an entity name!");
                return;
            }

            if (FindAsset(entityName + "Controller"))
            {
                Debug.Log("This entity already exists");
                return;
            }

            string entityFolder = entityName; // Get the entity name from the variable
            string sceneFolder = "_Scene"; // Name of the folder for scenes
            string scriptsFolder = "Scripts"; // Name of the folder for scripts
            string prefabsFolder = "Prefabs"; // Name of the folder for prefabs

            string basePath = "Assets"; // Base path for folder creation

            // Create the base folders for the entity
            AssetDatabase.CreateFolder(basePath, entityFolder);
            AssetDatabase.CreateFolder(Path.Combine(basePath, entityFolder), sceneFolder);
            AssetDatabase.CreateFolder(Path.Combine(basePath, entityFolder), scriptsFolder);
            AssetDatabase.CreateFolder(Path.Combine(basePath, entityFolder), prefabsFolder);

            // Generate and create the controller script
            string controllerScriptContent = GenerateControllerScript(entityFolder);
            CreateScript(Path.Combine(basePath, entityFolder, scriptsFolder), entityFolder + "Controller.cs", controllerScriptContent);

            // Generate and create the user interface script
            string uiScriptContent = GenerateUIScript(entityFolder);
            CreateScript(Path.Combine(basePath, entityFolder, scriptsFolder), "UI" + entityFolder + ".cs", uiScriptContent);

            // Generate and create the controller script
            string structScriptContent = GenerateStructScript(entityFolder);
            CreateScript(Path.Combine(basePath, entityFolder, scriptsFolder), entityFolder + "Struct.cs", structScriptContent);

            // Refresh the asset database in Unity
            AssetDatabase.Refresh();

            // Create a new empty scene and save it with the entity name
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            EditorSceneManager.SaveScene(scene, Path.Combine(basePath, entityFolder, sceneFolder, entityFolder + ".unity"));
            EditorSceneManager.CloseScene(scene, true);
        }

        /// <summary>
        /// Method to create the entity's prefabs.
        /// </summary>
        public void CreatePrefabs()
        {
            string entityFolder = SceneManager.GetActiveScene().name; // Get the active scene name as the entity name
            string prefabsFolder = "Prefabs"; // Name of the folder for prefabs

            // Create a prefab for the entity's controller
            GameObject controllerPrefab = EntityCreatorGlobalTools.LoadPrefabFromFile("Controller") as GameObject;
            if (controllerPrefab != null)
            {
                GameObject controllerInstance = Instantiate(controllerPrefab, Vector3.zero, Quaternion.identity);
                controllerInstance.name = entityFolder + "Controller";

                // Get the script type based on the name
                System.Type MyScriptType = System.Type.GetType(entityFolder + "Controller" + ",Assembly-CSharp");

                // Add the script component to the object
                controllerInstance.AddComponent(MyScriptType);

                // Convert the controller instance into a prefab and save it in the "prefabs" folder
                GameObject controllerPrefabInstance = PrefabUtility.SaveAsPrefabAsset(controllerInstance, Path.Combine("Assets", entityFolder, prefabsFolder, entityFolder + "Controller.prefab"));
                DestroyImmediate(controllerInstance);

                // Add the prefab instance to the scene
                PrefabUtility.InstantiatePrefab(controllerPrefabInstance);
            }

            // Create a prefab for the user interface
            GameObject uiPrefab = EntityCreatorGlobalTools.LoadPrefabFromFile("UI") as GameObject;
            if (uiPrefab != null)
            {
                GameObject uiInstance = Instantiate(uiPrefab, Vector3.zero, Quaternion.identity);
                uiInstance.name = "UI" + entityFolder;

                // Get the script type based on the name
                System.Type MyScriptType = System.Type.GetType("UI" + entityFolder + ",Assembly-CSharp");

                // Add the script component to the object
                uiInstance.AddComponent(MyScriptType);

                // Convert the controller instance into a prefab and save it in the "prefabs" folder
                GameObject controllerPrefabInstance = PrefabUtility.SaveAsPrefabAsset(uiInstance, Path.Combine("Assets", entityFolder, prefabsFolder, "UI" + entityFolder + ".prefab"));
                DestroyImmediate(uiInstance);

                // Add the prefab instance to the scene
                PrefabUtility.InstantiatePrefab(controllerPrefabInstance);
            }
        }

        /// <summary>
        /// Delete an entity by name.
        /// </summary>
        public void DeleteEntity()
        {
            if (!FindAsset(entityNameToDelete + "Controller"))
            {
                Debug.Log("This entity does not exist!");
                return;
            }

            var path = Path.GetDirectoryName(GetScriptFilePath(entityNameToDelete + "Controller"));

            DirectoryInfo parentDirFromFile = Directory.GetParent(path);
            // var folderName = parentDirFromFile.Parent + parentDirFromFile.FullName;

            FileUtil.DeleteFileOrDirectory(parentDirFromFile.FullName);
            AssetDatabase.Refresh();
            // Cleanup();
            // AssetDatabase.Refresh();
        }

        /// <summary>
        /// Generate an entity controller by name.
        /// </summary>
        /// <param name="entityName">Controller's name</param>
        /// <returns>The generated content of the controller script.</returns>
        private static string GenerateControllerScript(string entityName)
        {
            return $"using EntityCreator;\n" +
                   $"using UnityEngine;\n\n" +
                   $"public class {entityName}Controller : SceneDataOperator<{entityName}Struct>\n" +
                   $"{{\n" +
                   $"    public const string SCENE_NAME = \"{entityName}\";\n" +
                   $"    [SerializeField] private UI{entityName} ui{entityName};\n"+
                   $"    public override void Initialize({entityName}Struct data)\n" +
                   $"    {{\n" +
                   $"        ui{entityName}.Initialize();\n" +
                   $"    }}\n\n"  +
                   $"}}\n\n" ;
        }

        /// <summary>
        /// Generate an entity UI script by name.
        /// </summary>
        /// <param name="entityName">UI's name</param>
        /// <returns>The generated content of the UI script.</returns>
        private static string GenerateUIScript(string entityName)
        {
            return $"using UnityEngine;\n\n" +
                   $"public class UI{entityName} : MonoBehaviour\n" +
                   $"{{\n" +
                   $"    public void Initialize()\n" +
                   $"    {{\n\n" +
                   $"    }}\n" +
                   $"}}";
        }

        /// <summary>
        /// Genereta screen data struct
        /// </summary>
        /// <param name="entityName"> struct name </param>
        /// <returns></returns>
        private static string GenerateStructScript(string entityName)
        {
            return $"using EntityCreator;\n\n" +
                   $"public struct {entityName}Struct\n" +
                   $"{{\n" +
                   $"}}";
        }

        /// <summary>
        /// Create a script file.
        /// </summary>
        /// <param name="path">Path to create the script.</param>
        /// <param name="scriptName">Script name.</param>
        /// <param name="content">Script content.</param>
        private static void CreateScript(string path, string scriptName, string content)
        {
            File.WriteAllText(Path.Combine(path, scriptName), content);
        }

        /// <summary>
        /// Find assets in the Assets folder by name.
        /// </summary>
        /// <param name="assetName">Asset name to look for.</param>
        /// <returns>True if the asset is found; otherwise, false.</returns>
        private static bool FindAsset(string assetName)
        {
            string assetNameToFind = assetName;

            // Use AssetDatabase.FindAssets to find the assets with the specified name
            string[] assetGUIDs = AssetDatabase.FindAssets(assetNameToFind);

            if (assetGUIDs.Length > 0)
            {
                foreach (string assetGUID in assetGUIDs)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Return the file path of an entity by name.
        /// </summary>
        /// <param name="assetName">Entity name.</param>
        /// <returns>The file path of the entity script.</returns>
        private static string GetScriptFilePath(string assetName)
        {
            string scriptNameToFind = assetName;

            MonoScript[] scripts = MonoImporter.GetAllRuntimeMonoScripts();

            foreach (MonoScript script in scripts)
            {
                if (script != null && script.name == scriptNameToFind)
                {
                    string scriptFilePath = AssetDatabase.GetAssetPath(script);
                    Debug.Log("Script Path: " + scriptFilePath);
                    return scriptFilePath;
                }
            }

            return null;
        }

        /*  private static string deletedFolders;

          [MenuItem("Tools/Clean Empty Folders")]
          private static void Cleanup()
          {
              deletedFolders = string.Empty;

              var directoryInfo = new DirectoryInfo(Application.dataPath);
              foreach (var subDirectory in directoryInfo.GetDirectories("*.*", SearchOption.AllDirectories))
              {
                  if (subDirectory.Exists)
                  {
                      ScanDirectory(subDirectory);
                  }
              }

              AssetDatabase.Refresh();

              Debug.Log("Deleted Folders:\n" + (deletedFolders.Length > 0 ? deletedFolders : "NONE"));
          }

          private static string ScanDirectory(DirectoryInfo subDirectory)
          {
              Debug.Log("Scanning Directory: " + subDirectory.FullName);

              var filesInSubDirectory = subDirectory.GetFiles("*.*", SearchOption.AllDirectories);

              if (filesInSubDirectory.Length == 0 || !filesInSubDirectory.Any(t => t.FullName.EndsWith(".meta") == false))
              {
                  deletedFolders += subDirectory.FullName + "\n";
                  subDirectory.Delete(true);
              }

              return deletedFolders;
          }*/
    }

    public enum ScreenType
    {
        Usings,
        Core
    }
}

#endif
