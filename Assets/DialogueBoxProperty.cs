using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueBoxProperty : MonoBehaviour, ILoadMusicTrack
{
    public List<string> dialogues = new();
    public TMPro.TextMeshProUGUI dialogueTextBox;
    public List<GameObject> stuffsToActive;
    public List<GameObject> stuffsToInactive;

    private int dialogueIndex = 0;

    private void Start()
    {
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

    public void LoadMusicTrack(MusicTrack track)
    {
        dialogues = track.DialogueAsset.text.Split('\n').ToList();
        dialogueTextBox.text = dialogues[0];
        gameObject.SetActive(true);
    }
}
