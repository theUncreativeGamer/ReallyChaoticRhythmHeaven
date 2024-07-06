using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxProperty : MonoBehaviour
{
    public List<string> dialogues = new();
    public TMPro.TextMeshProUGUI dialogueTextBox;
    public List<GameObject> stuffsToActive;
    public List<GameObject> stuffsToInactive;

    private int dialogueIndex = 0;

    private void Start()
    {
        dialogueTextBox.text = dialogues[0];
    }

    public void ContinueDialogue()
    {
        dialogueIndex++;
        if (dialogueIndex < dialogues.Count)
        {
            dialogueTextBox.text = dialogues[dialogueIndex];
        }
        else
        {
            foreach (GameObject go in stuffsToActive) { go.SetActive(true); }
            foreach (GameObject go in stuffsToInactive) { go.SetActive(false); }
        }
    }
}
