namespace Editor
{
    using System.Collections.Generic;

    using Data;

    using UnityEditor;

    using UnityEngine;

    public class ShipStatsMultiplierView : BaseView<ShipStatsMultipliers>
    {
        private readonly GUIStyle _boxStyle;

        public ShipStatsMultiplierView(List<ShipStatsMultipliers> models) : base(models)
        {
            Models = models;

            _boxStyle = new GUIStyle
            {
                alignment = TextAnchor.MiddleCenter,
                margin = new RectOffset(10, 10, 10, 10),
                border = new RectOffset(5, 5, 5, 5)
            };
        }

        public override void Show()
        {
            Update();
            GUILayout.Box("Ship stats multiplier", _boxStyle);

            if (GUILayout.Button("Add"))
                Properties.Add(new WindowProperty<ShipStatsMultipliers>
                {
                    Model = new ShipStatsMultipliers
                    {
                        Type = $"TYPE M {Properties.Count + 1}"
                    },
                    ActionOnUpdate = ActionOnUpdate.Add
                });

            foreach (var shipStats in Properties)
            {
                ShowProperty(shipStats);
            }
        }

        protected override void Show(ShipStatsMultipliers stats)
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
    }
}