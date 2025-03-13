using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class talkingcharacter : InteractalbleObj
{
    [SerializeField] private string[] dialogue;
    [SerializeField] private TextMeshProUGUI dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Activate()
    {
        // Enables the text field and sets the text to the first string in the dialogue array
        Debug.Log("Interactable object activated!");
        if (dialogueText != null && dialogue.Length > 0)
        {
            dialogueText.text = dialogue[0];
            StartCoroutine(ShowTextForDuration(10f));
        }
    }

    private IEnumerator ShowTextForDuration(float duration)
    {
        dialogueText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        dialogueText.gameObject.SetActive(false);
    }
}