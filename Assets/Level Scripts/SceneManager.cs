using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject ntbkScene;
    public GameObject summaryScene;
    public void SetSecondSceneInactive()
    {
        if (ntbkScene != null && summaryScene != null)
        {
            ntbkScene.SetActive(false);
            summaryScene.SetActive(true);
        }
        else
        {
            Debug.LogError("No prefab found");
        }
    }
}
