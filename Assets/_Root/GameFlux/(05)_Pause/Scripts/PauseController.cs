using EntityCreator;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseController : SceneDataOperator<PauseStruct>
{
    public const string SCENE_NAME = "Pause";
    [SerializeField] private UIPause uiPause;

    private void Awake()
    {
        ControllerTools.SetTransitionIn(() =>
        {
            lockScene = false;
        });
    }

    public override void Initialize(PauseStruct data)
    {
        SetUpButtonActions();
        uiPause.Initialize();
    }

    /// <summary>
    /// Set ui button actions
    /// </summary>
    public void SetUpButtonActions()
    {
        uiPause.resumeButtonClicked = () =>
        {
            Resume();
        };

        uiPause.restartButtonClicked = () =>
        {
            Restart();
        };

        uiPause.menuButtonClicked = () =>
        {
            Menu();
        };
    }

    private void Resume()
    {
        if (lockScene)
            return;

        lockScene = true;

        ControllerTools.SetTransitionOut(() =>
        {
            PauseController.HideScene(PauseController.SCENE_NAME);
            EventDispatcher.Dispatch<ResumeStruct>(new ResumeStruct { });
        });
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

            PauseController.HideScene(PauseController.SCENE_NAME);
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

            PauseController.HideScene(PauseController.SCENE_NAME);
            GameplayController.HideScene(GameplayController.SCENE_NAME);
        });
    }
}

public struct ResumeStruct
{ }


