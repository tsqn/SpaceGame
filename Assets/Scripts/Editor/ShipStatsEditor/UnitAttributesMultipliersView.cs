namespace Editor.ShipStatsEditor
{
    using System.Collections.Generic;

    using Data;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// Преставление модификаторов характеристик корабля.
    /// </summary>
    public class UnitAttributesMultipliersView : BaseView<UnitAttributesMultipliers>
    {
        private readonly GUIStyle _boxStyle;

        public UnitAttributesMultipliersView(List<UnitAttributesMultipliers> models) : base(models)
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
                ViewModels.Add(CreateNewModel());

            foreach (var shipStats in ViewModels)
            {
                ShowViewModel(shipStats);
            }
        }

        /// <inheritdoc />
        protected override void ShowModel(UnitAttributesMultipliers model)
        {
            EditorGUILayout.BeginVertical();

            model.Type = EditorUtils.ShowLine(nameof(UnitAttributesMultipliers.Type), model.Type);

            model.Health = EditorUtils.ShowLine(nameof(UnitAttributesMultipliers.Health), model.Health);

            model.ShootSpeed = EditorUtils.ShowLine(nameof(UnitAttributesMultipliers.ShootSpeed), model.ShootSpeed);

            model.MoveSpeed = EditorUtils.ShowLine(nameof(UnitAttributesMultipliers.MoveSpeed), model.MoveSpeed);

            model.Mobility = EditorUtils.ShowLine(nameof(UnitAttributesMultipliers.Mobility), model.Mobility);

            model.WeaponDamage =
                EditorUtils.ShowLine(nameof(UnitAttributesMultipliers.WeaponDamage), model.WeaponDamage);

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Возвращает новую вьюмодель с базовыми значениями.
        /// </summary>
        private static ViewModel<UnitAttributesMultipliers> CreateNewModel()
        {
            var unitAttributesMultipliers = ScriptableObject.CreateInstance<UnitAttributesMultipliers>();
            const string defaultName = "New";
            var assetPath = $"{Utils.ShipStatsDataPath}/{defaultName}.asset";

            AssetDatabase.CreateAsset(unitAttributesMultipliers,
                EditorUtils.GetUniqueAssetName<UnitAttributesMultipliers>(assetPath));

            var windowProperty = new ViewModel<UnitAttributesMultipliers>
            {
                Model = unitAttributesMultipliers,
                ActionOnUpdate = ActionOnUpdate.Add
            };

            windowProperty.Model.Type = defaultName;
            return windowProperty;
        }
    }
}