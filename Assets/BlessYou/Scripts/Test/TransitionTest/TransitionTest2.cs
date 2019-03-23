using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTest2 : MonoBehaviour
{
    public SceneTransition sceneTransition;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Allow", 3f);
    }

    void Allow()
    {
        sceneTransition.AllowCompleteLoading();
    }
}
