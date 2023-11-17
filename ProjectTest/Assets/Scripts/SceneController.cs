 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{

    public string destinationSceneName;
    public string currentScene;
    // DataManager
    private DataManager dataManager;

    void Start()
    {
        // Get the dataManager
        dataManager = FindObjectOfType<DataManager>();
    }

    void Update()
    {
        if (currentScene == "Dead")
        {
            Destroy(dataManager.gameObject);
        }
        //if (Input.GetKeyDown(KeyCode.M)) // Change this to the desired trigger key or condition
        //{
        //SceneManager.LoadScene(destinationSceneName);
        //}
    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        this.currentScene = sceneName;
    }

    public void SetCurrentScene(string sceneName) 
    {
        this.currentScene = sceneName;
    }
}

