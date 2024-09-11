using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTranform = collision.transform;
        if (hitTranform.CompareTag("Player"))
        {
            Debug.Log("hit player");
            hitTranform.GetComponent<PlayerHealth>().TakeDamage(10);
        }

        Destroy(gameObject);
    }
}
