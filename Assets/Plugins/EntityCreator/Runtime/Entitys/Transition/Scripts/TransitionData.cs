using UnityEngine;

/// <summary>
/// Create a new scriptable object class called TransitionData, inheriting from ScriptableObject.
/// </summary>
[CreateAssetMenu(fileName = "TransitionData", menuName = "ScriptableObjects/TransitionData", order = 1)]
public class TransitionData : ScriptableObject
{    
    [Header("Transition fade time")]
    public float transitionTime; // Public variable to store the transition fade time
}