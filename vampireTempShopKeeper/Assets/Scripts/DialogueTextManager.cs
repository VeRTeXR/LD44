using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueTextManager : MonoBehaviour
{
    private TextImporter _textImporter;
    
    public TextMeshProUGUI CurrentText;

    public int CurrentLine;
    public int EndAtLine;

    public TextAsset TextFile;
    public string[] TextLines;

    public bool IsActive; 
    private bool _isTyping;
    private bool _cancelTyping;
    public float TypeSpeed;

    private void OnEnable()
    {
        CurrentText = GetComponentInChildren<TextMeshProUGUI>();
        _textImporter = GetComponentInChildren<TextImporter>();
        
        if (TextFile != null)
        {
            TextLines = (TextFile.text.Split('\n'));
        }

        if (EndAtLine == 0)
        {
            EndAtLine = TextLines.Length - 1;
        }

        if (IsActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }
    }

    private void DisableTextBox()
    {
        gameObject.SetActive(false);
        IsActive = false;
    }

    private void EnableTextBox()
    {
       gameObject.SetActive(true);
       		IsActive = true;
       		TextLines = GetComponent<TextImporter>().TextLines;
       		StartCoroutine(TextScroll(TextLines[CurrentLine]));
    }
    
    private IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
        CurrentText.text = "";
        _isTyping = true;
        _cancelTyping = false;

        while (_isTyping && !_cancelTyping && (letter < lineOfText.Length - 1))
        {
            CurrentText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(TypeSpeed);
        }

        CurrentText.text = lineOfText;
        _isTyping = false;
        _cancelTyping = false;
    }
    
    
    void Update()
    {
        if (!IsActive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!_isTyping)
            {
                CurrentLine += 1;

                if (CurrentLine > EndAtLine)
                {
                    var levelConfigurator = GameStateManager.Instance.GetLevelConfigurator();
                    levelConfigurator.TextSequenceFinished(TextFile.name);
                    DisableTextBox();
                }
                else
                {
                    StartCoroutine(TextScroll(TextLines[CurrentLine]));
                }
            }
            else if (_isTyping && !_cancelTyping)
            {
                _cancelTyping = true;
            }
        }
    }


    public void LoadDialogue(LevelConfiguration.Dialogue key)
    {
        if (_textImporter == null) GetComponentInChildren<TextImporter>();
        _textImporter.Load(key);
        EnableTextBox();
    }
}
