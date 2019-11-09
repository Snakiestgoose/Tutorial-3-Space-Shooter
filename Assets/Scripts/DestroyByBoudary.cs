using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoudary : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
