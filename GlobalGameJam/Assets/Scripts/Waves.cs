using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{

    public float waveDuration;
    public float waitDuration;
    
    public float scaleMax;
    public float scaleMin;
    private bool decreaseScale = false;
    private bool increaseScale = false;
    private float scaleStart;
    public float scaleDuration;

    private bool waitForNextInvoke = false;
    private bool isWaveActive = true;

    public PlayerMovementController controller;

    // Update is called once per frame
    void Update()
    {
        if (!waitForNextInvoke)
        {
            if (isWaveActive)
            {
                Invoke("ChangeWaveStatus", waveDuration);
                waitForNextInvoke = true;
            }
            else
            {
                Invoke("ChangeWaveStatus", waitDuration);
                waitForNextInvoke = true;
            }
        }
        

        if (decreaseScale)
        {
            float scaleMultiplier = 1 - ((Time.time - scaleStart) / scaleDuration);
            float scale = scaleMin + scaleMax * scaleMultiplier;
            transform.localScale = new Vector3 (scale, scale, scale);
            if (scaleMultiplier <= 0)
            {
                decreaseScale = false;
            }
        }
        if(increaseScale)
        {
            float scaleMultiplier = (Time.time - scaleStart) / scaleDuration;
            float scale = scaleMin + scaleMax * scaleMultiplier;
            transform.localScale = new Vector3(scale, scale, scale);
            if (scaleMultiplier >= 1)
            {
                increaseScale = false;
            }
        }
        
    }

    private void ChangeWaveStatus()
    {
        isWaveActive = !isWaveActive;
        waitForNextInvoke = false;
        scaleStart = Time.time;
        if (isWaveActive)
        {
            increaseScale = true;
        }
        else
        {
            decreaseScale = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            controller.shocked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            controller.shocked = false;
        }
    }
}
