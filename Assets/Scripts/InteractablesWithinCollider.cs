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

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getFirstInteractable() {
        return (GameObject)interactableList[0];
    }


    private void OnTriggerEnter(Collider other) {
        GameObject collidedObject = other.gameObject;
        if (collidedObject.GetComponent<Interactable>() != null) {
            interactableList.Add(collidedObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        GameObject exitObject = other.gameObject;
        interactableList.Remove(exitObject);
    }

    private List<GameObject> getInteractableList() {
        return interactableList;
    }
}
