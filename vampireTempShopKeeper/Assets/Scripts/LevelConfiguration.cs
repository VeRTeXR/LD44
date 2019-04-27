using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfiguration : MonoBehaviour {
    [SerializeField] private TextBoxManager _textBoxManager;
    private State _currentState;

    enum State {
        Day,
        Night,
        PrepNight
    }

    private int _currentDay;

    private void Start () {
        _textBoxManager.DisableTextBox ();
    }

    public void LoadIntro () {
        Debug.LogError ("Load Intro");
        _textBoxManager.EnableTextBox ();
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

    internal void TextSequenceFinished (string sequenceName) {
        Debug.LogError (sequenceName + " Finished");
        if (sequenceName.Equals ("Intro")) {
            GameStateManager.Instance.StartMenu.GetComponentInChildren<ShowPanels> ().FadeOutPanelBackground ();
            LoadDay (1);
        }

    }
}