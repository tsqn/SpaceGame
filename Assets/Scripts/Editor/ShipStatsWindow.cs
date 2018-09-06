﻿namespace Editor
{
    using System.Collections.Generic;

    using Data;

    using UnityEditor;

    using UnityEngine;

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
        private void Load()
        {
            ShipStats = ShipStats.Instance;
        }

        private void OnEnable()
        {
            Load();
            BaseStatsView = new BaseShipStatsView(ShipStats.BaseShipStats);
            ShipMultipliersView = new ShipStatsMultiplierView(ShipStats.ShipStatsMultipliers);
            
        }

        private void OnGUI()
        {
            BaseStatsView.Show();
            ShipMultipliersView.Show();
        }
    }
}