using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTest3 : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public string[] loadSceneList;
    private bool[] isLoaded;

    void Awake()
    {
        isLoaded = new bool[loadSceneList.Length];
    }

    void SetScenes()
    {
        for (int i = 0; i < isLoaded.Length; i++) isLoaded[i] = false;

        string[] currentSceneList = sceneTransition.GetActiveSceneName();
        string mainScene = sceneTransition.GetMainSceneName();

        foreach(string cur in currentSceneList)
        {
            if (cur == mainScene) continue;

            for(int i = 0;i < loadSceneList.Length; i++)
            {
                if(cur == loadSceneList[i])
                {
                    isLoaded[i] = true;
                    break;
                }

                if (i == loadSceneList.Length - 1) sceneTransition.Unload(cur);
            }
        }

        for(int i = 0;i < isLoaded.Length; i++)
        {
            if (!isLoaded[i]) sceneTransition.LoadAddictive(loadSceneList[i]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            SetScenes();
        }
    }
}
