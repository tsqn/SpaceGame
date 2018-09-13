namespace Editor.LevelEditor
{
    using System;
    using System.Collections.Generic;

    using Data;

    using Entities;

    using UnityEditor;

    using UnityEngine;

    public class LevelEditorWindow : EditorWindow
    {
        private static LevelEditorWindow _levelEditorWindow;

        private string _levelNameText;

        private LevelUnitPositions _levelUnitPositions;

        /// <summary>
        /// Отобразить окно.
        /// </summary>
        [MenuItem("Window/Level Editor")]
        public static void ShowWindow()
        {
            _levelEditorWindow = GetWindow<LevelEditorWindow>(false, "Level Editor");
            _levelEditorWindow.minSize = new Vector2(300, 80);
        }

        /// <summary>
        /// Создает новый объект уровня.
        /// </summary>
        private void CreateLevel()
        {
            var units = FindObjectsOfType<Unit>();

            var levelUnitPositions = CreateInstance<LevelUnitPositions>();
            levelUnitPositions.Name = _levelNameText;

            levelUnitPositions.UnitPositionModels = new List<UnitPositionModel>();
            foreach (var unit in units)
            {
                levelUnitPositions.UnitPositionModels.Add(new UnitPositionModel
                {
                    GameObject = unit.GetPrefab(),
                    Rotation = unit.transform.rotation,
                    Position = unit.transform.position
                });
            }

            AssetDatabase.CreateAsset(levelUnitPositions,
                EditorUtils.GetUniqueAssetName<LevelUnitPositions>($"{Utils.DataRoot}/levels/{_levelNameText}.asset"));
        }

        /// <summary>
        /// Редактирование уровня.
        /// </summary>
        private void EditLevel()
        {
            var units = FindObjectsOfType<Unit>();
            _levelUnitPositions.Name = _levelNameText;
            _levelUnitPositions.UnitPositionModels = new List<UnitPositionModel>();
            foreach (var unit in units)
            {
                _levelUnitPositions.UnitPositionModels.Add(new UnitPositionModel
                {
                    GameObject = unit.GetPrefab(),
                    Rotation = unit.transform.rotation,
                    Position = unit.transform.position
                });
            }

            var oldFilePath = AssetDatabase.GetAssetPath(_levelUnitPositions);

            AssetDatabase.RenameAsset(oldFilePath, _levelNameText);
        }

        /// <summary>
        /// Загрузка указанного уровня.
        /// </summary>
        private void LoadLevel()
        {
            SceneLoader.LoadScene(_levelUnitPositions);
            _levelNameText = _levelUnitPositions.Name;
        }

        private void OnGUI()
        {
            _levelNameText = EditorUtils.ShowLine("New Level Name", _levelNameText);
            if (GUILayout.Button("Create Level"))
                CreateLevel();

            _levelUnitPositions = EditorUtils.ShowLine("Level Asset", _levelUnitPositions);
            if (GUILayout.Button("Load Level"))
                LoadLevel();


            if (GUILayout.Button("Edit Current Level"))
                EditLevel();
        }
    }
}