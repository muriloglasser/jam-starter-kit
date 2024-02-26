using UnityEngine;

/// <summary>
/// Create a new scriptable object class called TransitionData, inheriting from ScriptableObject.
/// </summary>
[CreateAssetMenu(fileName = "LoadingData", menuName = "ScriptableObjects/LoadingData", order = 1)]
public class LoadingData : ScriptableObject
{
    [Header("Loading time")]
    public float loadingTime; // Public variable to store the transition fade time
}