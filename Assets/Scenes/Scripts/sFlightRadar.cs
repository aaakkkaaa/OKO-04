using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sFlightRadar : MonoBehaviour
{

    // Класс, содержащий общие параметры и методы для работы с ними
    private sCommonParameters _ComPars;

    // Объекты, подлежащие масштабированию при изменении WorldScale
    [SerializeField] private Transform _UUEE_Surface; // Дороги, ВПП и рулежки аэропорта Шеререметьево

    // Объекты, подлежащие позиционированию при изменении WorldScale
    [SerializeField] private Transform _Mortar; // Ступа с наблюдателем

    // Start is called before the first frame update
    void Start()
    {
        // Класс, содержащий общие параметры и методы для работы с ними
        _ComPars = gameObject.GetComponent<sCommonParameters>();
    }

    // Update is called once per frame
    void Update()
    {
        // Увеличение масштаба карты на один шаг (0.1f)
        if (Input.GetKeyDown("m"))
        {
            SetScale(_ComPars.GetZoom() + 0.1f);
        }
        // Уменьшение масштаба карты на один шаг (0.1f)
        else if (Input.GetKeyDown("l"))
        {
            SetScale(_ComPars.GetZoom() - 0.1f);

        }
    }

    // Изменение глобального масшаба в зависимости от новой величины масшаба карты MapBox (Map/AbstractMap/GENERAL/Zoom)
    private void SetScale(float newZoom)
    {

        //  Округлим новый масштаб карты до десятых
        newZoom = (float)Math.Round(newZoom, 1, MidpointRounding.AwayFromZero);

        // Приращение масштаба карты по сравнению с первоначальным масштабом (тоже округлим до десятых).
        float increment = (float)Math.Round((newZoom - _ComPars.MapZoom0), 1, MidpointRounding.AwayFromZero);
        // Новый коэффициент для глобального масштаба (от начального масштаба WorldScale0) равен 2 в степени Приращения
        float twoInPowerIncr = Mathf.Pow(2, increment);
        // Предыдущий коэффициент для глобального масштаба
        float PreviousTwoInPower = Mathf.Pow(2, (_ComPars.GetZoom() - _ComPars.MapZoom0));

        // Новый глобальный масштаб
        _ComPars.WorldScale = _ComPars.WorldScale0 * twoInPowerIncr;

        // Если новый масштаб карты установлен успешно (ограничения min и max)
        if (_ComPars.SetZoom(newZoom))
        {
            // Mасштабирование
            _UUEE_Surface.localScale = _ComPars.WorldScale;
            // Позиционироание
            _Mortar.localPosition = _Mortar.localPosition / PreviousTwoInPower * twoInPowerIncr;
        }

        print("Новый масштаб карты: " + newZoom + " Приращение: " + increment + " 2 в степени = " + twoInPowerIncr + " Масштаб моделей = " + _ComPars.WorldScale.x);
    }
}
