using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bullt : MonoBehaviour
{
    bool des = false;
    Test _Test;
    private void Awake()
    {
         _Test = FindObjectOfType<Test>();
    }
 
    private void Update()
    {
      
        if (des)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _Test.gameObject.SetActive(false);
        }
          
           
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Test>())
        {
            
            des = true;
        }
    }
}
