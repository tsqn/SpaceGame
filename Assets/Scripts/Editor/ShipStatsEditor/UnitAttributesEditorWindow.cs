namespace Editor.ShipStatsEditor
{
    using System.Linq;

    using Models;

    using UnityEditor;

    using UnityEngine;

    public class UnitAttributesEditorWindow : EditorWindow
    {
        private static UnitAttributesEditorWindow _unitAttributesEditorEditorWindow;

        private Vector2 _scrollPosition;

        public UnitAttributes UnitAttributes;

        public UnitAttributesMultipliersView UnitAttributesMultipliersView { private get; set; }

        public UnitBaseAttributesView UnitBaseAttributesView { private get; set; }


        /// <summary>
        /// Отобразить окно.
        /// </summary>
        [MenuItem("Window/Units Attributes Editor")]
        public static void ShowWindow()
        {
            _unitAttributesEditorEditorWindow = GetWindow<UnitAttributesEditorWindow>(false, "Units Attributes Editor");
            _unitAttributesEditorEditorWindow.minSize = new Vector2(300, 80);
        }

        private void Load()
        {
            UnitAttributes = UnitAttributes.Instance;
        }

        private void OnEnable()
        {
            Load();
            UnitBaseAttributesView = new UnitBaseAttributesView(UnitAttributes.ShipBaseAttributes);
            UnitAttributesMultipliersView = new UnitAttributesMultipliersView(UnitAttributes.ShipAttributesMultipliers);
        }

        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, false, false,
                GUILayout.Width(_unitAttributesEditorEditorWindow.position.width),
                GUILayout.Height(_unitAttributesEditorEditorWindow.position.height));

            UnitBaseAttributesView.Show();
            UnitAttributesMultipliersView.Show();
            EditorGUILayout.EndScrollView();
        }

        private void OnLostFocus()
        {
            foreach (var baseAttributes in UnitAttributes.ShipBaseAttributes)
            {
                var oldFilePath = AssetDatabase.GetAssetPath(baseAttributes);
                var newFilePath =
                    EditorUtils.GetUniqueAssetName<UnitBaseAttributes>(
                        $"{baseAttributes.Type}.asset");

                AssetDatabase.RenameAsset(oldFilePath, newFilePath);
            }

            foreach (var attributesMultipliers in UnitAttributes.ShipAttributesMultipliers)
            {
                var oldFilePath = AssetDatabase.GetAssetPath(attributesMultipliers);
                var newFilePath =
                    EditorUtils.GetUniqueAssetName<UnitBaseAttributes>(
                        $"{attributesMultipliers.Type}.asset");

                AssetDatabase.RenameAsset(oldFilePath, newFilePath);
            }

            UnitAttributes.ShipBaseAttributes =
                UnitAttributes.ShipBaseAttributes.OrderBy(stats => stats.Type).ToList();
            UnitAttributes.ShipAttributesMultipliers =
                UnitAttributes.ShipAttributesMultipliers.OrderBy(stats => stats.Type).ToList();
        }
    }
}