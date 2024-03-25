using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Vector3 mouseLocation;
    public RaycastHit2D hit;
    public Camera cam;
    public bool canClick = true;
    public bool gameOver = false;
    public bool timerRunning = false;

    public Text questionBox;
    public Text answerBox1;
    public Text answerBox2;
    public Text answerBox3;
    public Text answerBox4;
    GameObject[] blinders;
    GameObject questionBlinder;

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

    private IEnumerator Countdown5() {
        timerRunning = true;
        yield return new WaitForSeconds(5.0f); //wait 5 seconds

        foreach (GameObject blinder in blinders)
        {
            blinder.SetActive(true);
        }
        questionBlinder.SetActive(false);

        timerRunning = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        blinders = GameObject.FindGameObjectsWithTag("Blinder");
        questionBlinder = GameObject.FindGameObjectWithTag("QuestionBlinder");
        foreach (GameObject blinder in blinders)
        {
            blinder.SetActive(false);
        }
        StartCoroutine(Countdown5());

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

        finalAnswers = generateQuestion();

    }

    // Update is called once per frame
    void Update()
    {
        if (canClick && Input.GetMouseButtonDown(0) && gameOver && !timerRunning) {
            canClick = false;
            gameOver = false;

            for (int i = 1; i < 5; i++) {
                GameObject.Find("BlinderBackground" + i.ToString()).GetComponent<SpriteRenderer>().color = new Color(.275f, .275f, .275f, 1f);
            }

            foreach (GameObject blinder in blinders)
            {
                blinder.SetActive(false);
            }

            questionBlinder.SetActive(true);
            finalAnswers = generateQuestion();
            StartCoroutine(Countdown5());

        } else if (canClick && Input.GetMouseButtonDown(0) && !timerRunning) {
            canClick = false;

            mouseLocation = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            hit = Physics2D.Raycast(new Vector2(mouseLocation.x, mouseLocation.y), new Vector2(0, 0));

            if (hit.collider != null) {
                GameObject answer = hit.collider.gameObject;
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
        }

        if (Input.GetMouseButtonUp(0))
        {
            canClick = true;
        }
        
    }
}
