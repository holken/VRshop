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
    private bool gripButtonDown;
    private Transform interactionPoint;

    //private ARSpot[] objectsOnDisplay;
    private List<ARSpot> objectsOnDisplay;

    public float objDistance = 0.8f;
    public float rotationSpeed;

    private float[] itemRot;
    private float itemRot1 = 0;
    private float itemRot2 = 0;
    private float itemRot3 = 0;


    // Use this for initialization
    void Start () {
        indexAll = 0;
        activeObjects = 0;
        //objectsOnDisplay = new ARSpot[3];
        objectsOnDisplay = new List<ARSpot>();
        itemRot = new float[3];
        interactionPoint = new GameObject().transform;
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

    public bool getActiveStatus()
    {
        return active;
    }

    public void setWand(WandController newWand)
    {
        wand = newWand;
        interactionPoint.position = wand.GetComponent<Transform>().position;
    }
    public void gripButtonPressed(bool pressed)
    {
        
        gripButtonDown = pressed;
    }

    public void printOutShit()
    {
        foreach (ARSpot item in objectsOnDisplay)
        {
            //Debug.Log("starts");
            //Debug.Log("index: " + item.getIndex());
            //Debug.Log("name: " + item.getProd().getName());
        }
    }

    void objectRotation(Transform subject, float speedOfRotation)
    {
        subject.Rotate(Vector3.up * speedOfRotation, Time.deltaTime);
    }

    // Update is called once per frame
    void Update () {
        
        if (active)
        {

            if (gripButtonDown)
            {
                float distance = Vector3.Distance(wand.transform.position, interactionPoint.position);
                int count = 0;
                for (int i = objectsOnDisplay.Count - 1; i >= 0; i--) { 
                    // foreach (ARSpot item in objectsOnDisplay)
                    //{
                    if (Vector3.Distance(wand.transform.position, this.transform.right) > Vector3.Distance(interactionPoint.transform.position, this.transform.right))
                    {
                        itemRot[i] -= distance * rotationSpeed;
                    }
                    else
                    {
                        itemRot[i] += distance * rotationSpeed;
                    }

                    objectsOnDisplay[i].getObj().transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
                    Quaternion oldRot = objectsOnDisplay[i].getObj().transform.rotation;
                    objectsOnDisplay[i].getObj().transform.RotateAround(this.transform.position, Vector3.up, itemRot[i]);
                    objectsOnDisplay[i].getObj().transform.rotation = oldRot;


                    if (itemRot[i] > 30)
                    {
                        //Debug.Log("before destrying anus: " + objectsOnDisplay);
                        printOutShit();
                        float diff = Mathf.Abs(itemRot[i] - 30);
                        itemRot[i] = -30+diff;
                        //Debug.Log("Item to be destroyed: " + objectsOnDisplay[i].getProd().getName());
                        Destroy(objectsOnDisplay[i].getObj());
                        Destroy(objectsOnDisplay[i].returnTextObj());
                        listOfItems.Add(objectsOnDisplay[i].getProd());
                        objectsOnDisplay.RemoveAt(i);
                        Debug.Log("Removing at index: " + i);
                        activeObjects--;


                        //Debug.Log("after destrying anus: " + objectsOnDisplay);
                        printOutShit();
                    }
                    else if (itemRot[i] < -30)
                    {
                            //Debug.Log("before destrying anus: " + objectsOnDisplay);
                            printOutShit();
                            float diff = Mathf.Abs(itemRot[i] + 30);
                            itemRot[i] = 30-diff;
                            //Debug.Log("Item to be destroyed: " + objectsOnDisplay[i].getProd().getName());
                            Destroy(objectsOnDisplay[i].getObj());
                        Destroy(objectsOnDisplay[i].returnTextObj());
                        listOfItems.Add(objectsOnDisplay[i].getProd());
                            objectsOnDisplay.RemoveAt(i);
                        Debug.Log("Removing at index: " + i);
                        activeObjects--;
                    }
                //}
                }
                
            }
            //this is to check for if someone have thrown the box away, if then, remove quantity and if it have reached 0 quantity, replace it with a new one
            //int index = 0;
            //Debug.Log("before whore shit");
            //Debug.Log("whore shit: " + objectsOnDisplay);
            //printOutShit();

            //foreach (ARSpot item in objectsOnDisplay)
            for (int i = objectsOnDisplay.Count - 1; i >= 0; i--)
            {
                
                
                //Debug.Log("index: " + index);
                
                if (!object.ReferenceEquals(null, objectsOnDisplay[i]))
                {
                    //objectRotation(objectsOnDisplay[i].getObj().transform, 0.5f);
                    //checks if the position of object is far enough away
                    if (Vector3.Distance(objectsOnDisplay[i].getObj().transform.position, this.transform.position) >= 2.0f)
                    {
                        //Debug.Log("I was in here");


                        //checks if we have removed all objects
                        if (cartController.removeQuantityFromCart(objectsOnDisplay[i].getProd().getID(), -1))
                        {
                            //checks if there is any higher active objects in the indexActive list, otherwise we have to reset it
                            if (checkActiveIndex())
                            {
                                resetActiveIndex();

                            }
                            Destroy(objectsOnDisplay[i].getObj());
                            Destroy(objectsOnDisplay[i].returnTextObj());
                            //objectsOnDisplay[index] = null;
                            objectsOnDisplay.RemoveAt(i);
                            
                            
                            activeObjects--;
                            numbersOfItems--;


                        } else
                        {
                            objectsOnDisplay[i].setText("Quantity: " + objectsOnDisplay[i].getProd().getQuantity()); 
                            objectsOnDisplay[i].getObj().transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
                            
                                int index = objectsOnDisplay.IndexOf(objectsOnDisplay[i]);
                                objectsOnDisplay[i].getObj().GetComponent<Rigidbody>().isKinematic = true;

                                objectsOnDisplay[i].getObj().GetComponent<BoxCollider>().isTrigger = true;
                            if (index == 0)
                            {
                                    objectsOnDisplay[i].getObj().transform.RotateAround(this.transform.position, Vector3.up, itemRot[0]);
                            } else if (index == 1)
                            {
                                    objectsOnDisplay[i].getObj().transform.RotateAround(this.transform.position, Vector3.up, itemRot[1]);
                            } else if (index == 2)
                            {
                                    objectsOnDisplay[i].getObj().transform.RotateAround(this.transform.position, Vector3.up, itemRot[2]);
                            }

                            
                        }
                    }
                }
                //index++;
            }

            //creates new object if needed
            while (activeObjects < 3 && listOfItems.Count > 0)
            {

                ARSpot spot = new ARSpot();
                resetActiveIndex();
                GameObject temp = (GameObject)Instantiate(Resources.Load(listOfItems[0].getName()));
                GameObject text = (GameObject)Instantiate(Resources.Load("ARtext"));
                text.GetComponent<TextMesh>().text = "Quantity: " + listOfItems[0].getQuantity();

                temp.transform.parent = this.transform;
                text.transform.parent = temp.transform;
                text.transform.position = temp.transform.position + new Vector3(0f, 0.2f, 0f);
                bool index0Exists = false;
                bool index1Exists = false;
                bool index2Exists = false;
                foreach (ARSpot item in objectsOnDisplay)
                {

                    int index = item.getIndex();
                    if (index == 0)
                    {
                        index0Exists = true;
                    }
                    else if (index == 1)
                    {
                        index1Exists = true;
                    }
                    else if (index == 2)
                    {
                        index2Exists = true;
                    }
                }
                objectSettings(temp);
                temp.transform.parent = this.transform;
                temp.GetComponent<ProductController>().wand = this.wand;
                temp.GetComponent<Rigidbody>().isKinematic = true;
                //REMEMBER: add a check for what kind of collider in the future, or always use mesh?
                temp.GetComponent<BoxCollider>().isTrigger = true;
                //temp.transform.position = this.transform.position + new Vector3(Mathf.Cos(Mathf.PI / 4), 0, Mathf.Sin(Mathf.PI / 4));
                spot.assignTextObj(text);
                spot.setObj(temp);
                spot.setProd(listOfItems[0]);
                listOfItems.RemoveAt(0);
                //objectsOnDisplay[i] = spot;
                
                
                if (!index0Exists) {
                    spot.setIndex(0);
                    objectsOnDisplay.Insert(0, spot);
                    objectsOnDisplay[0].getObj().transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
                    objectsOnDisplay[0].getObj().transform.RotateAround(this.transform.position, Vector3.up, itemRot[0]);
                    index0Exists = false;
                    Debug.Log("Adding at index: " + 0);


                } else if (!index1Exists)
                {
                    spot.setIndex(1);
                    objectsOnDisplay.Insert(1, spot);
                    objectsOnDisplay[1].getObj().transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
                    objectsOnDisplay[1].getObj().transform.RotateAround(this.transform.position, Vector3.up, itemRot[1]);
                    index1Exists = false;
                    Debug.Log("Adding at index: " + 1);


                } else if (!index2Exists)
                {
                    spot.setIndex(2);
                    objectsOnDisplay.Insert(2, spot);
                    objectsOnDisplay[2].getObj().transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
                    objectsOnDisplay[2].getObj().transform.RotateAround(this.transform.position, Vector3.up, itemRot[2]);
                    index2Exists = false;
                    Debug.Log("Adding at index: " + 2);


                }
            }
                /*if (indexActive >= 3)
                {
                    indexActive = 0;
                }*/

            
        }
	
	}

    private void objectSettings(GameObject temp)
    {
        Color oldColor = temp.GetComponent<Renderer>().material.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f);
        temp.GetComponent<Renderer>().material.color = newColor;
        Vector3 newSize = temp.transform.localScale.normalized;
        temp.transform.localScale = new Vector3(newSize.x * 0.2f, newSize.y * 0.2f, newSize.z * 0.2f);
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
       
        while (activeObjects < 3 && 0 < listOfItems.Count)
        {
            
            ARSpot spot = new ARSpot();
            string objName = listOfItems[0].getName();
            
            GameObject temp = (GameObject)Instantiate(Resources.Load(objName));
            GameObject text = (GameObject)Instantiate(Resources.Load("ARtext"));
            text.GetComponent<TextMesh>().text = "Quantity: " + listOfItems[0].getQuantity();
            temp.transform.parent = this.transform;
            text.transform.parent = temp.transform;
            text.transform.position = temp.transform.position + new Vector3(0f, 0.2f, 0f);
            spot.assignTextObj(text);
            temp.GetComponent<ProductController>().wand = this.wand; 
            
            temp.GetComponent<Rigidbody>().isKinematic = true;
            //REMEMBER: add a check for what kind of collider in the future, or always use mesh?
            temp.GetComponent<BoxCollider>().isTrigger = true;

            objectSettings(temp);
        

            if (count2 == 0 )
            {

                /* 
                 //temp.transform.position = this.transform.position +  this.transform.forward + new Vector3(Mathf.Cos(Mathf.PI-Mathf.PI/3), 0, Mathf.Sin(Mathf.PI - Mathf.PI / 3));
                 //temp.transform.position = this.transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * this.transform.rotation.eulerAngles.y + Mathf.PI - Mathf.PI / 3), 0, Mathf.Sin(Mathf.Deg2Rad * this.transform.rotation.eulerAngles.y + Mathf.PI - Mathf.PI / 3));
                 //Debug.Log("transform.position: " + this.transform.position);
                 //Debug.Log("transform.forward: " + this.transform.forward);

                 float degree = Vector3.Angle(this.transform.position,this.transform.localEulerAngles);
                 float degree2 = degree - 30.0f;
                 Vector3 please = new Vector3(this.transform.forward.x * 0.01f, 0.0f, this.transform.forward.y * 0.01f);

                 //Debug.Log("degrees: " + degree2);
                 //temp.transform.position = this.transform.position + new Vector3(Mathf.Cos(degree2*Mathf.Deg2Rad), 0, Mathf.Sin(degree2 * Mathf.Deg2Rad));
                 //temp.transform.position = this.transform.position + this.transform.forward - new Vector3(this.transform.right.x*0.8f, 0, this.transform.right.z*0.8f);*/
                itemRot1 = 30;
                itemRot[0] = 30;
                temp.transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
                temp.transform.RotateAround(this.transform.position, Vector3.up, itemRot[0]);
                spot.setIndex(0);

                 //transform.RotateAround(transform.TransformPoint(this.transform.position), Vector3.up, 30);
            } else if (count2 == 1)
            {
                //temp.transform.position = this.transform.position + this.transform.forward + new Vector3(Mathf.Cos(Mathf.PI/2), 0, Mathf.Sin(Mathf.PI/2));
                itemRot[1] = 0;
                temp.transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
                spot.setIndex(1);
            } else
            {
                itemRot3 = -30;
                itemRot[2] = -30;
                temp.transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
                temp.transform.RotateAround(this.transform.position, Vector3.up, itemRot[2]);
                spot.setIndex(2);
            }
            spot.setObj(temp);
            spot.setProd(listOfItems[0]);
            listOfItems.RemoveAt(0);
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
            foreach (ARSpot item in objectsOnDisplay)
            {
                Destroy(item.getObj());
                Destroy(item.returnTextObj());
            }
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
