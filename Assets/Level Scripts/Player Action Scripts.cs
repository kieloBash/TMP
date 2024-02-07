using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerActionScripts : MonoBehaviour
{
    private Vector3 initialPosition;
    private float initialRotation;
    private bool isDragging = false;
    private bool isSelected = false;
    public int angle;

    private void Start()
    {
        angle = 45;
        initialPosition = transform.position;
        initialRotation = transform.rotation.eulerAngles.z;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        isSelected = true;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        // transform.position = initialPosition;
        // transform.rotation = Quaternion.Euler(0, 0, initialRotation); // Reset rotation to initial angle
    }

    void Update()
    {
        if (isSelected)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            transform.position = new Vector3(mousePosition.x+2, mousePosition.y+2, mousePosition.z);
        }
    }
}
