using System;
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
        Debug.LogError("try set Text : "+actionName);
        _textMesh = GetComponent<TextMeshProUGUI>();
        _textMesh.text = actionName;
    }

    public void SetAction(UnityAction action)
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(action);
        Debug.LogError("setListener");
        
    }
}
