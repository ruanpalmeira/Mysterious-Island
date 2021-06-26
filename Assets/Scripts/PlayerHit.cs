using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Crab"){
            other.GetComponent<EnemyFollow>().damage(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.tag == "Crab"){
           // other.GetComponent<EnemyFollow>().damage(1);
            other.gameObject.GetComponent<EnemyFollow>().damage(1);
        }
    }
}
