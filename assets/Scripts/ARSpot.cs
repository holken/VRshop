using UnityEngine;
using System.Collections;

public class ARSpot : MonoBehaviour {
    GameObject obj;
    Products prod;
    int index;
    GameObject text;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void assignTextObj(GameObject text)
    {
        this.text = text;
    }

    public GameObject returnTextObj()
    {
        return text;
    }

    public void setText(string text)
    {
        this.text.GetComponent<TextMesh>().text = text;
    }

    public string getText()
    {
        return text.GetComponent<TextMesh>().text;
    }

    public void setIndex(int index)
    {
        this.index = index;
    }

    public int getIndex()
    {
        return index;
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
