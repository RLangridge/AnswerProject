using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GlobalProject;
using System;

[RequireComponent(typeof(Canvas))]
public class UI_DialogueOptions : MonoBehaviour, ICheckInitialization {

    //The buttons that we wish to use to interact with the world
    [Tooltip("The buttons that we wish to use to interact with the world")]
    public Button[] DialogueButtons;

    //The dialogue that will be inserted onto the buttons
    [Tooltip("The dialogue that will be inserted onto the buttons (In order)")]
    public string[] DialogueOptions;

    //The responses to each dialogue option (in order)
    [Tooltip("The responses to each dialogue option (in order)")]
    public string[] DialogueResponses;

    //The text that will display the worlds text to us
    [Tooltip("The text that will display the worlds text to us")]
    public Text SpeechText;

    void Awake()
    {
#if (DEBUG)
        CheckInitializationForExceptions();
#endif
        SetupButtons();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetupButtons()
    {
        for (int index = 0; index < DialogueButtons.Length; ++index)
        {
            PushTextOntoDialogueButtons(index);
            SetupOnClickFunctionsForButtons(index);
        }
    }

    /// <summary>
    /// We retrieve the first child (which in my case is going to be a text object)
    /// and place the appropriate dialogue option onto the text field.
    /// </summary>
    private void PushTextOntoDialogueButtons(int _index)
    {
        GetTextFromButton(DialogueButtons[_index]).text = DialogueOptions[_index];
    }

    /// <summary>
    /// We use this function to assign an onclick function to each button. As the functionality of the buttons
    /// does not differ except from which index we should be using when referencing dialogue, it's better
    /// to just throw in a lambda here so that we don't have to re-write a heap of code.
    /// </summary>
    /// <param name="_index"></param>
    private void SetupOnClickFunctionsForButtons(int _index)
    {
        var btn = DialogueButtons[_index];
        btn.onClick.AddListener(() => OnButtonClick(_index));
    }

    private void OnButtonClick(int _index)
    {
        SpeechText.text = DialogueResponses[_index];
    }

    /// <summary>
    /// This retrieves the text element from the button. This is done by retrieving the transform of the button,
    /// getting its first child (A button by default only has one child which is a text element), then retrives the
    /// text component.
    /// </summary>
    /// <param name="_buttonToExtractTextFrom">The button that we want to extract the text element from</param>
    /// <returns>The text element of the child of the button</returns>
    private Text GetTextFromButton(Button _buttonToExtractTextFrom)
    {
        return _buttonToExtractTextFrom.transform.GetChild(0).GetComponent<Text>();
    }

    public void CheckInitializationForExceptions()
    {
        if(DialogueButtonsNotEqualToDialogueOptions())
        {
            throw new InitializeException("Error: The number of dialogue buttons are not equal to the number of dialogue options.");
        }
        if(InvalidDialogueButtons())
        {
            throw new InitializeException("Error: There are null entries for the dialogue buttons.");
        }
        if(InvalidDialogueOptions())
        {
            throw new InitializeException("Error: There are empty entries in the dialogue options.");
        }
        if(InvalidDialogueResponses())
        {
            throw new InitializeException("Error: There are empty entries in the dialogue responses.");
        }
    }

    private bool DialogueButtonsNotEqualToDialogueOptions()
    {
        return DialogueButtons.Length != DialogueOptions.Length || DialogueButtons.Length != DialogueResponses.Length;
    }

    private bool InvalidDialogueButtons()
    {
        foreach(var btn in DialogueButtons)
        {
            if (btn == null)
                return true;
        }

        return false;
    }

    private bool InvalidDialogueOptions()
    {
        foreach(var dialogue in DialogueOptions)
        {
            if (dialogue.Length == 0)
                return true;
        }

        return false;
    }

    private bool InvalidDialogueResponses()
    {
        foreach (var response in DialogueResponses)
        {
            if (response.Length == 0)
                return true;
        }

        return false;
    }
}
