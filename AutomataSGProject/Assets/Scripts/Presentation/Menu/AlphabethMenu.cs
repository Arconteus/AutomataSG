using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphabethMenu : MonoBehaviour
{
    [Header("Game object Reference")]
    public InputField _inputField;

    public void SetButton()
    {
        HashSet<char> Symbols = new HashSet<char>();
        foreach(char symbol in _inputField.text)
        {
            if(symbol != ',')
            {
                Symbols.Add(symbol);
            }
        }
    }
}
