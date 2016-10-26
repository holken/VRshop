using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Checkout : MonoBehaviour {
	public CartController cart;
	public GameObject player;
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
        //to be url for script sending some sort of custom URL?
        Debug.Log("URL : " + "http://fiskeapp.se/wp-content/plugins/wc_vr/WC-VR2.php/" + urlAddition);
		Application.OpenURL("http://fiskeapp.se/wp-content/plugins/wc_vr/WC-VR2.php/" + urlAddition);
	}

	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0) && Vector3.Distance (transform.position, player.transform.position) <= 3) {

			makePurchase ();
		}
	}

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("entered trigger");
        if (collider.gameObject.GetComponent<WandController>() != null)
            makePurchase();

    }
}
