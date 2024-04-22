using UnityEngine;
using UnityEngine.UI;

public class SaveStats : MonoBehaviour
{
    public int KnapSackScore;
    public Text knapSackText;
    public int YahtzeeScore;
    public Text YahtzeeText;
    public int MazesSolved;
    public Text mazeText;

    public Text focusTime;
    public float FocusOneTime;
    public float FocusTwoTime;
    public float FocusThreeTime;

    void Start()
    {
        FocusOneTime = PlayerPrefs.GetFloat("Focus1Time", 0);
        FocusTwoTime = PlayerPrefs.GetFloat("Focus2Time", 0);
        FocusThreeTime = PlayerPrefs.GetFloat("Focus3Time", 0);
        focusTime.text = "Total Time in Focus: " + (FocusOneTime + FocusTwoTime + FocusTwoTime).ToString();

        KnapSackScore = PlayerPrefs.GetInt("KnapSackHigh", 0);
        knapSackText.text = "KnapSack High Score: " + KnapSackScore;
        YahtzeeScore = PlayerPrefs.GetInt("YahtzeeHigh", 0);
        YahtzeeText.text = "Yahtzee High Score: " + YahtzeeScore;
        MazesSolved = PlayerPrefs.GetInt("mazesSolved", 0);
        mazeText.text = "Mazes Solved: " + MazesSolved;


    }

    void Update()
    {
        
    }

    
}
