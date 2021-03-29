using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStreetMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
