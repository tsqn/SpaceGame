namespace Editor.ShipStatsEditor
{
    using System.Collections.Generic;

    using Models;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// Преставление базовых характеристик корабля.
    /// </summary>
    public class UnitBaseAttributesView : BaseView<UnitBaseAttributes>
    {
        public UnitBaseAttributesView(List<UnitBaseAttributes> models) : base(models)
        {
            OrderByName = true;
        }

        /// <inheritdoc />
        public override void Show()
        {
            Update();
            GUILayout.Box("Base ship stats", BoxStyle);
            if (GUILayout.Button("Add"))
                ViewModels.Add(CreateNewView(Utils.ShipStatsDataPath));

            foreach (var viewModel in ViewModels)
            {
                ShowViewModel(viewModel);
            }
        }

        /// <inheritdoc />
        protected override void ShowModel(UnitBaseAttributes model)
        {
            EditorGUILayout.BeginVertical();

            model.Type = EditorUtils.ShowLine(Utils.SplitByCamelCase(nameof(UnitBaseAttributes.Type)), model.Type);

            model.Health = EditorUtils.ShowLine(Utils.SplitByCamelCase(nameof(UnitBaseAttributes.Health)), model.Health);

            model.ShootSpeed = EditorUtils.ShowLine(Utils.SplitByCamelCase(nameof(UnitBaseAttributes.ShootSpeed)), model.ShootSpeed);

            model.MoveSpeed = EditorUtils.ShowLine(Utils.SplitByCamelCase(nameof(UnitBaseAttributes.MoveSpeed)), model.MoveSpeed);

            model.Mobility = EditorUtils.ShowLine(Utils.SplitByCamelCase(nameof(UnitBaseAttributes.Mobility)), model.Mobility);

            EditorGUILayout.EndVertical();
        }
    }
}