using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SuperTags;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {

    public static GameStateManager Instance = null;

    public int Level;
    public GameObject StartMenu;
    public GameObject GameplayArea;
    private LevelConfiguration _levelConfigurator;
    private Text _levelText;

    private int _currentMoney;
    private int _currentBlood;

    void Start () {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy (gameObject);

        DontDestroyOnLoad (gameObject);

        StartMenu = GameObject.FindGameObjectWithTag ("StartMenu");
        GameplayArea = GameObject.FindGameObjectWithTag ("GameplayArea");
        if (GameplayArea != null) {
            var levelConf = GameplayArea.GetComponent<LevelConfiguration> ();
            if (levelConf != null)
                _levelConfigurator = levelConf;
            else
                Debug.LogError ("LevelConfigurator Missing");
        }
    }

    public LevelConfiguration GetLevelConfigurator () {
        return _levelConfigurator;
    }

    public void GameOver () {

        //ShowGameOver(_currentMoney, _currentBlood);
        ResetPlayerCurrency ();
    }

    public void Restart () {
        Debug.LogError ("Restart");
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
        ResetPlayerCurrency ();
    }

    private void ResetPlayerCurrency () {
        _currentMoney = 0;
        _currentBlood = 0;
    }

    public void InitGameManager () {
        // var eventManager = SuperTagsSystem.GetObjectsWithTag("EventManager");
        // eventManager[0].GetComponent<PlayerEventManager> ().Initialize ();
        // var scoreManager = SuperTagsSystem.GetObjectsWithTag("ScoreManager");
        // scoreManager[0].GetComponent<ScoreManager> ().Initialize ();
    }
}