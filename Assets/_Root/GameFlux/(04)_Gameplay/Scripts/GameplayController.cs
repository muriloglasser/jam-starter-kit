using EntityCreator;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
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

    private void OnEnable()
    {
        EventDispatcher.RegisterObserver<ResumeStruct>(Resume);
    } 

    private void OnDisable()
    {
        EventDispatcher.UnregisterObserver<ResumeStruct>(Resume);
    }

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
        uiGameplay.Initialize(gameSettings);
    }

    private void Update()
    {
        KeyboardAndGamepadCheck();
    }

    private void Resume(ResumeStruct obj)
    {
        ControllerTools.SetTransitionIn(() =>
        {         
            lockScene = false;
        });
    }

    /// <summary>
    /// Set ui button actions
    /// </summary>
    public void SetUpButtonActions()
    {
        uiGameplay.pauseButtonClicked = () =>
        {
            Pause();
        };
    }

    private void KeyboardAndGamepadCheck()
    {
        Keyboard keyboard = Keyboard.current;
        Gamepad gamepad = Gamepad.current;

        // Checks gamepad input.
        if (gamepad != null)
        {
            if (gamepad.startButton.wasPressedThisFrame)
            {
                Pause();
            }
        }
        // Checks keyboard input.
        else if (keyboard != null)
        {
            if (keyboard.escapeKey.wasPressedThisFrame)
            {
                Pause();
            }

            if (keyboard.aKey.wasPressedThisFrame)
            {
                GameOver();
            }

            if (keyboard.bKey.wasPressedThisFrame)
            {
                Win();
            }
        }
    }

    private void Pause()
    {
        if (lockScene)
            return;

        if (SceneManager.GetSceneByName("GameOver").isLoaded)
            return;

        lockScene = true;

        ControllerTools.SetTransitionOut(() =>
        {
            PauseController.OpenScene(new PauseStruct
            {

            }, PauseController.SCENE_NAME, LoadSceneMode.Additive);
        });
    }

    private void GameOver()
    {
        if (lockScene)
            return;

        lockScene = true;

        ControllerTools.SetTransitionOut(() =>
        {
            GameOverController.OpenScene(new GameOverStruct
            {

            }, GameOverController.SCENE_NAME, LoadSceneMode.Additive);
        });
    }

    private void Win()
    {
        if (lockScene)
            return;

        lockScene = true;

        ControllerTools.SetTransitionOut(() =>
        {
            WinController.OpenScene(new WinStruct
            {

            }, WinController.SCENE_NAME, LoadSceneMode.Additive);
        });
    }

    #endregion
}

