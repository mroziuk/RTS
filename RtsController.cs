using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class RtsController : MonoBehaviour
{
    Camera cam;
    public LayerMask layerMask;
    public SelectedDictionary dictionary;
    Vector3 startPosition, endPosition;
    Vector3 p1, p2;
    bool dragSelect = false;
    private void Start() {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    void Update(){
        // selecting
        if(Input.GetMouseButtonDown(0)){
            p1 = Input.mousePosition;
        }
        if(Input.GetMouseButton(0)){
            if((p1 - Input.mousePosition).magnitude > 20){
                dragSelect = true;
            }
        }
        if(Input.GetMouseButtonUp(0)){
            if(dragSelect == false){
                Ray ray = cam.ScreenPointToRay(p1);
                if(Physics.Raycast(ray, out var hit, 50000f, layerMask)){
                    Debug.DrawLine(cam.ScreenToWorldPoint(p1), hit.point, Color.red, 100.0f);
                    if(!Input.GetKey(KeyCode.LeftShift)){
                        dictionary.removeAll();
                    }
                    if(hit.transform.GetComponent<Selectable>() != null)
                        dictionary.add(hit.transform.gameObject);
                }else{
                    if(!Input.GetKey(KeyCode.LeftShift)){
                        dictionary.removeAll();
                    }
                }
            }else{
                int i=0;
                Vector3[] verts = new Vector3[4];
                Vector3[] vecs = new Vector3[4];
                p2 = Input.mousePosition;
                var corners = getBoundingBox(p1,p2);
                foreach(var corner in corners){
                    Ray ray = cam.ScreenPointToRay(corner);
                    if(Physics.Raycast(ray, out var hit)){
                        verts[i] = hit.point;
                        vecs[i] = ray.origin - hit.point;
                        Debug.DrawLine(cam.ScreenToWorldPoint(corner), hit.point, Color.red, 1.0f);
                    }
                    i++;
                }
                var selectionMesh = generateSelectedMesh(verts, vecs);
                var selectionBox = gameObject.AddComponent<MeshCollider>();
                selectionBox.sharedMesh = selectionMesh;
                selectionBox.convex = true;
                selectionBox.isTrigger = true;

                if(!Input.GetKey(KeyCode.LeftShift)){
                    dictionary.removeAll();
                }

                Destroy(selectionBox, .05f);   
            }
            dragSelect = false;
        }
    }
    private void OnGUI() {
        if(dragSelect == true){
            var rect = Utils.GetScreenRect(p1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }
    Vector2[] getBoundingBox(Vector2 p1, Vector2 p2){
        var res = new List<Vector2> {p1,p2, new Vector2(p1.x, p2.y), new Vector2(p2.x, p1.y)};
        return res.OrderBy(a => a.x).ThenBy(a => a.y).ToArray();
    }
    Mesh generateSelectedMesh(Vector3[] corners, Vector3[] vecs){
        Vector3[] verts = new Vector3[8];
        int[] tris = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 0, 1, 1, 4, 0, 4, 5, 6, 6, 5, 7 };
        for(int i = 0; i < 4; i++){
            verts[i] = corners[i];
        }
        for(int j = 4; j < 8; j++){
            verts[j] = corners[j - 4] + vecs[j - 4];
        }
        Mesh selectionMesh = new Mesh();
        selectionMesh.vertices = verts;
        selectionMesh.triangles = tris; 
        return selectionMesh;
    }
    private void OnTriggerEnter(Collider other){
        if(!other.transform.gameObject.GetComponent<Selectable>()) return;
        dictionary.add(other.gameObject);
    }
}
