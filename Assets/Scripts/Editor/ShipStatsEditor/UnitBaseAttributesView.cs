namespace Editor.ShipStatsEditor
{
    using System.Collections.Generic;

    using Data;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// Преставление базовых характеристик корабля.
    /// </summary>
    public class UnitBaseAttributesView : BaseView<UnitBaseAttributes>
    {
        private readonly GUIStyle _boxStyle;

        public UnitBaseAttributesView(List<UnitBaseAttributes> models) : base(models)
        {
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
            GUILayout.Box("Base ship stats", _boxStyle);
            if (GUILayout.Button("Add"))
                ViewModels.Add(CreateNewView());

            foreach (var viewModel in ViewModels)
            {
                ShowViewModel(viewModel);
            }
        }

        /// <inheritdoc />
        protected override void ShowModel(UnitBaseAttributes model)
        {
            EditorGUILayout.BeginVertical();

            model.Type = EditorUtils.ShowLine(nameof(UnitBaseAttributes.Type), model.Type);

            model.Health = EditorUtils.ShowLine(nameof(UnitBaseAttributes.Health), model.Health);

            model.ShootSpeed = EditorUtils.ShowLine(nameof(UnitBaseAttributes.ShootSpeed), model.ShootSpeed);

            model.MoveSpeed = EditorUtils.ShowLine(nameof(UnitBaseAttributes.MoveSpeed), model.MoveSpeed);

            model.Mobility = EditorUtils.ShowLine(nameof(UnitBaseAttributes.Mobility), model.Mobility);

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Возвращает новую вьюмодель с базовыми значениями.
        /// </summary>
        private static ViewModel<UnitBaseAttributes> CreateNewView()
        {
            var unitBaseAttributes = ScriptableObject.CreateInstance<UnitBaseAttributes>();
            const string defaultName = "New";
            var assetPath = $"{Utils.ShipStatsDataPath}/{defaultName}.asset";

            AssetDatabase.CreateAsset(unitBaseAttributes,
                EditorUtils.GetUniqueAssetName<UnitBaseAttributes>(assetPath));

            var windowProperty = new ViewModel<UnitBaseAttributes>
            {
                Model = unitBaseAttributes,
                ActionOnUpdate = ActionOnUpdate.Add
            };

            windowProperty.Model.Type = defaultName;
            return windowProperty;
        }
    }
}