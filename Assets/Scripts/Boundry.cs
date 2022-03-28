//Final Project AI
//Kennedy Adams 100632983
//Dylan Brush 100700305
//Maija Kinnunen 100697620
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundry : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Animal")
        {
            collision.gameObject.GetComponent<Animals>().moveCounter = 0;
        }
    }
}
