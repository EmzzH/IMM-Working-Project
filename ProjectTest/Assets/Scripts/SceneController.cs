 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    // DataManager
    public DataManager dataManager;

    public string destinationSceneName;
    public string currentScene;
    public bool isShop;

    void Start()
    {
 
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M)) // Change this to the desired trigger key or condition
        //{
        //SceneManager.LoadScene(destinationSceneName);
        //}
    }


    public void LoadScene(string sceneName)
    {
        print("Scene change");
        print(sceneName);
        this.currentScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }

    public void SkipTutoiral(int roundCounter) 
    {
        dataManager.roundCounter = roundCounter;
    }

    public void SkipTutorialBool(bool skipTutorial) 
    {
        dataManager.SetSkippedTutorial(skipTutorial);
    }


}

