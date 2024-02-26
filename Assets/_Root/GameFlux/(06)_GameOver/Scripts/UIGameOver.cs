using EntityCreator;
using System;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    #region Properties

    [SerializeField] private ButtonController restartButton;
    public Action restartButtonClicked;
    [SerializeField] private ButtonController menuButton;
    public Action menuButtonClicked;

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
        restartButton.SetAsFirstButtonSelected();
       
        restartButton.ButtonClick((baseEventData) =>
        {
            restartButtonClicked?.Invoke();
        });

        menuButton.ButtonClick((baseEventData) =>
        {
            menuButtonClicked?.Invoke();
        });
    }

    #endregion
}