using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{

    public GameObject GrabUI;
    public Transform ItemHoldPlace;
    private GameObject ItemHold;
    public Transform DropItem;
    private bool itemIsHolding = false;

    private void Update()
    {
        if (itemIsHolding)
        {
            if (Input.GetButtonUp("action"))
            {
                ItemHold.transform.SetPositionAndRotation(DropItem.position, DropItem.rotation);
                itemIsHolding = false;
            }
            else
            {
                ItemHold.transform.SetPositionAndRotation(ItemHoldPlace.position, ItemHoldPlace.rotation);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("item"))
        {
            if (Input.GetButtonDown("action"))
            {
                ItemHold = other.gameObject;
                ItemHold.transform.SetPositionAndRotation(ItemHoldPlace.position, ItemHoldPlace.rotation);
                itemIsHolding = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("item"))
        {
            GrabUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("item"))
        {
            GrabUI.SetActive(false);
        }
    }
}
