//Final Project AI
//Kennedy Adams 100632983
//Dylan Brush 100700305
//Maija Kinnunen 100697620
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Canvas : MonoBehaviour
{

    private MapManager mapManager;
    // Start is called before the first frame update
    void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            mapManager.isOverUI = true;
        }
        else
        {
            mapManager.isOverUI = false;
        }
    }
}
