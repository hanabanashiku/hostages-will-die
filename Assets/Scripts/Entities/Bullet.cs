using Hanabanashiku.GameJam.Models;
using UnityEngine;

namespace Hanabanashiku.GameJam.Entities {
 public class Bullet : MonoBehaviour {
     public Weapon Weapon { get; set; }
     
     [SerializeField]
     private int _speed = 1000;

     private Vector3 _origin;

     private static GameObject prefab; 

     private void Awake() {
         _origin = transform.position;
     }

     private void Update() {
         transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
         DestroyIfTooFar();
     }

     private void OnTriggerEnter(Collider other) {
         if(other.CompareTag(Constants.Tags.PLAYER)) {
             return;
         }
         
         Destroy(gameObject);
     }

     private void DestroyIfTooFar() {
         if(Vector3.Distance(_origin, transform.position) < Weapon.MaxShotDistance) {
             return;
         }
         
         Destroy(gameObject);
     }

     public static void InstantiateBullet(GameObject origin, Weapon weapon, Quaternion rotation) {
         prefab ??= (GameObject)Resources.Load("Bullet");
         var obj = Instantiate(prefab, origin.transform.position, rotation);
         var bullet = obj.GetComponent<Bullet>();
         bullet.Weapon = weapon;
     }
 }
}
