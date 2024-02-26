using EntityCreator;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// This class is responsible for managing the behavior of a menu screen in a Unity game.
/// </summary>
public class MenuController : SceneDataOperator<MenuStruct>
{
    #region Properties

    public const string SCENE_NAME = "Menu";
    [SerializeField] private UIMenu uiMenu;

    #endregion

    #region Unity Methods
    private void Awake()
    {
        ControllerTools.SetTransitionIn(() =>
        {
            EventDispatcher.Dispatch<PlayAudioStruct>(new PlayAudioStruct
            {
                soundName = "Menu"
            });
            lockScene = false;
            TransitionController.HideScene(TransitionController.SCENE_NAME);
        });
    }
    /// <summary>
    /// Override of the Initialize method from the base class SceneDataOperator.
    /// </summary>
    /// <param name="data"> MenuController screen data </param>
    public override void Initialize(MenuStruct data)
    {
        SetUpButtonActions();
        uiMenu.Initialize();
    }

    #endregion

    #region Core Methods

    /// <summary>
    /// Set ui button actions
    /// </summary>
    public void SetUpButtonActions()
    {
        uiMenu.playButtonClicked = () =>
        {
            if (lockScene)
                return;

            lockScene = true;

            ControllerTools.SetTransitionOut(() =>
            {
                EventDispatcher.Dispatch<StopAudioStruct>(new StopAudioStruct
                {
                    soundName = "Menu"
                });

                LoadingController.OpenScene(new LoadingStruct
                {
                    sceneToLoad = () =>
                    {
                        GameplayController.OpenScene(new GameplayStruct
                        {

                        }, GameplayController.SCENE_NAME, LoadSceneMode.Additive);
                    }

                }, LoadingController.SCENE_NAME, LoadSceneMode.Additive);
                MenuController.HideScene(MenuController.SCENE_NAME);
            });
        };
    }



    #endregion

}

