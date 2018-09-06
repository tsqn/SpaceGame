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
                Properties.Add(CreateNewModel());

            foreach (var shipStats in Properties)
            {
                ShowProperty(shipStats);
            }
        }
        
        private static WindowProperty<ShipStatsMultipliers> CreateNewModel()
        {
            var windowProperty = new WindowProperty<ShipStatsMultipliers>
            {
                Model = ScriptableObject.CreateInstance<ShipStatsMultipliers>(),
                ActionOnUpdate = ActionOnUpdate.Add
            };
            windowProperty.Model.Type = $"New";
            return windowProperty;
        }

        protected override void Show(ShipStatsMultipliers model)
        {
            EditorGUILayout.BeginVertical();

            model.Type = EditorCommon.ShowLine(nameof(ShipStatsMultipliers.Type), model.Type);

            model.Health = EditorCommon.ShowLine(nameof(ShipStatsMultipliers.Health), model.Health);

            model.ShootSpeed = EditorCommon.ShowLine(nameof(ShipStatsMultipliers.ShootSpeed), model.ShootSpeed);

            model.MoveSpeed = EditorCommon.ShowLine(nameof(ShipStatsMultipliers.MoveSpeed), model.MoveSpeed);

            model.Mobility = EditorCommon.ShowLine(nameof(ShipStatsMultipliers.Mobility), model.Mobility);

            model.WeaponDamage = EditorCommon.ShowLine(nameof(ShipStatsMultipliers.WeaponDamage), model.WeaponDamage);

            EditorGUILayout.EndVertical();
        }
    }
}