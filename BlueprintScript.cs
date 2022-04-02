using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintScript : MonoBehaviour{
    RaycastHit hit;
    Vector3 movePoint;
    public LayerMask mask;
    public GameObject prefab;
    public LayerMask groundMask;
    private Color goodColor;
    private Color badColor;
    private int colidedObjects;
    void Start(){
        colidedObjects = 0;
        badColor = new Color(1,0,0,.3f);
        goodColor = new Color(0,1,0,.3f);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 50000f,groundMask)){
            transform.position = hit.point;
        }
    }

    void Update(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 50000f, groundMask)){
            transform.position = hit.point;
        }
        if(colidedObjects == 0){
            GetComponentInChildren<Renderer>().material.color = goodColor;
            if(Input.GetMouseButtonDown(0)){
                Instantiate(prefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }else{
            GetComponentInChildren<Renderer>().material.color = badColor;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.name != "Ground"){
            colidedObjects += 1;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.name != "Ground"){
            colidedObjects -= 1;
        }
    }
}
