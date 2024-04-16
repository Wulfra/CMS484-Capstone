using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Store scripts
    public DialogueScript dScript;
    public FileScript fScript;

    // Store dialogue to be printed
    public List<string> dialogueQueue = new List<string>{};

    // Store Requii variables
    public GameObject requii;
    public Animator requiiAnimator;

    // Driver ienumerator
    private IEnumerator runDialogueTest() {
        // Wait a bit
        yield return new WaitForSeconds(2.0f);
        dScript.dialogueRunning = true;

        // Start up requii
        requii.SetActive(true);
        yield return new WaitForSeconds(.8f);

        // Run dialogue function
        StartCoroutine(dScript.printDialogue(dialogueQueue));
    }

    private IEnumerator exitRequii() {
        requiiAnimator.SetBool("isExiting", true);

        yield return new WaitForSeconds(.8f);

        requii.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        // Add some lines
        dialogueQueue.Add("Well... hello there.");
        dialogueQueue.Add("If you're seeing this, the test worked.");
        dialogueQueue.Add("So uh... that's cool, isn't it?");
        */
        
        /*
        dialogueQueue = new List<string>(fScript.testDialogue2);

        StartCoroutine("runDialogueTest");
        */
        
        
        string tempString = "";

        requiiAnimator.SetBool("isTurning", true);

        List<int> testData = new List<int>(fScript.testVariable3);
        tempString = "Old Data: ";
        for (int i = 0; i < testData.Count; i++) {
            tempString += testData[i].ToString() + ' ';
        }
        dialogueQueue.Add(tempString);

        fScript.testVariable3 = new List<int>{105, 205};

        testData = new List<int>(fScript.testVariable3);
        tempString = "New Data: ";
        for (int i = 0; i < testData.Count; i++) {
            tempString += testData[i].ToString() + ' ';
        }
        dialogueQueue.Add(tempString);

        StartCoroutine("runDialogueTest");
        

    }

    // Update is called once per frame
    void Update()
    {

        if (requii.activeSelf && !dScript.dialogueRunning) {
            StartCoroutine("exitRequii");
        }
        
    }
}
