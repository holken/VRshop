using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ARController : MonoBehaviour {
    public CartController cartController;
    private bool active;
    //private Products[] listOfItems;
    private List<Products> listOfItems;
    private int indexAll;
    private int activeObjects;
    private int numbersOfItems;
    public WandController wand;
    
    //private ARSpot[] objectsOnDisplay;
    private List<ARSpot> objectsOnDisplay;

    public float objDistance = 0.8f;
    

    // Use this for initialization
    void Start () {
        indexAll = 0;
        activeObjects = 0;
        //objectsOnDisplay = new ARSpot[3];
        objectsOnDisplay = new List<ARSpot>();
    }

    private bool checkActiveIndex()
    {
        return listOfItems.Count < indexAll;
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
            //int index = 0;
            foreach (ARSpot item in objectsOnDisplay)
            {
                //Debug.Log("index: " + index);
                
                if (!object.ReferenceEquals(null, item))
                {
                    //checks if the position of object is far enough away
                    if (Vector3.Distance(item.getObj().transform.position, this.transform.position) >= 2.0f)
                    {

                        //checks if we have removed all objects
                        if (cartController.removeQuantityFromCart(item.getProd().getID(), -1))
                        {
                            //checks if there is any higher active objects in the indexActive list, otherwise we have to reset it
                            if (checkActiveIndex())
                            {
                                resetActiveIndex();

                            }
                            Destroy(item.getObj());
                            //objectsOnDisplay[index] = null;
                            objectsOnDisplay.Remove(item);
                            
                            
                            activeObjects--;
                            numbersOfItems--;


                        } else
                        {
                            item.getObj().transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
                            
                            int index = objectsOnDisplay.IndexOf(item);
                            item.getObj().GetComponent<Rigidbody>().isKinematic = true;
                            
                            item.getObj().GetComponent<BoxCollider>().isTrigger = true;
                            if (index == 0)
                            {
                                item.getObj().transform.RotateAround(this.transform.position, Vector3.up, 30);
                            } else if (index == 1)
                            {
                                item.getObj().transform.RotateAround(this.transform.position, Vector3.up, 0);
                            } else if (index == 2)
                            {
                                item.getObj().transform.RotateAround(this.transform.position, Vector3.up, -30);
                            }
                        }
                    }
                }
                //index++;
            }
            //creates new object if needed
            while (activeObjects < 3 && listOfItems.Count > activeObjects)
            {
                
                ARSpot spot = new ARSpot();
                resetActiveIndex();
                GameObject temp = Instantiate(listOfItems[indexAll].getObj());
                for (int i = 0; i <= 2; i++)
                {
                    if(!object.ReferenceEquals(null, objectsOnDisplay[i]))
                    {
                        temp.GetComponent<Rigidbody>().isKinematic = true;
                        //REMEMBER: add a check for what kind of collider in the future, or always use mesh?
                        temp.GetComponent<BoxCollider>().isTrigger = true;
                        temp.transform.position = this.transform.position + new Vector3(Mathf.Cos(Mathf.PI / 4), 0, Mathf.Sin(Mathf.PI / 4));
                        spot.setObj(temp);
                        spot.setProd(listOfItems[indexAll]);
                        //objectsOnDisplay[i] = spot;
                        objectsOnDisplay.Add(spot);
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
        //listOfItems = new Products[cart.Count];
        listOfItems = new List<Products>();
        objectsOnDisplay = new List<ARSpot>();
        int count = 0;
        int count2 = 0;
        numbersOfItems = 0;
        foreach (KeyValuePair<string, Products> item in cart)
        {
            listOfItems.Add(item.Value);
            count++;
            numbersOfItems++;


        }
       
        while (activeObjects < 3 && count2 < numbersOfItems)
        {
            
            ARSpot spot = new ARSpot();
            string objName = listOfItems[indexAll].getName();
            
            GameObject temp = (GameObject)Instantiate(Resources.Load(objName));
            temp.transform.parent = this.transform;
            temp.GetComponent<ProductController>().wand = this.wand; 
            
            temp.GetComponent<Rigidbody>().isKinematic = true;
            //REMEMBER: add a check for what kind of collider in the future, or always use mesh?
            temp.GetComponent<BoxCollider>().isTrigger = true;

            Color oldColor = temp.GetComponent<Renderer>().material.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f);
            temp.GetComponent<Renderer>().material.color = newColor;
            Vector3 newSize = temp.transform.localScale.normalized;
            temp.transform.localScale = new Vector3(newSize.x * 0.2f, newSize.y * 0.2f, newSize.z * 0.2f);

            if (count2 == 0 )
            {
                
                
                //temp.transform.position = this.transform.position +  this.transform.forward + new Vector3(Mathf.Cos(Mathf.PI-Mathf.PI/3), 0, Mathf.Sin(Mathf.PI - Mathf.PI / 3));
                //temp.transform.position = this.transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * this.transform.rotation.eulerAngles.y + Mathf.PI - Mathf.PI / 3), 0, Mathf.Sin(Mathf.Deg2Rad * this.transform.rotation.eulerAngles.y + Mathf.PI - Mathf.PI / 3));
                //Debug.Log("transform.position: " + this.transform.position);
                //Debug.Log("transform.forward: " + this.transform.forward);
                
                float degree = Vector3.Angle(this.transform.position,this.transform.localEulerAngles);
                float degree2 = degree - 30.0f;
                Vector3 please = new Vector3(this.transform.forward.x * 0.01f, 0.0f, this.transform.forward.y * 0.01f);

                //Debug.Log("degrees: " + degree2);
                //temp.transform.position = this.transform.position + new Vector3(Mathf.Cos(degree2*Mathf.Deg2Rad), 0, Mathf.Sin(degree2 * Mathf.Deg2Rad));
                //temp.transform.position = this.transform.position + this.transform.forward - new Vector3(this.transform.right.x*0.8f, 0, this.transform.right.z*0.8f);
                temp.transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
                temp.transform.RotateAround(this.transform.position, Vector3.up, 30);

                 transform.RotateAround(transform.TransformPoint(this.transform.position), Vector3.up, 30);
            } else if (count2 == 1)
            {
                //temp.transform.position = this.transform.position + this.transform.forward + new Vector3(Mathf.Cos(Mathf.PI/2), 0, Mathf.Sin(Mathf.PI/2));
                temp.transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
            } else
            {
                temp.transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
                temp.transform.RotateAround(this.transform.position, Vector3.up, -30);
            }
            spot.setObj(temp);
            spot.setProd(listOfItems[indexAll]);
            //objectsOnDisplay[count2] = spot;
            objectsOnDisplay.Add(spot);
            activeObjects++;
            indexAll++;
            count2++;

        }
    }

    public void toggleAR()
    {
     
        if (active)
        {
            /*
            for (int i = 0; i < activeObjects; i++)
            {
                
                Destroy(objectsOnDisplay[i].getObj());
                
            }*/
            objectsOnDisplay.Clear();
            objectsOnDisplay = null;
            active = false;
            indexAll = 0;
            activeObjects = 0;
            //objectsOnDisplay = new ARSpot[3];

            listOfItems.Clear();
            listOfItems = null;
        } else
        {
            startAR();
            active = true;
           
        }
    }

    public void makeThrowable(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic = false;
        obj.GetComponent<BoxCollider>().isTrigger = false;
    }
}
