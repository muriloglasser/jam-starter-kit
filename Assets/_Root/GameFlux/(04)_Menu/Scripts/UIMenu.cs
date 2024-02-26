using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using EntityCreator;
/// <summary>
/// This class represents a UI menu controller.
/// </summary>
public class UIMenu : MonoBehaviour
{
    #region Properties

    [SerializeField] private ButtonController playButton;
    public Action playButtonClicked;

    #endregion

    #region Core Methods

    /// <summary>
    /// Class controller initializer
    /// </summary>
    public void Initialize()
    {
        SetUIButtons();
    }

    /// <summary>
    /// Add listeners to UI buttons
    /// </summary>
    public void SetUIButtons()
    {
        playButton.SetAsFirstButtonSelected();

        playButton.ButtonClick((baseEventData)=> 
        {
            playButtonClicked?.Invoke();        
        });
    }

    #endregion
}