using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Checkout : MonoBehaviour {
	public CartController cart;
	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}

	private void makePurchase(){
		
		Dictionary<string, Products> checkoutCart = cart.getCart();
		string urlAddition = "?";
		int count = 1;
		foreach (KeyValuePair<string, Products> item in checkoutCart) {
			int quantity = item.Value.getQuantity ();;
			urlAddition += "product" + count + "=" + item.Key + "&productQ" + count + "=" + quantity + "&";
			count++;
		}
		urlAddition = urlAddition.Remove (urlAddition.Length - 1);
        //to be url for script sending some sort of custom URL?
        Debug.Log("URL : " + "https://scandinavianhemp.se/" + urlAddition);
		Application.OpenURL("https://scandinavianhemp.se/" + urlAddition);
	}

	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0) && Vector3.Distance (transform.position, player.transform.position) <= 3) {

			makePurchase ();
		}
	}

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("entered trigger");
        makePurchase();

    }
}
