using UnityEngine;
using System.Collections;

public class ProductSpawning : MonoBehaviour {
    public GameObject product;
    private GameObject currentProduct;
    public WandController wand;
    public GameObject player;
    private GameObject priceText;
    bool playerFacing = false;
	// Use this for initialization
	void Start () {
        currentProduct = Instantiate(product);
        currentProduct.transform.position = this.transform.position;
        if (product.GetComponent<ProductInstantiation>() != null && !product.GetComponent<ProductInstantiation>().isInstantiated())
        {
            product.GetComponent<ProductInstantiation>().instantiateProduct();
        }
        currentProduct.GetComponent<ProductController>().setWand(this.wand);
        player = GameObject.Find("Camera (eye)");
    }
	
	// Update is called once per frame
	void Update () {
	    if (currentProduct == null)
        {
            currentProduct = Instantiate(product);
            
            currentProduct.transform.position = this.transform.position;
            
        }
        if (product.GetComponent<ProductInstantiation>() != null && !product.GetComponent<ProductInstantiation>().isInstantiated())
        {
            product.GetComponent<ProductInstantiation>().instantiateProduct();
        }
        currentProduct.GetComponent<ProductController>().setWand(this.wand);
        

        Vector3 cubeDir = transform.position - player.transform.position;
        float angle = Vector3.Angle(cubeDir, player.transform.forward);

        if (angle < 30 && Vector3.Distance(transform.position, player.transform.position) <= 1 && playerFacing != true)
        {
            Debug.Log("I'm facing the player");
            priceText = (GameObject)Instantiate(Resources.Load("PriceText"));
            
            
            
            priceText.GetComponent<TextMesh>().text = "price: " + currentProduct.GetComponent<ProductController>().getPrice() + "\n product name: " + currentProduct.GetComponent<ProductController>().getProductName();
            playerFacing = true;
        }

        if ((angle > 30 || Vector3.Distance(transform.position, player.transform.position) > 1) && playerFacing == true)
        {
            Debug.Log("I'm facing the player not anymore");
            Destroy(priceText);
            playerFacing = false;
        }
        if (playerFacing == true)
        {
            priceText.transform.LookAt(player.transform);
            cubeDir = player.transform.position - transform.position;
            float vectorSum = Mathf.Abs(cubeDir.x + cubeDir.y + cubeDir.z);
            float fatness = product.transform.localScale.z;
            float vectorScaled = fatness / vectorSum;
            
            priceText.transform.position = new Vector3(this.transform.position.x + cubeDir.normalized.x * fatness, this.transform.position.y + cubeDir.normalized.y* fatness, this.transform.position.z + cubeDir.normalized.z* fatness); 
            priceText.transform.Rotate(0f, 180f, 0f);


            //ALT 1 fking bugging
            //priceText.transform.position = new Vector3(this.transform.position.x + priceText.transform.forward.x, this.transform.position.y + priceText.transform.forward.y, this.transform.position.z + priceText.transform.forward.z);


            //ALT 2 MUST CHECK THE OBJECTS ROTATION AND THEN IF FOR THIS TO WORK
            //float fatness = product.transform.lossyScale.z;
            //priceText.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - fatness);


        }

        /*
        RaycastHit hit;
        Vector3 cameraCenter = player.GetComponent<Camera>().WorldToScreenPoint(new Vector3(Screen.width / 2, Screen.height / 2, player.GetComponent<Camera>().nearClipPlane));
        if (Physics.Raycast(cameraCenter, player.transform.forward, out hit, 300))
        {
            Debug.Log("raycast is working");
            GameObject objHit = hit.transform.gameObject;
            if (objHit == this)
            {
                Debug.Log("objHit is working");
                priceText = (GameObject)Instantiate(Resources.Load("PriceText"));
                priceText.transform.position = this.transform.position;
                priceText.GetComponent<TextMesh>().text = "price: " + product.GetComponent<ProductController>().getPrice();
            }
        } else
        {
            if (priceText != null)
            {
                Destroy(priceText);
            }
        }
        */

    }



}
