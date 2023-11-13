 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{

    public string destinationSceneName;

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
        SceneManager.LoadScene(sceneName);
    }
}

