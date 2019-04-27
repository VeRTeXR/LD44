using UnityEngine;
using System.Collections;

public class ActivateTextAtLine : MonoBehaviour {

	public TextAsset theText;

	public int startLine;
	public int endLine;
	public bool requiredButtonPress;
	private bool waitForPress;

	public TextBoxManager theTextBox;

	// Use this for initialization
	void Start () {
		theTextBox = FindObjectOfType<TextBoxManager>();
	}
	
	// Update is called once per frame
	void Update () {
	 if(waitForPress && Input.GetKeyDown(KeyCode.Return)){
	 		theTextBox.ReloadScript(theText);
			theTextBox.currentLine = startLine;
			theTextBox.endAtLine = endLine;
			theTextBox.EnableTextBox();
	 }
	}

	void OnTriggerEnter2D(Collider2D c) {

		if(c.name == "Player2"){
			if(requiredButtonPress) {
				waitForPress = true;
				return;
			}
			theTextBox.ReloadScript(theText);
			theTextBox.currentLine = startLine;
			theTextBox.endAtLine = endLine;
			theTextBox.EnableTextBox();
		}
	}

	void OnTriggerExit2D(Collider2D c) {
		if(c.name == "Player") {
			waitForPress = false;
		}
	}
}
