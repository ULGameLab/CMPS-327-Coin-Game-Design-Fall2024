using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate the coin
        transform.localEulerAngles += new Vector3(0, 0.5f, 0);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }
}
