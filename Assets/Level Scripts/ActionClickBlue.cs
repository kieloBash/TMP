using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionClickBlue : MonoBehaviour
{
    public ActionsManagerScript manager;

    private Vector3 initialPosition;
    private float initialRotation;
    public int angle;

    private void Start()
    {
        angle = 45;
        initialPosition = transform.position;
        initialRotation = transform.rotation.eulerAngles.z;
    }

    private void OnMouseDown()
    {
        manager.action = "blue";
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Debug.Log(manager.action);
    }

    void Update()
    {
        if (manager.action == "blue")
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            transform.position = new Vector3(mousePosition.x + 2, mousePosition.y + 2.7f, mousePosition.z);
        }
        else
        {
            transform.position = initialPosition;
            transform.rotation = Quaternion.Euler(0, 0, initialRotation); // Reset rotation to initial angle
        }
    }
}
