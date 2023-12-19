using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AlphabethMenu : MonoBehaviour
{
    [Header("Game object Reference")]
    public TMP_InputField _inputField;
    public Button _closeButton;
    public GameObject _blockPanel;
    public void Start()
    {
        this.restart();
    }
    public void restart()
    {
        this._closeButton.interactable = false;
        this.UpdateText();
    }
    public void SetButton()
    {
        DFA.Alphabet.Clear();
        string AlphabetToSet = this._inputField.text;
        foreach (char symbol in AlphabetToSet)
        {
            if((symbol!=',')&&(symbol!='{')&&(symbol!='}')&&(symbol!=' ')&&(symbol!='e'))
            {
                DFA.Alphabet.Add(symbol);
            }
        }
        this.UpdateText();
    }
    public void UpdateText()
    {
        if ( !(DFA.Alphabet.Count > 0) ) return;
        List<char> symbols = DFA.Alphabet.ToList<char>();
        string output = string.Empty;
        foreach (char symbol in symbols)
        {
            output += symbol+", ";
        }
        output = output.Remove(output.Length - 1);
        output = output.Remove(output.Length - 1);
        this._inputField.text = output;
        if(DFA.Alphabet.Count > 0)
        {
            this._closeButton.interactable=true;
        }
    }

    public void CloseButton()
    {
        this.restart();
        this._blockPanel.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
