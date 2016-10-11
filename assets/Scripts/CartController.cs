using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CartController : MonoBehaviour {
	public string url;
	Dictionary<string, Products> cart;	
	// Use this for initialization
	void Start () {
		cart = new Dictionary<string, Products>();
	}
	
	public void addToCart(string productId, int quantity, double price){
		
		if (!cart.ContainsKey(productId)) {
			Products temp = new Products ();
			temp.setProductId(productId);
			temp.setQuantity(quantity);
			temp.setPrice(price);
			cart.Add (productId, temp);
		} else {
			cart[productId].setQuantity(quantity);
			cart[productId].setPrice(price);


		}

		Debug.Log("the price: " + calculatePrice ());
	}

	public void removeFromCart(string productId){
		cart.Remove (productId);
	}

	public int getQuantity(string productId){
		return cart [productId].getQuantity ();
	}

	public Dictionary<string, Products> getCart(){
		return cart;
	}

	public double calculatePrice(){
		double cost = 0;
		foreach (KeyValuePair<string, Products> item in cart) {
			cost += item.Value.getPrice ();
		}
		return cost;
	}
}
