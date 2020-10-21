using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;
    public void ScreenLoader(string scene)
    {
        sceneName = scene;
        SceneManager.LoadScene(scene);
    }
}
