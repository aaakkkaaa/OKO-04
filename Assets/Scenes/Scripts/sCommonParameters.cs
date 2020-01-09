using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class sCommonParameters : MonoBehaviour
{
    public Vector3 WorldScale0 = new Vector3(0.000242f, 0.000242f, 0.000242f);
    [SerializeField]
    private AbstractMap _AbsMap;

    [NonSerialized]
    public float MapZoom0;
    [NonSerialized]
    public Vector3 WorldScale;

    // Скорости перемещений ступы с наблюдателем
    public float MortarPanSpeed = 5f;
    public float MortarVertSpeed = 2f;

    // Ограничения высоты перемещений ступы с наблюдателем
    public float MortarHeightMin = 100f;
    public float MortarHeightMax = 20000f;

    // Продолжительность перелета в заданную точку, сек
    public float MortarFlightTime = 2.0f;
    // Положение в начале сеанса
    [NonSerialized] public Vector3 MortarHomePos = new Vector3(0, 3000, -4500);
    [NonSerialized] public Vector3 MortarHomeEu = new Vector3(0, 0, 0);
    // Положение "на вышке"
    [NonSerialized] public Vector3 MortarTowerPos = new Vector3(280, 100, 1100);
    [NonSerialized] public Vector3 MortarTowerEu = new Vector3(0, 165, 0);
    // Положение "на хвосте" - локальный сдвиг относительно самолета-носителя
    [NonSerialized] public Vector3 MortarTailPos = new Vector3(225, 300, -650);



    // Start is called before the first frame update
    void Start()
    {
        MapZoom0 = _AbsMap.Zoom;
        WorldScale = WorldScale0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left shift") || Input.GetKey("right shift")) // Если нажата кнопка Shift
        {
            if (Input.GetKeyDown("1")) // Переход к новым координатам
            {

                print("UnityTileSize = " + _AbsMap.UnityTileSize);
                print("WorldRelativeScale = " + _AbsMap.WorldRelativeScale);
                print("Zoom = " + _AbsMap.Zoom);
                print("CenterLatitudeLongitude = " + _AbsMap.CenterLatitudeLongitude);
                print("CenterMercator = " + _AbsMap.CenterMercator);
                print("");

                _AbsMap.SetZoom(_AbsMap.Zoom + 0.1f);
                _AbsMap.UpdateMap();

                Vector2d myNewCoord = new Vector2d(47.26666667f, 11.35f); // Иннсбрук
                _AbsMap.UpdateMap(myNewCoord, 12f);
            }
            if (Input.GetKeyDown("2")) // Переход от плоской карты к объемной
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

    public Vector3 GeoToWorldPosition(float Latitude, float Longitude, bool queryHeight = true)
    {
        Vector2d latitudeLongitude = new Vector2d(Latitude, Longitude);
        Vector3 worldPos = _AbsMap.GeoToWorldPosition(latitudeLongitude, queryHeight);
        return worldPos;
    }
}
