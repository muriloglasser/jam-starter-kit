using EntityCreator;
using UnityEngine;

/// <summary>
/// This class is responsible for controlling scene transitions with fading effects in a Unity game.
/// </summary>
public class TransitionController : SceneDataOperator<TransitionStruct>
{
    #region Properties

    public const string SCENE_NAME = "Transition";
    [SerializeField] private UITransition uiTransition;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Override of the Initialize method from the base class SceneDataOperator.
    /// </summary>
    /// <param name="data"> Transition screen data </param>
    public override void Initialize(TransitionStruct data)
    {
        uiTransition.Initialize();
        SetFade();
    }

    #endregion

    #region Core Methods

    /// <summary>
    ///  Sets the fade effect based on the data stored in the TransitionStruct.
    /// </summary>
    public void SetFade()
    {
        // Retrieve cached transition data from the DataManager.
        var cachedData = DataManager.GetData<TransitionStruct>();

        switch (cachedData.fadeType)
        {
            case FadeType.In:
                // Set a fade-in effect with the specified duration and callback.
                uiTransition.SetFadeIn(cachedData.onTransitionEnd);
                break;
            case FadeType.Out:
                // Set a fade-out effect with the specified duration and callback.
                uiTransition.SetFadeOut(cachedData.onTransitionEnd);
                break;
            default:
                break;
        }
    }

    #endregion
}