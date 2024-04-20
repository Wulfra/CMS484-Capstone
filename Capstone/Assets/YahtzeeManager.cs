using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class YahtzeeManager : MonoBehaviour
{
    public Button dieOne;
    public Button dieTwo;
    public Button dieThree;
    public Button dieFour;
    public Button dieFive;
    public Button roll;
    public Text scoreText;

    public GameObject holdOne;
    public GameObject holdTwo;
    public GameObject holdThree;
    public GameObject holdFour;
    public GameObject holdFive;

    public int score = 0;
    public int[] myArray = new int[5];
    public bool rollOne = true;
    public Color redColor;
    public Color whiteColor;

    bool dieOneHold = false;
    bool dieTwoHold = false;
    bool dieThreeHold = false;
    bool dieFourHold = false;
    bool dieFiveHold = false;
    bool gameActive = true;

    public Text timerText;
    public Text rollText;




    // Function to activate one of the specified child objects randomly
    public GameObject ActivateRandomChildObject(GameObject parentObject)
    {
        if (parentObject.name == "DiceOne" && dieOneHold == false ||
            parentObject.name == "DiceTwo" && dieTwoHold == false ||
            parentObject.name == "DiceThree" && dieThreeHold == false ||
            parentObject.name == "DiceFour" && dieFourHold == false ||
            parentObject.name == "DiceFive" && dieFiveHold == false)
        {
            GameObject[] childObjects = new GameObject[parentObject.transform.childCount];

            // Initialize the array with the references to the child objects
            for (int i = 0; i < parentObject.transform.childCount; i++)
            {
                childObjects[i] = parentObject.transform.GetChild(i).gameObject;
            }

            // Deactivate all child objects
            foreach (GameObject childObject in childObjects)
            {
                childObject.SetActive(false);
            }

            // Activate a random child object
            int randomIndex = UnityEngine.Random.Range(0, childObjects.Length);
            childObjects[randomIndex].SetActive(true);
            int storage = int.Parse(childObjects[randomIndex].name);
            if (parentObject.name == "DiceOne" && !dieOneHold)
            {
                myArray[0] = storage;
            }
            else if (parentObject.name == "DiceTwo" && !dieTwoHold)
            {
                myArray[1] = storage;
            }
            else if (parentObject.name == "DiceThree" && !dieThreeHold)
            {
                myArray[2] = storage;
            }
            else if (parentObject.name == "DiceFour" && !dieFourHold)
            {
                myArray[3] = storage;
            }
            else if (parentObject.name == "DiceFive" && !dieFiveHold)
            {
                myArray[4] = storage;
            }
            return childObjects[randomIndex];
        }
        return null;
    }



    void locateScore()
    {
        IsYahtzeeSmallStraight(myArray);
        IsYahtzeeLargeStraight(myArray);
        IsFullHouse(myArray);
        IsYahtzee(myArray);
        scoreText.text = "Score: " + score.ToString();
    }

    void IsYahtzeeSmallStraight(int[] values)
    {
        Array.Sort(values);
        for (int i = 0; i < values.Length - 1; i++)
        {
            if (values[i + 1] - values[i] != 1)
                return;
        }
        score += 30;
        return;
    }

    void IsYahtzeeLargeStraight(int[] values)
    {
        Array.Sort(values);
        for (int i = 0; i < values.Length - 1; i++)
        {
            if (values[i + 1] - values[i] != 1)
                return;
        }
        score += 10;
        return;

    }

    void IsFullHouse(int[] values)
    {
        Array.Sort(values);
        if ((values[0] == values[1] && values[1] == values[2] && values[3] == values[4]) ||
            (values[0] == values[1] && values[2] == values[3] && values[3] == values[4]))
            score += 25;
        return;

    }



    void IsYahtzee(int[] values)
    {
        for (int i = 0; i < values.Length - 1; i++)
        {
            if (values[i] != values[i + 1])
                return;
        }
        score += 50;
        return;
    }

    // Example usage
    void Start()
    {
        rollText.text = "Start";
        StartCoroutine(Countdown(60));
        roll.onClick.AddListener(RollDice);
        dieOne.onClick.AddListener(HoldOne);
        dieTwo.onClick.AddListener(HoldTwo);
        dieThree.onClick.AddListener(HoldThree);
        dieFour.onClick.AddListener(HoldFour);
        dieFive.onClick.AddListener(HoldFive);
        gameActive = true;
        score = 0;
    }

    void RollDice()
    {
        if (gameActive)
        {
            if (rollOne == true)
            {
                rollText.text = "Roll 2";
                rollOne = false;
                dieOneHold = false;
                dieTwoHold = false;
                dieThreeHold = false;
                dieFourHold = false;
                dieFiveHold = false;

            }
            else
            {
                rollText.text = "New Set";
                rollOne = true;
                locateScore();
                holdOne.SetActive(false);
                holdTwo.SetActive(false);
                holdThree.SetActive(false);
                holdFour.SetActive(false);
                holdFive.SetActive(false);
            }

            GameObject one = ActivateRandomChildObject(dieOne.gameObject);
            GameObject Two = ActivateRandomChildObject(dieTwo.gameObject);
            GameObject Three = ActivateRandomChildObject(dieThree.gameObject);
            GameObject Four = ActivateRandomChildObject(dieFour.gameObject);
            GameObject Five = ActivateRandomChildObject(dieFive.gameObject);

        }
    }

    private IEnumerator Countdown(float timeRemaining)
    {
        while (timeRemaining > 0)
        {
            timerText.text = "Time: " + timeRemaining.ToString();
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
        }

        // Timer is done
        Debug.Log("Time has run out!");
        timeRemaining = 0;

    }

    void HoldOne()
    {
        dieOneHold = !dieOneHold;
        holdOne.SetActive(true);

    }

    void HoldTwo()
    {
        dieTwoHold = !dieTwoHold;
        holdTwo.SetActive(true);
    }

    void HoldThree()
    {
        dieThreeHold = !dieThreeHold;
        holdThree.SetActive(true);
    }

    void HoldFour()
    {
        dieFourHold = !dieFourHold;
        holdFour.SetActive(true);
    }

    void HoldFive()
    {
        dieFiveHold = !dieFiveHold;
        holdFive.SetActive(true);
    }

    void ColorButton(Button button, bool isHold)
    {
        ColorBlock colors = button.colors;
        if (isHold)
        {

        }
        else
        {

        }

    }
}
