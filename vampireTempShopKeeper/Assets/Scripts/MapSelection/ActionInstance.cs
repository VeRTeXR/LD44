using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionInstance : MonoBehaviour
{

    private Button _button;
    private TextMeshProUGUI _textMesh;
    
    public void SetNameUI(string actionName)
    {
        _textMesh.text = actionName;
    }

    public void SetAction(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }
}
