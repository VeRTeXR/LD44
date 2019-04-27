using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelController : MonoBehaviour {
    private NewGameButton _newGameButton;
    private LoadGameButton _loadGameButton;
    private OptionButton _optionButton;
    private ExitButton _exitButton;
    // Start is called before the first frame update
    void Start () {
        _newGameButton = GetComponentInChildren<NewGameButton> ();
        _loadGameButton = GetComponentInChildren<LoadGameButton> ();
        _optionButton = GetComponentInChildren<OptionButton> ();
        _exitButton = GetComponentInChildren<ExitButton> ();
    }

    // Update is called once per frame
    void Update () {

    }

    internal void RemoveAllButtonListener () {
        // _newGameButton.
    }
}