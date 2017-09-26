using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class PawnController : MonoBehaviour {

    public Material frozenMaterial;
    protected Material[] originalMaterial;
    protected Material[] mats;

    public delegate void DeadPawnDelegate(GameObject enemy);
    public DeadPawnDelegate deadPawn;
    protected List<GameObject> enemiesInRange;
    //controller variables
    [HideInInspector]
    public enum PawnState { None ,Idle, Battle, Walking,FindTarget,Homing, Destroying, Dead};
    [HideInInspector]
    public enum PawnType {Wanderer, Hunter, Boss}

    public PawnState currentState = PawnState.None;
    protected PawnType type = PawnType.Wanderer;

    public Transform finalDestination;
    public NavMeshAgent nav;
    public float speed;

    public Vector3 currentDestination;

    public GameObject target;

    //used for allied troopers
    public Vector3 homePosition;
    public List<Transform> waypoints;
    int nextWaypoint = 1;



#region Waypoint
    GameObject waypointLane1;
    GameObject waypointLane2;
    GameObject waypointLane3;
    GameObject waypointLane4;
#endregion
    public int myLane;

    protected virtual void Awake()
    {
        waypoints = new List<Transform>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = speed;

        

 

       



    }

    public void SetupWaypoints(int lane_, int waypoint_)
    {
        myLane = lane_;
       

        if (myLane == 1)
        {
            waypointLane1 = GameObject.FindGameObjectWithTag("WaypointsLane1");
            for (int i = 0; i < waypointLane1.transform.childCount; i++)
            {
                waypoints.Add(waypointLane1.transform.GetChild(i));
            }

        }
        else if (myLane == 2)
        {
            waypointLane2 = GameObject.FindGameObjectWithTag("WaypointsLane2");

            //here we decide which waypoint the monster will follow.
           

            if (waypoint_ == 0 )
            {
                Transform waypointA = waypointLane2.transform.Find("waypointA");
                
                for (int i = 0; i < waypointA.childCount; i++)
                {
                    waypoints.Add(waypointA.GetChild(i));
                }

            }
            else
            {
                Transform waypointB = waypointLane2.transform.Find("waypointB");

                for (int i = 0; i < waypointB.childCount; i++)
                {
                    waypoints.Add(waypointB.GetChild(i));
                }
            }

            
        }
        else if (myLane == 3)
        {
            waypointLane3 = GameObject.FindGameObjectWithTag("WaypointsLane3");
            for (int i = 0; i < waypointLane3.transform.childCount; i++)
            {
                waypoints.Add(waypointLane3.transform.GetChild(i));
            }
        }
        else if (myLane == 4)
        {
            waypointLane4 = GameObject.FindGameObjectWithTag("WaypointsLane4");
            for (int i = 0; i < waypointLane4.transform.childCount; i++)
            {
                waypoints.Add(waypointLane4.transform.GetChild(i));
            }
        }
        //Debug.Log(waypoint.name);
        ChangeState(PawnState.Walking);
    }

    private void Start()
    {
      
    }

    public PawnState CurrentState {  get { return currentState; }
    }

    IEnumerator HitStop()
    {
        float waitSeconds = 0.3f;

        while(waitSeconds >= 0.0f)
        {
            //nav.speed = 0.1f;
            nav.isStopped = true;
            waitSeconds -= Time.deltaTime;

            yield return null;
        }
        nav.isStopped = false;
        //nav.speed = speed;
    }
   

    private void OnChangingState()
    {

    }

    public void ChangeState(PawnState newstate)
    {
        if (currentState == PawnState.Dead)
            return;

        if (currentState != newstate)
        {

            currentState = newstate;
        }
    }


	protected bool IsAtLocation()
	{
		float dist = nav.remainingDistance;
        //Debug.Log(dist);

       


		if (!nav.pathPending)
		{
			if(nav.remainingDistance <= nav.stoppingDistance)
			{
                return true;
				//if(!nav.hasPath || nav.velocity.magnitude == 0f)
				//{
				//	return true;
				//}
			}
		}
		return false;
	}


    public virtual void EnterFrozenTime()
    {
        nav.speed = 0.5f;
    }

    public virtual void LeaveFrozenTime()
    {
        nav.speed = speed;
    }

    // Update is called once per frame
    protected virtual void Update () {
        //Debug.DrawLine(transform.position, finalDestination.position);
        //Debug.Log(finalDestination.transform);
        if(nav != null)
        {
            if (gameObject.GetComponent<PawnCharacter>().isSlow)
                EnterFrozenTime();
            else
                LeaveFrozenTime();

        }


        if (currentState != PawnState.Dead) {


            if (currentState == PawnState.Idle)
            {
                nav.isStopped = true;
            }
            else if (currentState == PawnState.Walking)
            {
                nav.isStopped = false;

                
                if (nextWaypoint < waypoints.Count)
                {
                    if(waypoints[nextWaypoint].position != currentDestination)
                    {
                        currentDestination = waypoints[nextWaypoint].position;
                        nav.SetDestination(currentDestination);
                    }
                }


                if (IsAtLocation())
                    nextWaypoint++;


            }
            else if (currentState == PawnState.Battle)
            {
                OnBattle();
            }
            else if (currentState == PawnState.FindTarget)
            {
                nav.isStopped = false;
                

                if (target != null || !target.GetComponent<PawnCharacter>().isDead)
                {

                    nav.SetDestination(target.transform.position);
                    if (IsAtLocation() || enemiesInRange.Contains(target))
                    {
                        LookToTarget();
                        ChangeState(PawnState.Battle);
                    }
                    
                   
                }
                else
                {
                    if (gameObject.tag == "Ally")
                    {
                        if( enemiesInRange.Count > 0)
                        {
                            target = enemiesInRange[0];
                            LookToTarget();
                            ChangeState(PawnState.Battle);
                        }
                        else
                            ChangeState(PawnState.Homing);

                    }
                    else
                        ChangeState(PawnState.Walking);
                }


            }
            else if (currentState == PawnState.Homing)
            {
                nav.isStopped = false;
                nav.SetDestination(homePosition);
                if (IsAtLocation())
                {
                    
                    ChangeState(PawnState.Idle);

                }

            }

        }
        else
        {
            if (nav.isActiveAndEnabled)
                nav.isStopped = true;
        }

        

        if (GameController.gameState == GameState.GameOver)
            ChangeState(PawnState.Idle);
	}

    public virtual void OnMoving() { }

    public virtual void OnBattle() { }

    public virtual void OnIdle()
    {

    }

    public virtual void OnHoming() { }

    protected virtual void OnTriggerEnter(Collider other)
    {
       
    }

    protected virtual void OnTriggerExit(Collider other)
    {

    }

    protected void LookToTarget()
    {
        Vector3 lookDir = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDir);
        Vector3 rotation = lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
