using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Creates a list of all interactable gameobjects within the collider.
 */
public class InteractablesWithinCollider : MonoBehaviour
{
    public List<GameObject> interactableList;
    private Collider interactableAreaCollider;

   // private Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        interactableList = new List<GameObject>();
        interactableAreaCollider = GetComponent<Collider>();
    }

    public GameObject getFirstInteractable() {
        pruneList();
        return (GameObject)interactableList[0];
    }

    /**
     * Adds interactable objects that enter the trigger
     */
    private void OnTriggerEnter(Collider other) {
        GameObject collidedObject = other.gameObject;
        if (collidedObject.GetComponent<Interactable>() != null) {
            interactableList.Add(collidedObject);
        }
    }

    /*
     * Removes interactable objects that exit the trigger
     */
    private void OnTriggerExit(Collider other) {
        GameObject exitObject = other.gameObject;
        interactableList.Remove(exitObject);
    }

    private List<GameObject> getInteractableList() {
        return interactableList;
    }

    /**
     * Removes any objects that haven't been pruned by ontriggerexit.
     */
    private void pruneList() {
        foreach (var potentialNullObject in interactableList)
        {
            if (potentialNullObject == null || !potentialNullObject.GetComponent<Interactable>().pickUpable)
            {
                interactableList.Remove(potentialNullObject);
            }
        }
    }
}
