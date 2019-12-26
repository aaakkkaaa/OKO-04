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

    // ===========================================================================================
    // Параметры положения и перемещения ступы с наблюдателем, которые меняются  при изменении глобального масштаба
    // ===========================================================================================

    // Класс, управляющий перемещением ступы
    sMortarMovement _mortarMovement;

    // Начальные скорости перемещений ступы
    float _MortarPanSpeed0;
    float _MortarVertSpeed0;
    // Начальные ограничения высоты перемещений ступы
    float _MortarHeightMin0;
    float _MortarHeightMax0;

    // Стандартные положения ступы

    // Начальное положение
    Vector3 _MortarHomePos0;
    // Положение "на вышке"
    Vector3 _MortarTowerPos0;
    // Положение "на хвосте" - локальный сдвиг относительно самолета-носителя
    Vector3 _MortarTailPos0;

    // ===========================================================================================


    // Start is called before the first frame update
    void Start()
    {
        // Класс, содержащий общие параметры и методы для работы с ними
        _ComPars = gameObject.GetComponent<sCommonParameters>();

        // Начальные значения параметров положения и перемещения ступы с наблюдателем
        // Скорости перемещений
        _MortarPanSpeed0 = _ComPars.MortarPanSpeed;
        _MortarVertSpeed0 = _ComPars.MortarVertSpeed;
        // Начальные ограничения высоты перемещений
        _MortarHeightMin0 = _ComPars.MortarHeightMin;
        _MortarHeightMax0 = _ComPars.MortarHeightMax;
        // Начальное положение
        _MortarHomePos0 = _ComPars.MortarHomePos;
        // Положение "на вышке"
        _MortarTowerPos0 = _ComPars.MortarTowerPos;
        // Положение "на хвосте" - локальный сдвиг относительно самолета-носителя
        _MortarTailPos0 = _ComPars.MortarTailPos;

        // Установить текушие параметрs положения и перемещения ступы с наблюдателем с учетом начального глобального масштаба
        SetMortarSpeedAndRestrictions(_ComPars.WorldScale0);

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

        // Если новый масштаб карты установлен успешно (ограничения min и max)
        if (_ComPars.SetZoom(newZoom))
        {
            // Новый глобальный масштаб
            _ComPars.WorldScale = _ComPars.WorldScale0 * twoInPowerIncr;

            // Установить текушие скорости перемещений и ограничения высоты ступы с наблюдателем с учетом глобального масштаба
            SetMortarSpeedAndRestrictions(_ComPars.WorldScale);

            // Mасштабирование
            _UUEE_Surface.localScale = _ComPars.WorldScale;
            // Позиционироание
            _Mortar.localPosition = _Mortar.localPosition / PreviousTwoInPower * twoInPowerIncr;
        }

        print("Новый масштаб карты: " + newZoom + " Приращение: " + increment + " 2 в степени = " + twoInPowerIncr + " Масштаб моделей = " + _ComPars.WorldScale.x);
    }

    // Установить скорости перемещений и ограничения высоты ступы с наблюдателем с учетом глобального масштаба
    private void SetMortarSpeedAndRestrictions(Vector3 Scale)
    {
        // Скорости перемещений
        _ComPars.MortarPanSpeed = _MortarPanSpeed0 * Scale.x;
        _ComPars.MortarVertSpeed = _MortarVertSpeed0 * Scale.y;

        // Ограничения высоты перемещений
        _ComPars.MortarHeightMin = _MortarHeightMin0 * Scale.y;
        _ComPars.MortarHeightMax = _MortarHeightMax0 * Scale.y;

        // Начальное положение
        _ComPars.MortarHomePos = new Vector3(_MortarHomePos0.x * Scale.x, _MortarHomePos0.y * Scale.y, _MortarHomePos0.z * Scale.z);
        // Положение "на вышке"
        _ComPars.MortarTowerPos = new Vector3(_MortarTowerPos0.x * Scale.x, _MortarTowerPos0.y * Scale.y, _MortarTowerPos0.z * Scale.z);
        // Положение "на хвосте" - локальный сдвиг относительно самолета-носителя
        _ComPars.MortarTailPos = new Vector3(_MortarTailPos0.x * Scale.x, _MortarTailPos0.y * Scale.y, _MortarTailPos0.z * Scale.z);

    }
}
