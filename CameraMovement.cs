using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CameraMovement : MonoBehaviour{
    
    float speed = 2f;
    float offset = 50f;

    private void Awake() {
        transform.position -= transform.forward * 200f;
    }
    private void Start() {
        #region editor
            #if UNITY_EDITOR    
                UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
            #endif
        #endregion

    }
    private void Update() {
        if(transform.position.y > 40f){
            transform.position += transform.forward * Time.deltaTime * 100;
        }
        if(Input.GetKey(KeyCode.LeftControl)){
            speed = 15f;
        }else{
            speed = 5f;
        }
        // moving camera TODO: refactor, make dynamic camera speed
        if(Input.GetKey("w")|| (Input.mousePosition.y > Screen.height - offset)){
            transform.position += (Vector3.back + Vector3.left)* Time.deltaTime * speed;
        }
        if(Input.GetKey("s")|| (Input.mousePosition.y < offset)){
            transform.position += (Vector3.forward + Vector3.right)* Time.deltaTime * speed;
        }
        if(Input.GetKey("a")|| (Input.mousePosition.x < offset)){
            transform.position += (Vector3.back + Vector3.right )* Time.deltaTime * speed;
        }
        if(Input.GetKey("d") || (Input.mousePosition.x > Screen.width - offset)){
            transform.position += (Vector3.forward + Vector3.left )* Time.deltaTime * speed;
        }
    }
}
