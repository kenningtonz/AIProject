using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(xAxisValue, yAxisValue, 0.0f));
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");

        GetComponent<Camera>().orthographicSize += scrollValue;

    }
}
