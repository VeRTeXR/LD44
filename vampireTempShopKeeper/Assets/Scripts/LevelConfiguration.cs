using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfiguration : MonoBehaviour
{
    [SerializeField] private TextBoxManager _introTextBoxManager;
    [SerializeField] private DialogueSequenceManager _dialogueSequenceManager;
    [SerializeField] private MapSelectionManager _mapSelectionManager;


    private State _currentState;

    public enum State
    {
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

    private void Awake()
    {
        _introTextBoxManager.DisableTextBox();
        _mapSelectionManager = GetComponentInChildren<MapSelectionManager>();
    }

    public void LoadIntro()
    {
        Debug.LogError("LoadIntro");
        _introTextBoxManager.EnableTextBox();
    }

    public void LoadDay(int day)
    {
        Debug.LogError("day : " + day);
        _currentDay = day;
        _currentState = State.PrepNight;

        GoToMapSelection(_currentState);
    }


    private void GoToMapSelection(State state)
    {
        _mapSelectionManager.SetupElement(state, _currentDay);
    }

    public void GoToDialogueSequence(Dialogue dialogue)
    {
        if (dialogue == Dialogue.SleepInCoffin)
        {
            _dialogueSequenceManager.StartDialogueSequence(dialogue);
        }
    }


    public void TextSequenceFinished(string sequenceName)
    {
        if (sequenceName.Equals("Intro"))
        {
            GameStateManager.Instance.StartMenu.GetComponentInChildren<ShowPanels>().FadeOutPanelBackground();
        
            _currentDay = 1;
            LoadDay(_currentDay);
        }
    }
}