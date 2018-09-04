namespace Editor
{
    using System.Collections.Generic;

    using Data;

    using UnityEditor;

    using UnityEngine;

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

        public override void Show()
        {
            Update();
            GUILayout.Box("Base ship stats", _boxStyle);
            if (GUILayout.Button("Add"))
                Properties.Add(new WindowProperty<BaseShipStats>
                {
                    Model = new BaseShipStats
                    {
                        Type = $"TYPE {Properties.Count + 1}"
                    },
                    ActionOnUpdate = ActionOnUpdate.Add
                });

            foreach (var shipStats in Properties)
            {
                ShowProperty(shipStats);
            }
        }

        protected override void Show(BaseShipStats model)
        {
            EditorGUILayout.BeginVertical();

            EditorCommon.ShowLine(nameof(BaseShipStats.Type), model.Type);

            EditorCommon.ShowLine(nameof(BaseShipStats.Health), model.Health);

            EditorCommon.ShowLine(nameof(BaseShipStats.ShootSpeed), model.ShootSpeed);

            EditorCommon.ShowLine(nameof(BaseShipStats.MoveSpeed), model.MoveSpeed);

            EditorCommon.ShowLine(nameof(BaseShipStats.Mobility), model.Mobility);

            EditorGUILayout.EndVertical();
        }
    }
}