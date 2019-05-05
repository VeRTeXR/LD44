using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class TurnStateIcon : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private RuntimeAnimatorController[] _animatorControllers;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    
    public void SetTurnAnimatorState(LevelConfiguration.State state) 
    {
        if (state == LevelConfiguration.State.Day)
        {
            _animator.runtimeAnimatorController = _animatorControllers[0];
        }
        else if (state == LevelConfiguration.State.PrepNight)
        {
            _animator.runtimeAnimatorController = _animatorControllers[1];
        }
        else if(state == LevelConfiguration.State.Night)
        {
            _animator.runtimeAnimatorController = _animatorControllers[2];
        }
        else
        {
            Debug.LogError("animator ref not found");
        }
    }

}
