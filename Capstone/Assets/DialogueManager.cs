using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Store script
    public DialogueScript dScript;

    // Store dialogue to be printed
    public List<string> dialogueQueue = new List<string>{};

    // Driver ienumerator
    private IEnumerator runDialogueTest() {
        // Wait a bit
        yield return new WaitForSeconds(2.0f);

        // Run dialogue function
        StartCoroutine(dScript.printDialogue(dialogueQueue));
    }

    // Start is called before the first frame update
    void Start()
    {
        // Add some lines
        dialogueQueue.Add("Well... hello there.");
        dialogueQueue.Add("If you're seeing this, the test worked.");
        dialogueQueue.Add("So uh... that's cool, isn't it?");

        StartCoroutine("runDialogueTest");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
