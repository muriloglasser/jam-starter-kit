using UnityEngine;

/// <summary>
/// Create a new scriptable object class called TransitionData, inheriting from ScriptableObject.
/// </summary>
[CreateAssetMenu(fileName = "SplashData", menuName = "ScriptableObjects/SplashData", order = 1)]
public class SplashData : ScriptableObject
{
    [Header("Splash time")]
    public float splashTime; // Public variable to store the transition fade time
}