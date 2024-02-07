using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummaryManagerScript : MonoBehaviour
{
    private int currentPage = 0;
    public NotebookManagerScript notebook_manager;
    public TMP_Text grade;

    // Start is called before the first frame update
    void Start()
    {
        grade.text = notebook_manager.CalculateScore(currentPage) + "%";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
