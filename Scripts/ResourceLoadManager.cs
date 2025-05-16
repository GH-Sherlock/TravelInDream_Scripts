using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoadManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Resources.Load("Prefabs/CloudPlane_A_02_prefab");
        Resources.Load("Prefabs/CloudPlane_BaseA");

    }
}
