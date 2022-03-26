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

      //  if (transform.position.x < 11f && transform.position.x > 0f && transform.position.y < 9 && transform.position.y > -7.59f)
     //   {
        transform.Translate(new Vector3(xAxisValue, yAxisValue, 0.0f));

     //   }
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");

        GetComponent<Camera>().orthographicSize += scrollValue;

    }
}
