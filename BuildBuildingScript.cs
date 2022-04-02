using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBuildingScript : MonoBehaviour{
    public GameObject blueprint1;
    public GameObject blueprint2;
    public FractionResources resources;
    public void spawnBlueprintObject1(){
        if(resources.subtractGold(100)) // bank
            Instantiate(blueprint1);
    }
    public void spawnBlueprintObject2(){ // barracks
        if (resources.subtractGold(50))
            Instantiate(blueprint2);
    }
}
