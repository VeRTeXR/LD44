using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
  
    private Button _optionButton;
    private StartMenuController _startMenuController;


    void Awake()
    {
        _optionButton = GetComponent<Button>();
        if (_optionButton == null)
        {
            Debug.LogError("optionButton component missing");
        }
        else
        {
            _startMenuController = GetComponentInParent<StartMenuController>();
            if (!_startMenuController)
            {
                Debug.LogError("startMenuController missing from parent");
                return;
            }
            _optionButton.onClick.AddListener(_startMenuController.OptionButtonClicked);
        }
    }
}
