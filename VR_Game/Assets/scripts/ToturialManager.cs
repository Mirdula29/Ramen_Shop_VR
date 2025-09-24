using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using System;

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
        arrowInstance=Instantiate(arrowPrefab);
        arrowInstance.SetActive(false);

        objectPicked = new bool[objectSpot.Length];
        ShowMessage("Go near the chopstick");
    }

    // Update is called once per frame
    void Update()
    {
        
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
            // Show arrow to the current object
            arrowInstance.transform.position = objectSpot[steps].position;
            arrowInstance.SetActive(true);

            ShowMessage($"Pick object {steps + 1} and place it on the marked spot.");
        }

    }

    // call this when chopstick is picked
    
    public void OnObjectPicked(int index)
    {
        if (index == steps && !objectPicked[index])
        {
            objectPicked[index] = true;

            // Move arrow to placement spot
            arrowInstance.transform.position = placedSpot[index].position;
        }
    }

    // when chopstick is place
    
    public void OnObjectPlaced(int index)
    {
        if (index == steps)
        {
            arrowInstance.SetActive(false);

            steps++;

            if (steps < objectSpot.Length)
            {
                ShowMessage($"Great! You placed object {index}. Now go pick object {steps + 1}.");
            }
            else
            {
                ShowMessage("Tutorial complete! Well done!");
            }
        }
    }

}
