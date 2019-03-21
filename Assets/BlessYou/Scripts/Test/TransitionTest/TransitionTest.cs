using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTest : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public string loadSceneName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            sceneTransition.LoadWithLoadingScene(loadSceneName);
        }
    }
}
