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
    public GameObject SelectionLight;
    public UnityAction ShipSelected;

    [HideInInspector]
    public string ShipSid;

    public float SpinSpeed;

    public void Deselect()
    {
        SelectionLight.SetActive(false);
    }

    public void Select()
    {
        Settings.Instance.PlayerShipSid = ShipSid;
        SelectionLight.SetActive(true);
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
            Ship.gameObject.transform.Rotate(0, SpinSpeed * Time.deltaTime, 0);
    }
}