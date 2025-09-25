using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class ToturialManager : MonoBehaviour
{
    [Header("Arrow")]

    public GameObject arrowPrefab;

    [Header("Object originally placed")]
    public Transform[] objectSpot;

    [Header("Object to be snaped")]
    public Transform[] placedSpot;


    [Header("UI Elements")]
    public Transform UIShift;
    public GameObject messagepanel;
    public TMP_Text messageText;
    public Button okButton;

    private int steps = 0;
    private bool[] objectPicked;
    private GameObject arrowInstance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arrowInstance = Instantiate(arrowPrefab);
        arrowInstance.SetActive(false);

        objectPicked = new bool[Mathf.Max(1, objectSpot.Length)];
        ShowMessage("Press 'OK' Button to start the game. Follow the Arrow to know the place of the objects");
    }
   
    void ShowMessage(string text)
    {
        messagepanel.SetActive(true);
        messageText.text = text;
        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(OnOkPressed);
    }
    void OnOkPressed()
    {
        messagepanel.SetActive(false);

        if (steps < objectSpot.Length)
        {
            // Use the arrow component's SetBasePosition method (keeps floating behavior correct)
            var floatScript = arrowInstance.GetComponent<Arrow>();
            if (floatScript != null)
            {
                floatScript.SetBasePosition(objectSpot[steps].position);
            }
            else
            {
                // fallback
                arrowInstance.transform.position = objectSpot[steps].position;
            }
            arrowInstance.SetActive(true);
        }

    }

    // call this when chopstick is picked

    public void OnObjectPicked(int index)
    {
        Debug.Log($"Tutorial: OnObjectPicked({index}), current step={steps}");
        if (index == steps && !objectPicked[index])
        {
            objectPicked[index] = true;

            // hide arrow at object immediately (player grabbed it)
            if (arrowInstance != null) arrowInstance.SetActive(false);

            // then move arrow to placement spot and show it after a short delay
            StartCoroutine(RepositionArrowToPlace(index));
        }
    }

    private IEnumerator RepositionArrowToPlace(int index)
    {
        // small delay so the arrow disappears while the player's hand holds object
        yield return new WaitForSeconds(0.15f);

        if (index < placedSpot.Length && arrowInstance != null)
        {
            arrowInstance.transform.position = placedSpot[index].position;
            arrowInstance.SetActive(true);
            Debug.Log($"Tutorial: arrow moved to placedSpot[{index}] and shown");
        }
    }

    // when chopstick is place

    public void OnObjectPlaced(int index)
    {
        Debug.Log($"Tutorial: OnObjectPlaced({index}), current step={steps}");
        if (index == steps)
        {
            // hide arrow when placed
            if (arrowInstance != null) arrowInstance.SetActive(false);

            steps++;

            if (steps < objectSpot.Length)
            {
                // show next instruction and wait for OK to show the arrow
                ShowMessage($"Great! You placed object {index + 1}. Press OK to continue to the next item.");
            }
            else
            {
                ShowMessage("Grate!You have placed all the object correctly, Well done!");
            }
        }
    }

}
