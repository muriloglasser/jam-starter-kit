using EntityCreator;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that controls auto save screen
/// </summary>
public class AutoSaveController : SceneDataOperator<AutoSaveStruct>
{
    #region Properties

    public const string SCENE_NAME = "AutoSave";
    [SerializeField] private UIAutoSave uiAutoSave;
    [SerializeField] private AutoSaveData autoSaveData;

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
    public override void Initialize(AutoSaveStruct data)
    {
        uiAutoSave.Initialize();

        StartCoroutine(ControllerTools.WaitAndExecute(autoSaveData.autoSaveTime, () =>
        {
            GoToMenu();
        }));
    }

    #endregion

    #region Core Methods

    /// <summary>
    /// Initiates a transition to the main menu scene with a fade-in effect and hides the current splash scene.
    /// </summary>
    public void GoToMenu()
    {
        ControllerTools.SetTransitionOut(() =>
        {
            PressToStartController.OpenScene(new PressToStartStruct
            {


            }, PressToStartController.SCENE_NAME, LoadSceneMode.Additive);
            AutoSaveController.HideScene(AutoSaveController.SCENE_NAME);
            TransitionController.HideScene(TransitionController.SCENE_NAME);
        });        
    }

  

    #endregion

}

