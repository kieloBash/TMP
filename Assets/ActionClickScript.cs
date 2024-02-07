using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionClickScript : MonoBehaviour
{
    public ActionsManagerScript manager;

    private void OnMouseDown()
    {
        manager.action = "blue";
        Debug.Log(manager.action);
    }
}
