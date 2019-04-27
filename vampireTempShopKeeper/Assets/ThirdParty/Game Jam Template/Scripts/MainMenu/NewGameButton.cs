using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour {

    private Button _newGameButton;
    private StartMenuController _startMenuController;

    void Awake () {
        _newGameButton = GetComponent<Button> ();
        if (_newGameButton == null) {
            Debug.LogError ("loadGameButton component missing");
        } else {
            _startMenuController = GetComponentInParent<StartMenuController> ();
            if (!_startMenuController) {
                Debug.LogError ("startMenuController missing from parent");
                return;
            }
            _newGameButton.onClick.AddListener (_startMenuController.NewGameButtonClicked);
        }
    }
}