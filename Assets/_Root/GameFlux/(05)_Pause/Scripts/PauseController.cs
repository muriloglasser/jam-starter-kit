using EntityCreator;
using UnityEngine;

public class PauseController : SceneDataOperator<PauseStruct>
{
    public const string SCENE_NAME = "Pause";
    [SerializeField] private UIPause uiPause;
    public override void Initialize(PauseStruct data)
    {
        uiPause.Initialize();
    }

}

