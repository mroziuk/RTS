using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

enum State{
    Move,       // move towards target, than stop. if target is another entity follow
    MoveAttack, // move towards target if there are enemies in range, stop and shoot
    Hold,       // if there are enemies in range, shoot
    Patrol      // patrol between points
};

public class UnitMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Camera cam;
    public LayerMask unitLayerMask;
    public float sightRadius;

    [HideInInspector]public Vector3 target;
    [HideInInspector] public bool hasArrived;
    [HideInInspector] public  Queue<Vector3> targetQueue;
    private float maxDistFromTarget = 0.1f;
    private float minDistFromAnotherUnitInTarget = 2.0f;
    private Rigidbody body;
    private Selectable selectable;
    // private State state;

    [HideInInspector] public bool isAttacking;
    [HideInInspector] public List<GameObject> objectsInRange;
    private int fractionID;
    public float range = 40;

    private LayerMask targetMask;
    private LayerMask obstacleMask;

    private void Awake() {
        targetMask = GetComponent<UnitAttack>().targetMask;
        obstacleMask = GetComponent<UnitAttack>().obstacleMask;
        objectsInRange = new List<GameObject>();
        // state = State.Move;
        selectable = GetComponent<Selectable>();
        body = GetComponent<Rigidbody>();
        fractionID = GetComponent<Fraction>().ID;
        target = transform.position;
        agent.SetDestination(transform.position);
        agent.isStopped = true;
        targetQueue = new Queue<Vector3>();
        isAttacking = false;
    }
    private void Start() {
        targetQueue = new Queue<Vector3>();
    }

    private void Update() {
        // getObjectsInRange();
        if (targetQueue.Count > 0){
            Move(targetQueue.Peek());
        }
        hasArrived = Vector3.SqrMagnitude(transform.position - target) <= maxDistFromTarget * maxDistFromTarget;
        if(hasArrived){
            if(targetQueue.Count > 0){
                targetQueue.Dequeue();
            }else{
                Stop();
            }
        }

        Debug.DrawLine(transform.position, target, Color.red, Time.deltaTime);
        Debug.DrawLine(transform.position, transform.position+agent.desiredVelocity, Color.green, Time.deltaTime);
        Debug.DrawLine(transform.position, transform.position + agent.velocity, Color.green, Time.deltaTime);
        Debug.DrawLine(transform.position, agent.nextPosition, Color.yellow, Time.deltaTime);

        
        if(hasArrived) return; // guard clause

        var otherUnits = GameObject.FindObjectsOfType<UnitMovement>().Where(o => o.hasArrived && o.target == target).ToArray();
        foreach(var unit in otherUnits){
            if(Vector3.SqrMagnitude(transform.position - unit.transform.position) <= minDistFromAnotherUnitInTarget*minDistFromAnotherUnitInTarget){
                Stop();
            }
        }
    }
    public void getObjectsInRange(){
        objectsInRange.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetMask);
        foreach (var unit in colliders)
        {
            var tmp = unit.GetComponent<Fraction>();
            if (tmp == null || tmp.ID == fractionID)
                continue;
            Vector3 direction = (unit.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, unit.transform.position);
            if (!Physics.Raycast(transform.position, direction, out var hit, distance, obstacleMask))
            {
                Debug.DrawLine(transform.position, unit.transform.position, new Color(.3f,.3f,.9f), .02f);
                objectsInRange.Add(unit.transform.gameObject);
                // Debug.DrawLine(transform.position, unit.transform.position,Color.magenta,1.0f/60f);
            }
            else if (hit.transform.gameObject.Equals(unit.gameObject))
            {
                Debug.DrawLine(transform.position, unit.transform.position, new Color(.6f,.6f,.9f), .02f);
                objectsInRange.Add(unit.transform.gameObject);
                // Debug.DrawLine(transform.position, unit.transform.position,Color.magenta,1.0f/60f);
            }
        }
    }
    public void Move(Vector3 dst){
            target = dst;
        target.y = transform.position.y;
        agent.isStopped = false;
        agent.SetDestination(target);
        var outline = GetComponent<Outline>();
        if (outline != null) outline.OutlineColor = Color.yellow;   
    }
    public void Stop(){
        hasArrived = true;
        agent.isStopped = true;
        targetQueue.Clear();
        var outline = GetComponent<Outline>();
        if(outline != null) outline.OutlineColor = Color.magenta;
    }
    public void SetTarget(Vector3 dst){
        Stop();
        targetQueue.Clear();
        targetQueue.Enqueue(dst);
        Move(dst);
    }
}
