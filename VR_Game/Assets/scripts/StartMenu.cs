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

    void Start()
    {
        // Lock player movement at the beginning
        if (moveProvider != null)
            moveProvider.enabled = false;

        // Show start menu
        if (startMenuPanel != null)
            startMenuPanel.SetActive(true);

        // Hook button
        playButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        // Hide menu
        if (startMenuPanel != null)
            startMenuPanel.SetActive(false);

        // Enable movement
        if (moveProvider != null)
            moveProvider.enabled = true;
    }
}
