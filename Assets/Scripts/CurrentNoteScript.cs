using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class CurrentNoteScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public string current_note;
    public TMP_Text note_textbox;
    private bool isMouseDown = false;
    public NotebookHandlerScript notebookHandler;
    List<string> highlightedTexts = new List<string>();

    void Start()
    {
        note_textbox = GetComponent<TMP_Text>();
    }

    public void updateCurrentNote(string new_note)
    {
        current_note = new_note;
        WrapWordsWithLinkTags(ref current_note);
        note_textbox.text = current_note;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isMouseDown = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isMouseDown = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isMouseDown)
        {
            int index = TMP_TextUtilities.FindIntersectingLink(note_textbox, eventData.position, null);
            if (index >= 0)
            {
                changeColor(index);
            }
        }
    }

    public void changeColor(int linkIndex)
    {
        string linkTag = note_textbox.textInfo.linkInfo[linkIndex].GetLinkText();

        // Highlight the word
        if (notebookHandler.playerStatus == "Against")
        {
            if (!highlightedTexts.Contains(linkTag))
            {
                highlightedTexts.Add(linkTag);
            }
            current_note = current_note.Replace($"<link={linkIndex}>{linkTag}</link>", $"<color=red><link={linkIndex}>{linkTag}</link></color>");
        }
        else if (notebookHandler.playerStatus == "For")
        {
            if (!highlightedTexts.Contains(linkTag))
            {
                highlightedTexts.Add(linkTag);
            }
            current_note = current_note.Replace($"<link={linkIndex}>{linkTag}</link>", $"<color=blue><link={linkIndex}>{linkTag}</link></color>");
        }
        else
        {
            if (highlightedTexts.Contains(linkTag))
            {
                highlightedTexts.Remove(linkTag);
                current_note = current_note.Replace($"<link={linkIndex}>{linkTag}</link>", $"<color=black><link={linkIndex}>{linkTag}</link></color>");
            }
        }

        note_textbox.text = current_note;
    }


    private void WrapWordsWithLinkTags(ref string text)
    {
        string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < words.Length; i++)
        {
            words[i] = $"<link={i}>{words[i]}</link>";
        }
        text = string.Join(" ", words);
    }

    public float score()
    {
        float temp = (float)highlightedTexts.Count / notebookHandler.answers.Count;
        Debug.Log(temp);
        return temp * 100;
    }
}
