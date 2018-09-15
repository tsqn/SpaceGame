using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class ShipSelector : MonoBehaviour
{
    public List<ShipButton> ShipButtons;

    public void BindEvents()
    {
        ShipButtons.ForEach(button => button.ShipSelected = UpdateSelection);
    }

    private void Start()
    {
        ShipButtons = GetComponentsInChildren<ShipButton>().ToList();
        BindEvents();
        SelectDefaultShip();
    }

    private void SelectDefaultShip()
    {
        ShipButtons.First(button => button.ShipSid == Settings.Instance.PlayerShipSid).Select();
    }

    private void UpdateSelection()
    {
        foreach (var shipButton in ShipButtons)
        {
            if (shipButton.ShipSid != Settings.Instance.PlayerShipSid)
                shipButton.Deselect();
        }
    }
}