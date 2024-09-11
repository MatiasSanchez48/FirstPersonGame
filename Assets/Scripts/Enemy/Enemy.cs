using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private Vector3 lastKnowPos;

    public NavMeshAgent Agent { get => agent; }
    public Path path;
    public GameObject debugSphere;

    public GameObject Player {  get => player; }
    public Vector3 LastKnowPos { get => lastKnowPos; set => lastKnowPos = value; }

    [Header("Sight Values")]
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float eyeHight;

    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate;

    //Justify for debugging purposes.
    [SerializeField]
    private string currentState;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
        debugSphere.transform.position = lastKnowPos;
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            //is the player close enough to be seen?
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance) {

                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHight);
                float angleToPlayer = Vector3.Angle(targetDirection,transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView) { 

                    Ray ray= new Ray(transform.position + (Vector3.up * eyeHight),targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray,out hitInfo,sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin,ray.direction * sightDistance);    
                            return true;
                        }
                    }
                }
            }
        } 
        return false;
    }
}
