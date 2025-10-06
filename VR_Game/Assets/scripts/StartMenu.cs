using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class StartMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject startMenuPanel;
    public Button playButton;

    [Header("Player Movement Controller")]
    public ContinuousMoveProvider moveProvider; // XR locomotion script

    public ToturialManager toturialManager;
    void Start()
    {
        Debug.Log("StartMenu script initialized.");

        // Show only start menu at the beginning
        if (startMenuPanel != null)
            startMenuPanel.SetActive(true);

        // Make sure tutorial UI is hidden initially
        if (toturialManager != null && toturialManager.messagepanel != null)
            toturialManager.messagepanel.SetActive(false);

        // Hook up the Play button
        playButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        // Hide start menu
        if (startMenuPanel != null)
            startMenuPanel.SetActive(false);

        // Show the first tutorial message
        if (toturialManager != null)
        {
            toturialManager.ShowMessage("Follow the Arrow to know the place of the objects. Press 'OK' button to close ui and start the game");
        }
    }
}
