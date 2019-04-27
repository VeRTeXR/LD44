using UnityEngine;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour
{
    
    private Button _loadGameButton;
    private StartMenuController _startMenuController;

    void Awake()
    {
        _loadGameButton = GetComponent<Button>();
        if (_loadGameButton == null)
        {
            Debug.LogError("loadGameButton component missing");
        }
        else
        {
            _startMenuController = GetComponentInParent<StartMenuController>();
            if (!_startMenuController)
            {
                Debug.LogError("startMenuController missing from parent");
                return;
            }
            _loadGameButton.onClick.AddListener(_startMenuController.LoadGameButtonClicked);
        }
    }
    
}
