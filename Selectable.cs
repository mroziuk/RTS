using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Selectable : MonoBehaviour{
    [HideInInspector] public bool selected;
    private Outline outline;
    public Guid groupId { get; private set; }
    private void Start() {
        selected = false;
        GetComponent<Outline>().enabled = false;
        outline = GetComponent<Outline>();
    }
    public void Select(){
        selected = true;
        if (outline != null) outline.enabled = true;

    }
    public void Deselect(){
        selected = false;
        if (outline != null) outline.enabled = false;
    }
}
