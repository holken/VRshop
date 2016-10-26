using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemIDtoName : MonoBehaviour {

    private Dictionary<string, string> productsInSystem;

    void Start()
    {
        productsInSystem = new Dictionary<string, string>();
        productsInSystem.Add("22", "Product1");
        productsInSystem.Add("2", "cbd20");
        productsInSystem.Add("3", "Product3");
        productsInSystem.Add("4", "Product4");

    }

    public string getName(string id)
    {
        if (productsInSystem.ContainsKey(id))
        {
            string name = productsInSystem[id];
            return name;
        }
        return "";
    }
}
