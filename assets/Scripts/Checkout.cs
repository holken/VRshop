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
		
		Dictionary<string, int> checkoutCart = cart.getCart();
		string urlAddition = "?";
		int count = 1;
		foreach (KeyValuePair<string, int> item in checkoutCart) {
			urlAddition += "product" + count + "=" + item.Key + "&";
			count++;
		}
		urlAddition = urlAddition.Remove (urlAddition.Length - 1);
		//to be url for script sending some sort of custom URL?
		Application.OpenURL("https://scandinavianhemp.se/" + urlAddition);
	}

	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0) && Vector3.Distance (transform.position, player.transform.position) <= 3) {

			makePurchase ();
		}
	}
}
