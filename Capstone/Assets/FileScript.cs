using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileScript : MonoBehaviour
{
    
    // Requii dialogue
    public List<string> testDialogue1 = new List<string> {
        "Well... this is getting harder, isn't it?",
        "Don't worry, I'm sure you'll get it soon",
        "Maybe... maybe not. Hopefully yes!"
    };
    public List<string> testDialogue2 = new List<string> {
        "Let's get that search function tested!",
        "I wonder if this will be picked up?",
        "If you're seeing this, the answer is yes!"
    };
    public List<string> testDialogue3 = new List<string> {
        "The end of the line is now.",
        "Okay, maybe that's a bit too dramatic.",
        "Maybe... maybe not. Hopefully yes!"
    };

    // Module data
    public List<int> testVariable1 = new List<int> {
        34
    };
    public List<int> testVariable2 = new List<int> {
        1, 2, 3, 4, 5
    };
    public List<int> testVariable3 = new List<int> {
        100, 200
    };
    public List<int> outlierVariable = new List<int> {
        8, 0, 0, 8
    };

}
