using EntityCreator;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A utility class for handling common UI operations.
/// </summary>
/// 
namespace EntityCreator
{
    public static class ControllerTools
    {
        /// <summary>
        /// Waits and excute an action
        /// </summary>   
        /// <param name="duration"> The duration in seconds. </param>
        /// <param name="callBack"> Action to execute in the end </param>
        /// <returns>An IEnumerator for use with StartCoroutine.</returns>
        public static IEnumerator WaitAndExecute(float duration, Action callBack = null)
        {
            yield return new WaitForSeconds(duration);
            callBack?.Invoke();
        }

        public static void SetTransitionIn(Action onTransitionEnd)
        {
            TransitionController.OpenScene(new TransitionStruct
            {
                fadeType = FadeType.Out,
                onTransitionEnd = onTransitionEnd

            }, TransitionController.SCENE_NAME, LoadSceneMode.Additive);
        }

        public static void SetTransitionOut(Action onTransitionEnd)
        {
            TransitionController.OpenScene(new TransitionStruct
            {
                fadeType = FadeType.In,
                onTransitionEnd = onTransitionEnd

            }, TransitionController.SCENE_NAME, LoadSceneMode.Additive);
        }
    }
}
