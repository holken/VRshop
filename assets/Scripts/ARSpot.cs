using UnityEngine;
using System.Collections;

public class ARSpot : MonoBehaviour {
    GameObject obj;
    Products prod;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setObj(GameObject obj)
    {
        this.obj = obj;
    }

    public void setProd(Products prod)
    {
        this.prod = prod;
    }

    public GameObject getObj()
    {
        return obj;
    }

    public Products getProd()
    {
        return prod;
    }
}
