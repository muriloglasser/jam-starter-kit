using EntityCreator;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that control press to start scene
/// </summary>
public class PressToStartController : SceneDataOperator<PressToStartStruct>
{
    #region Properties

    public const string SCENE_NAME = "PressToStart";
    public bool liberate = false;
    public UIPressToStart uiPressToStart;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        // Opens the transition scene with a fade out effect.
        ControllerTools.SetTransitionIn(() =>
        {
            EventDispatcher.Dispatch<PlayAudioStruct>(new PlayAudioStruct
            {
                soundName = "PressToStart"
            });

            lockScene = false;
            TransitionController.HideScene(TransitionController.SCENE_NAME);
        });
    }
    public override void Initialize(PressToStartStruct data)
    {
        uiPressToStart.Initialize();
    }
    private void Update()
    {
        // Checks for user input to proceed to the next scene.
        CheckForPress();
    }

    #endregion

    #region Core Methods

    /// <summary>
    /// Checks for user input (keyboard or gamepad) to end the scene.
    /// </summary>
    public void CheckForPress()
    {
        Keyboard keyboard = Keyboard.current;
        Gamepad gamepad = Gamepad.current;

        if (liberate)
        {
            liberate = false;
            uiPressToStart.Initialize();
        }

        // Checks gamepad input.
        if (gamepad != null)
        {
            if (Gamepad.current.aButton.wasPressedThisFrame ||
                Gamepad.current.bButton.wasPressedThisFrame ||
                Gamepad.current.xButton.wasPressedThisFrame ||
                Gamepad.current.yButton.wasPressedThisFrame ||
                Gamepad.current.xButton.wasPressedThisFrame ||
                Gamepad.current.startButton.wasPressedThisFrame ||
                Gamepad.current.selectButton.wasPressedThisFrame ||
                Gamepad.current.dpad.left.wasPressedThisFrame)
            {
                // Ends the scene.
                End();
            }
        }
        // Checks keyboard input.
        else if (keyboard != null)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                // Ends the scene.
                End();
            }
        }
    }

    /// <summary>
    /// Ends the scene and initiates the transition effect.
    /// </summary>
    private void End()
    {
        if (lockScene)
            return;

        lockScene = true;
        // Initiates the transition effect after a delay.
        StartCoroutine(SetTransition());
    }

    /// <summary>
    /// Initiates the transition effect between scenes.
    /// </summary>
    /// <returns></returns>
    public IEnumerator SetTransition()
    {
        // Waits for a short delay before transitioning.
        yield return null;


        ControllerTools.SetTransitionOut(() =>
        {
            EventDispatcher.Dispatch<StopAudioStruct>(new StopAudioStruct
            {
                soundName = "PressToStart"
            });

            // Loads the Menu scene and initializes MenuController.
            LoadingController.OpenScene(new LoadingStruct
            {
                sceneToLoad = () =>
                {
                    MenuController.OpenScene(new MenuStruct
                    {

                    }, MenuController.SCENE_NAME, LoadSceneMode.Additive);
                },

            }, LoadingController.SCENE_NAME, LoadSceneMode.Additive);

            // Hides the PressToStart scene.
            PressToStartController.HideScene(PressToStartController.SCENE_NAME);
            TransitionController.HideScene(TransitionController.SCENE_NAME);
        });
    }

    #endregion
}
