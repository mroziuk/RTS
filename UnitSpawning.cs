using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitSpawning : MonoBehaviour{
    public GameObject unitType;
    public int MAX_UNITS = 30;
    public float dalay;
    public float cost;
    public Vector3 offset;

    private float nextTimeToSpawn;
    private Selectable selectable;
    private Camera cam;
    private FractionResources resources;

    private Vector3 destination;
    private int objectsInQueue;

    private void Awake() {
        objectsInQueue = 0;
        resources = GameObject.FindObjectsOfType<FractionResources>().First(x => x.ID == GetComponent<Fraction>().ID);
        cam = GameObject.FindObjectOfType<Camera>();
        selectable = GetComponent<Selectable>();
        offset = new Vector3(2.5f,.5f,2.5f);
        destination = transform.position + offset*4;
    }
    private void Update() {
        if(objectsInQueue > 0 && Time.time > nextTimeToSpawn){
            SpawnObject();
            objectsInQueue --;
            nextTimeToSpawn = Time.time + dalay;
        }
        if(selectable == null || !selectable.selected) return;

        if(Input.GetMouseButtonDown(1)){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit)){
                destination = hit.point;
            }
        }
    }
    public void AddSpawningObjectToQueue(){
        if(resources.units < MAX_UNITS)
            objectsInQueue ++;
    }
    private void SpawnObject(){
        var obj = Instantiate(unitType, transform.position + offset, Quaternion.identity);
        obj.GetComponent<UnitMovement>().cam = cam;
        obj.GetComponent<UnitMovement>().SetTarget(destination);
    }
}
