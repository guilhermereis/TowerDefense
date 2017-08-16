using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PawnController : MonoBehaviour {

    //controller variables
    public enum PawnState { Idle, Battle, Walking,FindTarget,Homing };
    public enum PawnType {Wanderer, Hunter, Boss}
    public PawnState currentState = PawnState.Walking;
    protected PawnType type = PawnType.Wanderer;

    public Transform finalDestination;
    protected NavMeshAgent nav;
    public float speed;

    public GameObject target;

    //used for allied troopers
    public Vector3 homePosition;
    

    protected virtual void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.speed = speed;
    }

    public PawnState CurrentState {  get { return currentState; }
    }
   

    private void OnChangingState()
    {

    }

    public void ChangeState(PawnState newstate)
    {
        if(currentState != newstate)
        {
            currentState = newstate;
        }
    }

    // Update is called once per frame
    protected virtual void Update () {
        //Debug.DrawLine(transform.position, finalDestination.position);
        //Debug.Log(finalDestination.transform);
        if(currentState == PawnState.Idle)
        {

        }else if (currentState == PawnState.Walking)
        {
			nav.isStopped = false;
            nav.SetDestination(finalDestination.position);

        }else if(currentState == PawnState.Battle)
        {
            OnBattle();
        }
        else if(currentState == PawnState.FindTarget)
        {
            if (target != null)
            {
                nav.SetDestination(target.transform.position);

            }
			else
			{
				if (gameObject.tag == "Ally")
					ChangeState(PawnState.Homing);
				else
					ChangeState(PawnState.Walking);
			}
        }
        else if(currentState == PawnState.Homing)
        {
            Vector3 tolerance = homePosition - transform.position;
            if (homePosition == transform.position || tolerance.magnitude < 0.5)
                ChangeState(PawnState.Idle);
            else
            {
                nav.SetDestination(homePosition);
                //Debug.Log("Going Home");
            }
        }
	}

    public virtual void OnMoving() { }

    public virtual void OnBattle() { }

    public virtual void OnIdle()
    {

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
       
    }

    protected virtual void OnTriggerExit(Collider other)
    {

    }

}
