using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingGame : MonoBehaviour
{
    public TMP_Text instructionText;
    public TMP_Text sentenceText;
    public TMP_InputField typingInputField;
    public TMP_Text feedbackText;
    public Button submitButton;

    private string[] sentences = {
        "The quick brown fox jumps over the lazy dog.",
        "Cybersecurity is important to stay safe online.",
        "A strong password should include numbers and symbols."
    };

    private string currentSentence;
    private float startTime;
    private bool isGameActive = false;

    void OnEnable()
    {
        StartNewGame();
        submitButton.onClick.AddListener(CheckTyping);
    }

    void StartNewGame()
    {
        // Pick a random sentence
        currentSentence = sentences[Random.Range(0, sentences.Length)];
        sentenceText.text = currentSentence;
        typingInputField.text = "";
        feedbackText.text = "";
        startTime = Time.time;
        isGameActive = true;
    }

    public void CheckTyping()
    {
        if (!isGameActive) return;

        string playerInput = typingInputField.text;
        float timeTaken = Time.time - startTime;

        if (playerInput == currentSentence)
        {
            feedbackText.text = $"✅ Well done! Time: {timeTaken:F2} seconds";
            isGameActive = false;
            Invoke("CloseGame", 1.5f);
        }
        else
        {
            feedbackText.text = "❌ Typo detected! Try again.";
        }
    }

    private void CloseGame()
    {
        gameObject.SetActive(false);
    }
}

