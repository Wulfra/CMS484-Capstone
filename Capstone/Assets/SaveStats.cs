using System.Collections.Generic;
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

    public float RelaxOneTime;
    public float RelaxTwoTime;
    public float RelaxThreeTime;

    public Text RelaxTimeTotal;
    public Text RelaxOne;
    public Text RelaxTwo;
    public Text RelaxThree;

    public float LogicOneTime;
    public float LogicTwoTime;
    public float LogicThreeTime;

    public Text LogicTimeTotal;
    public Text LogicTimeOne;
    public Text LogicTimeTwo;
    public Text LogicTimeThree;

    public float MemoryOneTime;
    public float MemoryTwoTime;
    public float MemoryThreeTime;

    public Text MemoryTimeTotal;
    public Text MemoryTimeOne;
    public Text MemoryTimeTwo;
    public Text MemoryTimeThree;

    public Text cat;
    public Text mod;
    public List<float> allTimes = new List<float>();

    public Text mostTimeModuleText; 
    public Text mostTimeCategoryText; 

    private Dictionary<string, float> moduleTimes = new Dictionary<string, float>();
    private Dictionary<string, float> categoryTimes = new Dictionary<string, float>();


    void Update()
    {
        FocusOneTime = PlayerPrefs.GetFloat("Focus1Time", 0);
        FocusTwoTime = PlayerPrefs.GetFloat("Focus2Time", 0);
        FocusThreeTime = PlayerPrefs.GetFloat("Focus3Time", 0);
        focusTime.text = "Total Time in Focus: " + (((int)FocusOneTime) + ((int)FocusTwoTime) + ((int)FocusThreeTime)).ToString();
        
        KnapSackScore = PlayerPrefs.GetInt("KnapSackHigh", 0);
        knapSackText.text = "KnapSack High Score: " + KnapSackScore;
        YahtzeeScore = PlayerPrefs.GetInt("YahtzeeHigh", 0);
        YahtzeeText.text = "Yahtzee High Score: " + YahtzeeScore;
        MazesSolved = PlayerPrefs.GetInt("MazesSolved", 0);
        mazeText.text = "Mazes Solved: " + MazesSolved;

        RelaxOneTime = PlayerPrefs.GetFloat("RelaxOneClock", 0);
        RelaxTwoTime = PlayerPrefs.GetFloat("RelaxTwoClock", 0);
        RelaxThreeTime = PlayerPrefs.GetFloat("RelaxThreeClock", 0);

        MemoryOneTime = PlayerPrefs.GetFloat("MemoryOneClock", 0);
        MemoryTwoTime = PlayerPrefs.GetFloat("MemoryTwoClock", 0);
        MemoryThreeTime = PlayerPrefs.GetFloat("MemoryThreeClock", 0);

        LogicOneTime = PlayerPrefs.GetFloat("LogicOneClock", 0);
        LogicTwoTime = PlayerPrefs.GetFloat("LogicTwoClock", 0);
        LogicThreeTime = PlayerPrefs.GetFloat("LogicThreeClock", 0);

        RelaxOne.text = ("Time Spent In Bit Rain: " + (int)RelaxOneTime);
        RelaxTwo.text = ("Time Spent In Painting: " + (int)RelaxTwoTime);
        RelaxThree.text = ("Time Spent In Sorting: " + (int)RelaxThreeTime);
        RelaxTimeTotal.text = "Total Time in Relax: " + (((int)RelaxOneTime) + ((int)RelaxTwoTime) + ((int)RelaxThreeTime)).ToString();

        LogicTimeOne.text = ("Time Spent In Converter: " + (int)LogicOneTime);
        LogicTimeTwo.text = ("Time Spent In Cipher: " + (int)LogicTwoTime);
        LogicTimeThree.text = ("Time Spent In Truth: " + (int)LogicThreeTime);
        LogicTimeTotal.text = "Total Time in Logic: " + (((int)LogicOneTime) + ((int)LogicTwoTime) + ((int)LogicThreeTime)).ToString();

        MemoryTimeOne.text = ("Time Spent In A/Q Quiz: " + (int)MemoryOneTime);
        MemoryTimeTwo.text = ("Time Spent In Memory: " + (int)MemoryTwoTime);
        MemoryTimeThree.text = ("Time Spent In Match: " + (int)MemoryThreeTime);
        MemoryTimeTotal.text = "Total Time in Memory: " + (((int)MemoryOneTime) + ((int)MemoryTwoTime) + ((int)MemoryThreeTime)).ToString();
    }

    void Start()
    {
        UpdateTimeTracking();
    }

    void UpdateTimeTracking()
    {
        // Reset dictionaries
        moduleTimes.Clear();
        categoryTimes.Clear();

        // Update module times
        UpdateModuleTime("Focus", new float[] { FocusOneTime, FocusTwoTime, FocusThreeTime });
        UpdateModuleTime("Relax", new float[] { RelaxOneTime, RelaxTwoTime, RelaxThreeTime });
        UpdateModuleTime("Logic", new float[] { LogicOneTime, LogicTwoTime, LogicThreeTime });
        UpdateModuleTime("Memory", new float[] { MemoryOneTime, MemoryTwoTime, MemoryThreeTime });

        // Find module with the most time
        string mostTimeModule = GetKeyWithMaxValue(moduleTimes);

        // Update category times
        UpdateCategoryTime("Focus", FocusOneTime + FocusTwoTime + FocusThreeTime);
        UpdateCategoryTime("Relax", RelaxOneTime + RelaxTwoTime + RelaxThreeTime);
        UpdateCategoryTime("Logic", LogicOneTime + LogicTwoTime + LogicThreeTime);
        UpdateCategoryTime("Memory", MemoryOneTime + MemoryTwoTime + MemoryThreeTime);

        // Find category with the most time
        string mostTimeCategory = GetKeyWithMaxValue(categoryTimes);

        // Update UI
        mostTimeModuleText.text = "Favorite Module is in: " + mostTimeModule;
        mostTimeCategoryText.text = "Category With Most Time: " + mostTimeCategory;
    }

    void UpdateModuleTime(string moduleName, float[] times)
    {
        float totalTime = 0f;
        foreach (float time in times)
        {
            totalTime += time;
        }
        moduleTimes.Add(moduleName, totalTime);
    }

    void UpdateCategoryTime(string categoryName, float time)
    {
        if (!categoryTimes.ContainsKey(categoryName))
            categoryTimes.Add(categoryName, time);
        else
            categoryTimes[categoryName] += time;
    }

    string GetKeyWithMaxValue(Dictionary<string, float> dict)
    {
        string maxKey = "";
        float maxValue = float.MinValue;
        foreach (var pair in dict)
        {
            if (pair.Value > maxValue)
            {
                maxKey = pair.Key;
                maxValue = pair.Value;
            }
        }
        return maxKey;
    }

   


}
