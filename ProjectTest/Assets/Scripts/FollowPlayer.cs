using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Game object for player
    public GameObject player;
    // Offset for camera
    private Vector3 offset = new Vector3(0,20,0);
    void Start()
    {
        
    }

    
    void Update()
    {
        // Offset camera above player
        transform.position = player.transform.position + offset;
    }
}
