using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CipherScript : MonoBehaviour
{
    [SerializeField] private TMP_Text cipheredTextMeshPro;
    [SerializeField] private TMP_Text originalTextMeshPro;
    [SerializeField] private TMP_Text shiftValueTextMeshPro;
    [SerializeField] private TMP_InputField answerInputField;
    [SerializeField] private Button submitButton;

    private List<string> originalSentences = new List<string>();
    private List<string> cipheredSentences = new List<string>();
    private List<int> shiftValues = new List<int>();
    private int currentShiftValue;

    void Start()
    {
        InitializeUI();
        ProcessFileContent();
    }

    
    private void InitializeUI()
    {
        answerInputField.interactable = true;
        answerInputField.onValueChanged.AddListener(OnInputFieldValueChanged);
        submitButton.onClick.AddListener(CheckAnswer);
    }

    // Locate Cipherable.txt
    private void ProcessFileContent()
    {
        TextAsset textFile = Resources.Load<TextAsset>("Cipherable");
        if (textFile == null)
        {
            Debug.LogError("File not found in Resources folder.");
            return;
        }

        ProcessFile(textFile.text);
    }

    // Read Cipherable.txt line by line and display the ciphered sentence
    private void ProcessFile(string fileContent)
    {
        try
        {
            string[] lines = fileContent.Split('\n');
            foreach (string line in lines)
            {
                ProcessLine(line);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error processing file content: " + e.Message);
        }

        DisplayRandomCipheredSentence();
    }

    // Split and clean up sentences
    private void ProcessLine(string line)
    {
        // for each line, find and replace - with a newline return character in front
        // store the modified line in a list
        string modifiedLine = line.Replace("-", "\n-");
        originalSentences.Add(modifiedLine);

        // split each line upon the - character,
        // store the split line in a list
        // if list parts has elements
        // trim the first element to remove any lingering extra characters
        // store the trimmed parts as strings
        // Unity picks a random number from 1 to 26
        // pass the sentence and to CipherKey()
        // store the result as a string
        // add the resulting string to a list of type string
        // add the shift to a list of stype int
        // set the current shift value to the shift used for the line used
        string[] parts = line.Split('-');
        if (parts.Length > 0)
        {
            string sentence = parts[0].Trim();
            int shift = UnityEngine.Random.Range(1, 26);
            string ciphered = CipherKey(sentence, shift);
            cipheredSentences.Add(ciphered);
            shiftValues.Add(shift);
            //currentShiftValue = shift;
            // CheckAnswer(currentShiftValue);
        }
    }

    // Convert the string input into an array of chars
    // Add the value of the shift to each individual char
    // return the char array as a string
    private string CipherKey(string input, int shift)
    {
        char[] buffer = input.ToCharArray();
        for (int i = 0; i < buffer.Length; i++)
        {
            char letter = buffer[i];
            if (char.IsLetter(letter))
            {
                char shifted = (char)(letter + shift);
                if (char.IsUpper(letter))
                {
                    if (shifted > 'Z')
                        shifted = (char)(shifted - 26);
                }
                else
                {
                    if (shifted > 'z')
                        shifted = (char)(shifted - 26);
                }
                buffer[i] = shifted;
            }
        }
        return new string(buffer);
    }

    // if there is a ciphered sentence, pick one randomly
    // set the ciphered text display to the randomly picked sentence
    private void DisplayRandomCipheredSentence()
    {
        if (cipheredSentences.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, cipheredSentences.Count);
            cipheredTextMeshPro.text = cipheredSentences[randomIndex];
            currentShiftValue = shiftValues[randomIndex];

        }
    }

    // try to read the user's input as a valid format
    // if it's invalid, display an empty string
    // if it is valid, check if their value
    // matches the value of the shift

    // * pass int currentShiftValue *
    private void CheckAnswer()
    {
        string userAnswer = answerInputField.text;
        if (int.TryParse(userAnswer, out int userShiftValue))
        {
            bool isCorrect = userShiftValue == currentShiftValue;
            DisplayShiftValue(currentShiftValue, isCorrect);
            originalTextMeshPro.color = isCorrect ? Color.green : Color.red;
            originalTextMeshPro.text = originalSentences[cipheredSentences.IndexOf(cipheredTextMeshPro.text)];
        }
        else
        {
            answerInputField.text = "";
            Debug.LogError("Please enter a valid number.");
        }
    }

    private void DisplayShiftValue(int currentShiftValue, bool isCorrect)
    {
        Color textColor = isCorrect ? Color.green : Color.red;
        shiftValueTextMeshPro.color = textColor;
        shiftValueTextMeshPro.text = $"Shift Value: {currentShiftValue}";
    }

    private void OnInputFieldValueChanged(string value)
    {
        // Additional logic if needed
    }
}
