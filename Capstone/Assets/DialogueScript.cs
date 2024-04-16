using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    // Store game objects
    public Text dialogueBox;
    public AudioSource dialogueSound;
    public GameObject dialogueBoxObject;

    // Store each individual character to print
    private List<char> charactersToPrint = new List<char>{};

    // Check is dialogue is running
    public bool dialogueRunning = false;

    public void resetDialogue() {
        // Set dialogue to empty string
        dialogueBox.text = "";
    }

    public void enableDialogueBox() {
        dialogueBoxObject.SetActive(true);
    }

    public void disableDialogueBox() {
        dialogueBoxObject.SetActive(false);
    }

    public IEnumerator printDialogue (List<string> dialogue) {
        dialogueRunning = true;

        // Enable dialogue box object
        enableDialogueBox();
        
        // Repeat for each line of dialogue given in the queue
        for (int d = 0; d < dialogue.Count; d++) {
            // Reset characters to print
            charactersToPrint = new List<char>{};

            // Store current list item
            string currentDialogue = dialogue[d];

            // Get all the individual characters of a string
            for (int i = 0; i < currentDialogue.Length; i++) {
                charactersToPrint.Add(currentDialogue[i]);
            }

            // Print each character with a small delay and sound effect
            for (int i = 0; i < charactersToPrint.Count; i++) {
                // Cut last character off dialogue box text
                string currentText = "";
                if (dialogueBox.text != "") {
                    currentText = dialogueBox.text.Substring(2, i);
                }

                dialogueBox.text = "> " + currentText + charactersToPrint[i] + '|';

                dialogueSound.Play();

                yield return new WaitForSeconds(.075f);
            }

            for (int t = 0; t < 5; t++) {
                yield return new WaitForSeconds(.5f);

                if (dialogueBox.text != "") {
                    if (dialogueBox.text.Substring(dialogueBox.text.Length - 1) == "|") {
                        dialogueBox.text = dialogueBox.text.Substring(0, dialogueBox.text.Length - 1);
                    } else {
                        dialogueBox.text = dialogueBox.text + "|";
                        dialogueSound.Play();
                    }
                }
                
            }

            // Reset dialogue box
            resetDialogue();
        }

        // Disable dialogue box object
        disableDialogueBox();
        
        dialogueRunning = false;
    }
}
