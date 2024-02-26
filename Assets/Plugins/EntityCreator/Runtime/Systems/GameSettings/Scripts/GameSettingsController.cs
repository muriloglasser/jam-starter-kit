using EntityCreator;
using UnityEngine;

/// <summary>
/// Class responsible for handling game settings.
/// </summary>
public class GameSettingsController : MonoBehaviour
{
    void Start()
    {
        SetupGameSettings();
    }

    /// <summary>
    /// Sets up game settings based on the target device.
    /// </summary>
    private void SetupGameSettings()
    {
        GameDevice gameDevice = GameDevice.NONE;

#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX
        gameDevice = GameDevice.DESKTOP;
#elif UNITY_ANDROID || UNITY_IOS
            gameDevice = GameDevice.MOBILE;
#elif UNITY_WEBGL
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX
                gameDevice = GameDevice.WEB_AND_DESKTOP;
#elif UNITY_ANDROID || UNITY_IOS
                gameDevice = GameDevice.WEB_AND_MOBILE;
#endif
#endif

        DataManager.AddData<GameSettingsStruct>(new GameSettingsStruct { targetDevice = gameDevice });
    }
}

/// <summary>
/// Structure representing game settings.
/// </summary>
public struct GameSettingsStruct
{
    public GameDevice targetDevice;
}

/// <summary>
/// Enumeration representing different game devices.
/// </summary>
public enum GameDevice
{
    NONE,
    WEB_AND_MOBILE,
    WEB_AND_DESKTOP,
    DESKTOP,
    MOBILE,
}
