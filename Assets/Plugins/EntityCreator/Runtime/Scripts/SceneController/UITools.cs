using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A utility class for handling common UI operations.
/// </summary>
public static class UITools
{
    /// <summary>
    /// Fades a CanvasGroup in or out over a specified duration.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup to fade.</param>
    /// <param name="targetAlpha">The target alpha value (0 for fade-out, 1 for fade-in).</param>
    /// <param name="duration">The duration of the fade in seconds.</param>
    /// <param name="callBack"> Action to execute in the end of fade </param>
    /// <returns>An IEnumerator for use with StartCoroutine.</returns>
    public static IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float duration, Action callBack = null)
    {
        float startAlpha = canvasGroup.alpha;
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            float progress = (Time.time - startTime) / duration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, progress);
            yield return null;
        }

        // Ensure that the CanvasGroup's alpha reaches the target value precisely.
        canvasGroup.alpha = targetAlpha;

        callBack?.Invoke();
    }
}
