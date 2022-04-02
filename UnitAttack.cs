using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class UnitAttack : MonoBehaviour
{
    private UnitMovement movement;
    private int fractionID;
    // unit stats
    public float attackCooldown = 2.0f;
    public float range = 20;
    // layer masks
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public LayerMask buildingsMask;
    [HideInInspector] public List<GameObject> objectsInRange;
    //shooting
    public Transform bullet;
    private float nexTimeToFire = 0;
    public float fireRate = 2;
    private void Start() {
        objectsInRange = new List<GameObject>();
        fractionID = GetComponent<Fraction>().ID;
        movement = GetComponent<UnitMovement>();
    }
    private void Update() {
        getObjectsInRange();
        if(objectsInRange.Count > 0){
            var closest = objectsInRange.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).First();
            var direction = (closest.transform.position - transform.position).normalized;
            
            if(Time.time > nexTimeToFire && movement.hasArrived){
                performAttack(direction);
                nexTimeToFire = Time.time + 1.0f/fireRate;
            }
        }
    }
    public void performAttack(Vector3 direction){
        Transform bulletTransform = Instantiate(bullet, transform.position, Quaternion.Euler(0,0,90));
        bulletTransform.GetComponent<Bullet>().Setup(direction, fractionID);
        //audio.Play();
    }

    public void getObjectsInRange(){
        objectsInRange.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position,range, targetMask);
        foreach(var unit in colliders){
            var tmp = unit.GetComponent<Fraction>();
            if(tmp == null || tmp.ID == fractionID)
                continue;
            Vector3 direction = (unit.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, unit.transform.position);
            if(!Physics.Raycast(transform.position, direction, out var hit, distance, obstacleMask)){
                // Debug.DrawLine(transform.position, unit.transform.position, Color.black, .02f);
                objectsInRange.Add(unit.transform.gameObject);
            }else if(hit.transform.gameObject.Equals(unit.gameObject)){
                Debug.DrawLine(transform.position, unit.transform.position, Color.white, .02f);
                objectsInRange.Add(unit.transform.gameObject);
            }
        }
    }
}
