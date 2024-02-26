using EntityCreator;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for managing the behavior of a gameplay screen in a Unity game.
/// </summary>
public class GameplayController : SceneDataOperator<GameplayStruct>
{
    #region Properties

    public const string SCENE_NAME = "Gameplay";
    [SerializeField] private UIGameplay uiGameplay;

    #endregion

    #region Unity Metods

    private void Awake()
    {
        ControllerTools.SetTransitionIn(() =>
        {
            EventDispatcher.Dispatch<PlayAudioStruct>(new PlayAudioStruct
            {
                soundName = "Gameplay"
            });
            lockScene = false;
        });       
    }

    /// <summary>
    /// Override of the Initialize method from the base class SceneDataOperator.
    /// </summary>
    /// <param name="data"> Gameplay screen data </param>
    public override void Initialize(GameplayStruct data)
    {
        SetUpButtonActions();
        uiGameplay.Initialize();
    }

    /// <summary>
    /// Set ui button actions
    /// </summary>
    public void SetUpButtonActions()
    {
        /*uiGameplay.backToMenuButtonClicked = () =>
        {
            if (lockScene)
                return;

            lockScene = true;

            ControllerTools.SetTransitionOut(() =>
            {
                EventDispatcher.Dispatch<StopAudioStruct>(new StopAudioStruct
                {
                    soundName = "Gameplay"
                });

                LoadingController.OpenScene(new LoadingStruct
                {
                    sceneToLoad = () =>
                    {
                        MenuController.OpenScene(new MenuStruct
                        {

                        }, MenuController.SCENE_NAME, LoadSceneMode.Additive);
                    }

                }, LoadingController.SCENE_NAME, LoadSceneMode.Additive);
                GameplayController.HideScene(GameplayController.SCENE_NAME);
            });

          
        };*/
    }

    #endregion

}

