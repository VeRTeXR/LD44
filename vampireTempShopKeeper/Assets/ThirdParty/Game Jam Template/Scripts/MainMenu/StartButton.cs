using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
    private Button _startButton;
    private StartMenuController _startMenuController;

    void Start () {
        _startButton = GetComponent<Button> ();
        if (_startButton == null) {
            Debug.LogError ("startButton component missing");
        } else {
            _startMenuController = GetComponentInParent<StartMenuController> ();
            if (!_startMenuController) {
                Debug.LogError ("startMenuController missing from parent");
                return;
            }
            _startButton.onClick.AddListener (_startMenuController.StartButtonClicked);
        }
    }

    void onClick () {
        _startButton.onClick.RemoveAllListeners ();
    }
}