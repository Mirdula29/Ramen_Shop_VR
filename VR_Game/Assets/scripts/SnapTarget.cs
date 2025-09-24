using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapTarget : MonoBehaviour
{
    [Header("Allowed Snap Traget")]
    public string[] snapTargets;

    [Header("snapping settings")]
    public float snapSpeed = 5f;
    public bool freezeSnap = false;

    [Header("toturial manager")]
    public int tutorialIndex = 0;
    [SerializeField]private ToturialManager tutorialManager;

    private XRGrabInteractable interactable;
    private Transform snapPoint;
    private Coroutine snapRoutine;
    private Rigidbody rb;

    private enum SnapState
    {
        Ideal, //not snapping
        Snapping, // while snapping
        Rest, // fininshed snapping 
    }

    private SnapState snapState = SnapState.Ideal;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();

        rb = GetComponent<Rigidbody>();

        interactable.selectExited.AddListener(OnRealese);
        interactable.selectEntered.AddListener(OnGrab);

        if (tutorialManager == null)
        {
            tutorialManager = Object.FindFirstObjectByType<ToturialManager>();

        }
        

        Debug.Log(gameObject.name + " started in state" + snapState);
        
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Entered: " + other.gameObject.tag + " tag: " + other.gameObject.tag);
        if (snapTargets.Contains(other.gameObject.tag))
        {
            snapPoint = other.transform;
            Debug.Log(gameObject.name + "found snap point " + snapPoint.name);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (snapPoint != null && snapTargets.Contains(other.tag) && snapPoint == other.transform)
        {
            Debug.Log(gameObject.name + " left snap point: " + snapPoint.name);
            snapPoint = null;
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = false;
        }
        snapState = SnapState.Ideal; // reset while grabbing
        Debug.Log(gameObject.name + " grabbed state: " + snapState);

        if (tutorialManager != null)
        {
            Debug.Log($"SnapTarget: notifying tutorial OnObjectPicked({tutorialIndex})");
            tutorialManager.OnObjectPicked(tutorialIndex);
        }
    }

    private void OnRealese(SelectExitEventArgs args)
    {
        Debug.Log("Released object");
        if (snapPoint != null)
        {
            Debug.Log(gameObject.name + " released near snap point  snapping!");
            if (snapRoutine != null) StopCoroutine(snapRoutine);
            snapRoutine = StartCoroutine(SmoothSnap());
        }
        else
        {
            Debug.Log(gameObject.name + " released but no snap point found staying Idle");
        }
    }

    

    private IEnumerator SmoothSnap()
    {
        if (snapPoint == null)
        {
            Debug.LogWarning("snap poi is null befor snapping");
            yield break;
        }
        snapState = SnapState.Snapping;
        Debug.Log(gameObject.name + " state changed  " + snapState);

        // disable grabbing while snapping 
        if (interactable != null)
        {
            interactable.enabled = false;
        }

        if (rb != null)
        {
            rb.isKinematic = true;
        }

        while (Vector3.Distance(transform.position, snapPoint.position) > 0.2f)
        {
            Debug.Log("Snapping... Distance: " + Vector3.Distance(transform.position, snapPoint.position));
            transform.position = Vector3.MoveTowards(transform.position, snapPoint.position, Time.deltaTime * snapSpeed);
            transform.rotation = Quaternion.RotateTowards (transform.rotation, snapPoint.rotation, Time.deltaTime * snapSpeed);
            yield return null;
        }
        if(snapPoint == null)
        {
            Debug.LogWarning("snappoint lost during snapping");
            yield break;
        }

        transform.position = snapPoint.position;
        transform.rotation = snapPoint.rotation;

        if (!freezeSnap && rb != null)
        {
            rb.isKinematic = false;
        }
        snapState = SnapState.Rest;
        Debug.Log(gameObject.name + " state changed" + snapState);

        // notify tutorial manager that this object has been placed
        if (tutorialManager != null)
        {
            Debug.Log($"SnapTarget: notifying tutorial OnObjectPlaced({tutorialIndex})");
            tutorialManager.OnObjectPlaced(tutorialIndex);
        }

    }
}
