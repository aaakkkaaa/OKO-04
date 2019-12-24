using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class sCommonParameters : MonoBehaviour
{
    public Vector3 WorldScale0 = new Vector3(0.000242f, 0.000242f, 0.000242f);
    [SerializeField] private AbstractMap _AbsMap;

    [NonSerialized] public float MapZoom0;
    [NonSerialized] public Vector3 WorldScale;

    // Start is called before the first frame update
    void Start()
    {
        MapZoom0 = _AbsMap.Zoom;
        WorldScale = WorldScale0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1")) // Переход к новым координатам
        {
            /*
            print("UnityTileSize = " + _AbsMap.UnityTileSize);
            print("WorldRelativeScale = " + _AbsMap.WorldRelativeScale);
            print("Zoom = " + _AbsMap.Zoom);
            print("CenterLatitudeLongitude = " + _AbsMap.CenterLatitudeLongitude);
            print("CenterMercator = " + _AbsMap.CenterMercator);
            print("");

            _AbsMap.SetZoom(_AbsMap.Zoom + 0.1f);
            _AbsMap.UpdateMap();
            */

            Vector2d myNewCoord = new Vector2d(47.26666667f, 11.35f); // Иннсбрук
            _AbsMap.UpdateMap(myNewCoord, 12f);
        }
        else if (Input.GetKeyDown("2")) // Переход от плоской карты к объемной
        {
            print(_AbsMap.Terrain.ElevationType);
            if (_AbsMap.Terrain.ElevationType != ElevationLayerType.TerrainWithElevation)
            {
                _AbsMap.Terrain.SetElevationType(ElevationLayerType.TerrainWithElevation);
            }
            else
            {
                _AbsMap.Terrain.SetElevationType(ElevationLayerType.FlatTerrain);
            }
            print(_AbsMap.Terrain.ElevationType);
        }
    }

    public float GetZoom()
    {
        return _AbsMap.Zoom;
    }

    public bool SetZoom(float NewZoom)
    {
        if (NewZoom >= 11.0f && NewZoom < 15.0f)
        {
            _AbsMap.UpdateMap(NewZoom);
            return true;
        }
        return false;
    }
}
