using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedDictionary : MonoBehaviour{
    Dictionary<int, GameObject> dictionary;
    private void Start() {
        dictionary = new Dictionary<int, GameObject>();
    }
    public void add(GameObject go){
        int id = go.GetInstanceID();
        if(!dictionary.ContainsKey(id)){
            dictionary.Add(id,go);
            var selectable = go.GetComponent<Selectable>();
            if(selectable != null) selectable.Select();
        }
    }
    public void remove(int id){
        if(dictionary.ContainsKey(id)){
            dictionary[id].GetComponent<Selectable>().Deselect();
            dictionary.Remove(id);
        }
    }
    public void removeAll(){
        foreach(KeyValuePair<int,GameObject> pair in dictionary){
            if(pair.Value != null)
                pair.Value.GetComponent<Selectable>().Deselect();
        }
        dictionary.Clear();
    }
}
