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
    public void Initialize()
    {
        SetUIButtons();
    }

    /// <summary>
    /// Add listeners to UI buttons
    /// </summary>
    public void SetUIButtons()
    {
    }

    #endregion
}