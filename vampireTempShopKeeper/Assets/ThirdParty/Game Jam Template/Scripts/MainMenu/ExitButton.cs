using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    
    private Button _exitButton;
    private StartMenuController _startMenuController;

    void Awake()
    {
        _exitButton = GetComponent<Button>();
        if (_exitButton == null)
        {
            Debug.LogError("exitButton component missing");
        }
        else
        {
            _startMenuController = GetComponentInParent<StartMenuController>();
            if (!_startMenuController)
            {
                Debug.LogError("startMenuController missing from parent");
                return;
            }
            _exitButton.onClick.AddListener(_startMenuController.ExitButtonClicked);
        }
    }

}
