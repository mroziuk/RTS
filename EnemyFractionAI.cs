using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyFractionAI : MonoBehaviour{
    private List<UnitSpawning> buildings;
    private FractionResources resources;
    private int ID = 2;
    void Start(){
        resources = GetComponent<FractionResources>();
        ID = resources.ID;
    }

    void Update(){
        buildings = GameObject.FindObjectsOfType<UnitSpawning>().ToList();
        foreach(var b in buildings){
            if(b.GetComponent<Fraction>().ID == ID){
                if(resources.subtractGold(10)){
                    b.AddSpawningObjectToQueue();
                }
            }
        }
    }
}
