using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager3 : MonoBehaviour
{
    // Requii script
    public RequiiScript rScript;
    public FileScript fScript;

    // Mouse control variables
    Vector3 mouseLocation;
    RaycastHit2D hit;
    public Camera cam;
    public bool canClick = true;
    public bool gameOver = false;
    public bool gameStarted = false;
    public bool timerRunning = false;

    // Touch control variables
    Vector3 touchLocation;
    public bool canTouch = true;

    // Object storing variables
    public Text questionBox;
    public Text answerBox1;
    public Text answerBox2;
    public Text answerBox3;
    public Text answerBox4;
    public Text answerBox5;
    public Text answerBox6;
    public Text answerBox7;
    public Text answerBox8;
    Text[] answerBoxes = new Text[8];
    GameObject[] cards;

    // Question/answer storing variables
    public List<string> javaQuestions = new List<string>();
    public List<string> javaScriptQuestions = new List<string>();
    public List<string> cSharpQuestions = new List<string>();
    public List<string> pythonQuestions = new List<string>();

    // Game variables
    public List<string> cardQuestions = new List<string>();
    public List<string> cardAnswers = new List<string>();
    public List<int> chosenCards = new List<int>();
    public int lives = 3;
    public int score = 0;
    public int difficulty = 3;

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

    private void DisableBackgroundText() {

        // Disable answer text boxes and their backgrounds
        foreach (GameObject card in cards)
        {
            card.SetActive(false);
        }
        foreach (Text answerBox in answerBoxes)
        {
            answerBox.text = "";
        }

    }

    private void EnableBackgroundText() {

        // Enable answer text boxes and their backgrounds
        foreach (GameObject card in cards)
        {
            card.SetActive(true);
        }
        
    }

    private void DisableForegroundText() {

        // Disable the question text box
        questionBox.text = "";

    }

    private void ResetCardColors() {

        // Select each card and set to default color
        foreach (GameObject card in cards) {
                card.GetComponent<SpriteRenderer>().color = new Color(.275f, .275f, .275f, 1f);
        }

    }

    private void ResetGameVariables() {

        // Reset the text
        DisableBackgroundText();
        DisableForegroundText();

        // Enable all the cards
        EnableBackgroundText();

        // Reset the card colors
        ResetCardColors();

        // Reset the variables
        cardQuestions = new List<string>();
        cardAnswers = new List<string>();
        chosenCards = new List<int>();
        lives = 3;
        score = 0;

    }

    private void checkCards(int cardNumber) {

        // Add card index to chosen cards
        chosenCards.Add(cardNumber - 1);

        // Highlight the cards
        GameObject.Find("Card" + cardNumber).GetComponent<SpriteRenderer>().color = new Color(.6f, .6f, .6f, 1f);

        // Reveal the card's contents
        answerBoxes[cardNumber - 1].text = cardQuestions[cardNumber - 1];

        // Compare cards if second chosen card
        if (chosenCards.Count == 2) {
            string previousCardAnswer = cardAnswers[chosenCards[0]];
            string currentCardAnswer = cardAnswers[chosenCards[1]];

            // Add score, remove cards if right, hide card contents and remove lives if wrong
            if (chosenCards[0] == chosenCards[1]) {
                lives -= 1;
                answerBoxes[chosenCards[0]].text = "";
                answerBoxes[chosenCards[1]].text = "";
            } else if (previousCardAnswer == currentCardAnswer) {
                score += 1;
                answerBoxes[chosenCards[0]].text = "";
                answerBoxes[chosenCards[1]].text = "";

                GameObject.Find("Card" + (chosenCards[0] + 1)).SetActive(false);
                GameObject.Find("Card" + (chosenCards[1] + 1)).SetActive(false);
            } else {
                lives -= 1;
                answerBoxes[chosenCards[0]].text = "";
                answerBoxes[chosenCards[1]].text = "";
            }

            // Reset chosen cards and card colors
            chosenCards = new List<int>();
            ResetCardColors();

            // End game with a loss if no more lives left
            if (lives <= 0) {
                DisableBackgroundText();
                questionBox.text = "You lose.";
                gameOver = true;
            }

            // End game with a win if score at 4 or above
            if (score >= 4) {
                DisableBackgroundText();
                questionBox.text = "You win!";
                gameOver = true;
            }
        }

    }

    private void RandomizeCards() {

        // Create temporary copies of questions lists
        List<string> tempJavaQuestions = new List<string>(javaQuestions);
        List<string> tempJavaScriptQuestions = new List<string>(javaScriptQuestions);
        List<string> tempCSharpQuestions = new List<string>(cSharpQuestions);
        List<string> tempPythonQuestions = new List<string>(pythonQuestions);

        // Create loop variables that makes pairs of coding languages be chosen
        bool secondInPair = false;
        int chosenCategoryNumber = 0;

        // Loop until completion of question and answer lists
        while (cardQuestions.Count < 8) {

            if (!secondInPair) {
                // Pick a random category of questions
                chosenCategoryNumber = Random.Range(1, 5);
                secondInPair = true;
            } else {
                secondInPair = false;
            }
            
            string chosenCategoryLetter = "";
            List<string> chosenCategory = new List<string>();

            // Pick letter of coding language based on chosen category
            if (chosenCategoryNumber == 1) {
                chosenCategoryLetter = "j";
            } else if (chosenCategoryNumber == 2) {
                chosenCategoryLetter = "s";
            } else if (chosenCategoryNumber == 3) {
                chosenCategoryLetter = "c";
            } else if (chosenCategoryNumber == 4) {
                chosenCategoryLetter = "p";
            }

            // Assign list in memory based on chosen category
            if (chosenCategoryNumber == 1) {
                chosenCategory = tempJavaQuestions;
            } else if (chosenCategoryNumber == 2) {
                chosenCategory = tempJavaScriptQuestions;
            } else if (chosenCategoryNumber == 3) {
                chosenCategory = tempCSharpQuestions;
            } else if (chosenCategoryNumber == 4) {
                chosenCategory = tempPythonQuestions;
            }

            // Pick a random question within chosen category
            int chosenQuestion = Random.Range(0, chosenCategory.Count);


            if (chosenCategory.Count != 0) {

                // Add to question and answer
                cardQuestions.Add(chosenCategory[chosenQuestion]);
                cardAnswers.Add(chosenCategoryLetter);

                // Remove chosen question from list
                chosenCategory.RemoveAt(chosenQuestion);

            }
        }

    }

    private IEnumerator CountdownToGameStart() {
        timerRunning = true;
        backButton.SetActive(false);
        requiiButton.SetActive(false);

        // Reset all variables
        ResetGameVariables();
        lives = difficulty;

        // Assign cards a question and answer
        RandomizeCards();

        // Reveal every card
        for (int i = 0; i < 8; i++) {
            answerBoxes[i].text = cardQuestions[i];
        }

        yield return new WaitForSeconds(5.0f); // wait 5 seconds

        // Hide every card
        for (int i = 0; i < 8; i++) {
            answerBoxes[i].text = "";
        }

        requiiButton.SetActive(true);
        backButton.SetActive(true);
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
        // Store GameObjects in variables
        cards = GameObject.FindGameObjectsWithTag("Blinder");
        answerBoxes[0] = answerBox1;
        answerBoxes[1] = answerBox2;
        answerBoxes[2] = answerBox3;
        answerBoxes[3] = answerBox4;
        answerBoxes[4] = answerBox5;
        answerBoxes[5] = answerBox6;
        answerBoxes[6] = answerBox7;
        answerBoxes[7] = answerBox8;

        // Store menu items
        menuBackground = GameObject.Find("MenuBackground");
        menuButton1 = GameObject.Find("MenuButton1");
        menuButton2 = GameObject.Find("MenuButton2");
        menuButton3 = GameObject.Find("MenuButton3");
        backButton = GameObject.Find("BackButton");
        requiiButton = GameObject.Find("RequiiButton");

        // Store code snippets for questions
        // Java
        javaQuestions.Add("System.out.println()");
        javaQuestions.Add("Dictionary<> dict");
        // JavaScript
        javaScriptQuestions.Add("console.log()");
        javaScriptQuestions.Add("let dict");
        // C#
        cSharpQuestions.Add("Console.WriteLine()");
        cSharpQuestions.Add("IDictionary<> dict");
        cSharpQuestions.Add("List<string>");
        cSharpQuestions.Add("Random.Range(0, 5)");
        // Python
        pythonQuestions.Add("print()");
        pythonQuestions.Add("dict()");

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

                gameOver = false;
                gameStarted = false;
                
                enableMenu();

            } else if (hit.collider != null) {
                GameObject answer = hit.collider.gameObject;

                if (answer.name == "RequiiButton" && !rScript.requiiRunning) {

                    StartCoroutine(rScript.runRequiiDialogue(fScript.memory3Tutorial));

                } else if (answer.name == "BackButton") {

                    if (gameStarted) {
                        gameOver = false;
                        gameStarted = false;
                        
                        enableMenu();
                    } else {
                        SceneManager.LoadScene("Menu");
                    }

                } else if (!gameStarted && (answer.name.Substring(0, 4) == "Menu")) {
                    // Check for menu button clicks if game not started

                    string answerName = answer.name;
                
                    if (answerName == "MenuButton1") {
                        difficulty = 3;
                    } else if (answerName == "MenuButton2") {
                        difficulty = 2;
                    } else if (answerName == "MenuButton3") {
                        difficulty = 1;
                    }

                    gameStarted = true;
                    disableMenu();

                    StartCoroutine(CountdownToGameStart());

                } else if ((answer.name != "RequiiButton") && gameStarted) {
                    // Check cards if game is started

                    string answerNumberString = answer.name.Substring(answer.name.Length - 1);
                    int answerNumber = int.Parse(answerNumberString);
                    
                    checkCards(answerNumber);
                } else {

                }
            }
        }

        if (Input.touchCount == 0)
        {
            canTouch = true;
        }
        // End of touch control block
        
    }
}
