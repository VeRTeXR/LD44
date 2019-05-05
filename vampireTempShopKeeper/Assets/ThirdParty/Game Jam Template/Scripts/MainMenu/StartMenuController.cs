using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour {
	public Animator AnimColorFade;
	public Animator MenuAlpha;
	public Animator GameplayAlpha;
	public AnimationClip FadeColorAnimationClip;
	public AnimationClip FadeAlphaAnimationClip;
	public bool ChangeMusicOnStart;

	private PlayMusic _playMusic;
	private float fastFadeIn = .01f;
	private ShowPanels _showPanels;
	private GameObject _player;
	private GameObject _startOptionSelector;
	[SerializeField] private GameObject _startButton;
	[SerializeField] private GameObject _menuPanel;
	[SerializeField] private float _startButtonFadeOutDelay = 2f;
	private float _menuPanelFadeOutDelay = 2f;

	public void Awake () {
		InitializeMenuAndPauseGameplay ();
	}

	private void InitializeMenuAndPauseGameplay () {
		_startButton = GetComponentInChildren<StartButton> ().gameObject;
		// _menuPanel = GetComponentInChildren<MenuPanelController> ().gameObject;
		_showPanels = GetComponentInParent<ShowPanels> ();
		_playMusic = GetComponentInParent<PlayMusic> ();
	}

	public void StartButtonClicked () {
		_startButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
		var seq = LeanTween.sequence ();
		seq.append (() => FadeOutThenDisableStartButton ());
		seq.append (_startButtonFadeOutDelay);
		seq.append (() => FadeInMenuPanel ());
	}

	internal void NewGameButtonClicked () {
		GameStateManager.Instance.GameplayArea.SetActive(true);
		var _levelConf = GameStateManager.Instance.GetLevelConfigurator ();
		_showPanels.HideStartPanel ();
		_levelConf.LoadIntro ();
	}

	public void OptionButtonClicked () {
		_showPanels.ShowOptionsPanel ();
	}

	private void FadeOutMenuPanel () {
		var panelCanvasGroup = _menuPanel.GetComponent<CanvasGroup> ();
		var seq = LeanTween.sequence ();
		seq.append (() => LeanTween.alphaCanvas (panelCanvasGroup, 1, 0));
		seq.append (() => LeanTween.alphaCanvas (panelCanvasGroup, 0, _menuPanelFadeOutDelay).setEaseInElastic ());
	}

	public void LoadGameButtonClicked () {
		Debug.LogError ("LoadGameButtonClick");
	}

	public void ExitButtonClicked () {
		Application.Quit ();
	}

	private void FadeInMenuPanel () {
		_menuPanel.SetActive (true);
		var panelCanvasGroup = _menuPanel.GetComponent<CanvasGroup> ();
		panelCanvasGroup.alpha = 0;
		LeanTween.alphaCanvas (panelCanvasGroup, 1, _startButtonFadeOutDelay);
	}

	private void FadeOutThenDisableStartButton () {

		var _startButtonCanvasGroup = _startButton.GetComponent<CanvasGroup> ();
		LeanTween.alphaCanvas (_startButtonCanvasGroup, 0, _startButtonFadeOutDelay).setOnComplete (() => _startButton.SetActive (false));
	}

	public void PlayNewMusic () {
		_playMusic.FadeUp (fastFadeIn);
		_playMusic.PlaySelectedMusic (1);
	}

}