using System;

using Entities;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShipButton : MonoBehaviour
{
    [HideInInspector]
    public Button Button;

    public Player Ship;

    public UnityAction ShipSelected;

    private bool _selected;
    
    [HideInInspector]
    public string ShipSid;

    public float SpinSpeed;

    public void Deselect()
    {
        _selected = false;
    }

    public void Select()
    {
        Settings.Instance.PlayerShipSid = ShipSid;
        _selected = true;
        ShipSelected?.Invoke();

    }

    // Use this for initialization
    private void Start()
    {
        Ship = GetComponentInChildren<Player>();
        ShipSid = Ship.Sid;
        Button = GetComponent<Button>();
        Button.onClick.AddListener(Select);
    }

    private void Update()
    {
        if (_selected)
            Ship.gameObject.transform.Rotate(0, SpinSpeed * Time.deltaTime, 0);
    }
}