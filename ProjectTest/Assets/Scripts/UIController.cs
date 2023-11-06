using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method for hiding text
    public void HideUI(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(false);
    }

    // Method for showing text
    public void ShowUI(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
    }

    // Methods for hiding and showing canvas
    public void HideCanvas(Canvas canvas) 
    { 
        canvas.gameObject.SetActive(false);
    }

    public void ShowCanvas(Canvas canvas) 
    {
        canvas.gameObject.SetActive(true);
    }
}
