using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Threading;



public class ProductController : MonoBehaviour {

	public string public_sku;
	public string product_id;
	public int quantity;
	public GameObject player;
	public CartController cart;
	public string url;

	// Use this for initialization
	void Start () {
		Debug.Log ("yo");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0) && Vector3.Distance (transform.position, player.transform.position) <= 3) {

			cart.addToCart (product_id, quantity);
			int fish = cart.getItem (product_id);
			Debug.Log ("Number of items in cart: " + fish);
			

			//obsolete
			/*Debug.Log ("productID: " + product_id +" has been pressed");
			//cart.addToCart (public_sku, product_id, quantity);
			string newURL = url + "?add-to-cart=%" +product_id + "%";
			//WWW request = new WWW("http://fiskeapp.se/produkt-kategori/orter-bar-och-froer/?add-to-cart=472");
			WWWForm form = new WWWForm ();
			form.AddField ("quantity", 1);
			form.AddField ("add-to-cart", 472);
			//StartCoroutine (WaitForRequest (www));
			//addToCart ();*/
		} else if (Input.GetMouseButtonDown (0) && Vector3.Distance (transform.position, player.transform.position) <= 3) {
			cart.removeFromCart (product_id);
		}
	}

//	IEnumerator addToCart(){
//		UnityWebRequest www = UnityWebRequest.Get ("http://fiskeapp.se/produkt-kategori/orter-bar-och-froer/?add-to-cart=472");
//
//		yield return www.Send();
//		Application.ExternalCall ("add-to-cart", 472);
//
//		if(www.isError) {
//			Debug.Log(www.error);
//		}
//		else {
//			// Show results as text
//			Debug.Log(www.downloadHandler.text);
//
//			// Or retrieve results as binary data
//			byte[] results = www.downloadHandler.data;
//		}
//
//	}


	IEnumerator WaitForRequest(WWW www){
		yield return www;

		if (www.error == null){
			Debug.Log("WWW ok!: " + www.text);

		} else {
			Debug.Log("WWW Error: " + www.error);
		}
	}
}
	