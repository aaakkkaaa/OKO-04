using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class sCommonParameters : MonoBehaviour
{
    public Vector3 GlobalScale = Vector3.one;
    [SerializeField] private AbstractMap _myAbsMap;
    [SerializeField] private Transform _UUEE_Surface;
    [SerializeField] private Transform _Mortar;

    private float _Zoom0;
    private Vector3 _Scale0;
    private Vector3 _MortarPos0;

    // Start is called before the first frame update
    void Start()
    {
        _Zoom0 = _myAbsMap.Zoom;
        _Scale0 = _UUEE_Surface.localScale;
        _MortarPos0 = _Mortar.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1")) // Переход к новым координатам
        {
            /*
            print("UnityTileSize = " + _myAbsMap.UnityTileSize);
            print("WorldRelativeScale = " + _myAbsMap.WorldRelativeScale);
            print("Zoom = " + _myAbsMap.Zoom);
            print("CenterLatitudeLongitude = " + _myAbsMap.CenterLatitudeLongitude);
            print("CenterMercator = " + _myAbsMap.CenterMercator);
            print("");

            _myAbsMap.SetZoom(_myAbsMap.Zoom + 0.1f);
            _myAbsMap.UpdateMap();
            */

            Vector2d myNewCoord = new Vector2d(47.26666667f, 11.35f); // Иннсбрук
            _myAbsMap.UpdateMap(myNewCoord, 12f);
        }
        else if (Input.GetKeyDown("2")) // Переход от плоской карты к объемной
        {
            print(_myAbsMap.Terrain.ElevationType);
            if (_myAbsMap.Terrain.ElevationType != ElevationLayerType.TerrainWithElevation)
            {
                _myAbsMap.Terrain.SetElevationType(ElevationLayerType.TerrainWithElevation);
            }
            else
            {
                _myAbsMap.Terrain.SetElevationType(ElevationLayerType.FlatTerrain);
            }
            print(_myAbsMap.Terrain.ElevationType);
        }
        else if (Input.GetKeyDown("3")) // Изменение масштаба
        {
            float NewZoom = _myAbsMap.Zoom + 0.1f;
            float my2Power = Mathf.Pow(2, NewZoom - _Zoom0);
            float ScaleX = _Scale0.x * my2Power;

            print("Новый масштаб карты: " + NewZoom + " Приращение: " + (NewZoom - _Zoom0) + " 2 в степени = " + my2Power + " Масштаб модели = " + ScaleX);



            _myAbsMap.UpdateMap(NewZoom);
            _UUEE_Surface.localScale = _Scale0 * my2Power;
            _Mortar.localPosition = _MortarPos0 * my2Power;
        }
    }
}
