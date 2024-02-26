using EntityCreator;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Video;
using System.Collections;

/// <summary>
/// This class is responsible for managing the behavior of a Splash screen in a Unity game.
/// </summary>
public class SplashController : SceneDataOperator<SplashStruct>
{
    #region Properties

    public const string SCENE_NAME = "Splash";
    [SerializeField] private UISplash uiSplash;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private SplashData splashData;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        ControllerTools.SetTransitionIn(() =>
        {
            TransitionController.HideScene(TransitionController.SCENE_NAME);
        });       
    }

    /// <summary>
    /// Override of the Initialize method from the base class SceneDataOperator.
    /// </summary>
    /// <param name="data"> Splash screen data </param>
    public override void Initialize(SplashStruct data)
    {
        uiSplash.Initialize();
        StartCoroutine(SetSplashVideo());

        StartCoroutine(ControllerTools.WaitAndExecute(splashData.splashTime, () =>
        {
            GoToAutoSave();
        }));
    }

    #endregion

    #region Core Methods

    /// <summary>
    /// Initiates a transition to the auto save scene with a fade-in effect and hides the current splash scene.
    /// </summary>
    public void GoToAutoSave()
    {
        ControllerTools.SetTransitionOut(() =>
        {
            EventDispatcher.Dispatch<StopAudioStruct>(new StopAudioStruct
            {
                soundName = "DopaminSplash"
            });
            AutoSaveController.OpenScene(AutoSaveController.SCENE_NAME, LoadSceneMode.Additive);
            SplashController.HideScene(SplashController.SCENE_NAME);
            TransitionController.HideScene(TransitionController.SCENE_NAME);
        });      
    }

    /// <summary>
    /// Set splash screen video and audio
    /// </summary>
    /// <returns></returns>
    public IEnumerator SetSplashVideo()
    {
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
            yield return null;

        videoPlayer.Play();

        EventDispatcher.Dispatch<PlayAudioStruct>(new PlayAudioStruct
        {
            soundName = "DopaminSplash"
        });
    }

    #endregion
}





