using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Checkout : MonoBehaviour {
	public CartController cart;
	public GameObject player;
    public string url;
	// Use this for initialization
	void Update () {
        this.transform.Rotate(0*Time.deltaTime, 0* Time.deltaTime, 40 * Time.deltaTime);
	}

    void Start()
    {
        
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

        Debug.Log("URL : " + url + urlAddition);
		Application.OpenURL(url + urlAddition);
	}

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("entered trigger");
        if (collider.gameObject.GetComponent<WandController>() != null)
            makePurchase();

    }
}
