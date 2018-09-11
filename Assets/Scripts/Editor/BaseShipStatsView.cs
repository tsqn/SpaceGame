namespace Editor
{
    using System.Collections.Generic;

    using Data;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// Преставление базовых характеристик корабля.
    /// </summary>
    public class BaseShipStatsView : BaseView<BaseShipStats>
    {
        private readonly GUIStyle _boxStyle;
      
        public BaseShipStatsView(List<BaseShipStats> models) : base(models)
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
                Properties.Add(CreateNewView());

            foreach (var shipStats in Properties)
            {
                ShowViewModel(shipStats);
            }
        }

        /// <summary>
        /// Возвращает новую вьюмодель с базовыми значениями.
        /// </summary>
        private static ViewModel<BaseShipStats> CreateNewView()
        {
            var windowProperty = new ViewModel<BaseShipStats>
            {
                Model = ScriptableObject.CreateInstance<BaseShipStats>(),
                ActionOnUpdate = ActionOnUpdate.Add
            };
            windowProperty.Model.Type = $"New";
            
            return windowProperty;
        }

        /// <inheritdoc />
        protected override void ShowModel(BaseShipStats model)
        {
            EditorGUILayout.BeginVertical();

            model.Type = EditorUtils.ShowLine(nameof(BaseShipStats.Type), model.Type);

            model.Health = EditorUtils.ShowLine(nameof(BaseShipStats.Health), model.Health);

            model.ShootSpeed = EditorUtils.ShowLine(nameof(BaseShipStats.ShootSpeed), model.ShootSpeed);

            model.MoveSpeed = EditorUtils.ShowLine(nameof(BaseShipStats.MoveSpeed), model.MoveSpeed);

            model.Mobility = EditorUtils.ShowLine(nameof(BaseShipStats.Mobility), model.Mobility);

            EditorGUILayout.EndVertical();
        }
    }
}