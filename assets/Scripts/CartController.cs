using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CartController : MonoBehaviour {
	
	Dictionary<string, Products> cart;
    public GameObject player;
    //public GameObject thisCart;
	// Use this for initialization
	void Start () {
		cart = new Dictionary<string, Products>();
        //this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
    }
    
    //this is for if we want the cart to be fastklistrad in front (if you want this then remove the InteractionItem script), otherwise comment out the update (take back the script)
    void Update()
    {
        Vector3 difference = new Vector3(-0.6f, 0.0f, 0f);

        this.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z) + difference;
        
        //TO-DO fix rotation and probably distance at the same time
        //this.transform.rotation = player.transform.rotation;
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

    public void OnTriggerStay(Collider collider)
    {
        if (collider != null && collider.gameObject.GetComponent<ProductController>() != null)
        {
            if (collider.GetComponent<InteractableItem>().IsInteracting() == false)
            {
                ProductController product = collider.GetComponent<ProductController>();
                addToCart(product.getProductID(), product.getQuantity(), product.getPrice());
                Destroy(collider.gameObject);
            }
        }
    }
}
