using EntityCreator;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class represents a UI gameplay controller.
/// </summary>
public class UIGameplay : MonoBehaviour
{
    #region Properties

    [SerializeField] private ButtonController pauseButton;
    public Action pauseButtonClicked;


    #endregion

    #region Core Methods

    /// <summary>
    /// Class controller initializer
    /// </summary>
    public void Initialize(GameSettingsStruct gameSettings)
    {
        SetUIButtons(gameSettings);
    }

    /// <summary>
    /// Add listeners to UI buttons
    /// </summary>
    public void SetUIButtons(GameSettingsStruct gameSettings)
    {
        switch (gameSettings.targetDevice)
        {
            case GameDevice.NONE:
                break;
            case GameDevice.WEB_AND_MOBILE:
                pauseButton.gameObject.SetActive(true);
                break;
            case GameDevice.WEB_AND_DESKTOP:
                pauseButton.gameObject.SetActive(false);
                break;
            case GameDevice.DESKTOP:
                pauseButton.gameObject.SetActive(false);
                break;
            case GameDevice.MOBILE:
                pauseButton.gameObject.SetActive(true);
                break;
            default:
                break;
        }

        pauseButton.ButtonClick((baseEventData) =>
        {
            pauseButtonClicked?.Invoke();
        });
    }

    #endregion
}