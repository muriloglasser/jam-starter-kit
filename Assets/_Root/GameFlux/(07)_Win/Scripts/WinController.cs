using EntityCreator;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : SceneDataOperator<WinStruct>
{
    public const string SCENE_NAME = "Win";
    [SerializeField] private UIWin uiGameOver;

    private void Awake()
    {
        ControllerTools.SetTransitionIn(() =>
        {
            lockScene = false;
        });
    }

    public override void Initialize(WinStruct data)
    {
        SetUpButtonActions();
        uiGameOver.Initialize();
    }

    /// <summary>
    /// Set ui button actions
    /// </summary>
    public void SetUpButtonActions()
    {
        uiGameOver.restartButtonClicked = () =>
        {
            Restart();
        };

        uiGameOver.menuButtonClicked = () =>
        {
            Menu();
        };
    }

    private void Restart()
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

            WinController.HideScene(WinController.SCENE_NAME);
            GameplayController.HideScene(GameplayController.SCENE_NAME);

            GameplayController.OpenScene(new GameplayStruct
            {

            }, GameplayController.SCENE_NAME, LoadSceneMode.Additive);
        });
    }

    private void Menu()
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

            WinController.HideScene(WinController.SCENE_NAME);
            GameplayController.HideScene(GameplayController.SCENE_NAME);
        });
    }

}

