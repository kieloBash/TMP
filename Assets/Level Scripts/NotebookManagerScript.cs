using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class NotebookManagerScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isMouseDown = false;
    public TMP_Text notePrefab;
    public TMP_Text gradeTMP;

    private List<string> paragraph = new List<string>()
    {
        "Students have suddenly begun changing in personality, becoming the opposite of who they once were.",
        "Shy and bookish students who were well known to be kind and help tutor lower grades have suddenly become delinquents, playing pranks on other kids and even bullying school staff, teachers, and other students.",
        "No one’s really sure what’s going on so the school needs your mystery solving abilities to get to the bottom of it."
    };

    private List<TMP_Text> notes = new List<TMP_Text>();
    private int currentIndex = 0;

    public ActionsManagerScript action_manager;
    public Dictionary<int, Dictionary<int, string>> highlightedWords = new Dictionary<int, Dictionary<int, string>>();
    public Dictionary<int, Dictionary<int, string>> answerKeys = new Dictionary<int, Dictionary<int, string>>
    {
        {
            0, new Dictionary<int, string>
            {
                {
                    0, "red"
                },
                {
                    1, "red"
                },
                {
                    2, "red"
                },
                {
                    3, "red"
                },
                {
                    4, "red"
                },
            }
        },
        {
            1, new Dictionary<int, string>
            {
                {
                    8, "red"
                },
                {
                    9, "red"
                },
                {
                    10, "red"
                },
                {
                    11, "red"
                },
                {
                    12, "red"
                },
            }
        },
    };


    private void Start()
    {
        foreach (string text in paragraph)
        {
            AddNote(text);
        }

        // Initially, the first note is displayed
        DisplayNote();
    }

    public void AddNote(string noteText)
    {
        TMP_Text newNote = Instantiate(notePrefab, transform);
        newNote.transform.name = "Page " + (notes.Count + 1); // Rename the note
        WrapWordsWithLinkTags(ref noteText); // wrapping teh words with <link> tags for selecting them
        newNote.text = noteText;
        newNote.enabled = false; // Initially, the note is not displayed
        notes.Add(newNote);
    }

    public void NextNote()
    {
        // Hide the current note
        notes[currentIndex].enabled = false;

        // Move to the next note
        currentIndex = (currentIndex + 1) % notes.Count;

        // Display the new current note
        DisplayNote();
    }

    public void PrevNote()
    {
        // Hide the current note
        notes[currentIndex].enabled = false;

        // Move to the previous note
        currentIndex--;
        if (currentIndex < 0) currentIndex += notes.Count; // Loop back to the end if we went past the beginning

        // Display the new current note
        DisplayNote();
    }

    private void DisplayNote()
    {
        // Show the current note
        notes[currentIndex].enabled = true;
        gradeTMP.text = CalculateScore(currentIndex) + "%";
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


    // EVENTS
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
            int index = TMP_TextUtilities.FindIntersectingLink(notes[currentIndex], eventData.position, null);
            if (index >= 0)
            {
                changeColor(index);
            }
        }
    }


    private void AddHighlightWord(int linkIndex, string color)
    {

        if (!highlightedWords.ContainsKey(currentIndex))
        {
            highlightedWords[currentIndex] = new Dictionary<int, string>();
        }
        highlightedWords[currentIndex][linkIndex] = color;
    }

    private void RemoveHighlightedWord(int linkIndex, int page)
    {
        if (highlightedWords.ContainsKey(page))
        {
            var innerDictionary = highlightedWords[page];
            if (innerDictionary.ContainsKey(linkIndex))
            {
                innerDictionary.Remove(linkIndex);
                if (innerDictionary.Count == 0)
                {
                    highlightedWords.Remove(page);
                }
            }
        }
    }

    private void changeColor(int linkIndex)
    {
        string linkTag = notes[currentIndex].textInfo.linkInfo[linkIndex].GetLinkText();

        string current_note = notes[currentIndex].text;

        switch (action_manager.action)
        {
            case "blue":
                current_note = current_note.Replace($"<link={linkIndex}>{linkTag}</link>", $"<color=blue><link={linkIndex}>{linkTag}</link></color>");
                AddHighlightWord(linkIndex, "blue");
                break;
            case "red":
                current_note = current_note.Replace($"<link={linkIndex}>{linkTag}</link>", $"<color=red><link={linkIndex}>{linkTag}</link></color>");
                AddHighlightWord(linkIndex, "red");
                break;
            case "black":
                current_note = current_note.Replace($"<link={linkIndex}>{linkTag}</link>", $"<color=black><link={linkIndex}>{linkTag}</link></color>");
                RemoveHighlightedWord(linkIndex, currentIndex);
                break;
            default: break;
        }

        notes[currentIndex].text = current_note;
        gradeTMP.text = CalculateScore(currentIndex) + "%";
    }

    public float CalculateScore(int pageIndex)
    {
        // Get the dictionary for the given page from both dictionaries
        Dictionary<int, string> highlightedWordsForPage;
        Dictionary<int, string> answerKeysForPage;
        if (highlightedWords.TryGetValue(pageIndex, out highlightedWordsForPage) &&
            answerKeys.TryGetValue(pageIndex, out answerKeysForPage))
        {
            // Count the number of correct answers
            int correctAnswers = 0;
            int incorrectAnswers = 0;
            foreach (var entry in highlightedWordsForPage)
            {
                if (answerKeysForPage.ContainsKey(entry.Key) && answerKeysForPage[entry.Key] == entry.Value)
                {
                    correctAnswers++;
                }
                else
                {
                    incorrectAnswers++;
                }
            }

            // Calculate the percentage of correct answers
            float score = ((float)correctAnswers / answerKeysForPage.Count * 100) - incorrectAnswers * 10;
            return score < 0 ? 0 : score;
        }

        // If the page does not exist in either dictionary, return 0
        return 0;
    }






    // PRINTING
    private void GetHighlightedWordsForPage(int pageNumber)
    {
        // List<string> highlightedWordsForPage = new List<string>();
        Debug.Log("---------------------------------------------------------");
        Debug.Log(CalculateScore(currentIndex));

        // Check if the highlightedWords dictionary contains the page number
        if (highlightedWords.ContainsKey(pageNumber))
        {
            // Get the inner dictionary for the page
            var pageDict = highlightedWords[pageNumber];

            // Iterate over the entries in the inner dictionary
            foreach (var entry in pageDict)
            {
                // Log the color, word, and page number
                Debug.Log("Color: " + entry.Value + ", Word: " + notes[pageNumber].textInfo.linkInfo[entry.Key].GetLinkText() + ", Page: " + pageNumber);

            }
        }
    }
}
