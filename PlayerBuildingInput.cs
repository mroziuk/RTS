using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBuildingInput : MonoBehaviour{
    private Selectable selectable;
    private FractionResources resources;
    private UnitSpawning spawning;
    void Start(){
        resources = GameObject.FindObjectsOfType<FractionResources>().First(x => x.ID == GetComponent<Fraction>().ID);
        selectable = GetComponent<Selectable>();
        spawning = GetComponent<UnitSpawning>();
    }

    void Update(){
        if(!selectable.selected) return;

        if(Input.GetKeyDown(KeyCode.R)){
            if(resources.subtractGold(10)){
                spawning.AddSpawningObjectToQueue();
            }
        }
    }
}
