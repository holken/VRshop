﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WandController : MonoBehaviour {
	
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId menuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;


	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input ((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	HashSet<InteractableItem> objectsHoveringOver = new HashSet<InteractableItem>();
	private InteractableItem closestItem;
	private InteractableItem interactingItem;
    private bool triggerDown;
    private bool ARobjGrabbed;

    public ARController AR;


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


       if (controller.GetPressDown(triggerButton)){
            triggerDown = true;
            Debug.Log("Trigger down is: " + triggerDown);
        }
        if (controller.GetPressUp(triggerButton))
        {
            triggerDown = false;
            Debug.Log("Trigger down is: " + triggerDown);
        }
        if (controller.GetPressDown(menuButton))
        {
            Debug.Log("menu button pressed");
            AR.toggleAR();
        }
        if (controller.GetPressDown (triggerButton)) {
            
            float minDistance = float.MaxValue;
			float distance;
            
			foreach (InteractableItem item in objectsHoveringOver) {
                
                if (item == null)
                {
                    
                    objectsHoveringOver.Remove(item);
                }
                else {
                    distance = (item.transform.position - transform.position).sqrMagnitude;
                    
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
                
                interactingItem.BeginInteraction (this);
			}


		}
		if (controller.GetPressUp(triggerButton) && interactingItem != null) {

			interactingItem.EndInteraction (this);
            ARobjGrabbed = false;

		}
	}

    public void grabbedARObj()
    {
        ARobjGrabbed = true;
    }
    
    public bool getTriggerDown()
    {
        return triggerDown;
    }

	private void OnTriggerEnter(Collider collider){
		InteractableItem collidedItem = collider.GetComponent<InteractableItem> ();
        
        if (collidedItem) {
            
            objectsHoveringOver.Add (collidedItem);
		}
        
	}	

	private void OnTriggerExit(Collider collider){
		InteractableItem collidedItem = collider.GetComponent<InteractableItem> ();
		if (collidedItem) {
            
            objectsHoveringOver.Remove(collidedItem);
		}
	}
}
