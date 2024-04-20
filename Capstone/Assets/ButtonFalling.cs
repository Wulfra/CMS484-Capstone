using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonFalling : MonoBehaviour
{
    public Button[] buttons;
    public float minSpeed = 1f;
    public float maxSpeed = 5f;

    public int weight;
    public int value;
    private int buttonsFallingCount = 2;
    private Button[] fallingButtons;

    public Text timeText;
    public Text weightText;
    public Text valueText;
    public GameObject storageVar;
    public GameObject failText;

    void Update()
    {
        weightText.text = "Weight: " + weight.ToString();
        valueText.text = "Value: " + value.ToString();

        if (weight > 50)
        {
            storageVar.SetActive(false);
            failText.SetActive(true);
            SceneManager.LoadScene("Menu");
        }
    }

    void Start()
    {
        StartCoroutine(Countdown(60));
        fallingButtons = new Button[buttonsFallingCount];
        // Add listeners to buttons
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => ButtonClicked(button));
        }

        // Start falling buttons
        StartCoroutine(StartFalling());
    }

    void ButtonClicked(Button button)
    {
        button.gameObject.SetActive(false);
        if (button.name == "1-1")
        {
            weight += 1;
            value += 1 * Random.Range(1, 2); ;
        }
        if (button.name == "1-2")
        {
            weight += 1;
            value += 2 * Random.Range(1, 2); ;
        }
        if (button.name == "1-3")
        {
            weight += 1;
            value += 3 * Random.Range(1, 2); ;
        }
        if (button.name == "2-1")
        {
            weight += 2;
            value += 1 * Random.Range(1, 3); ;
        }
        if (button.name == "2-2")
        {
            weight += 2;
            value += 2 * Random.Range(1, 3); ;
        }
        if (button.name == "2-3")
        {
            weight += 2;
            value += 3 * Random.Range(1, 3); ;
        }
        if (button.name == "3-1")
        {
            weight += 3;
            value += 1 * Random.Range(1, 4); ;
        }
        if (button.name == "3-2")
        {
            weight += 3;
            value += 2 * Random.Range(1, 4); ;
        }
        if (button.name == "3-3")
        {
            weight += 3;
            value += 3 * Random.Range(1, 4); ;
        }
        if (button.name == "4-1")
        {
            weight += 4;
            value += 1 * Random.Range(1, 5); ;
        }
        if (button.name == "4-2")
        {
            weight += 4;
            value += 2 * Random.Range(1, 5); ;
        }
        if (button.name == "4-3")
        {
            weight += 4;
            value += 3 * Random.Range(1, 5); ;
        }
        if (button.name == "5-1")
        {
            weight += 5;
            value += 3 * Random.Range(1, 6); ;
        }
        if (button.name == "5-2")
        {
            weight += 5;
            value += 2 * Random.Range(1, 6); ;
        }
        if (button.name == "5-3")
        {
            weight += 5;
            value += 3 * Random.Range(1, 6);
        }
    }

    private IEnumerator Countdown(float timeRemaining)
    {
        while (timeRemaining > 0)
        {
            timeText.text = "Time: " + timeRemaining.ToString();
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
        }

        // Timer is done
        Debug.Log("Time has run out!");
        timeRemaining = 0;

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1f);
        }
        SceneManager.LoadScene("Menu");
    }

    IEnumerator StartFalling()
    {
        while (true)
        {
            // Select buttons randomly for falling
            for (int i = 0; i < buttonsFallingCount; i++)
            {
                fallingButtons[i] = buttons[Random.Range(0, buttons.Length)];
                StartFalling(fallingButtons[i]);
                yield return new WaitForSeconds(Random.Range(0.5f, 1.5f)); // Delay between each falling pair
            }
            yield return new WaitForSeconds(3f); // Delay between each group of falling buttons
        }
    }

    void StartFalling(Button button)
    {
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        RectTransform canvasRectTransform = button.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        Camera mainCamera = Camera.main;

        // Calculate random viewport coordinates within the visible portion of the camera
        float viewportX = Random.Range(0.1f, 0.9f); // Adjust range as necessary
        float viewportY = 1.1f; // Slightly above the top of the screen

        Vector3 viewportPoint = new Vector3(viewportX, viewportY, 0f);
        Vector3 spawnPoint = mainCamera.ViewportToWorldPoint(viewportPoint);

        // Ensure the spawn point stays within the bounds of the canvas
        float halfCanvasWidth = canvasRectTransform.rect.width * 0.5f;
        spawnPoint.x = Mathf.Clamp(spawnPoint.x, mainCamera.ViewportToWorldPoint(Vector3.zero).x + halfCanvasWidth, mainCamera.ViewportToWorldPoint(Vector3.one).x - halfCanvasWidth);
        spawnPoint.x = -0.001f;
        spawnPoint.z = 10;
        spawnPoint.y -= 3;
        float speed = Random.Range(minSpeed, maxSpeed);

        buttonRectTransform.position = spawnPoint;
        button.gameObject.SetActive(true);

        // Start falling coroutine
        StartCoroutine(Fall(buttonRectTransform, speed, button));
    }


    IEnumerator Fall(RectTransform buttonTransform, float speed, Button button)
    {
        Camera mainCamera = Camera.main;

        while (buttonTransform.position.y > mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).y)
        {
            
            buttonTransform.Translate(Vector3.down * speed * Time.deltaTime);
            yield return null;
        }

        // Deactivate button when it falls out of camera view
        button.gameObject.SetActive(false);
    }
}
