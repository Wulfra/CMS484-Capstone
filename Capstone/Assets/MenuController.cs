using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button activities;
    public Button stats;
    public Button about;
    public Button backOne;
    public Button backZero;
    public Text aboutText;
    public GameObject aboutTitle;
    public GameObject mainTitle;
    public Button backTwo;
    public GameObject activityTitle;
    public Button a1;
    public Button a2;
    public Button a3;
    public Button a4;
    public Button[] buttons;
    public GameObject relaxKey;
    public GameObject memoryKey;
    public GameObject logicKey;
    public GameObject focusKey;
    public GameObject statKey;
    // Start is called before the first frame update

    void Start()
    {
        activities.gameObject.SetActive(true);
        stats.gameObject.SetActive(true);
        about.gameObject.SetActive(true);
        mainTitle.gameObject.SetActive(true);
        backZero.gameObject.SetActive(true);
        about.onClick.AddListener(ShowAbout);
        stats.onClick.AddListener(ShowStats);
        activities.onClick.AddListener(ShowActivities);
        backZero.onClick.AddListener(TitleBack);
        backOne.onClick.AddListener(AboutBack);
        backTwo.onClick.AddListener(MenuBackOne);
        a1.onClick.AddListener(LoadActivityOne);
        a2.onClick.AddListener(LoadActivityTwo);
        a3.onClick.AddListener(LoadActivityThree);
        a4.onClick.AddListener(LoadActivityFour);
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => ButtonClicked(button));
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ButtonClicked(Button button)
    {
        Debug.Log("Button " + button.name + " clicked!");
        if (button.name == "RelaxOne")
        {
            relaxKey.SetActive(false);
            SceneManager.LoadScene("Relaxation1");
        }
        if (button.name == "RelaxTwo")
        {
            relaxKey.SetActive(false);
            SceneManager.LoadScene("Relaxation2");
        }
        if (button.name == "RelaxThree")
        {
            relaxKey.SetActive(false);
            SceneManager.LoadScene("Relaxation3");
        }
        if (button.name == "RelaxFour")
        {
            relaxKey.SetActive(false);
        }
        if (button.name == "MemoryOne")
        {
            memoryKey.SetActive(false);
            SceneManager.LoadScene("Memory1");
        }
        if (button.name == "MemoryTwo")
        {
            memoryKey.SetActive(false);
            SceneManager.LoadScene("Memory2");
        }
        if (button.name == "MemoryThree")
        {
            memoryKey.SetActive(false);
            SceneManager.LoadScene("Memory3");
        }
        if (button.name == "MemoryFour")
        {
            memoryKey.SetActive(false);

        }
        if (button.name == "LogicOne")
        {
            logicKey.SetActive(false);
            SceneManager.LoadScene("Logic1");
        }
        if (button.name == "LogicTwo")
        {
            logicKey.SetActive(false);
            SceneManager.LoadScene("Logic2");
        }
        if (button.name == "LogicThree")
        {
            logicKey.SetActive(false);
            SceneManager.LoadScene("Logic3");
        }
        if (button.name == "LogicFour")
        {
            logicKey.SetActive(false);
        }
        if (button.name == "FocusOne")
        {
            focusKey.SetActive(false);
            SceneManager.LoadScene("Focus1");
        }
        if (button.name == "FocusTwo")
        {
            focusKey.SetActive(false);
            SceneManager.LoadScene("Focus2");
        }
        if (button.name == "FocusThree")
        {
            focusKey.SetActive(false);
            SceneManager.LoadScene("Focus3");
        }
        if (button.name == "FocusFour")
        {
            focusKey.SetActive(false);
        }
        if (button.name == "BackThree")
        {
            relaxKey.SetActive(false);
            backTwo.gameObject.SetActive(true);
            a1.gameObject.SetActive(true);
            a2.gameObject.SetActive(true);
            a3.gameObject.SetActive(true);
            a4.gameObject.SetActive(true);
            activityTitle.gameObject.SetActive(true);
        }
        if (button.name == "BackFour")
        {
            memoryKey.SetActive(false);
            backTwo.gameObject.SetActive(true);
            a1.gameObject.SetActive(true);
            a2.gameObject.SetActive(true);
            a3.gameObject.SetActive(true);
            a4.gameObject.SetActive(true);
            activityTitle.gameObject.SetActive(true);
        }
        if (button.name == "BackFive")
        {
            logicKey.SetActive(false);
            backTwo.gameObject.SetActive(true);
            a1.gameObject.SetActive(true);
            a2.gameObject.SetActive(true);
            a3.gameObject.SetActive(true);
            a4.gameObject.SetActive(true);
            activityTitle.gameObject.SetActive(true);
        }
        if (button.name == "BackSix")
        {
            focusKey.SetActive(false);
            backTwo.gameObject.SetActive(true);
            a1.gameObject.SetActive(true);
            a2.gameObject.SetActive(true);
            a3.gameObject.SetActive(true);
            a4.gameObject.SetActive(true);
            activityTitle.gameObject.SetActive(true);
        }
        if (button.name == "BackSeven")
        {
            activities.gameObject.SetActive(true);
            stats.gameObject.SetActive(true);
            about.gameObject.SetActive(true);
            mainTitle.gameObject.SetActive(true);
            backZero.gameObject.SetActive(true);
            statKey.SetActive(false);
        }

        

    }

    void ShowAbout()
    {
        activities.gameObject.SetActive(false);
        stats.gameObject.SetActive(false);
        about.gameObject.SetActive(false);
        mainTitle.gameObject.SetActive(false);
        backZero.gameObject.SetActive(false);
        aboutText.gameObject.SetActive(true);
        aboutTitle.gameObject.SetActive(true);
        backOne.gameObject.SetActive(true);
    }

    void ShowStats()
    {
        activities.gameObject.SetActive(false);
        stats.gameObject.SetActive(false);
        about.gameObject.SetActive(false);
        mainTitle.gameObject.SetActive(false);
        backZero.gameObject.SetActive(false);
        statKey.SetActive(true);
    }

    void ShowActivities()
    {
        activities.gameObject.SetActive(false);
        stats.gameObject.SetActive(false);
        about.gameObject.SetActive(false);
        mainTitle.gameObject.SetActive(false);
        backZero.gameObject.SetActive(false);
        backTwo.gameObject.SetActive(true);
        a1.gameObject.SetActive(true);
        a2.gameObject.SetActive(true);
        a3.gameObject.SetActive(true);
        a4.gameObject.SetActive(true);
        activityTitle.gameObject.SetActive(true);
    }

    void TitleBack() {
        SceneManager.LoadScene("StartScreen");
    }

    void AboutBack()
    {
        activities.gameObject.SetActive(true);
        stats.gameObject.SetActive(true);
        about.gameObject.SetActive(true);
        mainTitle.gameObject.SetActive(true);
        backZero.gameObject.SetActive(true);
        aboutText.gameObject.SetActive(false);
        aboutTitle.gameObject.SetActive(false);
        backOne.gameObject.SetActive(false);
    }

    void MenuBackOne()
    {
        activities.gameObject.SetActive(true);
        stats.gameObject.SetActive(true);
        about.gameObject.SetActive(true);
        mainTitle.gameObject.SetActive(true);
        backZero.gameObject.SetActive(true);
        backTwo.gameObject.SetActive(false);
        a1.gameObject.SetActive(false);
        a2.gameObject.SetActive(false);
        a3.gameObject.SetActive(false);
        a4.gameObject.SetActive(false);
        activityTitle.gameObject.SetActive(false);
    }

    void LoadActivityOne()
    {
        backTwo.gameObject.SetActive(false);
        a1.gameObject.SetActive(false);
        a2.gameObject.SetActive(false);
        a3.gameObject.SetActive(false);
        a4.gameObject.SetActive(false);
        activityTitle.gameObject.SetActive(false);
        relaxKey.SetActive(true);
        
    }

    void LoadActivityTwo()
    {
        backTwo.gameObject.SetActive(false);
        a1.gameObject.SetActive(false);
        a2.gameObject.SetActive(false);
        a3.gameObject.SetActive(false);
        a4.gameObject.SetActive(false);
        activityTitle.gameObject.SetActive(false);
        focusKey.SetActive(true);
    }

    void LoadActivityThree()
    {
        backTwo.gameObject.SetActive(false);
        a1.gameObject.SetActive(false);
        a2.gameObject.SetActive(false);
        a3.gameObject.SetActive(false);
        a4.gameObject.SetActive(false);
        activityTitle.gameObject.SetActive(false);
        memoryKey.SetActive(true);

        
    }

    void LoadActivityFour()
    {
        backTwo.gameObject.SetActive(false);
        a1.gameObject.SetActive(false);
        a2.gameObject.SetActive(false);
        a3.gameObject.SetActive(false);
        a4.gameObject.SetActive(false);
        activityTitle.gameObject.SetActive(false);
        logicKey.SetActive(true);
    }
}
