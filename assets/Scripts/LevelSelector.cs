using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {
    string levelName;
    public string fetchURL = "http://localhost/vrshop/allshops.php";
    public GameObject panel;

    public void setLevelname(string levelName)
    {
        this.levelName = levelName;
    }

    public void startnewLevel()
    {
        Application.LoadLevel(levelName);
    }

    public void choosePaymentOption()
    {
        PlayerPrefs.SetString("levelName", levelName);
        Application.LoadLevel("ChoosePay");
    }

    void Start()
    {
        StartCoroutine(loadFromDB());

    }

    IEnumerator loadFromDB()
    {
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
            string shop = data.Substring(0, data.IndexOf("||"));
            data = data.Substring(data.IndexOf("||") + 2);
            Debug.Log(shop);
            GameObject newButton = (GameObject)Instantiate(Resources.Load("StoreButton"));
            newButton.transform.SetParent(panel.transform);
            newButton.GetComponent<RectTransform>().localPosition = new Vector3(0, 310 - 35 * count, 0);
            newButton.GetComponent<Button>().onClick.AddListener(() => { setLevelname(shop); choosePaymentOption(); });
            newButton.GetComponentInChildren<Text>().text = shop;
            count++;
        }
    }

}
