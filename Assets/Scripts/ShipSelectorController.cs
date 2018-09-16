using System.Collections.Generic;
using System.Linq;

using UnityEngine;

/// <summary>
/// Контроллер элемента выбора корабля.
/// </summary>
public class ShipSelectorController : MonoBehaviour
{
    /// <summary>
    /// Список кнопок выборя корябля.
    /// </summary>
    public List<ShipButton> ShipButtons;

    /// <summary>
    /// Подписка на изменения кнопок.
    /// </summary>
    public void BindEvents()
    {
        ShipButtons.ForEach(button => button.ShipSelected = UpdateSelection);
    }

    /// <summary>
    /// Выбор дефолтного корабля.
    /// </summary>
    private void SelectDefaultShip()
    {
        ShipButtons.First(button => button.ShipSid == Settings.Instance.PlayerShipSid).Select();
    }

    private void Start()
    {
        ShipButtons = GetComponentsInChildren<ShipButton>().ToList();
        BindEvents();
        SelectDefaultShip();
    }

    /// <summary>
    /// Обновление при изменении выбранного корабля.
    /// </summary>
    private void UpdateSelection()
    {
        foreach (var shipButton in ShipButtons)
        {
            if (shipButton.ShipSid != Settings.Instance.PlayerShipSid)
                shipButton.Deselect();
        }
    }
}