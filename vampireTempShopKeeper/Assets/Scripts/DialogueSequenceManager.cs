﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSequenceManager : MonoBehaviour
{
    [SerializeField] private DialogueTextManager _dialogueTextManager;

    [SerializeField] private DialogueNpcManager _dialogueNpcManager;

    
    void Start()
    {
        _dialogueTextManager = GetComponentInChildren<DialogueTextManager>();
        _dialogueNpcManager = GetComponentInChildren<DialogueNpcManager>();
    }


    public void StartDialogueSequence(LevelConfiguration.Dialogue key)
    {
        Debug.LogError("key : "+key);
        gameObject.SetActive(true);
        _dialogueTextManager.LoadDialogue(key);
        _dialogueTextManager.gameObject.SetActive(true);
        
        _dialogueNpcManager.SetCharacter(key);
    }
}
