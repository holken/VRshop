using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Dataloader : MonoBehaviour {
    private string secretKey = "mySecretKey"; // Edit this value and make sure it's the same as the one stored on the server
    public string fetchURL = "http://fiskeapp.se/wp-content/plugins/wc_vr/downloaditems.php?";
    public int nbrOfSpawnPoints;
    public string shopName;
    public Material material;
    public GameObject checkout;
    int nbrObjects = 0;
    
    // Use this for initialization
    void Start () {
        Debug.Log("fish");
        StartCoroutine(loadFromDB());
        
	}

    IEnumerator loadFromDB()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(fetchURL);
        fetchURL = fetchURL  + "shopname=" + WWW.EscapeURL(scene.name);
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
        string shopurl = data.Substring(0, data.IndexOf("||"));
        data = data.Substring(data.IndexOf("||") + 2);
        checkout.GetComponent<Checkout>().setURL(shopurl);
        //loop through data where we cut out all the variables for each item and then create an object and assign to spawnpoints
        int count = 0;
        while (data.IndexOf("||") != -1 && count < 100)
        {
            string product = data.Substring(0, data.IndexOf("||"));
            string shopName = product.Substring(0, product.IndexOf("|"));
            product = product.Substring(product.IndexOf("|") + 1);
            
            string userName = product.Substring(0, product.IndexOf("|"));
            product = product.Substring(product.IndexOf("|") + 1);

            string shopType = product.Substring(0, product.IndexOf("|"));
            product = product.Substring(product.IndexOf("|") + 1);

            string spawnPoint = product.Substring(0, product.IndexOf("|"));
            product = product.Substring(product.IndexOf("|") + 1);

            string prodId = product.Substring(0, product.IndexOf("|"));
            product = product.Substring(product.IndexOf("|") + 1);

            string prodName = product.Substring(0, product.IndexOf("|"));
            product = product.Substring(product.IndexOf("|") + 1);

            string proddesc = product.Substring(0, product.IndexOf("|"));
            product = product.Substring(product.IndexOf("|") + 1);

            string prodquant = product.Substring(0, product.IndexOf("|"));
            product = product.Substring(product.IndexOf("|") + 1);

            string prodcost = product.Substring(0, product.IndexOf("|"));
            product = product.Substring(product.IndexOf("|") + 1);

            string uploadID = product.Substring(0, product.IndexOf("|"));
            product = product.Substring(product.IndexOf("|") + 1);

            string productImg = product;


            data = data.Substring(data.IndexOf("||")+2);
            count++;
            Debug.Log("before " + prodName);
            StartCoroutine(downloadImg(productImg, spawnPoint, prodName, prodId, prodquant, prodcost));
            Debug.Log("after");

        }
    }

    IEnumerator downloadImg(string url, string spawnPoint, string prodName, string prodId, string prodquant, string prodcost)
    {
        Debug.Log("entered");
        Texture2D texture = new Texture2D(1024, 1024);
        WWW www = new WWW(url);
        yield return www;
        www.LoadImageIntoTexture(texture);
        Material tempMaterial = new Material(material);
        tempMaterial.SetTexture("_MainTex", texture);
        tempMaterial.EnableKeyword("_NORMALMAP");
        tempMaterial.EnableKeyword("_DETAIL_MULX2");

        int point = int.Parse(spawnPoint);
        point--;
        GameObject spawner;
        if (point == 0)
        {
            spawner = GameObject.Find("ProductSpawner");
        } else
        {
            spawner = GameObject.Find("ProductSpawner (" + spawnPoint + ")");
        }
        //GameObject spawner = GameObject.Find("ProductSpawner (" + spawnPoint + ")");
        Debug.Log(spawner);

        GameObject product = spawner.GetComponent<ProductSpawning>().product;
        Debug.Log(product.GetComponent<ProductController>().getProductName());
        product.GetComponent<Renderer>().sharedMaterial.mainTexture = texture;
        product.GetComponent<ProductController>().setProductName(prodName);
        product.GetComponent<ProductController>().setProductID(prodId);
        product.GetComponent<ProductController>().setQuantity(int.Parse(prodquant));
        product.GetComponent<ProductController>().setPrice(double.Parse(prodcost));

    }
}
