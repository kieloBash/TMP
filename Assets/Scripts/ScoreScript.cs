using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    public CurrentNoteScript currentNote;
    public TMP_Text score_textbox;

    void Start()
    {
        score_textbox = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score_textbox.text = Mathf.FloorToInt(currentNote.score()).ToString() + "%";
    }
}
