using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotebookHandlerScript : MonoBehaviour
{
    public string playerStatus;
    public int page;
    public List<string> notes = new List<string>();
    public List<string> answers = new List<string>();
    [SerializeField] private TMP_Text note_textbox;

    public CurrentNoteScript current_note;
    // Start is called before the first frame update
    void Start()
    {
        playerStatus = "idle";
        page = 0;
        notes.Add("Students have suddenly begun changing in personality, becoming the opposite of who they once were.");
        notes.Add("Shy and bookish students who were well known to be kind and help tutor lower grades have suddenly become delinquents, playing pranks on other kids and even bullying school staff, teachers, and other students.");
        notes.Add("No one’s really sure what’s going on so the school needs your mystery solving abilities to get to the bottom of it.");

        answers.Add("Students have suddenly begun changing in personality");
        answers.Add("Shy and bookish students who were well known to be kind");
        answers.Add("mystery solving abilities to get to the bottom of it.");

        current_note.updateCurrentNote(notes[page]);
        Debug.Log("Game Start");
        Debug.Log(notes.Count);
    }


    // PAGES
    public bool HasNextPage()
    {
        if (page < notes.Count)
            return true;
        return false;
    }
    public bool HasPrevPage()
    {
        if (page != 0)
            return true;
        return false;
    }
    public void NextPage()
    {
        if (HasNextPage())
        {
            page += 1;
            current_note.updateCurrentNote(notes[page]);
        }
    }
    public void PrevPage()
    {
        if (HasPrevPage())
        {
            page -= 1;
            current_note.updateCurrentNote(notes[page]);
        }
    }

}
