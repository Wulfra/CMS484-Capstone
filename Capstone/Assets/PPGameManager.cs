using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    // Touch control variables
    Vector3 touchLocation;
    RaycastHit2D hit;
    public Camera cam;
    public bool canTouch = true;
    public bool isTouching = false;

    // Object storing variables
    private GameObject backButton;
    private GameObject requiiButton;
    public GameObject[] pixelArray;
    Color backgroundColor;
    Color storedColor;
    bool isErasing = false;

    // Start is called before the first frame update
    void Start()
    {
        pixelArray = GameObject.FindGameObjectsWithTag("pixel");
        backgroundColor = GameObject.Find("Square").GetComponent<SpriteRenderer>().color;
        storedColor = Color.white;

        // Store menu buttons
        backButton = GameObject.Find("BackButton");
        requiiButton = GameObject.Find("RequiiButton");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            isTouching = true;

            Touch touch = Input.GetTouch(0);

            touchLocation = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
            hit = Physics2D.Raycast(new Vector2(touchLocation.x, touchLocation.y), new Vector2(0, 0));

            if (hit.collider != null)
            {
                // Hit gameObject
                GameObject hitObject = hit.collider.gameObject;

                if (canTouch) {
                    canTouch = false;

                    if (hitObject.name == "Trash Can")
                    {
                        foreach (GameObject pixel in pixelArray)
                        {
                            pixel.GetComponent<SpriteRenderer>().color = backgroundColor;
                        }
                    }
                    else if (hitObject.name == "Paint Brush")
                    {
                        isErasing = false;
                    } else if (hitObject.name == "Eraser")
                    {
                        isErasing = true;
                    } else if (hitObject.name == "Color Swatch")
                    {
                        storedColor = hitObject.GetComponent<SpriteRenderer>().color;
                    } else if (hitObject.name == "BackButton")
                    {
                        SceneManager.LoadScene("Menu");
                    } else if (hitObject.name == "RequiiButton")
                    {
                        
                    } else
                    {
                        
                    }
                } else if (isTouching && (hitObject.name.Substring(0, 6) == "Canvas")) {
                    if (!isErasing)
                    {
                        hitObject.GetComponent<SpriteRenderer>().color = storedColor;
                    } else
                    {
                        hitObject.GetComponent<SpriteRenderer>().color = backgroundColor;
                    }  
                } else {

                }

            }
        }

        if (Input.touchCount == 0)
        {
            canTouch = true;
            isTouching = false;
        }
        
    }
}
