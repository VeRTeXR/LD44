using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class ActionCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _actionPrefab;
    private List<GameObject> _actionsInstance;
    
    
    public void GenerateActions(List<Action> actions)
    {
        for (var i = 0; i < actions.Count; i++)
        {
            Debug.LogError("ReadAction : " + actions[i].Method.Name);
            if (_actionPrefab != null)
            {
                if (_actionsInstance[i] != null)
                {
                    
                }
                else
                {
                    var actionInstance = Instantiate(_actionPrefab);
                    var action = actionInstance.GetComponent<ActionInstance>();
                    action.SetNameUI();
                    action.SetAction () 
                }
            }
            else
            {
                Debug.LogError("ActionPrefab is null");
            }
        }
    }
}
