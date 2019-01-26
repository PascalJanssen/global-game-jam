using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    //information UI wether the player can grab an item or not
    public GameObject grabUI;

    //place where the item is placed when grabed
    public Transform itemHoldPlace;

    private List<GameObject> holdableItems = new List<GameObject>();

    //item that the player is currently holding
    private GameObject itemHold;

    //place where the item is placed when droped
    public Transform dropItem;

    public Transform comparePosition;

    private bool canHoldItem = false;
    private bool dropable = true;
    private bool itemIsHolding = false;

    private void Update()
    {
        if (itemIsHolding)
        {
            if (Input.GetButtonDown("action") && dropable)
            {
                itemHold.transform.position = dropItem.position;
                itemHold.GetComponent<Rigidbody>().useGravity = true;
                itemIsHolding = false;
            }
            else
            {
                itemHold.transform.position = itemHoldPlace.position;
                itemHold.transform.rotation = itemHoldPlace.rotation;
            }
        }
        else
        {
            if (Input.GetButtonDown("action") && canHoldItem)
            {
                itemHold = GetNearestItem();
                itemHoldPlace.rotation = itemHold.transform.rotation;
                itemHold.transform.position = itemHoldPlace.position;
                itemHold.GetComponent<Rigidbody>().useGravity = false;
                itemIsHolding = true;
                grabUI.SetActive(false);
            }
        }
    }

    private GameObject GetNearestItem()
    {
        GameObject item = holdableItems[0];

        for (int i = 1; i < holdableItems.Count; i++)
        {
            if ((holdableItems[i].transform.position - comparePosition.position).sqrMagnitude < 
                (item.transform.position - comparePosition.position).sqrMagnitude)
            {
                item = holdableItems[i];
            }
        }

        return item;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("ground"))
        {
            dropable = false;
        }
    }

    //If the Player is near an holdable item, an information UI appears 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("item"))
        {
            canHoldItem = true;
            holdableItems.Add(other.gameObject);
            if (!itemIsHolding)
            {
                grabUI.SetActive(true);
            }
        }
    }

    //If the Player is to far away from an holdable item, the information UI disappears
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("item"))
        {
            holdableItems.Remove(other.gameObject);
            if (holdableItems.Count == 0)
            {
                canHoldItem = false;
                grabUI.SetActive(false);
            }
        }
        dropable = true;
    }
}
