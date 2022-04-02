using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fraction : MonoBehaviour{
    public int ID;
    private Dictionary<int,Color> colors;
    private void Awake() {
        transform.parent = null;
    }
    private void Start() {
        colors = new Dictionary<int, Color>();
        colors.Add(1, Color.blue);
        colors.Add(2, Color.red);
        colors.Add(3, Color.green);
        colors.Add(4, Color.magenta);
        colorChildren(transform);
    }
    private void colorChildren(Transform t){
        Renderer renderer = t.GetComponent<Renderer>();
        if(renderer != null){
            renderer.material.color = colors[ID];
        }
        if(t.childCount == 0) return;
        foreach (Transform child in t)
        {
            colorChildren(child);
        }
    }
}
