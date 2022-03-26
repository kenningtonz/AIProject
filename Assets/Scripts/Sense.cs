using UnityEngine;

public class Sense : MonoBehaviour
{
    public GameObject parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
          
            Vector3 targ = collision.gameObject.transform.position;
            //print(targ);
            targ.z = 0f;

            Vector3 objectPos = parent.transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;
            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            parent.GetComponent<Animals>().lookRotation = q;
          //  print(parent.GetComponent<Animals>().lookRotation);
            
            parent.GetComponent<Animals>().moveCounter = 10;
        }
    }

}
