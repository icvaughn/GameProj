using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class PasswordGame : MonoBehaviour
{
    public TMP_InputField passwordInput;
    public TMP_Text feedbackText;
    public Button submitButton;

    void Start()
    {
        submitButton.onClick.AddListener(CheckPassword);
    }

    public void CheckPassword()
    {
        string password = passwordInput.text;

        if (IsPasswordStrong(password))
        {
            feedbackText.text = "? Strong password!";
            Invoke("CloseGame", 1.5f);
        }
        else
        {
            feedbackText.text = "? Weak password! Try again.";
        }
    }

    private bool IsPasswordStrong(string password)
    {
        return password.Length >= 8 &&
               Regex.IsMatch(password, @"[A-Z]") &&
               Regex.IsMatch(password, @"[a-z]") &&
               Regex.IsMatch(password, @"\d") &&
               Regex.IsMatch(password, @"[\W_]");
    }

    private void CloseGame()
    {
        gameObject.SetActive(false);
    }
}

