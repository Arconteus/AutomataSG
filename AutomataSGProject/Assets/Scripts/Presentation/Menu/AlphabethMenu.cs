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
    public GameObject _blockPanel;
    public void Start()
    {
        this.UpdateText();
    }
    public void SetAlphabet()
    {
        DFA.Alphabet.Clear();
        string AlphabetToSet = this._inputField.text.ToLower();
        foreach (char symbol in AlphabetToSet)
        {
            DFA.Alphabet.Add(symbol);
        }
        this.UpdateText();
        if (_inputField.text != string.Empty)
        {
            this.CloseMenu();
        }
    }
    public void PresetLoad(int option)
    {
        if (option == 0)
        {
            _inputField.text = string.Empty;
        }
        if (option == 1)
        {
            _inputField.text = "0, 1";
        }
        if (option == 2)
        {
            _inputField.text = "w, x, y, z";
        }
        if (option == 3)
        {
            _inputField.text = "a, b, c, d";
        }
        if (option == 4)
        {
            _inputField.text = "0, 1, 2, 3 ,4 ,5 ,6 ,7 ,8 ,9";
        }
    }
    public void UpdateText()
    {
        if (!(DFA.Alphabet.Data.Count > 0))
        {
            _inputField.text = string.Empty;
            return;
        }
        List<char> symbols = DFA.Alphabet.Data.ToList<char>();
        string output = string.Empty;
        foreach (char symbol in symbols)
        {
            output += symbol+", ";
        }
        output = output.Remove(output.Length - 1);
        output = output.Remove(output.Length - 1);
        this._inputField.text = output;
    }

    public void OpenMenu()
    {
        this._blockPanel.SetActive(true);
        this.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        this.UpdateText();
        this._blockPanel.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
