using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInput : MonoBehaviour{
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
    void Start() {
        
    }
    void Update(){
        if (selectable.selected)
        {
            if (Input.GetKey(KeyCode.H)){
                unitMovement.Stop();
            }
            if (Input.GetKey(KeyCode.F)){
                unitMovement.isAttacking = true;
            }
            if (Input.GetMouseButtonDown(1)){
                unitMovement.isAttacking = false;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit)){
                    if (!Input.GetKey(KeyCode.LeftShift)){
                        unitMovement.targetQueue.Clear();
                    }
                    unitMovement.targetQueue.Enqueue(hit.point);
                    //unitMovement.Move(hit.point);
                    //Move(hit.point);
                }
            }
            else if (unitMovement.isAttacking){
                unitMovement.getObjectsInRange();
                var objects = unitMovement.objectsInRange;
                if (unitAttack.objectsInRange.Count > 0){
                    unitMovement.Stop();
                }
                else if (unitMovement.objectsInRange.Count > 0){
                    var closest = unitMovement.objectsInRange.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).First();
                    unitMovement.Move(closest.transform.position);
                }
            }
        }
    }
}
