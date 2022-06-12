using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //[HideInInspector]
    public bool actable;//与之交互的角色是否到位
    protected bool canBeActed;//本身是否能被交互
    protected float actableCoolDown;//内置可被交互CD
    protected int interactType;//1 = player; 2 = companion;
    protected GameObject interactedObject;
    protected float interactInput;
    protected virtual void Update()
    {
        if (!canBeActed)
        {
            actableCoolDown += Time.deltaTime;
            if (actableCoolDown >= 1f)
            {
                canBeActed = true;
                actableCoolDown = 0f;
            }
        }

        if (interactedObject != null)
        {
            if (interactedObject.GetComponent<PlayerControl>() != null)
            {
                interactInput = -Input.GetAxis("Interact");
            }
            else if (interactedObject.GetComponent<CompanionControl>() != null)
            {
                interactInput = Input.GetAxis("Interact B");
            }
            else
            {
                interactInput = 0;
            }
        }
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControl>() != null)
        {
            other.GetComponent<PlayerControl>().canInteract = true;
            actable = true;
            interactType = 1;
            interactedObject = other.gameObject;
        }
        else if (other.GetComponent<CompanionControl>() != null)
        {
            actable = true;
            interactType = 2;
            interactedObject = other.gameObject;
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerControl>() != null)
        {
            other.GetComponent<PlayerControl>().canInteract = false;
            actable = false;
            interactType = 0;
            interactedObject = null;
        }
        else if (other.GetComponent<CompanionControl>() != null)
        {
            actable = false;
            interactType = 0;
            interactedObject = null;
        }
    }
}