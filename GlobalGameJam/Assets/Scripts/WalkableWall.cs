using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableWall : MonoBehaviour
{

    public PlayerMovementController controller;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("walkable") || other.CompareTag("ground"))
        {
            Debug.Log("triggered");
            controller.WallIsWalkable(other);
        }
    }
}
