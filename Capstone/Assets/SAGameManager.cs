using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerSort : MonoBehaviour
{
    // Dialogue script
    public DialogueScript dScript;

    // Array storage variables
    public int[] sortingNumbers = new int[22];
    public GameObject[] sortingBars = new GameObject[22];
    public AudioSource[] beepSounds = new AudioSource[8];
    public AudioSource finishSound;

    // Game control variables
    Vector3 mouseLocation;
    RaycastHit2D hit;
    public Camera cam;
    public bool timerRunning = false;
    public bool canClick = false;
    public int chosenAlgorithm = 0;
    public bool testBool = false;

    // Touch control variables
    Vector3 touchLocation;
    public bool canTouch = true;

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

    private void displayBars() {
        // For each bar, display corresponding number from num array
        for (int i = 0; i < sortingBars.Length; i++) {
            sortingBars[i].transform.localScale = new Vector3(.25f, (float)sortingNumbers[i] * 1.8f, 1f);
        }
    }

    private void playRandomBeep() {
        // Select random sound
        int randomSound = Random.Range(0, beepSounds.Length);

        // Play random sound
        beepSounds[randomSound].Play();
    }

    private IEnumerator runBubbleSort() 
    {
        timerRunning = true;

        // Initiate the bubble sort
        for (int i = 0; i < (sortingNumbers.Length - 1); i++) {
            for (int j = 0; j < (sortingNumbers.Length - (1 + i)); j++) {
                var lowerNumber = 0;
                if (sortingNumbers[j] > sortingNumbers[j + 1]) {
                    lowerNumber = sortingNumbers[j + 1];
                    sortingNumbers[j + 1] = sortingNumbers[j];
                    sortingNumbers[j] = lowerNumber;

                    // Wait for .05 seconds
                    yield return new WaitForSeconds(.05f);

                    // Display current num array state on bars
                    displayBars();
                    playRandomBeep();
                }
            }
        }

        // Completion effects
        finishSound.Play();
        yield return new WaitForSeconds(1.0f);

        timerRunning = false;
    }

    private IEnumerator runSelectionSort() 
    { 
        timerRunning = true;

        int n = sortingNumbers.Length; 
  
        // One by one move boundary of unsorted subarray 
        for (int i = 0; i < n - 1; i++) 
        { 
            // Find the minimum element in unsorted array 
            int min_idx = i; 
            for (int j = i + 1; j < n; j++) 
                if (sortingNumbers[j] < sortingNumbers[min_idx]) 
                    min_idx = j; 
  
            // Swap the found minimum element with the first element 
            int temp = sortingNumbers[min_idx];
            sortingNumbers[min_idx] = sortingNumbers[i];
            sortingNumbers[i] = temp;

            // Wait for .1 seconds
            yield return new WaitForSeconds(.075f);

            // Display current num array state on bars
            displayBars();
            playRandomBeep();
        }

        // Completion effects
        finishSound.Play();
        yield return new WaitForSeconds(1.0f);

        timerRunning = false;
    }

    private IEnumerator runInsertionSort()
    {
        timerRunning = true;

        int n = sortingNumbers.Length;
        for (int i = 1; i < n; ++i) {
            int key = sortingNumbers[i];
            int j = i - 1;
 
            // Move elements of arr[0..i-1],
            // that are greater than key,
            // to one position ahead of
            // their current position
            while (j >= 0 && sortingNumbers[j] > key) {
                sortingNumbers[j + 1] = sortingNumbers[j];
                j = j - 1;
            }
            sortingNumbers[j + 1] = key;

            // Wait for .1 seconds
            yield return new WaitForSeconds(.075f);

            // Display current num array state on bars
            displayBars();
            playRandomBeep();
        }

        // Completion effects
        finishSound.Play();
        yield return new WaitForSeconds(1.0f);

        timerRunning = false;
    }

    private void runRandomSort() {
        // Pick one of three sorting algorithms
        int randomSortNum = Random.Range(1,4);

        // Initiate chosen sort
        if (randomSortNum == 1) {
            StartCoroutine(runBubbleSort());
        } else if (randomSortNum == 2) {
            StartCoroutine(runSelectionSort());
        } else {
            StartCoroutine(runInsertionSort());
        }
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
        textBox1.text = "Bubble Sort";
        textBox2.text = "Selection Sort";
        textBox3.text = "Insertion Sort";
    }

    // Start is called before the first frame update
    void Start()
    {
        // Store menu items
        menuBackground = GameObject.Find("MenuBackground");
        menuButton1 = GameObject.Find("MenuButton1");
        menuButton2 = GameObject.Find("MenuButton2");
        menuButton3 = GameObject.Find("MenuButton3");
        backButton = GameObject.Find("BackButton");
        requiiButton = GameObject.Find("RequiiButton");

        // Store sorting bars
        for (int i = 1; i < sortingBars.Length + 1; i++) {
            sortingBars[i - 1] = GameObject.Find("Bar" + i.ToString());
        }

        // Create a random array of ints
        for (int i = 0; i < sortingNumbers.Length; i++) {
            sortingNumbers[i] = Random.Range(1, 11);
        }

        // Store beep sounds
        for (int i = 1; i < beepSounds.Length + 1; i++) {
            beepSounds[i - 1] = GameObject.Find("Beep" + i.ToString()).GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (chosenAlgorithm == 0) {
            // Enable the menu
            enableMenu();
        } else if ((chosenAlgorithm == 1) && !timerRunning) {
            disableMenu();

            // Create a random array of ints
            for (int i = 0; i < sortingNumbers.Length; i++) {
                sortingNumbers[i] = Random.Range(1, 11);
            }

            // Display starting numbers
            displayBars();

            StartCoroutine("runBubbleSort");
        } else if ((chosenAlgorithm == 2) && !timerRunning) {
            disableMenu();

            // Create a random array of ints
            for (int i = 0; i < sortingNumbers.Length; i++) {
                sortingNumbers[i] = Random.Range(1, 11);
            }

            // Display starting numbers
            displayBars();

            StartCoroutine("runSelectionSort");
        } else if ((chosenAlgorithm == 3) && !timerRunning) {
            disableMenu();

            // Create a random array of ints
            for (int i = 0; i < sortingNumbers.Length; i++) {
                sortingNumbers[i] = Random.Range(1, 11);
            }

            // Display starting numbers
            displayBars();

            StartCoroutine("runInsertionSort");
        }

        if (canTouch && (Input.touchCount > 0)) {
            canTouch = false;

            Touch touch = Input.GetTouch(0);

            touchLocation = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
            hit = Physics2D.Raycast(new Vector2(touchLocation.x, touchLocation.y), new Vector2(0, 0));

            if (hit.collider != null) {
                GameObject answer = hit.collider.gameObject;
                string answerName = answer.name;
                
                if (answerName == "BackButton") {

                    if (timerRunning) {
                        // Reset algorithm
                        chosenAlgorithm = 0;

                        // Stop any running algorithms
                        StopCoroutine("runBubbleSort");
                        StopCoroutine("runSelectionSort");
                        StopCoroutine("runInsertionSort");
                        timerRunning = false;
                        
                        // Reenable menu
                        enableMenu();
                    } else {
                        SceneManager.LoadScene("Menu");
                    }

                } else if (answerName == "MenuButton1") {
                    chosenAlgorithm = 1;
                } else if (answerName == "MenuButton2") {
                    chosenAlgorithm = 2;
                } else if (answerName == "MenuButton3") {
                    chosenAlgorithm = 3;
                }

            } else {

            }
        }

        if (Input.touchCount == 0)
        {
            canTouch = true;
        }

    }
}
