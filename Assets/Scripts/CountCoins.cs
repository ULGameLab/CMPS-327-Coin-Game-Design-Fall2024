using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountCoins : MonoBehaviour
{
    private int numCoinsCollected = 0; 
    public TextMeshProUGUI countText;

    // Start is called before the first frame update
    void Start()
    {
        countText.text = "Coins Collected = " + numCoinsCollected.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin")) 
        { 
            numCoinsCollected += 1; 
            countText.text = "Coins Collected = " + numCoinsCollected.ToString(); 
        }
    }
}
