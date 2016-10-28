using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemIDtoName : MonoBehaviour {

    private Dictionary<string, string> productsInSystem;

    void Start()
    {
        productsInSystem = new Dictionary<string, string>();
        //productsInSystem.Add("22", "Product1");
        //productsInSystem.Add("2", "cbd20");
        //productsInSystem.Add("3", "Product3");
        //productsInSystem.Add("4", "Product4");
        
        productsInSystem.Add("3827", "cbd20cit");
        productsInSystem.Add("3829", "cbd20mynt");
        productsInSystem.Add("144", "cbd50");
        productsInSystem.Add("3822", "cbd50cit");
        productsInSystem.Add("3824", "cbd50mynt");
        productsInSystem.Add("3847", "cbd60caps");
        productsInSystem.Add("4720", "cbd250caps");
        productsInSystem.Add("7770", "cbdcat");
        productsInSystem.Add("27", "cbddog");
        productsInSystem.Add("5137", "goldenhemp60");
        productsInSystem.Add("5150", "goldenhemp250");
        productsInSystem.Add("7606", "hampaoljafresh");
        productsInSystem.Add("22", "cbd20");

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
