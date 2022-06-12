using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : Interactable
{
    bool isOn = false;
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.15f);
        Gizmos.DrawSphere(transform.position, transform.localScale.x * 0.5f);
    }
    protected override void Update()
    {
        base.Update();
        if (actable && canBeActed)
        {
            if (  (Input.GetKeyDown(KeyCode.Space) || interactInput == 1) && interactType == 1 && interactedObject.GetComponent<BasicControl>().controlled )
            {
                canBeActed = false;
                if (!isOn)
                {
                    isOn = true;
                    GetComponent<Animator>().Play("LightRadiusAppear");
                    transform.parent.GetComponent<MeshRenderer>().material.color = Color.yellow;
                }
                else
                {
                    isOn = false;
                    GetComponent<Animator>().Play("LightRadiusDisappear");
                    transform.parent.GetComponent<MeshRenderer>().material.color = Color.black;
                }
            }
            else if (  (Input.GetKeyDown(KeyCode.RightControl) || interactInput == 1) && interactType == 2 && interactedObject.GetComponent<BasicControl>().controlled )
            {
                canBeActed = false;
                if (!isOn)
                {
                    isOn = true;
                    GetComponent<Animator>().Play("LightRadiusAppear");
                    transform.parent.GetComponent<MeshRenderer>().material.color = Color.yellow;
                }
                else
                {
                    isOn = false;
                    GetComponent<Animator>().Play("LightRadiusDisappear");
                    transform.parent.GetComponent<MeshRenderer>().material.color = Color.black;
                }
            }
        }
    }
}