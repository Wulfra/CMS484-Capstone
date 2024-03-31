using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TruthTables : MonoBehaviour
{

    [SerializeField] private TMP_Text Variable_1;
    [SerializeField] private TMP_Text Variable_2;
    [SerializeField] private TMP_Text Expression;
    [SerializeField] private TMP_Text V1_T;
    [SerializeField] private TMP_Text V1_T2;
    [SerializeField] private TMP_Text V1_F;
    [SerializeField] private TMP_Text V1_F2;
    [SerializeField] private TMP_Text V2_T;
    [SerializeField] private TMP_Text V2_F;
    [SerializeField] private TMP_Text V2_T2;
    [SerializeField] private TMP_Text V2_F2;
    [SerializeField] private TMP_Text Expression_V1;
    [SerializeField] private TMP_Text Expression_V2;
    [SerializeField] private TMP_Text Expression_V3;
    [SerializeField] private TMP_Text Expression_V4;

    [SerializeField] private TMP_InputField UserInputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_Text CorrectAnswer;

    private List<string> ExpressionList = new List<string>
    {
        "A ^ B",
        "~(A ^ B)",
        "A ⊕ B",
        "~(A ⊕ B)",
        "A v B",
        "~(A v B)"
    };

    // Start is called before the first frame update
    void Start()
    {
        V1_T.text = "T";
        V1_T2.text = "T";
        V1_F.text = "F";
        V1_F2.text = "F";
        V2_T.text = "T";
        V2_F.text = "F";
        V2_T2.text = "T";
        V1_F2.text = "F";
        Expression_V1.text = "?";
        Expression_V2.text = "?";
        Expression_V3.text = "?";
        Expression_V4.text = "?";

        submitButton.onClick.AddListener(() => CheckAnswer(Expression.text));
        ExpressionSelector();

    }

    private void ExpressionSelector()
    {
        // Select a random expression
        int randomIndex = UnityEngine.Random.Range(0, ExpressionList.Count);
        string selectedExpression = ExpressionList[randomIndex];
        Expression.text = selectedExpression;
    }

    private void CheckAnswer(string selectedExpression)
    {
        string CorrectAnswerString = "";
        if (selectedExpression == "A ^ B")
        {
            CorrectAnswerString = "TFFF";
        }
        if (selectedExpression == "~(A ^ B)")
        {
            CorrectAnswerString = "FTTT";
        }
        if (selectedExpression == "A ⊕ B")
        {
            CorrectAnswerString = "FTTF";
        }
        if (selectedExpression == "~(A ⊕ B)")
        {
            CorrectAnswerString = "TFFT";
        }
        if (selectedExpression == "A v B")
        {
            CorrectAnswerString = "TTTF";
        }
        if (selectedExpression == "~(A v B)")
        {
            CorrectAnswerString = "FFFT";
        }

        string userAnswer = UserInputField.text;
        if (userAnswer == CorrectAnswerString)
        {
            Color textColor = Color.green;
            CorrectAnswer.color = textColor;
        }
        else
        {
            Color textColor = Color.red;
            CorrectAnswer.color = textColor;
        }
        CorrectAnswer.text = selectedExpression + " is: " + CorrectAnswerString;

    }

    private void OnInputFieldValueChanged(string value)
    {
        // Additional logic if needed
    }
}
