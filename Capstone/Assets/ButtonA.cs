using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonA : MonoBehaviour
{
    //public Button ButtonA;
    public TextMeshProUGUI optionA;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnButtonClick()
    {
        this.optionA.text = "Correct";
        GetComponent<Image>().color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
