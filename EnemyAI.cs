using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyAI : MonoBehaviour{
    private UnitMovement unitMovement;
    private UnitAttack unitAttack;
    private Selectable selectable;
    private Camera cam;
    void Awake(){
        unitMovement = GetComponent<UnitMovement>();
        unitAttack = GetComponent<UnitAttack>();
        selectable = GetComponent<Selectable>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    void Start(){
        
    }

    void Update(){
        // if object in range shot
        unitMovement.getObjectsInRange();
        if(unitAttack.objectsInRange.Count > 0){
            if(unitMovement != null) unitMovement.Stop();
        }
        // else if objects in sight move
        else if(unitMovement.objectsInRange.Count > 0){
            // unitMovement.Move(objects.First().transform.position);
            if (unitMovement != null) unitMovement.SetTarget(unitMovement.objectsInRange.First().transform.position);
        }
    }
}
