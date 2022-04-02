using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour{
    public int maxHealth = 100;
    private int health;
    public SelectedDictionary dictionary;
    public HealthBarScript healthBar;
    private void Awake() {
        healthBar = GetComponentInChildren<HealthBarScript>();
    }
    private void Start() {
        health = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }
    public void Damage(int value){
        health -= value;
        healthBar.setHealth(health);
        if(health <= 0){
            if(dictionary != null)
                dictionary.remove(gameObject.GetInstanceID());
            Destroy(transform.gameObject);
        }
    }
}
