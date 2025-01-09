using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aro : MonoBehaviour
{
    private static AroManager aroManager;
    void Start()
    {
        if (aroManager == null) aroManager = FindObjectOfType<AroManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other .gameObject.CompareTag("Player"))
        {
            aroManager.CheckRing(this);
        }
    }
}
