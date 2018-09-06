namespace Editor
{
    using System.Collections.Generic;

    using Data;

    using Newtonsoft.Json;

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

        private ShipStats GetShipStats()
        {
            ShipStats shipStats = null;


            if (EditorPrefs.HasKey(nameof(ShipStats)))
            {
                var statsString = EditorPrefs.GetString(nameof(Data.ShipStats));
                Debug.Log("LOAD" + statsString);
                shipStats = JsonConvert.DeserializeObject<ShipStats>(statsString);
            }

            if (shipStats == null)
                shipStats = CreateInstance<ShipStats>();

            if (shipStats.BaseShipStats == null)
                shipStats.BaseShipStats = new List<BaseShipStats>();

            if (shipStats.ShipStatsMultipliers == null)
                shipStats.ShipStatsMultipliers = new List<ShipStatsMultipliers>();

            return shipStats;
        }

        private void Load()
        {
            ShipStats = GetShipStats();
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

        private void OnLostFocus()
        {
            Save();
        }

        private void Save()
        {
            EditorPrefs.SetString(nameof(Data.ShipStats), JsonConvert.SerializeObject(ShipStats));
        }
    }
}