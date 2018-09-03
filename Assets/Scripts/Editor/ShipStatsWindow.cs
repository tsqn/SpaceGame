namespace Editor
{
    using System.Collections.Generic;

    using Data;

    using UnityEditor;

    using UnityEngine;

    [CustomEditor(typeof(ShipStats))]
    public class ShipStatsWindow : EditorWindow
    {
        private static ShipStatsWindow _styleChangerWindow;

        public ShipStats ShipStats;

        public List<WindowProperty<BaseShipStats>> BaseShipStatsProperties { get; set; }

        public BaseShipStatsView BaseStatsView { get; set; }

        public ShipStatsMultiplierView ShipMultipliersView { get; set; }

        public List<WindowProperty<ShipStatsMultipliers>> ShipStatsMultipliersProperties { get; set; }

        /// <summary>
        /// Отобразить окно.
        /// </summary>
        [MenuItem("Window/Ship Stats Editor")]
        public static void ShowWindow()
        {
            _styleChangerWindow = GetWindow<ShipStatsWindow>(false, "Ship Stats Editor");
            _styleChangerWindow.minSize = new Vector2(300, 80);
        }

        private void OnEnable()
        {
            InitializeData();
            BaseStatsView = new BaseShipStatsView(ShipStats.BaseShipStats);

            ShipMultipliersView = new ShipStatsMultiplierView(ShipStats.ShipStatsMultipliers);
        }

        private void InitializeData()
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
        }

        private void OnGUI()
        {
            BaseStatsView.ShowBaseShipStats();
            ShipMultipliersView.Show();
            Debug.Log($"COUNT {ShipStats.ShipStatsMultipliers.Count}");
        }
    }
}