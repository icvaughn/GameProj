using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PhishingGame : MonoBehaviour
{
    public TMP_Text emailText;
    public TMP_Text feedbackText;
    public Button realButton;
    public Button fakeButton;

    private List<(string, bool)> emails = new List<(string, bool)>()
    {
        ("Your PayPal account has been locked! Click here to verify.", false),
        ("Google security alert: Login detected from a new device.", true),
        ("Congratulations! You won a free iPhone! Click this link.", false),
        ("Amazon: Your order #123456 has been shipped.", true),
        ("Suspicious activity detected on your bank account. Click to secure.", false)
    };

    private (string, bool) currentEmail;

    void OnEnable()
    {
        StartNewRound();
        realButton.onClick.AddListener(() => CheckAnswer(true));
        fakeButton.onClick.AddListener(() => CheckAnswer(false));
    }

    void StartNewRound()
    {
        currentEmail = emails[Random.Range(0, emails.Count)];
        emailText.text = currentEmail.Item1;
        feedbackText.text = "";
    }

    private void CheckAnswer(bool playerChoice)
    {
        if (playerChoice == currentEmail.Item2)
        {
            feedbackText.text = "✅ Correct!";
        }
        else
        {
            feedbackText.text = "❌ Incorrect! That was a phishing attempt.";
        }
        Invoke("CloseGame", 1.5f);
    }

    private void CloseGame()
    {
        gameObject.SetActive(false);
    }
}
