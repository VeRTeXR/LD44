using System;
using UnityEngine;

public class ShowPanels : MonoBehaviour {

	public GameObject OptionsPanel; //Store a reference to the Game Object OptionsPanel 
	public GameObject OptionsTint; //Store a reference to the Game Object OptionsTint 
	public GameObject StartPanel; //Store a reference to the Game Object MenuPanel 
	public GameObject PausePanel; //Store a reference to the Game Object PausePanel 
	public GameObject GameplayPanel;
	public GameObject FinishPanel;
	public GameObject MainMenuPanel;
	public GameObject PanelBackground;

	public void ShowOptionsPanel () {
		Debug.LogError ("showOption");
		OptionsPanel.SetActive (true);
		OptionsTint.SetActive (true);
		FadeInOptionTint ();

	}

	public void FadeOutPanelBackground () {
		
		var panelCanvasGroup = PanelBackground.GetComponent<CanvasGroup> ();
		Debug.LogError(panelCanvasGroup );
		LeanTween.alphaCanvas (panelCanvasGroup, 0, 0.3f).setOnComplete (() => PanelBackground.SetActive (false));
	}

	public void FadeInPanelBackground () {
		PanelBackground.SetActive (true);
		var panelCanvasGroup = PanelBackground.GetComponent<CanvasGroup> ();
		LeanTween.alphaCanvas (panelCanvasGroup, 1, 0.3f);
	}

	private void FadeInOptionTint () {
		var tintCanvasGroup = OptionsTint.GetComponent<CanvasGroup> ();
		tintCanvasGroup.alpha = 0;
		LeanTween.alphaCanvas (tintCanvasGroup, 1, 0.3f).setOnComplete (() => FadeInOptionPanel ());
	}

	private void FadeInOptionPanel () {
		var optionCanvasGroup = OptionsPanel.GetComponent<CanvasGroup> ();
		optionCanvasGroup.alpha = 0;
		LeanTween.alphaCanvas (optionCanvasGroup, 1, 0.3f);
	}

	public void HideOptionsPanel () {
		FadeOutOptionPanel ();
	}

	private void FadeOutOptionPanel () {

		var optionCanvasGroup = OptionsPanel.GetComponent<CanvasGroup> ();
		LeanTween.alphaCanvas (optionCanvasGroup, 0, 0.3f).setOnComplete (() => {
			OptionsPanel.SetActive (false);
			OptionsTint.SetActive (false);

		});

	}

	public void ShowStartPanel () {
		StartPanel.SetActive (true);
	}

	public void HideStartPanel () {
		StartPanel.SetActive (false);
	}

	public void ShowMainMenuPanel () {
		MainMenuPanel.SetActive (true);
	}

	public void HideMainMenuPanel () {
		MainMenuPanel.SetActive (false);
	}

	public void ShowGameplay () {
		GameplayPanel.SetActive (true);
	}

	public void HideGameplay () {
		GameplayPanel.SetActive (false);
	}

	public void ShowFinishPanel () {
		FinishPanel.SetActive (true);
	}

	public void HideFinishPanel () {
		FinishPanel.SetActive (false);
	}

	public void ShowPausePanel () {
		PausePanel.SetActive (true);
		OptionsTint.SetActive (true);
	}

	public void HidePausePanel () {
		PausePanel.SetActive (false);
		OptionsTint.SetActive (false);
	}

	public GameObject GetMenuPanel () {
		return StartPanel;
	}
}