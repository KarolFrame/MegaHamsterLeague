using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaDeCarga : MonoBehaviour
{
    void Start()
    {
        string levelToLoad = LevelLoader.nextLevel;

        StartCoroutine(this.MakeTheLoad(levelToLoad));

    }
    IEnumerator MakeTheLoad(string level)
    {
        //Quitar cuando se juegue
        yield return new WaitForSeconds(2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(level);

        while(operation.isDone == false)
        {
            yield return null;
        }
    }
}
