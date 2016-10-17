using UnityEngine;
using System.Collections;

public class InteractableItem : MonoBehaviour
{
    public Rigidbody rigidbody;

    private bool currentlyInteracting;

    private float velocityFactor = 20000f;
    private Vector3 posDelta;

    private float rotationFactor = 400f;
    private Quaternion rotationDelta;
    private float angle;
    private Vector3 axis;

    private WandController attachedWand;

    private Transform interactionPoint;
    private Vector3 cartInteractionpoint;
    private Vector3 cartInteractionRotation;
    

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        interactionPoint = new GameObject().transform;
        velocityFactor /= rigidbody.mass;
        rotationFactor /= rigidbody.mass;
    }

    // Update is called once per frame
    void Update()
    {
        if (attachedWand && currentlyInteracting)
        {
          
            if (this.GetComponentInParent<CartController>() != null)
            {
               
                    //Vector3 cartInteractionpoint = this.transform.position - attachedWand.transform.position;
                    
                //TO-DO add smoother movement by dragging from interactionPoint
                    this.transform.parent.position = new Vector3(attachedWand.transform.position.x + cartInteractionpoint.x, this.transform.position.y, attachedWand.transform.position.z + cartInteractionpoint.z);
                    Debug.Log("X: " + this.transform.parent.position.x + "Z: " + this.transform.parent.position.z);
                    //Rotates cart
                    float yRotation = attachedWand.transform.eulerAngles.y + cartInteractionRotation.y;
                
                this.transform.parent.eulerAngles = new Vector3(this.transform.parent.eulerAngles.x, yRotation, this.transform.parent.eulerAngles.z);
                    
              



            }
            else {
                posDelta = attachedWand.transform.position - interactionPoint.position;
                this.rigidbody.velocity = posDelta * velocityFactor * Time.fixedDeltaTime;

                rotationDelta = attachedWand.transform.rotation * Quaternion.Inverse(interactionPoint.rotation);
                rotationDelta.ToAngleAxis(out angle, out axis);

                if (angle > 180)
                {
                    angle -= 360;
                }

                this.rigidbody.angularVelocity = (Time.fixedDeltaTime * angle * axis) * rotationFactor;
            }
        }
    }

    public void BeginInteraction(WandController wand)
    {
        attachedWand = wand;
        if (this.GetComponentInParent<CartController>() != null)
        {
            cartInteractionpoint = this.transform.position - attachedWand.transform.position;
            cartInteractionRotation = this.transform.eulerAngles - attachedWand.transform.eulerAngles;
        }
        interactionPoint.position = wand.transform.position;
        interactionPoint.rotation = wand.transform.rotation;
        interactionPoint.SetParent(transform, true);
        currentlyInteracting = true;
    }

    public void EndInteraction(WandController wand)
    {
        if (wand == attachedWand)
        {
            
            attachedWand = null;
            currentlyInteracting = false;
        }
    }

    public bool IsInteracting()
    {
        return currentlyInteracting;
    }

    void OnTriggerStay()
    {
        if (IsInteracting())
        {
            if (attachedWand.getTriggerDown() && this.GetComponent<BoxCollider>().isTrigger == true)
            {
                Debug.Log("jaha");
                attachedWand.grabbedARObj();
                this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                this.gameObject.transform.parent = null;

            }
        }
    }
}
 