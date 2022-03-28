using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class acenemanager : MonoBehaviour
{
    // Start is called before the first frame update
   public void play()
    {
        SceneManager.LoadScene("SampleScene");
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
