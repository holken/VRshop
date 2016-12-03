using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Threading;



public class ProductController : MonoBehaviour {

	public string name;
	public string product_id;
	public int quantity;
	public double price;
    public WandController wand;
    public ARController AR;
    public string objName;

    public string getProductID()
    {
        return product_id;

    }

    public void setProductID(string product_id)
    {
        this.product_id = product_id;

    }

    public string getProductName()
    {
        return name;

    }
    public void setProductName(string name)
    {
        this.name = name;

    }

    public int getQuantity()
    {
        return quantity;
    }
    public void setQuantity(int quantity)
    {
        this.quantity = quantity;

    }

    public double getPrice()
    {
        return price;
    }
    public void setPrice(double price)
    {
        this.price = price;

    }

    public void setWand(WandController wand)
    {
        this.wand = wand;
    }

    public string getARCopy()
    {
        return objName;
    }

}
	