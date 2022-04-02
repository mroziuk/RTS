using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class GameMenager : MonoBehaviour{
    public GameObject gameOver;
    public TextMeshProUGUI text;
    List<Fraction> allEntities;
    void Start(){
        allEntities = new List<Fraction>();
    }
    void Update(){
        allEntities = GameObject.FindObjectsOfType<Fraction>().ToList();
        if(allEntities.Where(x => x.ID == 1).Count() == 0){
            gameOver.SetActive(true);
            text.text = "Game over";
        }else if(allEntities.Where(x => x.ID == 2).Count() == 0){
            gameOver.SetActive(true);
            text.text = "You won!";
        }
    }
}
