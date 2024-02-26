using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// This class represents a Loading transition controller.
/// </summary>
public class UILoading : MonoBehaviour
{
    #region Properties
    [SerializeField] private TMP_Text loadingText;  // Reference to the TextMeshPro Text component for loading text
    [SerializeField] private float pointCountSpeed = 0.04f;  // Speed at which the loading text changes its appearance
    private string firstText;  // The initial text before animation
    private int textCount = 3;  // Count to determine the state of loading text animation

    #endregion

    #region Core Metods
    public void Initialize()
    {
        StartCoroutine(Loading());  // Start the loading animation coroutine when the object is initialized
    }

    /// <summary>
    /// Coroutine to handle the loading text animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator Loading()
    {
        yield return null;  // Wait for one frame before starting the animation

        firstText = loadingText.text;  // Store the initial text

        var secondText = loadingText.text + ".";  // Text with one dot added for the loading animation
        var thirdText = loadingText.text + "..";  // Text with two dots added for the loading animation
        var fourthText = loadingText.text + "...";  // Text with three dots added for the loading animation

        while (true)
        {
            yield return new WaitForSeconds(pointCountSpeed);  // Wait for a specific duration before changing the loading text

            switch (textCount)
            {
                case 3:
                    loadingText.text = firstText;  // Display the initial text
                    break;
                case 2:
                    loadingText.text = secondText;  // Display the text with one dot
                    break;
                case 1:
                    loadingText.text = thirdText;  // Display the text with two dots
                    break;
                case 0:
                    loadingText.text = fourthText;  // Display the text with three dots
                    break;
            }

            textCount--;  // Decrement the text count to change the animation state

            if (textCount == -1)
                textCount = 3;  // Reset the text count when it reaches -1 to restart the animation
        }
    }
    #endregion
}