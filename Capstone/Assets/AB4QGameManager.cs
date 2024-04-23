using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Requii script
    public RequiiScript rScript;
    public FileScript fScript;

    // Game control variables
    public Vector3 mouseLocation;
    public RaycastHit2D hit;
    public Camera cam;
    public bool canClick = true;
    public bool gameOver = false;
    public bool gameStarted = false;
    public bool timerRunning = false;
    public float difficulty = 8.0f;

    // Touch control variables
    Vector3 touchLocation;
    public bool canTouch = true;

    // Object storage variables
    public Text questionBox;
    public Text answerBox1;
    public Text answerBox2;
    public Text answerBox3;
    public Text answerBox4;
    GameObject[] blinders;
    GameObject questionBlinder;

    // Menu storage variables
    private GameObject menuBackground;
    private GameObject menuButton1;
    private GameObject menuButton2;
    private GameObject menuButton3;
    private GameObject backButton;
    private GameObject requiiButton;
    public Text textBox1;
    public Text textBox2;
    public Text textBox3;

    // Game data variables
    public List<string> questions = new List<string>{};
    public List<string> answers = new List<string>{};
    public List<string> finalAnswers = new List<string>{};
    int chosenQuestionNumber;

    private List<string> generateAnswers() {

        // Create a list of 4 random answers
        List<string> temporaryAnswers = new List<string>(answers);
        List<string> chosenAnswers = new List<string>{};
        int numAnswers = 0;
        int randomAnswer;
        

        while (numAnswers < 4) {
            randomAnswer = Random.Range(0, temporaryAnswers.Count);
            chosenAnswers.Add(temporaryAnswers[randomAnswer]);
            temporaryAnswers.RemoveAt(randomAnswer);
            numAnswers++;
        }

        return chosenAnswers;
    }

    private List<string> generateQuestion() {

        // Choose a question
        chosenQuestionNumber = Random.Range(0, questions.Count);
        questionBox.text = questions[chosenQuestionNumber];

        // Check if a random answer set has at least one correct answer for the question
        List<string> tentativeAnswers = new List<string>{};
        bool hasCorrectAnswer = false;

        while (!hasCorrectAnswer) {
            tentativeAnswers = generateAnswers();

            // Loop through answers, check if each are correct for the question
            foreach (string tentativeAnswer in tentativeAnswers) {
                if (tentativeAnswer.Substring(tentativeAnswer.Length - 1) == chosenQuestionNumber.ToString()) {
                    hasCorrectAnswer = true;
                }
            }
        }

        answerBox1.text = tentativeAnswers[0].Substring(0, tentativeAnswers[0].Length - 1);
        answerBox2.text = tentativeAnswers[1].Substring(0, tentativeAnswers[1].Length - 1);
        answerBox3.text = tentativeAnswers[2].Substring(0, tentativeAnswers[2].Length - 1);
        answerBox4.text = tentativeAnswers[3].Substring(0, tentativeAnswers[3].Length - 1);
        

        return tentativeAnswers;

    }

    private IEnumerator Countdown() {
        timerRunning = true;
        backButton.SetActive(false);
        requiiButton.SetActive(false);

        yield return new WaitForSeconds(difficulty);

        foreach (GameObject blinder in blinders)
        {
            blinder.SetActive(true);
        }
        questionBlinder.SetActive(false);

        backButton.SetActive(true);
        requiiButton.SetActive(true);
        timerRunning = false;
    }

    private void disableMenu() {
        // Disable menu and its buttons except for back
        menuBackground.SetActive(false);
        menuButton1.SetActive(false);
        menuButton2.SetActive(false);
        menuButton3.SetActive(false);
        textBox1.text = "";
        textBox2.text = "";
        textBox3.text = "";
    }

    private void enableMenu() {
        // Disable menu and its buttons except for back
        menuBackground.SetActive(true);
        menuButton1.SetActive(true);
        menuButton2.SetActive(true);
        menuButton3.SetActive(true);
        textBox1.text = "Easy";
        textBox2.text = "Medium";
        textBox3.text = "Hard";
    }

    // Start is called before the first frame update
    void Start()
    {

        // Find and store game objects
        blinders = GameObject.FindGameObjectsWithTag("Blinder");
        questionBlinder = GameObject.FindGameObjectWithTag("QuestionBlinder");

        // Store menu items
        menuBackground = GameObject.Find("MenuBackground");
        menuButton1 = GameObject.Find("MenuButton1");
        menuButton2 = GameObject.Find("MenuButton2");
        menuButton3 = GameObject.Find("MenuButton3");
        backButton = GameObject.Find("BackButton");
        requiiButton = GameObject.Find("RequiiButton");

        // Questions
        questions.Add("Create a list of type int!");
        questions.Add("Write a XOR operation!");
        questions.Add("Which is incorrect Syntax?");
        questions.Add("Access list vars at index 1!");
        
        // Answers
        answers.Add("int[] list;f");
        answers.Add("list = int[1, 2, 3]f");
        answers.Add("List<int> list;f");
        answers.Add("list = int<1, 2, 3>0");
        answers.Add("bool1 | bool2f");
        answers.Add("bool1 ^ bool21");
        answers.Add("bool1 !| bool2f");
        answers.Add("bool1 & !bool2f");
        answers.Add("var1 =! 2;2");
        answers.Add("var1 <= 3;f");
        answers.Add("var1 == var2;f");
        answers.Add("var1 = var2;f");
        answers.Add("vars.indexat(1)f");
        answers.Add("vars.[1]f");
        answers.Add("vars[1]3");
        answers.Add("vars(1)f");

        // Prepare the game
        foreach (GameObject blinder in blinders)
        {
            blinder.SetActive(false);
        }
        finalAnswers = generateQuestion();

    }

    // Update is called once per frame
    void Update()
    {
        
        // Start of touch control block
        if (canTouch && (Input.touchCount > 0) && !timerRunning) {
            canTouch = false;

            Touch touch = Input.GetTouch(0);

            touchLocation = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
            hit = Physics2D.Raycast(new Vector2(touchLocation.x, touchLocation.y), new Vector2(0, 0));

            if (gameOver) {
                // If game is over and mouse is clicked, reset game and go back to menu
                
                gameOver = false;
                gameStarted = false;

                for (int i = 1; i < 5; i++) {
                    GameObject.Find("BlinderBackground" + i.ToString()).GetComponent<SpriteRenderer>().color = new Color(.275f, .275f, .275f, 1f);
                }

                foreach (GameObject blinder in blinders)
                {
                    blinder.SetActive(false);
                }

                questionBlinder.SetActive(true);
                finalAnswers = generateQuestion();
                enableMenu();

            } else if (hit.collider != null) {

                GameObject answer = hit.collider.gameObject;

                if (answer.name == "RequiiButton" && !rScript.requiiRunning) {

                    StartCoroutine(rScript.runRequiiDialogue(fScript.memory1Tutorial));

                } else if (answer.name == "BackButton") {
                    // If back is clicked while game is started, reset the game and go back to menu
                    // Otherwise, go back to menu scene
                    if (gameStarted) {
                        gameOver = false;
                        gameStarted = false;

                        for (int i = 1; i < 5; i++) {
                            GameObject.Find("BlinderBackground" + i.ToString()).GetComponent<SpriteRenderer>().color = new Color(.275f, .275f, .275f, 1f);
                        }

                        foreach (GameObject blinder in blinders)
                        {
                            blinder.SetActive(false);
                        }

                        questionBlinder.SetActive(true);
                        finalAnswers = generateQuestion();
                        enableMenu();
                    } else {
                        SceneManager.LoadScene("Menu");
                    }
                    
                } else if (!gameStarted && (answer.name.Substring(0, 4) == "Menu")) {
                    // Check for menu button clicks

                    string answerName = answer.name;

                    if (answerName == "MenuButton1") {
                        difficulty = 8.0f;
                    } else if (answerName == "MenuButton2") {
                        difficulty = 5.0f;
                    } else if (answerName == "MenuButton3") {
                        difficulty = 2.0f;
                    }

                    gameStarted = true;
                    disableMenu();
                    StartCoroutine("Countdown");                    
                    
                } else if ((answer.name != "RequiiButton") && gameStarted) {
                    string answerNumberString = answer.name.Substring(answer.name.Length - 1);
                    int answerNumber = int.Parse(answerNumberString) - 1;
                    string chosenAnswerString = finalAnswers[answerNumber].Substring(finalAnswers[answerNumber].Length - 1);
                    int chosenAnswerNumber;
                    if (chosenAnswerString == "f") {
                        chosenAnswerNumber = -1;
                    } else {
                        chosenAnswerNumber = int.Parse(chosenAnswerString);
                    }

                    questionBlinder.SetActive(false);

                    if (chosenAnswerNumber == chosenQuestionNumber) {
                        questionBox.text = "Correct!";
                        GameObject.Find("BlinderBackground" + answerNumberString).GetComponent<SpriteRenderer>().color = new Color(.24f, .6f, .35f, 1f);

                        foreach (GameObject blinder in blinders)
                        {
                            blinder.SetActive(false);
                        }
                    } else {
                        questionBox.text = "Incorrect!";
                        GameObject.Find("BlinderBackground" + answerNumberString).GetComponent<SpriteRenderer>().color = new Color(.6f, .28f, .24f, 1f);

                        for (int i = 1; i < 5; i++) {
                            int tempAnswerNumber = i - 1;
                            string tempChosenAnswerString = finalAnswers[tempAnswerNumber].Substring(finalAnswers[tempAnswerNumber].Length - 1);
                            int tempChosenAnswerNumber;
                            if (tempChosenAnswerString == "f") {
                                tempChosenAnswerNumber = -1;
                            } else {
                                tempChosenAnswerNumber = int.Parse(tempChosenAnswerString);
                            }

                            if (tempChosenAnswerNumber == chosenQuestionNumber) {
                                GameObject.Find("BlinderBackground" + i.ToString()).GetComponent<SpriteRenderer>().color = new Color(.24f, .6f, .35f, 1f);
                            }
                        }

                        foreach (GameObject blinder in blinders)
                        {
                            blinder.SetActive(false);
                        }
                    }

                    gameOver = true;
                } else {

                }

            } else {

            }
        }

        if (Input.touchCount == 0)
        {
            canTouch = true;
        }
        // End of touch control block

    }
}
