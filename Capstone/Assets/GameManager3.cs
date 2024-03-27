using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager3 : MonoBehaviour
{
    // Mouse control variables
    Vector3 mouseLocation;
    RaycastHit2D hit;
    public Camera cam;
    public bool canClick = true;
    public bool gameOver = false;
    public bool timerRunning = false;

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
            if (previousCardAnswer == currentCardAnswer) {
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

        // Reset all variables
        ResetGameVariables();

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

        timerRunning = false;
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

        // Start the game
        StartCoroutine(CountdownToGameStart());

    }

    // Update is called once per frame
    void Update()
    {
        if (canClick && Input.GetMouseButtonDown(0) && gameOver && !timerRunning) {
            canClick = false;
            gameOver = false;
            
            StartCoroutine(CountdownToGameStart());

        } else if (canClick && Input.GetMouseButtonDown(0) && !timerRunning) {
            canClick = false;

            mouseLocation = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            hit = Physics2D.Raycast(new Vector2(mouseLocation.x, mouseLocation.y), new Vector2(0, 0));

            if (hit.collider != null) {

                GameObject answer = hit.collider.gameObject;
                string answerNumberString = answer.name.Substring(answer.name.Length - 1);
                int answerNumber = int.Parse(answerNumberString);
                
                checkCards(answerNumber);

            } else {

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            canClick = true;
        }
        
    }
}
