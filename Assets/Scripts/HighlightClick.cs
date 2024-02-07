using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightClick : MonoBehaviour
{
    public NotebookHandlerScript notebookHandler;
    public GameObject For_action_btn;
    public GameObject Against_action_btn;
    public GameObject Erase_action_btn;
    // Start is called before the first frame update
    private void Start()
    {
        ResetPosition();
        Debug.Log(For_action_btn.transform.position.y);
    }
    public void ChangeStatusFor()
    {
        ResetPosition();
        notebookHandler.playerStatus = "For";
        For_action_btn.transform.position = new Vector3(For_action_btn.transform.position.x, For_action_btn.transform.position.y + 50, For_action_btn.transform.position.z);
        Debug.Log(notebookHandler.playerStatus);
    }
    public void ChangeStatusAgainst()
    {
        ResetPosition();
        notebookHandler.playerStatus = "Against";
        Against_action_btn.transform.position = new Vector3(Against_action_btn.transform.position.x, Against_action_btn.transform.position.y + 50, Against_action_btn.transform.position.z);
        Debug.Log(notebookHandler.playerStatus);
    }
    public void ChangeStatusErase()
    {
        ResetPosition();
        notebookHandler.playerStatus = "Erase";
        Erase_action_btn.transform.position = new Vector3(Erase_action_btn.transform.position.x, Erase_action_btn.transform.position.y + 50, Erase_action_btn.transform.position.z);
        Debug.Log(notebookHandler.playerStatus);
    }
    public void ResetPosition()
    {
        For_action_btn.transform.position = new Vector3(For_action_btn.transform.position.x, -60, For_action_btn.transform.position.z);
        Against_action_btn.transform.position = new Vector3(Against_action_btn.transform.position.x, -60, Against_action_btn.transform.position.z);
        Erase_action_btn.transform.position = new Vector3(Erase_action_btn.transform.position.x, -60, Erase_action_btn.transform.position.z);
    }
}
