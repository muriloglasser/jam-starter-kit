using System;
using UnityEngine;

/// <summary>
/// This class represents a UI transition controller.
/// </summary>
public class UITransition : MonoBehaviour
{
    #region Properties
    [SerializeField] private CanvasGroup canvasGroup;
    public TransitionData transitionData; 
    #endregion

    #region Core Methods
    /// <summary>
    /// Class controller initializer
    /// </summary>
    public void Initialize()
    {
        // This method currently does not contain any specific initialization logic.
        // It can be expanded in the future if necessary.
    }

    /// <summary>
    ///   Sets the UI element to perform a fade-in effect.
    /// </summary>
    /// <param name="duration"> fade duration </param>
    /// <param name="callBack"> callback to execute </param>
    public void SetFadeIn(Action callBack = null, TransitionData transitionData = null)
    {
        var duration = transitionData == null ? this.transitionData.transitionTime : transitionData.transitionTime;
       
        canvasGroup.alpha = 0;

        StartCoroutine(UITools.FadeCanvasGroup(canvasGroup, 1, duration, callBack));
    }

    /// <summary>
    ///   Sets the UI element to perform a fade-out effect.
    /// </summary>
    /// <param name="duration"> fade duration </param>
    /// <param name="callBack"> callback to execute </param>
    public void SetFadeOut(Action callBack = null, TransitionData transitionData = null)
    {

        var duration = transitionData == null ? this.transitionData.transitionTime : transitionData.transitionTime;

        // Initialize the UI element's transparency to 1 (fully opaque).
        canvasGroup.alpha = 1;

        // Start a coroutine to gradually fade the UI element out over the specified duration.
        // When the fade-out is complete, the optional callBack action is executed.
        StartCoroutine(UITools.FadeCanvasGroup(canvasGroup, 0, duration, callBack));
    }

    #endregion
}