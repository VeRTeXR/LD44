using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.WSA.Input;

public class ActionCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _actionPrefab;
    private List<GameObject> _actionsInstances = new List<GameObject>();


    public void GenerateActions(List<UnityAction> actions)
    {
        for (var i = 0; i < actions.Count; i++)
        {
            Debug.LogError("ReadAction : " + actions[i].Method.Name);
            if (_actionPrefab != null)
            {
//                if (_actionsInstances[i] != null)
//                {
//                    Debug.LogError("already have some instance");
//                }
//                else
//                {
                var actionInstance = Instantiate(_actionPrefab);
                var action = actionInstance.GetComponent<ActionInstance>();
                action.SetNameUI(actions[i].Method.Name);
                action.SetAction(actions[i]);
                actionInstance.transform.SetParent(transform);
                actionInstance.transform.localScale = Vector3.one;
                _actionsInstances.Add(actionInstance);
                Debug.LogError("created and add");
//                }
            }
            else
            {
                Debug.LogError("ActionPrefab is null");
            }
        }
    }
}
