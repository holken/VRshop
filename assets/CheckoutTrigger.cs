using UnityEngine;
using System.Collections;

public class CheckoutTrigger : MonoBehaviour {
    public GameObject shop;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider collider)
    {
        Debug.Log("entered");
        if (collider.gameObject.GetComponent<InteractableItem>() == null )
        {
            shop.SetActive(true);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        shop.SetActive(false);
    }
}
