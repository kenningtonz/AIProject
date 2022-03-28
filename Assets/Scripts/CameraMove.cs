using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private int MAX_X = 29;
    private int MIN_X = -29;
    private int MAX_Y = 29;
    private int MIN_Y = -29;
  

    // Update is called once per frame
    void Update()
    {
        //gets inputs
        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");
        
        //sets size of camera
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        GetComponent<Camera>().orthographicSize += -scrollValue * 5;
   
        //moves camera
        transform.Translate(new Vector3(xAxisValue, yAxisValue, 0.0f));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, MIN_X, MAX_X), Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y ), -12);


    }
}
