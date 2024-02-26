using UnityEngine;

/// <summary>
/// Create a new scriptable object class called TransitionData, inheriting from ScriptableObject.
/// </summary>
[CreateAssetMenu(fileName = "AutoSaveData", menuName = "ScriptableObjects/AutoSaveData", order = 1)]
public class AutoSaveData : ScriptableObject
{
    [Header("Auto save time")]
    public float autoSaveTime; // Public variable to store the transition fade time
}