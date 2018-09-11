namespace Editor.ShipStatsEditor
{
    using System.Collections.Generic;

    using Data;

    using UnityEditor;

    using UnityEngine;

    public class ShipStatsEditorWindow : EditorWindow
    {
        private static ShipStatsEditorWindow _shipStatsEditorEditorWindow;
        private Vector2 scrollPosition;

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
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, false, false,
                GUILayout.Width(_shipStatsEditorEditorWindow.position.width), GUILayout.Height(_shipStatsEditorEditorWindow.position.height));

            BaseStatsView.Show();
            ShipMultipliersView.Show();
            EditorGUILayout.EndScrollView();
        }

        private void OnLostFocus()
        {
            foreach (var baseShipStats in ShipStats.BaseShipStats)
            {
                var oldFilePath = AssetDatabase.GetAssetPath(baseShipStats);
                var newFilePath =
                    EditorUtils.GetUniqueAssetName<BaseShipStats>(
                        $"{baseShipStats.Type}.asset");

                AssetDatabase.RenameAsset(oldFilePath, newFilePath);
            }

            foreach (var baseShipStats in ShipStats.ShipStatsMultipliers)
            {
                var oldFilePath = AssetDatabase.GetAssetPath(baseShipStats);
                var newFilePath =
                    EditorUtils.GetUniqueAssetName<BaseShipStats>(
                        $"{baseShipStats.Type}.asset");

                AssetDatabase.RenameAsset(oldFilePath, newFilePath);
            }
        }
    }
}