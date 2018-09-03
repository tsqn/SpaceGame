namespace Editor
{
    using System.Collections.Generic;
    using System.Linq;

    using Data;

    using UnityEditor;

    using UnityEngine;

    public class BaseShipStatsView
    {
        private readonly GUIStyle _boxStyle;

        private List<BaseShipStats> _baseShipStats;
        
        private List<WindowProperty<BaseShipStats>> _baseShipStatsProperties;

        public BaseShipStatsView(List<BaseShipStats> baseShipStats)
        {
            _baseShipStatsProperties = baseShipStats.Select(stats => new WindowProperty<BaseShipStats>
            {
                Model = stats
            }).ToList();
            
            _boxStyle = new GUIStyle
            {
                alignment = TextAnchor.MiddleCenter,
                margin = new RectOffset(10, 10, 10, 10),
                border = new RectOffset(5, 5, 5, 5)
            };
        }

        public void ShowBaseShipStats()
        {
            GUILayout.Box("Base ship stats", _boxStyle);
            if (GUILayout.Button("Add"))
                _baseShipStatsProperties.Add(new WindowProperty<BaseShipStats>
                {
                    Model = new BaseShipStats
                    {
                        Type = $"TYPE {_baseShipStatsProperties.Count + 1}"
                    }
                });
            
            foreach (var shipStats in _baseShipStatsProperties)
            {
                ShowProperty(shipStats);
            }
        }

        private void Show(BaseShipStats stats)
        {
            EditorGUILayout.BeginVertical();

            EditorCommon.ShowLine(nameof(Data.BaseShipStats.Type), stats.Type);

            EditorCommon.ShowLine(nameof(Data.BaseShipStats.Health), stats.Health.ToString());

            EditorCommon.ShowLine(nameof(Data.BaseShipStats.ShootSpeed), stats.ShootSpeed.ToString());

            EditorCommon.ShowLine(nameof(Data.BaseShipStats.MoveSpeed), stats.MoveSpeed.ToString());

            EditorCommon.ShowLine(nameof(Data.BaseShipStats.Mobility), stats.Mobility.ToString());

            EditorGUILayout.EndVertical();
        }


        private void ShowProperty(WindowProperty<BaseShipStats> property)
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(property.Model.Type))
                property.IsExpanded = !property.IsExpanded;

            if (GUILayout.Button("DELETE", GUILayout.Width(60)))
                _baseShipStatsProperties.Remove(property);

            EditorGUILayout.EndHorizontal();

            if (property.IsExpanded)
                Show(property.Model);
        }
        
        private void UpdateProperties()
        {
            _baseShipStatsProperties = new List<WindowProperty<BaseShipStats>>(
                _baseShipStats
                    .Select(stats => new WindowProperty<BaseShipStats>
                    {
                        Model = stats
                    }).ToList());
        }
    }
}