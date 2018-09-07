namespace Editor.ShipStatsEditor
{
    using System.Collections.Generic;

    using Data;

    using UnityEditor;

    using UnityEngine;

    public class ShipStatsEditorWindow : EditorWindow
    {
        private static ShipStatsEditorWindow _shipStatsEditorEditorWindow;

        public ShipStats ShipStats;

        public List<ViewModel<BaseShipStats>> BaseShipStatsProperties { get; set; }

        public BaseShipStatsView BaseStatsView { get; set; }

        public ShipStatsMultiplierView ShipMultipliersView { get; set; }

        public List<ViewModel<ShipStatsMultipliers>> ShipStatsMultipliersProperties { get; set; }

        /// <summary>
        /// Отобразить окно.
        /// </summary>
        [MenuItem("Window/Ship Stats Editor")]
        public static void ShowWindow()
        {
            _shipStatsEditorEditorWindow = GetWindow<ShipStatsEditorWindow>(false, "Ship Stats Editor");
            _shipStatsEditorEditorWindow.minSize = new Vector2(300, 80);
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