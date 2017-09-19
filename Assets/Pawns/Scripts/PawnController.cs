using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class PawnController : MonoBehaviour {

    public delegate void DeadPawnDelegate(GameObject enemy);
    public DeadPawnDelegate deadPawn;

    //controller variables
    [HideInInspector]
    public enum PawnState { Idle, Battle, Walking,FindTarget,Homing, Destroying, Dead};
    [HideInInspector]
    public enum PawnType {Wanderer, Hunter, Boss}

    public PawnState currentState = PawnState.Walking;
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

    protected virtual void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.speed = speed;
        waypoints = new List<Transform>();
        GameObject waypoint = GameObject.FindGameObjectWithTag("Waypoint");
        //Debug.Log(waypoint.name);
        for (int i = 0; i < waypoint.transform.childCount; i++)
        {
            waypoints.Add(waypoint.transform.GetChild(i));
        }
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

    // Update is called once per frame
    protected virtual void Update () {
        //Debug.DrawLine(transform.position, finalDestination.position);
        //Debug.Log(finalDestination.transform);

        if (gameObject.GetComponent<PawnCharacter>().isSlow)
            nav.speed = 0.5f;
        else
            nav.speed = speed;


        if (currentState != PawnState.Dead) {

            if (currentState == PawnState.Idle)
            {
                nav.isStopped = true;
            }
            else if (currentState == PawnState.Walking)
            {
                nav.isStopped = false;

                //to fix
                if (nextWaypoint < waypoints.Count)
                {
                    currentDestination = waypoints[nextWaypoint].position;
                    nav.SetDestination(currentDestination);
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
                if (target != null || target.GetComponent<PawnCharacter>().isDead)
                {
                    if (nav != null)
                    {

                        nav.SetDestination(target.transform.position);
                        //if (!IsAtLocation())
                        //else
                        //    ChangeState(PawnState.Battle);

                        //nav.isStopped = false;
                        //if(nav.hasPath)
                        //else
                        //{
                        //    if (gameObject.tag == "Ally")
                        //        //ChangeState(PawnState.Homing);
                        //        Debug.Log("");
                        //    else
                        //        ChangeState(PawnState.Walking);

                        //}

                    }

                }
                else
                {
                    if (gameObject.tag == "Ally")
                        ChangeState(PawnState.Homing);
                    else
                        ChangeState(PawnState.Walking);
                }
            }
            else if (currentState == PawnState.Homing)
            {
                Vector3 tolerance = homePosition - transform.position;
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
