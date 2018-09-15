﻿using Entities;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Кнопка выбора корабля.
/// </summary>
public class ShipButton : MonoBehaviour
{
    /// <summary>
    /// Контрол кнопки.
    /// </summary>
    [HideInInspector]
    public Button Button;

    /// <summary>
    /// Объект света который подствечивает корабль при выборе.
    /// </summary>
    public GameObject SelectionLight;

    /// <summary>
    /// Корабль.
    /// </summary>
    public Player Ship;

    /// <summary>
    /// Событие при выборе корабля.
    /// </summary>
    public UnityAction ShipSelected;

    /// <summary>
    /// Строковый идентификатор корабля.
    /// </summary>
    [HideInInspector]
    public string ShipSid;

    /// <summary>
    /// Скорость вращения корабля.
    /// </summary>
    public float SpinSpeed;

    /// <summary>
    /// "Развыбирает" корабль.
    /// </summary>
    public void Deselect()
    {
        SelectionLight.SetActive(false);
    }

    /// <summary>
    /// Выбор порабля.
    /// </summary>
    public void Select()
    {
        Settings.Instance.PlayerShipSid = ShipSid;
        SelectionLight.SetActive(true);
        ShipSelected?.Invoke();
    }

    private void Start()
    {
        Ship = GetComponentInChildren<Player>();
        ShipSid = Ship.Sid;
        Button = GetComponent<Button>();
        Button.onClick.AddListener(Select);
    }

    private void Update()
    {
        Rotate();
    }

    /// <summary>
    /// Вращает корабль вокруг своей оси.
    /// </summary>
    private void Rotate()
    {
        Ship.gameObject.transform.Rotate(0, SpinSpeed * Time.deltaTime, 0);
    }
}