using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float elapsedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (this.name == "RelaxOneClock")
        {
            elapsedTime = PlayerPrefs.GetFloat("RelaxOneClock", 0);
        }
        if (this.name == "RelaxTwoClock")
        {
            elapsedTime = PlayerPrefs.GetFloat("RelaxTwoClock", 0);
        }
        if (this.name == "RelaxThreeClock")
        {
            elapsedTime = PlayerPrefs.GetFloat("RelaxThreeClock", 0);
        }
        if (this.name == "MemoryOneClock")
        {
            elapsedTime = PlayerPrefs.GetFloat("MemoryOneClock", 0);
        }
        if (this.name == "MemoryTwoClock")
        {
            elapsedTime = PlayerPrefs.GetFloat("MemoryTwoClock", 0);
        }
        if (this.name == "MemoryThreeClock")
        {
            elapsedTime = PlayerPrefs.GetFloat("MemoryThreeClock", 0);
        }
        if (this.name == "LogicOneClock")
        {
            elapsedTime = PlayerPrefs.GetFloat("LogicOneClock", 0);
        }
        if (this.name == "LogicTwoClock")
        {
            elapsedTime = PlayerPrefs.GetFloat("LogicTwoClock", 0);
        }
        if (this.name == "LogicThreeClock")
        {
            elapsedTime = PlayerPrefs.GetFloat("LogicThreeClock", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (this.name == "RelaxOneClock")
        {
            PlayerPrefs.SetFloat("RelaxOneClock", elapsedTime);
            PlayerPrefs.Save();
        }
        if (this.name == "RelaxTwoClock")
        {
            PlayerPrefs.SetFloat("RelaxTwoClock", elapsedTime);
            PlayerPrefs.Save();
        }
        if (this.name == "RelaxThreeClock")
        {
            PlayerPrefs.SetFloat("RelaxThreeClock", elapsedTime);
            PlayerPrefs.Save();
        }
        if (this.name == "MemoryOneClock")
        {
            PlayerPrefs.SetFloat("MemoryOneClock", elapsedTime);
            PlayerPrefs.Save();
        }
        if (this.name == "MemoryTwoClock")
        {
            PlayerPrefs.SetFloat("MemoryTwoClock", elapsedTime);
            PlayerPrefs.Save();
        }
        if (this.name == "MemoryThreeClock")
        {
            PlayerPrefs.SetFloat("MemoryThreeClock", elapsedTime);
            PlayerPrefs.Save();
        }
        if (this.name == "LogicOneClock")
        {
            PlayerPrefs.SetFloat("LogicOneClock", elapsedTime);
            PlayerPrefs.Save();
        }
        if (this.name == "LogicTwoClock")
        {
            PlayerPrefs.SetFloat("LogicTwoClock", elapsedTime);
            PlayerPrefs.Save();
        }
        if (this.name == "LogicThreeClock")
        {
            PlayerPrefs.SetFloat("LogicThreeClock", elapsedTime);
            PlayerPrefs.Save();
        }
    }
}
