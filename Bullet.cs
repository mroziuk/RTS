using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    public GameObject explosion;
    private new AudioSource audio;
    public AudioClip[] clips;
    private int fractionID;
    public float speed = 50f;
    public int damage = 10;
    private Vector3 shootDir;
    private void Awake() {
        audio = GetComponent<AudioSource>();
        var random = new System.Random();
        audio.clip = clips[random.Next(clips.Length)];
        audio.Play();
    }
    public void Setup(Vector3 dir,int fractionID) {
        this.fractionID = fractionID;
        shootDir = dir;
        transform.rotation = Quaternion.LookRotation(shootDir);
        Destroy(gameObject, 2f);
    }
    private void Update() {
        transform.position += shootDir * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other) {
        var target = other.GetComponent<UnitHealth>();
        if(target != null){
            if(fractionID == target.GetComponent<Fraction>().ID)
                return;
            target.Damage(damage);
            var exp = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(exp, 1f);
            Destroy(gameObject);
        }
    }
}
