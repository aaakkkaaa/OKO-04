using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sChangeScale : MonoBehaviour
{

    // Класс, содержащий общие параметры
    sCommonParameters ComPars;

    // Start is called before the first frame update
    void Start()
    {
        ComPars = gameObject.GetComponent<sCommonParameters>();
    }

    // Update is called once per frame
    void Update()
    {
        // Увеличение масштаба
        if (Input.GetKeyDown("m"))
        {
            float NewZoom = ComPars.GetZoom() + 0.1f;
            float my2Power = Mathf.Pow(2, NewZoom - _Zoom0);
            float ScaleX = _Scale0.x * my2Power;

            print("Новый масштаб карты: " + NewZoom + " Приращение: " + (NewZoom - _Zoom0) + " 2 в степени = " + my2Power + " Масштаб модели = " + ScaleX);
        }
    }

    private void SetScale(float newZoom, Vector3 NewGlobalScale)
    {
        ComPars.SetZoom(newZoom);
    }
}
