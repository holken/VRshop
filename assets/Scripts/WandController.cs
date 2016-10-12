using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WandController : MonoBehaviour {
	
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;


	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input ((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	HashSet<InteractableItem> objectsHoveringOver = new HashSet<InteractableItem>();
	private InteractableItem closestItem;
	private InteractableItem interactingItem;


	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (controller == null) {
			Debug.Log ("Controller not initialized");
			return;
		}



       
        if (controller.GetPressDown (gripButton)) {
            Debug.Log("inside getpressed");
            float minDistance = float.MaxValue;
			float distance;
			foreach (InteractableItem item in objectsHoveringOver) {
                Debug.Log("inside loop");
                if (item == null)
                {
                    Debug.Log("item is null");
                    objectsHoveringOver.Remove(item);
                }
                else {
                    distance = (item.transform.position - transform.position).sqrMagnitude;
                    Debug.Log("Inside where you set closestItem");
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestItem = item;
                    }
                }
			}
			interactingItem = closestItem;

			if (interactingItem) {
				if (interactingItem.IsInteracting ()) {
                    Debug.Log("Is interacting");
					interactingItem.EndInteraction (this);
				}
                Debug.Log("Right before begin interaction");
                interactingItem.BeginInteraction (this);
			}


		}
		if (controller.GetPressUp(gripButton) && interactingItem != null) {

			interactingItem.EndInteraction (this);

		}
	}

	private void OnTriggerEnter(Collider collider){
		InteractableItem collidedItem = collider.GetComponent<InteractableItem> ();
        Debug.Log("trigger enter");
        if (collidedItem) {
            Debug.Log("adds to things that can be collided");
            objectsHoveringOver.Add (collidedItem);
		}
        
	}	

	private void OnTriggerExit(Collider collider){
		InteractableItem collidedItem = collider.GetComponent<InteractableItem> ();
		if (collidedItem) {
            Debug.Log("exited");
            objectsHoveringOver.Remove(collidedItem);
		}
	}
}
