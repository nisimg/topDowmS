using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Test : MonoBehaviour
{
    


    NavMeshAgent Nav;
    [SerializeField] Vector3 Target = new Vector3(0, 0, 0); 
    // Start is called before the first frame update
    void Start()
    {
        Nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                Target = hit.point;
            }
        }
        Nav.SetDestination(Target);
        
        
        
    }
}
