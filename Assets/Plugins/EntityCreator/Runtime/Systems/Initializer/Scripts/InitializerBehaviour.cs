using UnityEngine;

/// <summary>
/// Define a class called InitializerBehaviour, inheriting from MonoBehaviour.
/// </summary>
public class InitializerBehaviour : MonoBehaviour
{
    public static InitializerBehaviour instance;  // Declare a public static variable 'instance' of type InitializerBehaviour.

    private void Awake()
    {
        // Check if the 'instance' variable is null.
        if (instance == null)
        {
            // If it is null, set the current object as the instance.
            instance = this;

            // Don't destroy this object when loading new scenes.
            DontDestroyOnLoad(this);
        }
        // If 'instance' is not null and not equal to the current object.
        else if (this != instance)
        {
            // Output a debug message indicating that an extra instance is being destroyed.
            Debug.Log("Destroying extra GM");

            // Destroy the extra instance of the InitializerBehaviour script.
            Destroy(this.gameObject);
        }
    }
}