using UnityEngine;
using UnityEngine.UI;

public class MenuToggle : MonoBehaviour
{
    public GameObject menuParent; // Parent object to activate first
    public Canvas mainMenu;
    public Canvas settingsMenu;
    public Canvas creditsMenu;
    public Button closeButton; // Button to close the active menu

    private Canvas activeMenu = null; // Track the currently active menu

    private void Start()
    {
        // Ensure everything is inactive at the start
        menuParent.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);

        // Add listener to closeButton to close the menu when clicked
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseActiveMenu);
        }
    }

    private void Update()
    {
        // Check if Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (activeMenu != null)
            {
                CloseActiveMenu();
            }
            else
            {
                OpenMainMenu();
            }
        }
    }

    // Open main menu and pause the game
    public void OpenMainMenu()
    {
        if (activeMenu == null)
        {
            SetActiveMenu(mainMenu);
            Time.timeScale = 0f;
        }
    }

    // Open settings menu
    public void OpenSettingsMenu()
    {
        SetActiveMenu(settingsMenu);
    }

    // Open credits menu
    public void OpenCreditsMenu()
    {
        SetActiveMenu(creditsMenu);
    }

    // Close the currently active menu, deactivate the parent, and resume the game
    public void CloseActiveMenu()
    {
        if (activeMenu != null)
        {
            activeMenu.gameObject.SetActive(false);
            activeMenu = null;
            menuParent.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    // Helper method to set and open the active menu
    private void SetActiveMenu(Canvas menu)
    {
        // Activate the menu parent
        menuParent.SetActive(true);

        // Deactivate the current active menu, if any
        if (activeMenu != null)
        {
            activeMenu.gameObject.SetActive(false);
        }

        // Set and activate the new menu
        activeMenu = menu;
        activeMenu.gameObject.SetActive(true);
    }
}
