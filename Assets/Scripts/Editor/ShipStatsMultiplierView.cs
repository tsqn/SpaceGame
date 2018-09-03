namespace Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;

    using UnityEditor;

    using UnityEngine;

    public class ShipStatsMultiplierView
    {
        private readonly List<ShipStatsMultipliers> _baseShipStats;
        
        private readonly GUIStyle _boxStyle;

        private List<BaseShipStats> _collection;

        private List<WindowProperty<ShipStatsMultipliers>> _shipStatsMultipliersProperties;


        public ShipStatsMultiplierView(List<ShipStatsMultipliers> baseShipStats)
        {
            _baseShipStats = baseShipStats;
            UpdateProperties();

            _boxStyle = new GUIStyle
            {
                alignment = TextAnchor.MiddleCenter,
                margin = new RectOffset(10, 10, 10, 10),
                border = new RectOffset(5, 5, 5, 5)
            };
        }

        public void Show()
        {
            Update();
            GUILayout.Box("Ship stats multiplier", _boxStyle);

            if (GUILayout.Button("Add"))
                _shipStatsMultipliersProperties.Add(new WindowProperty<ShipStatsMultipliers>
                {
                    Model = new ShipStatsMultipliers
                    {
                        Type = $"TYPE M {_shipStatsMultipliersProperties.Count + 1}"
                    },
                    ActionOnUpdate = ActionOnUpdate.Add
                });

            foreach (var shipStats in _shipStatsMultipliersProperties)
            {
                ShowProperty(shipStats);
            }
        }


        private void Show(ShipStatsMultipliers stats)
        {
            EditorGUILayout.BeginVertical();

            EditorCommon.ShowLine(nameof(ShipStatsMultipliers.Type), stats.Type);

            EditorCommon.ShowLine(nameof(ShipStatsMultipliers.Health), stats.Health.ToString());

            EditorCommon.ShowLine(nameof(ShipStatsMultipliers.ShootSpeed), stats.ShootSpeed.ToString());

            EditorCommon.ShowLine(nameof(ShipStatsMultipliers.MoveSpeed), stats.MoveSpeed.ToString());

            EditorCommon.ShowLine(nameof(ShipStatsMultipliers.Mobility), stats.Mobility.ToString());

            EditorCommon.ShowLine(nameof(ShipStatsMultipliers.WeaponDamage), stats.WeaponDamage.ToString());

            EditorGUILayout.EndVertical();
        }

        private void ShowProperty(WindowProperty<ShipStatsMultipliers> property)
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(property.Model.Type))
                property.IsExpanded = !property.IsExpanded;

            if (GUILayout.Button("DELETE", GUILayout.Width(60)))
                property.ActionOnUpdate = ActionOnUpdate.Delete;

            EditorGUILayout.EndHorizontal();

            if (property.IsExpanded)
                Show(property.Model);
        }

        private void Update()
        {
            var changed = false;

            foreach (var property in _shipStatsMultipliersProperties)
            {
                switch (property.ActionOnUpdate)
                {
                    case ActionOnUpdate.None:
                        break;
                    case ActionOnUpdate.Add:
                        _baseShipStats.Add(property.Model);
                        changed = true;
                        break;
                    case ActionOnUpdate.Delete:
                        _baseShipStats.Remove(property.Model);
                        changed = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (changed)
                UpdateProperties();
        }

        private void UpdateProperties()
        {
            _shipStatsMultipliersProperties = new List<WindowProperty<ShipStatsMultipliers>>(
                _baseShipStats
                    .Select(stats => new WindowProperty<ShipStatsMultipliers>
                    {
                        Model = stats
                    }).ToList());
        }
    }
}