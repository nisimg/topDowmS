using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<Enemy> enemiesList =  new List<Enemy>();
    Enemy[] Enemies;
    public static GameManager incstnce;
    // Start is called before the first frame update
    void Start()
    {
        incstnce = this;


        Enemies = FindObjectsOfType<Enemy>();
        foreach (var item in Enemies)
        {
            enemiesList.Add(item);
        }
       
    }

    public void IHearSomething(Transform noisedLoc, float hearingDistance)
    {
        foreach (var item in enemiesList)
        {
            float NoisedDistance = Vector3.Distance(item.gameObject.transform.position,noisedLoc.position) ;
            float HearingDistance = hearingDistance;


            if (NoisedDistance < HearingDistance)
            {
                item.Stat = stat.alert;
            }
        }
    }

  
}
