using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CartController : MonoBehaviour {
	public string url;
	Dictionary<string, int> cart;	
	// Use this for initialization
	void Start () {
		cart = new Dictionary<string, int>();
	}
	
	public void addToCart(string productId, int quantity){

		if (!cart.ContainsKey(productId)) {
			cart.Add (productId, quantity);
		} else {
			int value = cart [productId];
			cart.Remove (productId);
			cart.Add (productId, value + quantity);

		}
	}

	public void removeFromCart(string productId){
		cart.Remove (productId);
	}

	public int getItem(string productId){
		return cart [productId];
	}

	public Dictionary<string, int> getCart(){
		return cart;
	}

}
