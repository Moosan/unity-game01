using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    //ローディング画面のクラス名
    private const string LOADING_SCENE = "taskTransitionLoading";
    void Start()
    {
        //ローディング画面のシーン用
        if (SceneManager.GetActiveScene().name == LOADING_SCENE)
        {
            isLoading = true;
            loadingSceneOperation = SceneManager.LoadSceneAsync(nextLoadSceneName);
            loadingSceneOperation.allowSceneActivation = false;
        }
        //それ以外用
        else
        {
            InitLoadingMenu();
        }
    }

    //ボタンやら外部の呼び出しやらでシーンをロードしたいときに呼び出す
    //SceneManagementは基本他のクラスでは使わないようにする感じ
    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Load(string sceneName, float time)
    {
        StartCoroutine(DelayMethod(time, () =>
        {
            Load(sceneName);
        }));
    }

    //ロード画面を挟むときはこれ
    public void LoadWithLoadingScene(string sceneName)
    {
        nextLoadSceneName = sceneName;
        Load(LOADING_SCENE);
    }
    public void LoadWithLoadingScene(string sceneName, float time)
    {
        nextLoadSceneName = sceneName;
        StartCoroutine(DelayMethod(time, () =>
        {
            Load(LOADING_SCENE);
        }));
    }

    //ロード画面シーンのときに外からメニューをいじるためのもろもろ
    private bool isLoading = false;
    private static string nextLoadSceneName;
    private AsyncOperation loadingSceneOperation;
    //ロード画面閉じるときのリセット
    private void InitLoadingMenu()
    {
        isLoading = false;
        nextLoadSceneName = null;
        loadingSceneOperation = null;
    }
    //ロード画面でしたい処理が終わったらこれを呼び出す
    public void AllowCompleteLoading()
    {
        if (!isLoading) return;

        loadingSceneOperation.allowSceneActivation = true;
    }
    //ロードの進み具合
    public float GetLoadingProgress()
    {
        if (!isLoading) return -1f;

        return loadingSceneOperation.progress;
    }
    //ロードができるところまで終わってるかどうか
    public bool GetIsFinishLoading()
    {
        if (!isLoading) return false;

        if (loadingSceneOperation.allowSceneActivation) return loadingSceneOperation.progress >= 0.9f;
        else return loadingSceneOperation.isDone;
    }

    //時間ずらすだけ
    private IEnumerator DelayMethod(float delayTime, Action action)
    {
        yield return new WaitForSeconds(delayTime);
        action();
    }
}
