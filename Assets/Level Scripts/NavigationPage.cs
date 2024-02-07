using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NavigationPage : MonoBehaviour
{
    public NotebookManagerScript manager;

    public void NextPage()
    {
        manager.NextNote();
    }
    public void PrevPage()
    {
        manager.PrevNote();
    }
}
