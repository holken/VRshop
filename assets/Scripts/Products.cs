using UnityEngine;
using System.Collections;

public class Products : MonoBehaviour {
	string product_id;
	int quantity;
	double price;
    GameObject obj;
    string objName;
   


	void start(){
		quantity = 0;
		price = 0;
        objName = "";
	}


	public void setProductId(string product_id){
		this.product_id = product_id;
	}

	public void setQuantity(int quantity){
		this.quantity += quantity;
	}

	public void setPrice(double price){
		this.price += price;
	}

	public int getQuantity(){
		return quantity;
	}

	public double getPrice(){
		return price;
	}

    public void setObj(GameObject obj)
    {
        this.obj = obj;
        

    }

    public void setName(string objName)
    {
        this.objName = objName;
    }

    public string getName()
    {
        return objName;
    }

    public string getID()
    {
        return product_id;
    }

    public GameObject getObj()
    {
        return obj;
    }
}
