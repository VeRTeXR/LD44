using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
	public Text currentText;

	public int currentLine;
	public int endAtLine;

	public TextAsset textFile;
	public string[] textLines;

	public bool isActive = false; 
	private bool isTyping = false;
	private bool cancelTyping = false;
	public float typeSpeed;

	void Start()
	{
		if (textFile != null)
		{
			textLines = (textFile.text.Split('\n'));
		}

		if (endAtLine == 0)
		{
			endAtLine = textLines.Length - 1;
		}

		if (isActive)
		{
			EnableTextBox();
		}
		else
		{
			DisableTextBox();
		}
	}

	public void AssignTextLines(string[] textLines)
	{
		this.textLines = textLines;
	}

	void Update()
	{
		if (!isActive)
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (!isTyping)
			{
				currentLine += 1;

				if (currentLine > endAtLine)
				{
					var levelConfigurator = GameStateManager.Instance.GetLevelConfigurator();
					levelConfigurator.TextSequenceFinished(textFile.name);
					DisableTextBox();
				}
				else
				{
					StartCoroutine(TextScroll(textLines[currentLine]));
				}
			}
			else if (isTyping && !cancelTyping)
			{
				cancelTyping = true;
			}
		}
	}

	private IEnumerator TextScroll(string lineOfText)
	{
		int letter = 0;
		currentText.text = "";
		isTyping = true;
		cancelTyping = false;

		while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
		{
			currentText.text += lineOfText[letter];
			letter += 1;
			yield return new WaitForSeconds(typeSpeed);
		}

		currentText.text = lineOfText;
		isTyping = false;
		cancelTyping = false;
	}

	public void EnableTextBox()
	{
		gameObject.SetActive(true);
		isActive = true;
		Debug.LogError(GetComponent<TextImporter>());
		textLines = GetComponent<TextImporter>().TextLines;
		Debug.LogError(textLines.Length);
		StartCoroutine(TextScroll(textLines[currentLine]));
	}

	public void DisableTextBox()
	{
		gameObject.SetActive(false);
		isActive = false;
	}

	public void ReloadScript(TextAsset textAsset)
	{
		if (textAsset != null)
		{
			textLines = new string[1];
			textLines = (textAsset.text.Split('\n'));
		}
	}
}