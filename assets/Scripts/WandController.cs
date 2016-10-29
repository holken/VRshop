using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WandController : MonoBehaviour {
	
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId menuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    //private Valve.VR.EVRButtonId thumbPlate = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;


	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input ((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	HashSet<InteractableItem> objectsHoveringOver = new HashSet<InteractableItem>();
	private InteractableItem closestItem;
	private InteractableItem interactingItem;
    private bool triggerDown;
    private bool ARobjGrabbed;

    public ARController AR;
    public CartController cart;


	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
        ARobjGrabbed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (controller == null) {
			Debug.Log ("Controller not initialized");
			return;
		}


       if (controller.GetPressDown(triggerButton)){
            triggerDown = true;
            
        }
        if (controller.GetPressUp(triggerButton))
        {
            triggerDown = false;
            
        }
        if (controller.GetPressDown(menuButton))
        {
            
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
                    
					interactingItem.EndInteraction (this);
				}
                
                interactingItem.BeginInteraction (this);
			}


		}

        if (controller.GetPress(triggerButton) && interactingItem != null)
        {
            if (controller.GetPressDown(gripButton) && interactingItem.GetComponent<ProductController>() != null)
            {

                interactingItem.EndInteraction(this);
                objectsHoveringOver.Remove(interactingItem);
                cart.fastAddToCart(interactingItem.GetComponent<ProductController>());
                interactingItem = null;
            }
        }

        if (controller.GetPressUp(triggerButton) && interactingItem != null) {

            if (ARobjGrabbed)
            {
                
                interactingItem.GetComponent<BoxCollider>().isTrigger = true;
                ARobjGrabbed = false;
            }
			interactingItem.EndInteraction (this);
            objectsHoveringOver.Remove(interactingItem);
            interactingItem = null;



        }


        if (controller.GetPressDown(gripButton) )
        {
            if (AR.getActiveStatus())
            {
                
                AR.setWand(this);
                AR.gripButtonPressed(true);
            }
        }

        if (controller.GetPressUp(gripButton))
        {
            AR.gripButtonPressed(false);
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
