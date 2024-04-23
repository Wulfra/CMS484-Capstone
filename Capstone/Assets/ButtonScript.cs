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
    public FileScript fScript;

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
                } else if (answer.name == "RequiiButton" && !rScript.requiiRunning) {
                    StartCoroutine(rScript.runRandomRequiiDialogue(fScript.Eggs));
                } else if (answer.name == "RequiiButtonRelax1" && !rScript.requiiRunning) {
                    StartCoroutine(rScript.runRequiiDialogue(fScript.relax1Tutorial));
                } else if (answer.name == "RequiiButtonRelax2" && !rScript.requiiRunning) {
                    StartCoroutine(rScript.runRequiiDialogue(fScript.relax2Tutorial));
                } else if (answer.name == "RequiiButtonRelax3" && !rScript.requiiRunning) {
                    StartCoroutine(rScript.runRequiiDialogue(fScript.relax3Tutorial));
                } else if (answer.name == "RequiiButtonFocus1" && !rScript.requiiRunning) {
                    StartCoroutine(rScript.runRequiiDialogue(fScript.focus1Tutorial));
                } else if (answer.name == "RequiiButtonFocus2" && !rScript.requiiRunning) {
                    StartCoroutine(rScript.runRequiiDialogue(fScript.focus2Tutorial));
                } else if (answer.name == "RequiiButtonFocus3" && !rScript.requiiRunning) {
                    StartCoroutine(rScript.runRequiiDialogue(fScript.focus3Tutorial));
                } else if (answer.name == "RequiiButtonLogic1" && !rScript.requiiRunning) {
                    StartCoroutine(rScript.runRequiiDialogue(fScript.logic1Tutorial));
                } else if (answer.name == "RequiiButtonLogic2" && !rScript.requiiRunning) {
                    StartCoroutine(rScript.runRequiiDialogue(fScript.logic2Tutorial));
                } else if (answer.name == "RequiiButtonLogic3" && !rScript.requiiRunning) {
                    StartCoroutine(rScript.runRequiiDialogue(fScript.logic3Tutorial));
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
