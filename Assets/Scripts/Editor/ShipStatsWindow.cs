namespace Editor
{
    using System.Collections.Generic;
    using System.Linq;

    using Data;

    using UnityEditor;

    using UnityEngine;

    [CustomEditor(typeof(ShipStats))]
    public class ShipStatsWindow : EditorWindow
    {
        private static ShipStatsWindow _styleChangerWindow;

        private GUIStyle _boxStyle;

        private bool _show;

        public ShipStats ShipStats;

        public List<WindowProperty<BaseShipStats>> BaseShipStatsProperties; 
        
        public List<WindowProperty<ShipStatsMultipliers>> ShipStatsMultipliersProperties;
        
        /// <summary>
        /// Отобразить окно.
        /// </summary>
        [MenuItem("Window/Ship Stats Editor")]
        public static void ShowWindow()
        {
            _styleChangerWindow = GetWindow<ShipStatsWindow>(false, "Ship Stats Editor");
            _styleChangerWindow.minSize = new Vector2(300, 80);
        }

        private void Awake()
        {
            if (ShipStats == null)
                ShipStats = new ShipStats
                {
                    BaseShipStats = new List<BaseShipStats>
                    {
                        new BaseShipStats
                        {
                            Type = "TYPE 1"
                        },
                        new BaseShipStats
                        {
                            Type = "TYPE 2"
                        }
                    },
                    ShipStatsMultipliers = new List<ShipStatsMultipliers>
                    {
                        new ShipStatsMultipliers
                        {
                            Type = "TYPE M 1"
                        },
                        new ShipStatsMultipliers
                        {
                            Type = "TYPE M 2"
                        }
                    }
                };

            BaseShipStatsProperties = ShipStats.BaseShipStats.Select(stats => new WindowProperty<BaseShipStats>()
            {
                Model = stats
            }).ToList();
            
            ShipStatsMultipliersProperties = ShipStats.ShipStatsMultipliers.Select(stats => new WindowProperty<ShipStatsMultipliers>()
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

        private void OnGUI()
        {
            
            DeleteShipStats();
            ShowBaseShipStats();
            ShowShipStatsMultiplier();
        }

        private void Show(BaseShipStats stats)
        {
            EditorGUILayout.BeginVertical();
            ShowLine(nameof(BaseShipStats.Type), stats.Type);

            ShowLine(nameof(BaseShipStats.Health), stats.Health.ToString());

            ShowLine(nameof(BaseShipStats.ShootSpeed), stats.ShootSpeed.ToString());

            ShowLine(nameof(BaseShipStats.MoveSpeed), stats.MoveSpeed.ToString());

            ShowLine(nameof(BaseShipStats.Mobility), stats.Mobility.ToString());

            var multipliers = stats as ShipStatsMultipliers;

            if (multipliers != null)
                ShowLine(nameof(ShipStatsMultipliers.WeaponDamage), multipliers.WeaponDamage.ToString());

            EditorGUILayout.EndVertical();
        }

        private void ShowBaseShipStats()
        {
            GUILayout.Box("Base ship stats", _boxStyle);
            if (GUILayout.Button("Add"))
                ShipStats.BaseShipStats.Add(new BaseShipStats
                {
                    Type = $"TYPE {ShipStats.BaseShipStats.Count + 1}"
                });

            foreach (var shipStats in BaseShipStatsProperties)
            {
                ShowProperty(shipStats);
            }
        }

        private static void ShowLine(string labelText, string valueText)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(labelText);
            EditorGUILayout.TextField(valueText);

            EditorGUILayout.EndHorizontal();
        }

        private BaseShipStats _shipStatsToDelete;
        
        private void ShowProperty<T>(T stats) where T : WindowProperty<BaseShipStats>
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(stats.Model.Type))
                stats.IsExpanded = ! stats.IsExpanded;

            if (GUILayout.Button("DELETE", GUILayout.Width(60)))
            {
                _shipStatsToDelete = stats.Model;

            }

            EditorGUILayout.EndHorizontal();

            if ( stats.IsExpanded)
                Show(stats.Model);
        }

        private void DeleteShipStats()
        {
            if (_shipStatsToDelete == null)
                return;
            var multipliers = _shipStatsToDelete as ShipStatsMultipliers;
            if (multipliers != null)
                ShipStats.ShipStatsMultipliers.Remove(multipliers);
            else
                ShipStats.BaseShipStats.Remove(_shipStatsToDelete);
        }

        private void ShowShipStatsMultiplier()
        {
            GUILayout.Box("Ship stats multiplier", _boxStyle);

            if (GUILayout.Button("Add"))
                ShipStats.ShipStatsMultipliers.Add(new ShipStatsMultipliers
                {
                    Type = $"TYPE M {ShipStats.BaseShipStats.Count + 1}"
                });

            foreach (var shipStats in ShipStatsMultipliersProperties)
            {
                ShowProperty(shipStats);
            }
        }

        public class WindowProperty<T>
        {
            public bool IsExpanded { get; set; }
            
            public T Model { get; set; }
        }
    }
}