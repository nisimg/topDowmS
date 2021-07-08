using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameProp : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    [SerializeField] bool endGame = false;
    // Start is called before the first frame update
    void Start()
    {
        endScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (endGame)
        {
            endLevel();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            endGame = true;       
        }
    }
    void endLevel()
    {
        endScreen.SetActive(true);
    }
}
