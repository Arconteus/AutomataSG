using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    [Header("Menu Area")]
    public GameObject _block;
    public GameObject _alphabetMenu;

    public void AlphabetButton()
    {
        this._alphabetMenu.SetActive(true);
        this._block.SetActive(true);
    }
}
