using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageScript : MonoBehaviour
{
    public NotebookHandlerScript notebookHandler;

    public void OnNextButtonClick()
    {
        notebookHandler.NextPage();
    }

    public void OnPrevButtonClick()
    {
        notebookHandler.PrevPage();
    }

}
