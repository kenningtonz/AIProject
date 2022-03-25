using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sense : MonoBehaviour
{
    public GameObject parent;
    public Quaternion look;

    public Vector3 spot;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Vector3 targ = collision.gameObject.transform.position;
            targ.z = 0f;

            Vector3 objectPos = parent.transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;

            parent.GetComponent<Animals>().lookRotation = Quaternion.Euler(new Vector3(0, 0, -angle));
            parent.GetComponent<Animals>().moveCounter = 10;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
