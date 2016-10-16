using UnityEngine;
using System.Collections;

public class ProductSpawning : MonoBehaviour {
    public GameObject product;
    private GameObject currentProduct;
    public WandController wand;
	// Use this for initialization
	void Start () {
        currentProduct = Instantiate(product);
        currentProduct.transform.position = this.transform.position;
        currentProduct.GetComponent<ProductController>().wand = this.wand;
    }
	
	// Update is called once per frame
	void Update () {
	    if (currentProduct == null)
        {
            currentProduct = Instantiate(product);
            currentProduct.transform.position = this.transform.position;
            currentProduct.GetComponent<ProductController>().setWand(this.wand);
        }
	}
}
