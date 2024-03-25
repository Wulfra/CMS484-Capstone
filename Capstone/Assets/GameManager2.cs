using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    // Mouse control variables
    Vector3 mouseLocation;
    RaycastHit2D hit;
    public Camera cam;
    public bool canClick = true;
    public bool gameOver = false;
    public bool timerRunning = false;

    // Obect storing variables
    public Text questionBox;
    public Text answerBox1;
    public Text answerBox2;
    public Text answerBox3;
    public Text answerBox4;
    Text[] answerBoxes = new Text[4];
    GameObject[] blinderBackgrounds;

    // Game variables
    public List<int> gameVariables = new List<int>();
    public List<int> chosenVariables = new List<int>();
    public int correctAnswerNumber;

    private void DisableBackgroundText() {

        // Disable answer text boxes and their backgrounds
        foreach (GameObject blinderBackground in blinderBackgrounds)
        {
            blinderBackground.SetActive(false);
        }
        foreach (Text answerBox in answerBoxes)
        {
            answerBox.text = "";
        }

    }

    private void EnableBackgroundText() {

        // Disable answer text boxes and their backgrounds
        foreach (GameObject blinderBackground in blinderBackgrounds)
        {
            blinderBackground.SetActive(true);
        }
        
    }

    private void DisableForegroundText() {

        // Disable the question text box
        questionBox.text = "";

    }

    private void ResetGameVariables() {

        // Reset the list
        gameVariables = new List<int>();

        // Populate the list with 26 0's
        for (int ae = 0; ae < 26; ae++) {
            gameVariables.Add(0);
        }

    }

    private string ShowVariableLetter(int indexNumber) {

        // Return alphabet letter based on index position given in function call
        if (indexNumber == 0) {
            return "a";
        } else if (indexNumber == 1) {
            return "b";
        } else if (indexNumber == 2) {
            return "c";
        } else if (indexNumber == 3) {
            return "d";
        } else if (indexNumber == 4) {
            return "e";
        } else if (indexNumber == 5) {
            return "f";
        } else if (indexNumber == 6) {
            return "g";
        } else if (indexNumber == 7) {
            return "h";
        } else if (indexNumber == 8) {
            return "i";
        } else if (indexNumber == 9) {
            return "j";
        } else if (indexNumber == 10) {
            return "k";
        } else if (indexNumber == 11) {
            return "l";
        } else if (indexNumber == 12) {
            return "m";
        } else if (indexNumber == 13) {
            return "n";
        } else if (indexNumber == 14) {
            return "o";
        } else if (indexNumber == 15) {
            return "p";
        } else if (indexNumber == 16) {
            return "q";
        } else if (indexNumber == 17) {
            return "r";
        } else if (indexNumber == 18) {
            return "s";
        } else if (indexNumber == 19) {
            return "t";
        } else if (indexNumber == 20) {
            return "u";
        } else if (indexNumber == 21) {
            return "v";
        } else if (indexNumber == 22) {
            return "w";
        } else if (indexNumber == 23) {
            return "x";
        } else if (indexNumber == 24) {
            return "y";
        } else if (indexNumber == 25) {
            return "z";
        } else {
            return "Invalid Index";
        }

    }

    private IEnumerator RunGame() {
        // Prevent input until completion of sequence
        timerRunning = true;

        // Difficulty variables
        int numVariables = Random.Range(1, 6);
        int numSteps = Random.Range(1, 6);

        // Reset chosen variables
        chosenVariables = new List<int>();

        // Choose indices to be used
        for (int ea = 0; ea < numVariables; ea++) {
            chosenVariables.Add(Random.Range(0, 26));
        }

        // Reset display and game variables
         for (int i = 1; i < 5; i++) {
                GameObject.Find("BlinderBackground" + i.ToString()).GetComponent<SpriteRenderer>().color = new Color(.275f, .275f, .275f, 1f);
        }
        DisableBackgroundText();;
        ResetGameVariables();

        // Display each starting variable
        foreach (int chosen in chosenVariables) {
            gameVariables[chosen] = Random.Range(0, 21);

            questionBox.text = ShowVariableLetter(chosen) + " = " + gameVariables[chosen].ToString();

            // Display each for 3 seconds
            yield return new WaitForSeconds(3.0f);
        }

        // Main game loop
        for (int stepCount = 0; stepCount < numSteps; stepCount++) {

            // Pick one chosen variable to be modified this loop
            int currentVariable = chosenVariables[Random.Range(0, chosenVariables.Count)];

            // Pick one random action to be performed on variable
            int randomAction = Random.Range(0, 4);

            if (randomAction == 0) {

                int randomNumber = Random.Range(1, 11);
                
                // Display for 3 seconds
                questionBox.text = ShowVariableLetter(currentVariable) + " += " + randomNumber.ToString();
                yield return new WaitForSeconds(3.0f);

                // Add 1 - 10
                gameVariables[currentVariable] += randomNumber;

            } else if (randomAction == 1) {

                int randomNumber = Random.Range(1, 11);
                
                // Display for 3 seconds
                questionBox.text = ShowVariableLetter(currentVariable) + " -= " + randomNumber.ToString();
                yield return new WaitForSeconds(3.0f);

                // Add 1 - 10
                gameVariables[currentVariable] -= randomNumber;

            } else if (randomAction == 2) {

                int randomNumber = Random.Range(1, 4);
                
                // Display for 3 seconds
                questionBox.text = ShowVariableLetter(currentVariable) + " *= " + randomNumber.ToString();
                yield return new WaitForSeconds(3.0f);

                // Add 1 - 10
                gameVariables[currentVariable] *= randomNumber;
                
            } else if (randomAction == 3) {

                int randomNumber = Random.Range(1, 4);
                
                // Display for 3 seconds
                questionBox.text = ShowVariableLetter(currentVariable) + " /= " + randomNumber.ToString();
                yield return new WaitForSeconds(3.0f);

                // Add 1 - 10
                gameVariables[currentVariable] /= randomNumber;
                
            }

        }
        
        // Enable answer background and disable question box
        EnableBackgroundText();
        DisableForegroundText();

        // Decide winning variable
        int winningVariable = chosenVariables[Random.Range(0, chosenVariables.Count)];

        // Decide which answer box will have the correct answer
        correctAnswerNumber = Random.Range(1, 5);

        for (int textBoxNumber = 1; textBoxNumber < 5; textBoxNumber++) {

            // Assign a random number if not correct box, assign correct answer to correct box
            if (textBoxNumber == correctAnswerNumber) {
                answerBoxes[textBoxNumber - 1].text = ShowVariableLetter(winningVariable) + " == " + gameVariables[winningVariable].ToString();
            } else {
                answerBoxes[textBoxNumber - 1].text = ShowVariableLetter(winningVariable) + " == " + (gameVariables[winningVariable] + Random.Range(1, 11)).ToString();
            }

        }

        // Re-enable input
        timerRunning = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Store GameObjects in variables
        blinderBackgrounds = GameObject.FindGameObjectsWithTag("Blinder");
        
        answerBoxes[0] = answerBox1;
        answerBoxes[1] = answerBox2;
        answerBoxes[2] = answerBox3;
        answerBoxes[3] = answerBox4;

        // Start the game
        StartCoroutine(RunGame());

    }

    // Update is called once per frame
    void Update()
    {
        if (canClick && Input.GetMouseButtonDown(0) && gameOver && !timerRunning) {
            canClick = false;
            gameOver = false;
            
            StartCoroutine(RunGame());

        } else if (canClick && Input.GetMouseButtonDown(0) && !timerRunning) {
            canClick = false;

            mouseLocation = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            hit = Physics2D.Raycast(new Vector2(mouseLocation.x, mouseLocation.y), new Vector2(0, 0));

            if (hit.collider != null) {
                GameObject answer = hit.collider.gameObject;
                string answerNumberString = answer.name.Substring(answer.name.Length - 1);
                int answerNumber = int.Parse(answerNumberString);
                
                if (answerNumber == correctAnswerNumber) {

                    GameObject.Find("BlinderBackground" + answerNumberString).GetComponent<SpriteRenderer>().color = new Color(.24f, .6f, .35f, 1f);

                } else {

                    GameObject.Find("BlinderBackground" + answerNumberString).GetComponent<SpriteRenderer>().color = new Color(.6f, .28f, .24f, 1f);

                    GameObject.Find("BlinderBackground" + correctAnswerNumber.ToString()).GetComponent<SpriteRenderer>().color = new Color(.24f, .6f, .35f, 1f);

                }

                gameOver = true;

            } else {

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            canClick = true;
        }
        
    }
}
