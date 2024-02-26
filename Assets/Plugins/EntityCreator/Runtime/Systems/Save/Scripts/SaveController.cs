using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using EntityCreator;

/// <summary>
///  SaveController class handles saving and loading game data.
/// </summary>
public class SaveController : MonoBehaviour
{
    #region Properties

    public SaveData gameSaveData; // Instance of SaveData class to store game data.
    private ISave iSave; // Interface for saving game data.
    public static SaveController saveController; // Static reference to the SaveController instance.
    private const string SAVE_FILE_PATH = "SaveData"; // Path to the save data file.
    public bool isProcessing = false;
    public CanvasGroup autoSaveCanvasGroup;
    public Coroutine autoSaveCoroutine = null;

    #endregion

    #region Unity methods

    public void Start()
    {
        InitializeSave(); // Initialize the save system.
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {

           /* EventDispatcher.Dispatch<StopAudioStruct>(new StopAudioStruct
            {
                soundName = "DopaminSplash"
            });*/
            /*CheckSaveDataDataExistence(data =>
            {
                if (data)
                {
                    Debug.Log("Exists");
                }
                else
                {
                    Debug.Log("Does not exist");
                }
            });*/
        }

        /*if (Input.GetKey(KeyCode.C))
        {
            CreateSaveData(data =>
            {
                Debug.Log("AOPA number created: " + data.aopa);
            });
        }

        if (Input.GetKey(KeyCode.S))
        {
            gameSaveData.aopa = 1;
            SaveData(data =>
            {

            });
        }

        if (Input.GetKey(KeyCode.L))
        {
            LoadData(data =>
            {
                Debug.Log("AOPA number loaded: " + data.aopa);
            });
        }

        if (Input.GetKey(KeyCode.D))
        {
            DeleteSaveData(() =>
            {

            });
        }*/
    }

    #endregion

    #region Core Methods

    /// <summary>
    /// Initialize the save system based on platform (Switch or PC).
    /// </summary>
    public void InitializeSave()
    {
#if !UNITY_EDITOR && PLATFORM_SWITCH
        // Add SwitchSaveBehaviour component and set it as the save system for Switch platform.
        SwitchSaveBehaviour swSB = gameObject.AddComponent<SwitchSaveBehaviour>();
        iSave = swSB;
#else
        PCSaveBehaviour pcSB = gameObject.AddComponent<PCSaveBehaviour>();
        iSave = pcSB;
#endif
    }

    /// <summary>
    /// Create save data or load existing data if available.
    /// </summary>
    /// <param name="onComplete">Callback function to execute after creating or loading data.</param>
    /// <param name="initialized">Flag indicating if the system is already initialized.</param>
    public void CreateSaveData(Action<SaveData> onComplete, bool initialized = false)
    {
        isProcessing = true;
        // Check if save file exists and perform actions accordingly.
        iSave.Exists(SAVE_FILE_PATH, AfterExistsCheck);

        void AfterExistsCheck(bool fileExists)
        {
            if (!fileExists)
            {
                SetAutoSaveIcon();
                // If save file does not exist, create new save data, serialize, and save it.
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();
                SaveData data = new SaveData();
                data.aopa = 0;
                bf.Serialize(stream, data);
                byte[] serializedData = stream.GetBuffer();
                iSave.Save(SAVE_FILE_PATH, serializedData);
                gameSaveData = data;
                stream.Close();
                onComplete.Invoke(gameSaveData);
                isProcessing = false;
            }
            else
            {
                // If save file exists, load existing data and invoke the callback.
                LoadData(data =>
                {
                    onComplete.Invoke(data);
                });
            }
        }
    }

    /// <summary>
    /// Save the current game data.
    /// </summary>
    /// <param name="onComplete">Callback function to execute after saving data.</param>
    public void SaveData(Action<SaveData> onComplete = null)
    {
        SetAutoSaveIcon();
        isProcessing = true;
        // Serialize the game data, save it, and invoke the callback.
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();
        bf.Serialize(stream, gameSaveData);
        byte[] serializedData = stream.GetBuffer();
        stream.Close();

        iSave.Save(SAVE_FILE_PATH, serializedData, () =>
        {
            onComplete.Invoke(gameSaveData);
            isProcessing = false;
        });
    }

    /// <summary>
    /// Load game data from the save file.
    /// </summary>
    /// <param name="onComplete">Callback function to execute after loading data.</param>
    public void LoadData(Action<SaveData> onComplete)
    {
        isProcessing = true;
        // Check if save file exists and load data accordingly.
        iSave.Exists(SAVE_FILE_PATH, AfterExistsCheck);

        void AfterExistsCheck(bool fileExists)
        {
            if (fileExists)
            {
                // If save file exists, load data and invoke the callback.
                iSave.Load(SAVE_FILE_PATH, fileContents => OnFileExists(fileContents, onComplete));
            }
            else
            {
                onComplete.Invoke(gameSaveData);
                isProcessing = false;
            }
        }

        void OnFileExists(byte[] fileContents, Action<SaveData> onComplete)
        {
            // Deserialize the file contents, update gameSaveData, and invoke the callback.
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(fileContents);
            SaveData data = (SaveData)bf.Deserialize(stream);
            gameSaveData = data;
            stream.Close();
            onComplete.Invoke(data);
            isProcessing = false;
        }
    }

    /// <summary>
    /// Check if save data file exists.
    /// </summary>
    /// <param name="onComplete">Callback function to execute with the result.</param>
    public void CheckSaveDataDataExistence(Action<bool> onComplete)
    {
        isProcessing = true;
        // Check if save file exists and invoke the callback with the result.
        iSave.Exists(SAVE_FILE_PATH, AfterExistsCheck);

        void AfterExistsCheck(bool fileExists)
        {
            onComplete.Invoke(fileExists);
            isProcessing = false;
        }
    }

    /// <summary>
    /// Delete the save data file.
    /// </summary>
    /// <param name="onComplete">Callback function to execute after deletion.</param>
    public void DeleteSaveData(Action onComplete)
    {
        isProcessing = true;
        // Delete the save file and invoke the callback.
        iSave.Delete(SAVE_FILE_PATH, onComplete, () => { isProcessing = false; });
    }

    /// <summary>
    /// Set auto save icon
    /// </summary>
    public void SetAutoSaveIcon()
    {
        if (autoSaveCoroutine != null)
            return;

        autoSaveCoroutine = StartCoroutine(UITools.FadeCanvasGroup(autoSaveCanvasGroup, 1, 0.4f, () =>
         {
             StartCoroutine(ControllerTools.WaitAndExecute(2f, () =>
             {
                 autoSaveCoroutine = StartCoroutine(UITools.FadeCanvasGroup(autoSaveCanvasGroup, 0, 0.4f, () =>
                 {
                     autoSaveCoroutine = null;
                 }));
             }));
         }));
    }
    #endregion
}

// Serializable class to store game data.
[Serializable]
public class SaveData
{
    public int aopa;
    public struct GameDataStruct { } // Structure for additional game data (unused).
    public struct AchievementsDataStruct { } // Structure for achievements data (unused).
    public struct SettingsDataStruct { } // Structure for settings data (unused).
}
