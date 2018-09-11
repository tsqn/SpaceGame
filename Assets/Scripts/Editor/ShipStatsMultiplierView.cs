namespace Editor
{
    using System.Collections.Generic;

    using Data;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// Преставление модификаторов характеристик корабля.
    /// </summary>
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

        /// <inheritdoc />
        public override void Show()
        {
            Update();
            GUILayout.Box("Ship stats multiplier", _boxStyle);

            if (GUILayout.Button("Add"))
                Properties.Add(CreateNewModel());

            foreach (var shipStats in Properties)
            {
                ShowViewModel(shipStats);
            }
        }
        
        /// <summary>
        /// Возвращает новую вьюмодель с базовыми значениями.
        /// </summary>
        private static ViewModel<ShipStatsMultipliers> CreateNewModel()
        {
            var windowProperty = new ViewModel<ShipStatsMultipliers>
            {
                Model = ScriptableObject.CreateInstance<ShipStatsMultipliers>(),
                ActionOnUpdate = ActionOnUpdate.Add
            };
            windowProperty.Model.Type = $"New";
            return windowProperty;
        }

        /// <inheritdoc />
        protected override void ShowModel(ShipStatsMultipliers model)
        {
            EditorGUILayout.BeginVertical();

            model.Type = EditorUtils.ShowLine(nameof(ShipStatsMultipliers.Type), model.Type);

            model.Health = EditorUtils.ShowLine(nameof(ShipStatsMultipliers.Health), model.Health);

            model.ShootSpeed = EditorUtils.ShowLine(nameof(ShipStatsMultipliers.ShootSpeed), model.ShootSpeed);

            model.MoveSpeed = EditorUtils.ShowLine(nameof(ShipStatsMultipliers.MoveSpeed), model.MoveSpeed);

            model.Mobility = EditorUtils.ShowLine(nameof(ShipStatsMultipliers.Mobility), model.Mobility);

            model.WeaponDamage = EditorUtils.ShowLine(nameof(ShipStatsMultipliers.WeaponDamage), model.WeaponDamage);

            EditorGUILayout.EndVertical();
        }
    }
}