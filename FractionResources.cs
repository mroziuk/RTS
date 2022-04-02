using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FractionResources : MonoBehaviour{
    public int gold = 100;
    public int units;
    public int ID;
    private void Update() {
        units = GameObject.FindObjectsOfType<PlayerInput>().Length;
    }
    public TextMeshProUGUI textMesh;
    public void addGold(int x){
        if(x > 0){
            gold += x;
            updateText();
        } 
    }
    public bool subtractGold(int x){
        if(x > 0 && x <= gold){
            gold -= x;
            updateText();
            return true;
        }
        return false;
    }

    void updateText(){
        if(textMesh != null){
            textMesh.text = "Gold:" + gold;
        }
    }
}
