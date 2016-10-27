using UnityEngine;
using System.Collections;

public class ProductInstantiation : MonoBehaviour {
    public string name;
    public string product_id;
    public int quantity;
    public double price;
    public bool alreadyInstantiated;
    // Use this for initialization
    void Start () {
        if (!alreadyInstantiated)
        {
            BoxCollider bc = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
            this.GetComponent<BoxCollider>().size = new Vector3(2, 2, 2);

            Rigidbody rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            this.GetComponent<Rigidbody>().mass = 10;

            InteractableItem ii = gameObject.AddComponent(typeof(InteractableItem)) as InteractableItem;

            ProductController pc = gameObject.AddComponent(typeof(ProductController)) as ProductController;
            pc.name = name;
            pc.product_id = product_id;
            pc.quantity = quantity;
            pc.price = price;
            alreadyInstantiated = true;
            Debug.Log("instantiate");
        }
    }

    public void instantiateProduct()
    {
        if (!alreadyInstantiated)
        {
            BoxCollider bc = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
            this.GetComponent<BoxCollider>().size = new Vector3(2, 2, 2);

            Rigidbody rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            this.GetComponent<Rigidbody>().mass = 10;

            InteractableItem ii = gameObject.AddComponent(typeof(InteractableItem)) as InteractableItem;

            ProductController pc = gameObject.AddComponent(typeof(ProductController)) as ProductController;
            pc.name = name;
            pc.product_id = product_id;
            pc.quantity = quantity;
            pc.price = price;
            alreadyInstantiated = true;
            Debug.Log("instantiate");
        }
    }

    public bool isInstantiated()
    {
        return alreadyInstantiated;
    }
	

}
