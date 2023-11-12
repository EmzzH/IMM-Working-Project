using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    // How long explosion lasts
    private float explosionTime = 2f;
    private float timeLeft = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Timer for how long explosion lasts
        timeLeft += Time.deltaTime;
        if (timeLeft > explosionTime)
        {
            Destroy(gameObject);
        }
    }
}
