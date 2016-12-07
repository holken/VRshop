using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PaymentSelector : MonoBehaviour {
    public string fetchURL = "http://localhost/vrshop/allpaymentoptions.php/?storeName=";
    
    public GameObject panel;

    public void setPayment(string paymentOption)
    {
        PlayerPrefs.SetString("paymentOption", paymentOption);
        
        Application.LoadLevel(PlayerPrefs.GetString("levelName"));
    }

    void Start()
    {
        Debug.Log(fetchURL);
        StartCoroutine(loadFromDB());

    }

    IEnumerator loadFromDB()
    {
        fetchURL += PlayerPrefs.GetString("levelName");
        Debug.Log(fetchURL);
        WWW hs_get = new WWW(fetchURL);
        yield return hs_get;
        string data = hs_get.text;

        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
            Debug.Log(hs_get.text);
        }

        //loop through data where we cut out all the variables for each item and then create an object and assign to spawnpoints
        int count = 0;
        while (data.IndexOf("||") != -1 && count < 100)
        {
            string payment = data.Substring(0, data.IndexOf("||"));
            data = data.Substring(data.IndexOf("||") + 2);
            Debug.Log(payment);
            GameObject newButton = (GameObject)Instantiate(Resources.Load("StoreButton"));
            newButton.transform.SetParent(panel.transform);
            newButton.GetComponent<RectTransform>().localPosition = new Vector3(0, 310 - 35 * count, 0);
            newButton.GetComponent<Button>().onClick.AddListener(() => { setPayment(payment); });
            newButton.GetComponentInChildren<Text>().text = payment;
            count++;
        }
    }
}
