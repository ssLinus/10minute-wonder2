using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject[] exps = GameObject.FindGameObjectsWithTag("Exp");
            foreach (GameObject exp in exps)
            {
                Exp expComponent = exp.GetComponent<Exp>();
                if (expComponent != null)
                {
                    expComponent.isMag = true;
                }
            }
            Destroy(gameObject);
        }
    }
}
