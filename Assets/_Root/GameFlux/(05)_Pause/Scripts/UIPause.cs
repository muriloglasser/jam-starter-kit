using EntityCreator;
using System;
using UnityEngine;

public class UIPause : MonoBehaviour
{
    #region Properties

    [SerializeField] private ButtonController resumeButton;
    public Action resumeButtonClicked;
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
        resumeButton.SetAsFirstButtonSelected();

        resumeButton.ButtonClick((baseEventData) =>
        {
            resumeButtonClicked?.Invoke();
        });

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