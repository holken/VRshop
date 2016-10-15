using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ARController : MonoBehaviour {
    public CartController cartController;
    private bool active;
    private Products[] listOfItems;
    private int indexAll;
    private int activeObjects = 0;
    //private GameObject[] objectsOnDisplay;
    private ARSpot[] objectsOnDisplay;
    // Use this for initialization
    void Start () {
        indexAll = 0;
        activeObjects = 0;
        objectsOnDisplay = new ARSpot[3];
    }

    private bool checkActiveIndex()
    {
        return listOfItems.Length > indexAll;
    }

    private void resetActiveIndex()
    {
        if (checkActiveIndex())
        {
            indexAll = 0;
        }
    }


	// Update is called once per frame
	void Update () {
        
        if (active)
        {
            //this is to check for if someone have thrown the box away, if then, remove quantity and if it have reached 0 quantity, replace it with a new one
            for (int i = 0; i < objectsOnDisplay.Length; i++)
            {

                //checks if the position of object is far enough away
                if (objectsOnDisplay[i] != null)
                {
                    if (Vector3.Distance(objectsOnDisplay[i].transform.position, this.transform.position) >= 5)
                    {

                        //checks if we have removed all objects
                        if (cartController.removeQuantityFromCart(objectsOnDisplay[i].getProd().getID(), -1))
                        {
                            //checks if there is any higher active objects in the indexActive list, otherwise we have to reset it
                            if (checkActiveIndex())
                            {
                                resetActiveIndex();

                            }
                            Destroy(objectsOnDisplay[i].getObj());
                            objectsOnDisplay[i] = null;
                            activeObjects--;

                        }
                    }
                }
            }

            while (activeObjects < 3 && listOfItems.Length > 3)
            {
                
                ARSpot spot = new ARSpot();
                resetActiveIndex();
                GameObject temp = Instantiate(listOfItems[indexAll].getObj());
                for (int i = 0; i <= 2; i++)
                {
                    if (objectsOnDisplay[i] == null)
                    {
                        temp.GetComponent<Rigidbody>().isKinematic = true;
                        //REMEMBER: add a check for what kind of collider in the future, or always use mesh?
                        temp.GetComponent<BoxCollider>().isTrigger = true;
                        temp.transform.position = this.transform.position + new Vector3(Mathf.Cos(Mathf.PI / 4), 0, Mathf.Sin(Mathf.PI / 4));
                        spot.setObj(temp);
                        spot.setProd(listOfItems[indexAll]);
                        objectsOnDisplay[i] = spot;
                    }
                }
                /*if (indexActive >= 3)
                {
                    indexActive = 0;
                }*/

            }
        }
	
	}

    private void startAR()
    {

        Dictionary<string, Products> cart = cartController.getCart();
        listOfItems = new Products[cart.Count];
        int count = 0;
        
        foreach (KeyValuePair<string, Products> item in cart)
        {
            listOfItems[count] = item.Value;
            Debug.Log("There was products: " + listOfItems[count].getID());
            count++;
            
        }
        Debug.Log("active items: " + indexAll);
        Debug.Log("There was products: " + listOfItems[indexAll].getID());
        if (listOfItems[indexAll] != null)
        {
            Debug.Log("active item " + listOfItems[indexAll].getID());
        }
        while (activeObjects < 3 && listOfItems[indexAll].getID() != null)
        {
            Debug.Log("We are inside the initial add loop for AR");
            ARSpot spot = new ARSpot();
            GameObject temp = Instantiate(listOfItems[indexAll].getObj());
            int count2 = 0;
            temp.GetComponent<Rigidbody>().isKinematic = true;
            //REMEMBER: add a check for what kind of collider in the future, or always use mesh?
            temp.GetComponent<BoxCollider>().isTrigger = true;
            if (count2 == 0 )
            {
                temp.transform.position = this.transform.position + new Vector3(Mathf.Cos(Mathf.PI), 0, Mathf.Sin(Mathf.PI));
            } else if (count2 == 1)
            {
                temp.transform.position = this.transform.position + new Vector3(Mathf.Cos(Mathf.PI/2), 0, Mathf.Sin(Mathf.PI/2));
            } else
            {
                temp.transform.position = this.transform.position + new Vector3(Mathf.Cos(Mathf.PI / 4), 0, Mathf.Sin(Mathf.PI / 4));
            }
            spot.setObj(temp);
            spot.setProd(listOfItems[indexAll]);
            objectsOnDisplay[count2] = spot;
            activeObjects++;
            indexAll++;

        }
    }

    public void toggleAR()
    {
        Debug.Log("inside ToggleAR");
        if (active)
        {
            
            active = false;
        } else
        {
            startAR();
            active = true;
            Debug.Log("active: " + active);
        }
    }

    public void makeThrowable(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.GetComponent<BoxCollider>().isTrigger = true;
    }
}
