
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ARController : MonoBehaviour {
    public CartController cartController;
    private bool active;
    private bool gripButtonDown;

    private List<Products> listOfItems;
    private List<ARSpot> objectsOnDisplay;
    private float[] itemRot;

    public float objDistance = 0.8f;
    private Transform interactionPoint;
    public float rotationSpeed;

    public WandController wand;

    void Start () {
        objectsOnDisplay = new List<ARSpot>();
        itemRot = new float[3];
        interactionPoint = new GameObject().transform;
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


    void objectRotation(Transform subject, float speedOfRotation)
    {
        subject.Rotate(Vector3.up * speedOfRotation, Time.deltaTime);
    }

    void updateText(int i, GameObject currObj)
    {
        objectsOnDisplay[i].returnTextObj().transform.LookAt(this.transform);
        Vector3 diffVec = objectsOnDisplay[i].returnTextObj().transform.position - this.transform.position;
        diffVec.Normalize();
        objectsOnDisplay[i].returnTextObj().transform.position = currObj.transform.position - new Vector3(diffVec.x * currObj.transform.lossyScale.x, diffVec.y * currObj.transform.lossyScale.y, diffVec.z * currObj.transform.lossyScale.z);
        objectsOnDisplay[i].returnTextObj().transform.Rotate(0f, 180f, 0f);
    }

    void updateObj(GameObject currObj, int i)
    {
        currObj.transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
        Quaternion oldRot = currObj.transform.rotation;
        currObj.transform.RotateAround(this.transform.position, Vector3.up, itemRot[i]);
        currObj.transform.rotation = oldRot;
    }

    void addBackToListAndRemove(int i)
    {
        listOfItems.Add(objectsOnDisplay[i].getProd());
        destroyTextAndObj(i);
    }

    void destroyTextAndObj(int i)
    {
        Destroy(objectsOnDisplay[i].getObj());
        Destroy(objectsOnDisplay[i].returnTextObj());
        objectsOnDisplay.RemoveAt(i);
    }

    void Update () {
        
        if (active)
        {
            //rotates objects
            if (gripButtonDown)
            {
                float distance = Vector3.Distance(wand.transform.position, interactionPoint.position);

                for (int i = objectsOnDisplay.Count - 1; i >= 0; i--) { 

                    if (Vector3.Distance(wand.transform.position, this.transform.right) > Vector3.Distance(interactionPoint.transform.position, this.transform.right))
                    {
                        itemRot[i] -= distance * rotationSpeed;
                    }
                    else
                    {
                        itemRot[i] += distance * rotationSpeed;
                    }

                    GameObject currObj = objectsOnDisplay[i].getObj();
                    updateObj(currObj, i);
                    updateText(i, currObj);

                    if (itemRot[i] > 30)
                    {

                        float diff = Mathf.Abs(itemRot[i] - 30);
                        itemRot[i] = -30 + diff;

                        addBackToListAndRemove(i);
                        

                    }
                    else if (itemRot[i] < -30)
                    {
                        
                        float diff = Mathf.Abs(itemRot[i] + 30);
                        itemRot[i] = 30 - diff;

                        addBackToListAndRemove(i);

                    }
                }
                
            }

            //check if object has been thrown
            for (int i = objectsOnDisplay.Count - 1; i >= 0; i--)
            {
                
                if (!object.ReferenceEquals(null, objectsOnDisplay[i]))
                {

                    if (Vector3.Distance(objectsOnDisplay[i].getObj().transform.position, this.transform.position) >= 2.0f)
                    {


                        if (cartController.removeQuantityFromCart(objectsOnDisplay[i].getProd().getID(), 1))
                        {

                            destroyTextAndObj(i);

                        } else
                        {
                            objectsOnDisplay[i].setText("Quantity: " + objectsOnDisplay[i].getProd().getQuantity()); 
                            objectsOnDisplay[i].getObj().transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
                            objectsOnDisplay[i].getObj().transform.parent = this.transform;
                            objectsOnDisplay[i].getObj().transform.rotation = new Quaternion(0f,0f,0f, 0f);


                           int index = objectsOnDisplay.IndexOf(objectsOnDisplay[i]);
                           objectsOnDisplay[i].getObj().GetComponent<Rigidbody>().isKinematic = true;

                           objectsOnDisplay[i].getObj().GetComponent<BoxCollider>().isTrigger = true;
                            if (i == 0)
                            {
                                    objectsOnDisplay[i].getObj().transform.RotateAround(this.transform.position, Vector3.up, itemRot[0]);
                            } else if (i == 1)
                            {
                                    objectsOnDisplay[i].getObj().transform.RotateAround(this.transform.position, Vector3.up, itemRot[1]);
                            } else if (i == 2)
                            {
                                    objectsOnDisplay[i].getObj().transform.RotateAround(this.transform.position, Vector3.up, itemRot[2]);
                            }

                            
                        }
                    }
                }
            }

            //creates new object if needed
            while (objectsOnDisplay.Count < 3 && listOfItems.Count > 0)
            {

                ARSpot spot = new ARSpot();
                GameObject temp = (GameObject)Instantiate(Resources.Load(listOfItems[0].getName()));
                GameObject text = (GameObject)Instantiate(Resources.Load("ARtext"));

                initializeARObj(text, temp);
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

                spotAssignement(spot, text, temp);

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
                aimARText(0);
            }

            
        }
	
	}

    private void objectSettings(GameObject temp)
    {
        Color oldColor = temp.GetComponent<Renderer>().material.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f);
        temp.GetComponent<Renderer>().material.color = newColor;
        Vector3 newSize = temp.transform.localScale.normalized;
        temp.transform.localScale = new Vector3(0.08f, 0.08f,0.08f);
    }

    private void initializeARObj(GameObject text, GameObject temp)
    {
        text.GetComponent<TextMesh>().text = "Quantity: " + listOfItems[0].getQuantity();
        temp.transform.parent = this.transform;
        text.transform.parent = temp.transform;

        if (temp.GetComponent<ProductInstantiation>() != null && !temp.GetComponent<ProductInstantiation>().isInstantiated())
        {
            temp.GetComponent<ProductInstantiation>().instantiateProduct();
        }
        temp.GetComponent<ProductController>().wand = this.wand;
        temp.GetComponent<Rigidbody>().isKinematic = true;
        temp.GetComponent<BoxCollider>().isTrigger = true;
    }

    private void spotAssignement(ARSpot spot, GameObject text, GameObject temp)
    {
        spot.assignTextObj(text);
        spot.setObj(temp);
        spot.setProd(listOfItems[0]);
        listOfItems.RemoveAt(0);
    }

    private void aimARText(int i)
    {
        GameObject text = objectsOnDisplay[i].returnTextObj();
        GameObject temp = objectsOnDisplay[i].getObj();
        text.transform.LookAt(this.transform);
        Vector3 diffVec = objectsOnDisplay[i].returnTextObj().transform.position - this.transform.position;
        diffVec.Normalize();
        text.transform.position = temp.transform.position - new Vector3(diffVec.x * temp.transform.lossyScale.x, diffVec.y * temp.transform.lossyScale.y, diffVec.z * temp.transform.lossyScale.z);
        text.transform.Rotate(0f, 180f, 0f);
    }

    private void startAR()
    {

        Dictionary<string, Products> cart = cartController.getCart();
        listOfItems = new List<Products>();
        objectsOnDisplay = new List<ARSpot>();
        int count2 = 0;
        foreach (KeyValuePair<string, Products> item in cart)
        {
            listOfItems.Add(item.Value);
        }
       
        while (objectsOnDisplay.Count < 3 && 0 < listOfItems.Count)
        {

            string objName = listOfItems[0].getName();

            ARSpot spot = new ARSpot();
            GameObject temp = (GameObject)Instantiate(Resources.Load(objName));
            GameObject text = (GameObject)Instantiate(Resources.Load("ARtext"));
            initializeARObj(text, temp);

            text.transform.position = temp.transform.position + new Vector3(0f, 0.2f, 0f);

            objectSettings(temp);

            if (count2 == 0 )
            {
                itemRot[0] = 20;
                temp.transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
                temp.transform.RotateAround(this.transform.position, Vector3.up, itemRot[0]);
                spot.setIndex(0);

            } else if (count2 == 1)
            {
                itemRot[1] = 0;
                temp.transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
                spot.setIndex(1);
            } else
            {
                itemRot[2] = -20;
                temp.transform.position = this.transform.position + this.transform.forward.normalized * objDistance;
                temp.transform.RotateAround(this.transform.position, Vector3.up, itemRot[2]);
                spot.setIndex(2);
            }
            spotAssignement(spot, text, temp);
            objectsOnDisplay.Add(spot);

            aimARText(count2);
            count2++;

        }
    }

    public void toggleAR()
    {
     
        if (active)
        {

            foreach (ARSpot item in objectsOnDisplay)
            {
                Destroy(item.getObj());
                Destroy(item.returnTextObj());
            }
            objectsOnDisplay.Clear();
            objectsOnDisplay = null;
            active = false;

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
