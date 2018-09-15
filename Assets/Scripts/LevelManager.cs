using Data;

using UnityEngine;

/// <summary>
/// Менеджер уровня.
/// </summary>
public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Спрайт бекграунда.
    /// </summary>
    public Sprite Background;

    /// <summary>
    /// Название уровня.
    /// </summary>
    public string Name;

    /// <summary>
    /// Точка спавна игрока.
    /// </summary>
    public PlayerSpawnPoint PlayerSpawnPoint;

    /// <summary>
    /// Расположение юнитов.
    /// </summary>
    public LevelUnitPositions UnitPositions;

    private void Start()
    {
        if (PlayerSpawnPoint == null)
            PlayerSpawnPoint = FindObjectOfType<PlayerSpawnPoint>();

        Name = UnitPositions.Name;
        SceneLoader.LoadScene(UnitPositions);

        var player = UnitsCollection.Instance.GetUnitPrefabBySid(Settings.Instance.PlayerShipSid);
        Instantiate(player, PlayerSpawnPoint.transform.position, PlayerSpawnPoint.transform.rotation);
    }
}