using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    // Touch control variables
    Vector3 touchLocation;
    bool canTouch = true;
    public RaycastHit2D hit;
    public Camera cam;

    // Scripts
    public RequiiScript rScript;
    public DialogueScript dScript;

    // Update is called once per frame
    void Update()
    {
        if (canTouch && (Input.touchCount > 0)) {
            canTouch = false;

            Touch touch = Input.GetTouch(0);

            touchLocation = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
            hit = Physics2D.Raycast(new Vector2(touchLocation.x, touchLocation.y), new Vector2(0, 0));

            if (hit.collider != null) {

                GameObject answer = hit.collider.gameObject;

                if (answer.name == "BackButton") {
                    // Load main menu
                    SceneManager.LoadScene("Menu");
                } else if (answer.name == "RequiiButton") {
                    // Load requii info
                } else if (answer.name == "TapHere") {
                    SceneManager.LoadScene("Menu");
                }

            }
        }

        if (Input.touchCount == 0) {
            canTouch = true;
        }
    }
}
