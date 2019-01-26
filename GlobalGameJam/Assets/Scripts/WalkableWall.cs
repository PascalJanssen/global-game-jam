using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableWall : MonoBehaviour
{

    public PlayerMovementController controller;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("walkable") || other.CompareTag("ground"))
        {
            controller.WallIsWalkable(other);
        }

    }
}
