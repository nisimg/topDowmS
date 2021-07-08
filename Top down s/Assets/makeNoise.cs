using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeNoise : MonoBehaviour
{
    [SerializeField] float NoiseDis = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameManager.incstnce.IHearSomething(transform, NoiseDis);
        }
    }
}
