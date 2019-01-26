using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{

    public bool detectionTop;
    public PlayerMovementController controller;

    private List<GameObject> grounds = new List<GameObject>();

    public void Update()
    {
        if (grounds.Count < 1)
        {
            if (detectionTop)
            {
                controller.canWalkForward = false;
            }
            else
            {
                controller.canWalkBackward = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (detectionTop)
        {
            controller.canWalkForward = true;
        }
        else
        {
            controller.canWalkBackward = true;
        }
        grounds.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        grounds.Remove(other.gameObject);
    }
}
