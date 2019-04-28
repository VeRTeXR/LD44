using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfiguration : MonoBehaviour {
    [SerializeField] private TextBoxManager _introTextBoxManager;
    [SerializeField] private DialogueSequenceManager _dialogueSequenceManager;
    
    
    private State _currentState;

    public enum State {
        Day,
        Night,
        PrepNight
    }

    public enum Dialogue
    {
        SleepInCoffin, 
        MeetingWithLocalGoons, 
        CatchUpOnTheNews,
        CheckMaterialStock,
        PrepareTheMerchandise,
        PactMaking,
        SaleOperation,
        HanginWithTheBois, 
    }

    private int _currentDay;

    private void Start () {
        _introTextBoxManager.DisableTextBox ();
    }

    public void LoadIntro () {
        Debug.LogError ("Load Intro");
        _introTextBoxManager.EnableTextBox ();
        
    }

    public void LoadDay (int day) {
        Debug.LogError ("day : " + day);
        _currentDay = day;
        _currentState = State.Day;

        GoToMapSelection (_currentState);
    }


    private void GoToMapSelection (State state) {
        if (state == State.Day) {

        } else if (state == State.PrepNight) {

        } else if (state == State.Night) {

        }

    }

    private void GoToDialogueSequence(Dialogue dialogue)
    {
        if (dialogue == Dialogue.SleepInCoffin)
        {
            Debug.LogError("sleepingIn");
            
            
               _dialogueSequenceManager.StartDialogueSequence(dialogue);
        }
        
        
    }
    

    internal void TextSequenceFinished (string sequenceName) {
        Debug.LogError (sequenceName + " Finished");
        if (sequenceName.Equals ("Intro")) {
            GameStateManager.Instance.StartMenu.GetComponentInChildren<ShowPanels> ().FadeOutPanelBackground ();
//            LoadDay (1);
            
            
            GoToDialogueSequence(Dialogue.SleepInCoffin);
        }

    }
}