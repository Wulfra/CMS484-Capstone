using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequiiScript : MonoBehaviour
{
    // Requii object
    public GameObject requii;
    public Animator requiiAnimator;

    // Store scripts
    public DialogueScript dScript;
    public FileScript fScript;

    public void runRequii() {
        // Set requii active
        requii.SetActive(true);
    }

    public IEnumerator stopRequii() {
        requiiAnimator.SetBool("isExiting", true);

        yield return new WaitForSeconds(.9f);

        requii.SetActive(false);
    }

    public IEnumerator runRequiiDialogue(List<string> dialogue) {
        // Set requii active
        runRequii();

        // Wait for startup animation to finish
        yield return new WaitForSeconds(.9f);

        // Run dialogue script
        dScript.printDialogue(dialogue);

        // Wait until dialogue is done
        while (dScript.dialogueRunning) {
            yield return new WaitForSeconds(.1f);
        }

        // Exit requii
        stopRequii();
    }

    public void runRandomRequiiDialogue(List<List<string>> randomDialogue) {
        // Pick a random line from the list of lists
        int chosenNum = Random.Range(0, randomDialogue.Count);

        // Play the chosen dialogue
        runRequiiDialogue(randomDialogue[chosenNum]);
    }

}
