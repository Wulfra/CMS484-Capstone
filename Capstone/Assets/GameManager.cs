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
    public List<int> correctAnswers = new List<int>{};
    public List<string> answers = new List<string>{};
    public int correctAnswerNumber;

    private int generateQuestion()
    {
        int questionNumber = Random.Range(0, questions.Count);
        int answerStart = 4 * questionNumber;

        questionBox.text = questions[questionNumber];
        answerBox1.text = answers[answerStart];
        answerBox2.text = answers[answerStart + 1];
        answerBox3.text = answers[answerStart + 2];
        answerBox4.text = answers[answerStart + 3];

        return correctAnswers[questionNumber];
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
        
        // Question 1
        questions.Add("Create a list of type int!");
        correctAnswers.Add(3);
        answers.Add("int[] list;");
        answers.Add("list = int[1, 2, 3]");
        answers.Add("List<int> list;");
        answers.Add("list = int<1, 2, 3>");

        // Question 2
        questions.Add("Write a XOR operation!");
        correctAnswers.Add(2);
        answers.Add("bool1 | bool2");
        answers.Add("bool1 ^ bool2");
        answers.Add("bool1 !| bool2");
        answers.Add("bool1 & !bool2");

        correctAnswerNumber = generateQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        if (canClick && Input.GetMouseButtonDown(0) && gameOver && !timerRunning) {
            canClick = false;
            gameOver = false;

            foreach (GameObject blinder in blinders)
            {
                blinder.SetActive(false);
            }
            questionBlinder.SetActive(true);
            correctAnswerNumber = generateQuestion();
            StartCoroutine(Countdown5());
        } else if (canClick && Input.GetMouseButtonDown(0) && !timerRunning) {
            canClick = false;

            mouseLocation = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            hit = Physics2D.Raycast(new Vector2(mouseLocation.x, mouseLocation.y), new Vector2(0, 0));

            if (hit.collider != null) {
                GameObject answer = hit.collider.gameObject;
                string answerNumberString = answer.name.Substring(answer.name.Length - 1);
                int answerNumber = int.Parse(answerNumberString);

                questionBlinder.SetActive(false);

                if (answerNumber == correctAnswerNumber) {
                    questionBox.text = "Correct!";
                } else {
                    questionBox.text = "Incorrect!";
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
