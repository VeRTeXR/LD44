using System.Collections;
using Assets.SuperTags;
using UnityEngine;
using UnityEngine.UI;

public class UiGameOverController : MonoBehaviour
{
	public GameObject ScoreText;
	public GameObject Epilogue1;
	public GameObject RetryButton;
	
	// Use this for initialization
	public void OnEnable ()
	{
		// ScoreManager.Instance.Save();
		StartEpilogueAnimation();
		ResetScoreText();
	}

	private void StartEpilogueAnimation()
	{
		// LeanTween.move(Epilogue1, new Vector2(950, 520), 2f).setEaseInExpo();
		StartCoroutine(waitAndShowResult());
	}

	private IEnumerator waitAndShowResult()
	{
		yield return  new WaitForSecondsRealtime(2.5f);
		// LeanTween.move(ScoreText, new Vector2(950, 490), 2f).setEaseInExpo();
		StartCoroutine(waitAndShowRetry());
	}

	private IEnumerator waitAndShowRetry()
	{
		yield return new WaitForSecondsRealtime(2.5f);
		// LeanTween.move(RetryButton, new Vector2(950, 350), 2f).setEaseInExpo();
	}
	
	public void ResetScoreText()
	{
			// ScoreText.GetComponent<Text>().text = "TOTAL FOLLOWER COUNT\n"+ScoreManager.Instance.GetTotalFollower();
	}


}
