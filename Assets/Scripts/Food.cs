using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.tag == "Animal")
        {
            collision.gameObject.GetComponent<Animals>().food = collision.gameObject.GetComponent<Animals>().food + collision.gameObject.GetComponent<Animals>().m_foreging;
            Destroy(gameObject);
            collision.gameObject.GetComponent<Animals>().moveCounter = collision.gameObject.GetComponent<Animals>().moveTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
