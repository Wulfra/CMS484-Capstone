using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Store script
    public RequiiScript rScript;

    // Store dialogue to be printed
    public List<string> dialogueQueue = new List<string>{};
    public List<List<string>> dialogueSet = new List<List<string>>{};

    // Start is called before the first frame update
    void Start()
    {
        
        // Add some lines
        dialogueQueue.Add("Well... hello there.");
        /*
        dialogueQueue.Add("If you're seeing this, the test worked.");
        dialogueQueue.Add("So uh... that's cool, isn't it?");
        */

        dialogueSet.Add(new List<string>() {"This is the first random set.", "Cool, isn't it? #1!"});
        dialogueSet.Add(new List<string>() {"This is the second random set.", "Cool, isn't it? #2!"});
        dialogueSet.Add(new List<string>() {"This is the third random set.", "Cool, isn't it? #3!"});
        
        StartCoroutine(rScript.runRandomRequiiDialogue(dialogueSet));
        

    }
}
