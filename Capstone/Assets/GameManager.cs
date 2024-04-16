using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class NewBehaviourScript : MonoBehaviour
{
    // Mouse control variables
    Vector3 mouseLocation;
    RaycastHit2D hit;
    public Camera cam;
    public bool canClick = true;
    public bool isClicking = false;

    // Object storing variables
    GameObject[] pixelArray;
    Color backgroundColor;
    Color storedColor;
    bool isErasing = false;

    // Start is called before the first frame update
    void Start()
    {
        pixelArray = GameObject.FindGameObjectsWithTag("pixel");
        backgroundColor = GameObject.Find("Square").GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (canClick && Input.GetMouseButtonDown(0))
        {
            canClick = false;
            isClicking = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            canClick = true;
            isClicking = false;
        }

        if (isClicking)
        {
            mouseLocation = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            hit = Physics2D.Raycast(new Vector2(mouseLocation.x, mouseLocation.y), new Vector2(0, 0));

            if (hit.collider != null)
            {
                // Hit gameObject
                GameObject hitObject = hit.collider.gameObject;

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
                } else
                {
                    if (!isErasing)
                    {
                        hitObject.GetComponent<SpriteRenderer>().color = storedColor;
                    } else
                    {
                        hitObject.GetComponent<SpriteRenderer>().color = backgroundColor;
                    }
                    
                }

            }
        }
        
    }
}
