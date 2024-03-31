using UnityEngine;
using TMPro; // Required for TextMeshPro components
using UnityEngine.UI; // Required for Toggle component
using System;

public class BaseConversion : MonoBehaviour
{
    public TMP_InputField inputField;
    public Toggle binaryToggle;
    public Toggle hexadecimalToggle;
    public Toggle asciiToggle;
    public Toggle decimalToggle;
    
    public TMP_Text BinaryOutput;
    public TMP_Text HexadecimalOutput;
    public TMP_Text asciiOutput;
    public TMP_Text decimalOutput;

    void Update()
    {
        string input = inputField.text;

        if (binaryToggle.isOn)
        {
            ResetDisplay();
            BinaryToDecimal(input);
            BinaryToHexadecimal(input);
            BinaryToAscii(input);
            BinaryOutput.text = "";
        }
        else if (hexadecimalToggle.isOn)
        {
            ResetDisplay();
            HexadecimalToDecimal(input);
            HexadecimalToAscii(input);
            HexadecimalToBinary(input);
            HexadecimalOutput.text = "";
        }
        else if (asciiToggle.isOn)
        {
            ResetDisplay();
            AsciiToBinary(input);
            AsciiToDecimal(input);
            AsciiToHexadecimal(input);
            asciiOutput.text = "";
        }
        else if (decimalToggle.isOn)
        {
            ResetDisplay();
            DecimalToBinary(input);
            DecimalToHexadecimal(input);
            DecimalToAscii(input);
            decimalOutput.text = "";
        }
    }

    private void ResetDisplay()
    {
        BinaryOutput.text = "";
        HexadecimalOutput.text = "";
        asciiOutput.text = "";
        decimalOutput.text = "";
    }

    private void BinaryToAscii(string input)
    {
        string asciiString = "";
        try
        {
            for (int i = 0; i < input.Length; i += 8)
            {
                string binary = input.Substring(i, 8);
                int decimalValue = Convert.ToInt32(binary, 2);
                char asciiCharacter = (char)decimalValue;
                asciiString += asciiCharacter;
            }
        }
        catch (Exception)
        {
            asciiString = ""; // Clear the string if an exception occurs
        }
        asciiOutput.text = "ASCII: " + asciiString;
    }

    private void HexadecimalToAscii(string input)
    {
        string ascii = "";
        try
        {
            for (int i = 0; i < input.Length; i += 2)
            {
                string hs = input.Substring(i, 2);
                uint decval = System.Convert.ToUInt32(hs, 16);
                char character = System.Convert.ToChar(decval);
                ascii += character;
            }
        }
        catch (Exception)
        {
            ascii = ""; // Clear the string if an exception occurs
        }
        asciiOutput.text = "ASCII: " + ascii;
    }

    private void DecimalToAscii(string input)
    {
        string asciiCharacter = "";
        try
        {
            int decimalNumber = int.Parse(input);
            asciiCharacter = ((char)decimalNumber).ToString();
        }
        catch (Exception)
        {
            asciiCharacter = ""; // Clear the string if an exception occurs
        }
        asciiOutput.text = "ASCII: " + asciiCharacter;
    }

    private void AsciiToHexadecimal(string input)
    {
        string hexString = "";
        try
        {
            foreach (char c in input)
            {
                hexString += Convert.ToString(c, 16).ToUpper();
            }
        }
        catch (Exception)
        {
            hexString = ""; // Clear the string if an exception occurs
        }
        HexadecimalOutput.text = "Hex: " + hexString;
    }

    private void AsciiToDecimal(string input)
    {
        string decimalString = "";
        try
        {
            int decimalNumber = 0;
            foreach (char c in input)
            {
                decimalNumber = decimalNumber * 10 + (int)c;
            }
            decimalString = "Decimal: " + decimalNumber;
        }
        catch (Exception)
        {
            decimalString = ""; // Clear the string if an exception occurs
        }
        decimalOutput.text = decimalString;
    }

    private void AsciiToBinary(string input)
    {
        string binaryString = "";
        try
        {
            foreach (char c in input)
            {
                binaryString += Convert.ToString(c, 2).PadLeft(8, '0');
            }
        }
        catch (Exception)
        {
            binaryString = ""; // Clear the string if an exception occurs
        }
        BinaryOutput.text = "Binary: " + binaryString;
    }

    private void HexadecimalToDecimal(string input)
    {
        string decimalString = "";
        try
        {
            int decimalNumber = Convert.ToInt32(input, 16);
            decimalString = "Decimal: " + decimalNumber;
        }
        catch (Exception)
        {
            decimalString = ""; // Clear the string if an exception occurs
        }
        decimalOutput.text = decimalString;
    }

    private void BinaryToDecimal(string input)
    {
        string decimalString = "";
        try
        {
            int decimalNumber = Convert.ToInt32(input, 2);
            decimalString = "Decimal: " + decimalNumber;
        }
        catch (Exception)
        {
            decimalString = ""; // Clear the string if an exception occurs
        }
        decimalOutput.text = decimalString;
    }

    private void BinaryToHexadecimal(string input)
    {
        string hexString = "";
        try
        {
            int binaryValue = Convert.ToInt32(input, 2);
            hexString = binaryValue.ToString("X");
        }
        catch (Exception)
        {
            hexString = ""; // Clear the string if an exception occurs
        }
        HexadecimalOutput.text = "Hex: " + hexString;
    }

    private void DecimalToHexadecimal(string input)
    {
        string hexString = "";
        try
        {
            int decimalNumber = int.Parse(input);
            hexString = decimalNumber.ToString("X");
        }
        catch (Exception)
        {
            hexString = ""; // Clear the string if an exception occurs
        }
        HexadecimalOutput.text = "Hex: " + hexString;
    }

    private void DecimalToBinary(string input)
    {
        string binaryString = "";
        try
        {
            int decimalNumber = int.Parse(input);
            binaryString = Convert.ToString(decimalNumber, 2).PadLeft(8, '0');
        }
        catch (Exception)
        {
            binaryString = ""; // Clear the string if an exception occurs
        }
        BinaryOutput.text = "Binary: " + binaryString;
    }

    private void HexadecimalToBinary(string input)
    {
        string binaryString = "";
        try
        {
            foreach (char c in input)
            {
                string binaryValue;
                switch (c)
                {
                    case '0': binaryValue = "0000"; break;
                    case '1': binaryValue = "0001"; break;
                    case '2': binaryValue = "0010"; break;
                    case '3': binaryValue = "0011"; break;
                    case '4': binaryValue = "0100"; break;
                    case '5': binaryValue = "0101"; break;
                    case '6': binaryValue = "0110"; break;
                    case '7': binaryValue = "0111"; break;
                    case '8': binaryValue = "1000"; break;
                    case '9': binaryValue = "1001"; break;
                    case 'A': binaryValue = "1010"; break;
                    case 'B': binaryValue = "1011"; break;
                    case 'C': binaryValue = "1100"; break;
                    case 'D': binaryValue = "1101"; break;
                    case 'E': binaryValue = "1110"; break;
                    case 'F': binaryValue = "1111"; break;
                    default: throw new ArgumentException("Invalid hexadecimal character.");
                }
                binaryString += binaryValue;
            }
        }
        catch (Exception)
        {
            binaryString = ""; // Clear the string if an exception occurs
        }
        BinaryOutput.text = "Binary: " + binaryString;
    }
}
