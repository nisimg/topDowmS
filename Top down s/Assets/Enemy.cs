
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum stat
{
    idle,
    patrol,
    alert,
    chese,
    attack
}
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] float Speed = 6;
    [SerializeField] float AlertSpeed;
    [SerializeField] float AlertviewDistance;
    [SerializeField] float viewDistance = 6;
    [SerializeField] float alermModMulti = 1.1f;
    Animator animator;
    NavMeshAgent agent;
   [SerializeField] Vector3[] wayPointArry;
    Vector3 tar;
    int index = 0;
    [SerializeField] LayerMask layerMask;
    public stat Stat;
    [SerializeField] float attackDis = 2f;
    [SerializeField] float DisToFlee = 10f;
    [SerializeField] GameObject Player;
    [SerializeField] float idleTimer;
    [SerializeField] float FireRate;

    [SerializeField] Transform muzzle;
    [SerializeField] GameObject bullet;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
       
    }
    // Start is called before the first frame update
    void Start()
    {
        tar = wayPointArry[0];
        Stat = stat.idle;
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Player == null)
        {
            return;
        }

        int rayCount = 50;

        RaycastHit raycastHit;
        Vector3 dirAngleS = (transform.forward.normalized + transform.right + new Vector3(0, 0.5F, 0)) * 3;
        Vector3 dirAngleE = (transform.forward.normalized - transform.right + new Vector3(0, 0.5F, 0)) * 3;



        for (int i = 0; i <= rayCount; i++)
        {
            Debug.Log("Lerp:" + Vector3.Lerp(dirAngleS, dirAngleE, (float)i / (float)rayCount));
            Debug.DrawRay(transform.position, Vector3.Lerp(dirAngleS, dirAngleE, (float)i / (float)rayCount), Color.blue, 0.1F);
            if (Physics.Raycast(transform.position, Vector3.Lerp(dirAngleS, dirAngleE, (float)i / (float)rayCount), out raycastHit, viewDistance, layerMask))
            {
                Controller e = raycastHit.collider.GetComponent<Controller>();
                //Debug.DrawRay(transform.position, UtilsClass.GetVectorFromAngle(angle), Color.red, 5F);
                if (raycastHit.collider.GetComponent<Controller>())
                {
                    Debug.Log("hi player");
                    Stat = stat.chese;
                }
                else
                {

                }
            }
        }
        if (Stat == stat.idle)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer > Random.Range(1,5))
            {
                idleTimer = 0;
                Stat = stat.patrol;
            }
        }
        
        if (Stat == stat.chese && Vector3.Distance(Player.transform.position, this.transform.position) <= attackDis)
        {
            Stat = stat.attack;
        }
        if (Stat == stat.attack && Vector3.Distance(Player.transform.position, this.transform.position) > attackDis)
        {
            Stat = stat.chese;
        }
        if (Stat == stat.chese && Vector3.Distance(Player.transform.position, this.transform.position) > DisToFlee)
        {
            Stat = stat.alert;
        }

        getTarget();

        switch (Stat)
        {
            case stat.idle:
                animator.SetBool("inMove", false);
                break;
            case stat.patrol:
                agent.speed = Speed;
                animator.SetBool("inMove", true);
                agent.SetDestination(tar);

                break;
            case stat.alert:
             
                animator.SetBool("inAlertMod", true);
                animator.SetBool("inMove", true);
                agent.speed = AlertSpeed;
                viewDistance = AlertviewDistance;
                agent.SetDestination(tar);
                break;
            case stat.chese:
                animator.SetBool("inMove", true);
                agent.speed = Speed;
                agent.SetDestination(Player.transform.position);
                break;
            case stat.attack:
                animator.SetBool("inMove", false);
                transform.LookAt(Player.transform.position);
                agent.speed = 0;
                animator.SetTrigger("shot");
                break;
            default:
                break;
        }

        AttackPlayer();
    }

    void AttackPlayer()
    {
        if (Stat == stat.attack)
        {
            FireRate += Time.deltaTime;
            if (FireRate > 0.1f)
            {
                FireRate = 0;
                GameObject bulletClone = Instantiate(bullet, muzzle.position, Quaternion.identity);
                Rigidbody bulletRb =  bulletClone.AddComponent<Rigidbody>();
                bulletRb.transform.LookAt(Player.transform.position);
                bulletRb. transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 50);
                Destroy(bulletRb, 0.5f);
              

            }
        }
    }
    private void getTarget()
    {
        Vector3 p1 = new Vector3(wayPointArry[index].x, 0, wayPointArry[index].z);
        Vector3 p2 = new Vector3(transform.position.x, 0, transform.position.z);
        bool reachedWaypoint = Vector3.Distance(p1, p2) < 0.001f;
        Debug.Log("w.x:" + wayPointArry[index].x + ",p.x:" + transform.position.x + ", w.z:" + wayPointArry[index].z + ", p.z:" + transform.position.z + " == " + reachedWaypoint);
        if (reachedWaypoint)
        {
            if (++index >= wayPointArry.Length)
            {
                index = 0;
                Stat = stat.idle;
                idleTimer = 0;
            }
            Debug.Log("index:" + index + ", wayPointArry.Length" + wayPointArry.Length);
        }
        tar = wayPointArry[index];
    }
}
