using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonFalling : MonoBehaviour
{
    public Button[] buttons;
    public float minSpeed = 1f;
    public float maxSpeed = 5f;

    private int buttonsFallingCount = 2;
    private Button[] fallingButtons;

    void Start()
    {
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
        Debug.Log("Button " + button.name + " clicked!");
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
