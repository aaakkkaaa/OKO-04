using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;

public class sCommonParameters : MonoBehaviour
{
    public Vector3 GlobalScale = Vector3.one;
    [SerializeField] private AbstractMap _myAbsMap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            print("InitializeOnStart = " + _myAbsMap.InitializeOnStart);
            print("InitialZoom = " + _myAbsMap.InitialZoom);
        }
    }
}
