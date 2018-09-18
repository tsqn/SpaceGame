namespace Editor.LevelsManager
{
    using System.Collections.Generic;

    using Models;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// Представление редактора уровней.
    /// </summary>
    public class LevelsManagerView : BaseView<Level>
    {
        public LevelsManagerView(List<Level> models) : base(models)
        {
            PositionChanging = true;
        }

        /// <inheritdoc />
        public override void Show()
        {
            Update();
            GUILayout.Box("Levels Manager", BoxStyle);

            if (GUILayout.Button("Add"))
                ViewModels.Add(CreateNewView(Utils.LevelsDataPath));

            foreach (var viewModel in ViewModels)
            {
                ShowViewModel(viewModel, viewModel.Model.Name);
            }
        }

        /// <inheritdoc />
        protected override void ShowModel(Level model)
        {
            EditorGUILayout.BeginVertical();

            model.Name = EditorUtils.ShowLine(Utils.SplitByCamelCase(nameof(Level.Name)), model.Name);

            model.UnitPositions =
                EditorUtils.ShowLine(Utils.SplitByCamelCase(nameof(Level.UnitPositions)), model.UnitPositions);

            model.Background = EditorUtils.ShowLine(Utils.SplitByCamelCase(nameof(Level.Background)), model.Background);

            EditorGUILayout.EndVertical();
        }
    }
}