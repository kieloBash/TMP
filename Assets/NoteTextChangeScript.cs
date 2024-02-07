using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class NoteTextChangeScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public NotebookHandlerScript notebookHandler;
    string original_text;
    List<string> highlightedTexts = new List<string>();
    private TMP_Text note_textbox;
    private bool isMouseDown = false;

    private void Start()
    {
        note_textbox = GetComponent<TMP_Text>();
        original_text = "Hi, I’m the detective’s son, I believe that I have what it takes to be a good detective, because my father teaches me everything he knows.";
        WrapWordsWithLinkTags(ref original_text);

        note_textbox.text = original_text;
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

        // Add the clicked word to the list
        highlightedTexts.Add(linkTag);

        // Highlight the word only if it is in the list
        if (highlightedTexts.Contains(linkTag))
        {
            // Highlight the word
            if (notebookHandler.playerStatus == "Against")
            {

                original_text = original_text.Replace($"<link={linkIndex}>{linkTag}</link>", $"<color=red><link={linkIndex}>{linkTag}</link></color>");
            }
            else if (notebookHandler.playerStatus == "For")
            {
                original_text = original_text.Replace($"<link={linkIndex}>{linkTag}</link>", $"<color=blue><link={linkIndex}>{linkTag}</link></color>");
            }
            else
            {
                original_text = original_text.Replace($"<link={linkIndex}>{linkTag}</link>", $"<color=black><link={linkIndex}>{linkTag}</link></color>");
            }
        }
        else
        {
            // Reset the color of the word
            original_text = original_text.Replace($"<color=red><link={linkIndex}>{linkTag}</link></color>", $"<link={linkIndex}>{linkTag}</link>");
        }

        note_textbox.text = original_text;
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
}
