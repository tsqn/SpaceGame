using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class ShipSelector : MonoBehaviour
{
    public List<ShipButton> ShipButtons;

    public void BindEvents()
    {
        ShipButtons.ForEach(button => button.ShipSelected = UpdateShips);
    }

    private void Start()
    {
        ShipButtons = GetComponentsInChildren<ShipButton>().ToList();
        BindEvents();
    }

    private void UpdateShips()
    {
        foreach (var shipButton in ShipButtons)
        {
            if (shipButton.ShipSid != Settings.Instance.PlayerShipSid)
                shipButton.Deselect();
        }
    }
}