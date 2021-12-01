using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum GameOptions
{
    Return,
    Restart,
    Controls,
    Credits,
    Exit
}

public class OptionMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    private bool isOptionMenuOpen;
    [SerializeField] private GameObject creditsMenu;
    private bool isCreditsMenuOpen;
    [SerializeField] private GameObject controlsMenu;
    private bool isControlsMenuOpen;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject returnButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject creditsButton;
    [SerializeField] private TMP_Text exitButtonText;
    [SerializeField] private TMP_Text returnButtonText;
    [SerializeField] private TMP_Text restartButtonText;
    [SerializeField] private TMP_Text creditsButtonText;
    [SerializeField] private TMP_Text controlsButtonText;
    [SerializeField] private GameObject exitButtonSelector;
    [SerializeField] private GameObject returnButtonSelector;
    [SerializeField] private GameObject restartButtonSelector;
    [SerializeField] private GameObject creditsButtonSelector;
    [SerializeField] private GameObject controlsButtonSelector;
    private GameOptions[] menuGameOptions = { GameOptions.Return, GameOptions.Controls, GameOptions.Credits, GameOptions.Exit};
    private GameOptions[] gameGameOptions = { GameOptions.Return, GameOptions.Restart, GameOptions.Controls, GameOptions.Credits, GameOptions.Exit };
    private GameOptions[] gameOptionsArray;
    private int selectorIndex;
    private int maxSelectorIndex;

    private void Start()
    {
        isCreditsMenuOpen = false;
        creditsMenu.SetActive(isCreditsMenuOpen);
        isControlsMenuOpen = false;
        controlsMenu.SetActive(isControlsMenuOpen);
        isOptionMenuOpen = false;
        optionMenu.SetActive(isOptionMenuOpen);
        exitButtonText.text = UIHelper.WriteStringToFont("Exit Game".ToUpper());
        returnButtonText.text = UIHelper.WriteStringToFont("Return".ToUpper());
        restartButtonText.text = UIHelper.WriteStringToFont("Restart".ToUpper());
        creditsButtonText.text = UIHelper.WriteStringToFont("Credits".ToUpper());
        controlsButtonText.text = UIHelper.WriteStringToFont("Controls".ToUpper());
        exitButtonSelector.SetActive(false);
        returnButtonSelector.SetActive(false);
        restartButtonSelector.SetActive(false);
        creditsButtonSelector.SetActive(false);
        controlsButtonSelector.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") || (Input.GetButtonDown("Submit") && !isOptionMenuOpen))
        {
            ToggleMenu();
            if (isCreditsMenuOpen)
            {
                ToggleCredits();
            }
            if (isControlsMenuOpen)
            {
                ToggleControls();
            }
        }

        if (isOptionMenuOpen)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                restartButton.SetActive(false);
                gameOptionsArray = menuGameOptions;
            }
            else // buildIndex == 1
            {
                restartButton.SetActive(true);
                gameOptionsArray = gameGameOptions;
            }

            HighlightOption(gameOptionsArray[selectorIndex]);
            SelectOption();

            if (Input.GetButtonDown("Submit"))
            {
                if (isCreditsMenuOpen)
                {
                    ToggleCredits();
                }
                else if (isControlsMenuOpen)
                {
                    ToggleControls();
                }
                else
                {
                    SubmitOption(gameOptionsArray[selectorIndex]);
                }
            }
        }
    }

    private void HighlightOption(GameOptions option)
    {
        switch (option)
        {
            case GameOptions.Return:
                exitButtonSelector.SetActive(false);
                returnButtonSelector.SetActive(true);
                restartButtonSelector.SetActive(false);
                creditsButtonSelector.SetActive(false);
                controlsButtonSelector.SetActive(false);
                break;
            case GameOptions.Restart:
                exitButtonSelector.SetActive(false);
                returnButtonSelector.SetActive(false);
                restartButtonSelector.SetActive(true);
                creditsButtonSelector.SetActive(false);
                controlsButtonSelector.SetActive(false);
                break;
            case GameOptions.Exit:
                exitButtonSelector.SetActive(true);
                returnButtonSelector.SetActive(false);
                restartButtonSelector.SetActive(false);
                creditsButtonSelector.SetActive(false);
                controlsButtonSelector.SetActive(false);
                break;
            case GameOptions.Credits:
                exitButtonSelector.SetActive(false);
                returnButtonSelector.SetActive(false);
                restartButtonSelector.SetActive(false);
                creditsButtonSelector.SetActive(true);
                controlsButtonSelector.SetActive(false);
                break;
            case GameOptions.Controls:
                exitButtonSelector.SetActive(false);
                returnButtonSelector.SetActive(false);
                restartButtonSelector.SetActive(false);
                creditsButtonSelector.SetActive(false);
                controlsButtonSelector.SetActive(true);
                break;
        }
    }

    private void SelectOption()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectorIndex = selectorIndex - 1 >= 0 ? selectorIndex - 1 : gameOptionsArray.Length - 1;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectorIndex = selectorIndex + 1 <= gameOptionsArray.Length - 1 ? selectorIndex + 1 : 0;
        }
    }

    private void SubmitOption(GameOptions option)
    {
        switch (option)
        {
            case GameOptions.Return:
                ToggleMenu();
                break;
            case GameOptions.Restart:
                SceneManager.LoadScene(1, LoadSceneMode.Single);
                ToggleMenu();
                break;
            case GameOptions.Controls:
                ToggleControls();
                break;
            case GameOptions.Credits:
                ToggleCredits();
                break;
            case GameOptions.Exit:
                Application.Quit();
                break;
        }
    }

    private void ToggleMenu()
    {
        Time.timeScale = Mathf.Abs(Time.timeScale - 1);
        isOptionMenuOpen = !isOptionMenuOpen;
        optionMenu.SetActive(isOptionMenuOpen);
        selectorIndex = 0;
    }

    private void ToggleCredits()
    {
        isCreditsMenuOpen = !isCreditsMenuOpen;
        creditsMenu.SetActive(isCreditsMenuOpen);
    }

    private void ToggleControls()
    {
        isControlsMenuOpen = !isControlsMenuOpen;
        controlsMenu.SetActive(isControlsMenuOpen);
    }
}
