using EntityCreator;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class used to pre load Async scenes
/// </summary>
public class LoadingController : SceneDataOperator<LoadingStruct>
{
    #region Properties 

    public const string SCENE_NAME = "Loading"; 
    [SerializeField] private UILoading uiLoading; 
    [SerializeField] private LoadingData loadingData; 
    private bool transitionIsDone = false; 
    private bool saveIsProcessing = false; 

    #endregion

    #region Unity Methods

    private void Awake()
    {
        ControllerTools.SetTransitionIn(() =>
        {
            transitionIsDone = true;
            TransitionController.HideScene(TransitionController.SCENE_NAME);
        });      
    }
    public override void Initialize(LoadingStruct data)
    {
        uiLoading.Initialize();
        StartCoroutine(Load());
    }

    #endregion


    #region Core Methods

    /// <summary>
    /// Load next scene
    /// </summary>
    /// <returns></returns>
    public IEnumerator Load()
    {
        // Wait for the transition effect to complete
        while (!transitionIsDone)
            yield return null;   

        // If a save operation is in progress, wait
        while (saveIsProcessing)
            yield return null;

        // Mocked loading time - wait for a specific duration
        var loadingMockedTime = loadingData.loadingTime;
        while (loadingMockedTime >= 0)
        {
            yield return null;
            loadingMockedTime -= Time.deltaTime;
        }

        // Open the loaded scene with a fade-in transition effect
        ControllerTools.SetTransitionOut(() =>
        {
            // Get cached loading data
            var loadingStruct = DataManager.GetData<LoadingStruct>();
            loadingStruct.sceneToLoad?.Invoke();
            LoadingController.HideScene(LoadingController.SCENE_NAME);
            TransitionController.HideScene(TransitionController.SCENE_NAME);
        });
    }
    #endregion
}
