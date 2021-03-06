﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class CartController : MonoBehaviour {
	
	Dictionary<string, Products> cart;
    public GameObject player;
    public GameObject priceText;
    public GameObject utilities;

	void Start () {
		cart = new Dictionary<string, Products>();
        priceText.GetComponent<TextMesh>().text = "0";
    }

    public void addToCart(string productId, int quantity, double price, GameObject obj){

        Debug.Log("adding to cart: " + quantity);
		if (!cart.ContainsKey(productId)) {
			Products temp = new Products ();
			temp.setProductId(productId);
			temp.setQuantity(quantity);
			temp.setPrice(price);
            temp.setObj(obj);
            temp.setName(utilities.GetComponent<ItemIDtoName>().getName(productId));
			cart.Add (productId, temp);
            
		} else {
			cart[productId].setQuantity(quantity);
			cart[productId].setPrice(price);


		}
        this.setPriceText();
        Debug.Log("the price: " + calculatePrice ());
	}

    public void setPriceText()
    {

        double tempPrice = calculatePrice();
        priceText.GetComponent<TextMesh>().text = tempPrice.ToString();
    }

	public void removeFromCart(string productId){
		cart.Remove (productId);
        setPriceText();

    }

    public bool removeQuantityFromCart(string productId, int quant)
    {
        if (cart.ContainsKey(productId))
        {
            if (cart[productId].getQuantity() - 1 <= 0)
            {
                cart[productId].setQuantity(-quant);
                removeFromCart(productId);
                return true;
            }
            cart[productId].setQuantity(-quant);
            setPriceText();
        }
        return false;
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

    public void OnTriggerStay(Collider collider)
    {
        if (collider != null && collider.gameObject.GetComponent<ProductController>() != null)
        {
            if (collider.GetComponent<InteractableItem>().IsInteracting() == false && collider.GetComponent<Collider>().isTrigger == false)
            {
                ProductController product = collider.GetComponent<ProductController>();
                addToCart(product.getProductID(), product.getQuantity(), product.getPrice(), product.gameObject);
                Destroy(collider.gameObject);
            }
        }
    }

    public void fastAddToCart(ProductController product)
    {
        if (product.gameObject.GetComponent<ProductController>() != null)
        {
            if (product.GetComponent<Collider>().isTrigger == false)
            {
                addToCart(product.getProductID(), product.getQuantity(), product.getPrice(), product.gameObject);
                Destroy(product.gameObject);
            }
        }
    }
}
