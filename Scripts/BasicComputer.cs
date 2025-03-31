using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BasicComputer : InteractalbleObj
{
    public GameObject uiPanel;  // Main computer screen panel
    public Image computerScreenImage;
    public Sprite defaultScreen;
    public Sprite loadingScreen;
    public Sprite newScreen;

    public GameObject[] miniGames; // Mini-game UI panels
    private GameObject activeMiniGame; // Stores the current active game

    private bool isPlayerNear = false;

    void Start()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }

        if (computerScreenImage != null && defaultScreen != null)
        {
            computerScreenImage.sprite = defaultScreen;
        }

        // Ensure all mini-games are disabled at the start
        foreach (var game in miniGames)
        {
            game.SetActive(false);
        }
    }

    void Update()
    {

    }

    private void Activate()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
            Debug.Log("Computer UI opened.");

            // Show loading screen
            if (computerScreenImage != null && loadingScreen != null)
            {
                computerScreenImage.sprite = loadingScreen;
            }

            // Choose a random mini-game
            if (miniGames.Length > 0)
            {
                int randomIndex = Random.Range(0, miniGames.Length);
                activeMiniGame = miniGames[randomIndex];
                StartCoroutine(LoadGameAfterDelay(activeMiniGame));
            }
        }
    }

    private IEnumerator LoadGameAfterDelay(GameObject miniGame)
    {
        yield return new WaitForSeconds(2f);  // Simulate loading screen

        if (computerScreenImage != null && newScreen != null)
        {
            computerScreenImage.sprite = newScreen;
        }

        miniGame.SetActive(true);
        Debug.Log("Mini-game loaded.");
    }

}
