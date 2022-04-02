using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BankScript : MonoBehaviour{
    public int amount = 5;
    public float delay = 1;
    private FractionResources resources;
    private float nextTimeToAction;

    private void Awake() {
        resources = GameObject.FindObjectsOfType<FractionResources>().First(x => x.ID == GetComponent<Fraction>().ID);
    }

    void Update(){
        if (Time.time > nextTimeToAction){
            resources.addGold(amount);
            nextTimeToAction = Time.time + delay;
        }
    }
}
