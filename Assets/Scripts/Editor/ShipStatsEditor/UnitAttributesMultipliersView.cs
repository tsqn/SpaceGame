namespace Editor.ShipStatsEditor
{
    using System.Collections.Generic;

    using Models;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// Преставление модификаторов характеристик корабля.
    /// </summary>
    public class UnitAttributesMultipliersView : BaseView<UnitAttributesMultipliers>
    {
        public UnitAttributesMultipliersView(List<UnitAttributesMultipliers> models) : base(models)
        {
            OrderByName = true;
        }

        /// <inheritdoc />
        public override void Show()
        {
            Update();
            GUILayout.Box("Ship stats multiplier", BoxStyle);

            if (GUILayout.Button("Add"))
                ViewModels.Add(CreateNewView(Utils.ShipStatsDataPath));

            foreach (var viewModel in ViewModels)
            {
                ShowViewModel(viewModel);
            }
        }

        /// <inheritdoc />
        protected override void ShowModel(UnitAttributesMultipliers model)
        {
            EditorGUILayout.BeginVertical();

            model.Type = EditorUtils.ShowLine(Utils.SplitByCamelCase(nameof(UnitAttributesMultipliers.Type)), model.Type);

            model.Health = EditorUtils.ShowLine(Utils.SplitByCamelCase(nameof(UnitAttributesMultipliers.Health)), model.Health);

            model.ShootSpeed = EditorUtils.ShowLine(Utils.SplitByCamelCase(nameof(UnitAttributesMultipliers.ShootSpeed)), model.ShootSpeed);

            model.MoveSpeed = EditorUtils.ShowLine(Utils.SplitByCamelCase(nameof(UnitAttributesMultipliers.MoveSpeed)), model.MoveSpeed);

            model.Mobility = EditorUtils.ShowLine(Utils.SplitByCamelCase(nameof(UnitAttributesMultipliers.Mobility)), model.Mobility);

            model.WeaponDamage =
                EditorUtils.ShowLine(Utils.SplitByCamelCase(nameof(UnitAttributesMultipliers.WeaponDamage)), model.WeaponDamage);

            EditorGUILayout.EndVertical();
        }
    }
}