namespace Editor.LevelEditor
{
    using System.Collections.Generic;

    using Data;

    using Entities;

    using UnityEditor;

    using UnityEngine;

    public class LevelEditorWindow : EditorWindow
    {
        private static LevelEditorWindow _levelEditorWindow;

        /// <summary>
        /// Отобразить окно.
        /// </summary>
        [MenuItem("Window/Level Editor")]
        public static void ShowWindow()
        {
            _levelEditorWindow = GetWindow<LevelEditorWindow>(false, "Level Editor");
            _levelEditorWindow.minSize = new Vector2(300, 80);
        }

        private string _levelNameText;
        private LevelUnitPositions _levelUnitPositions;
        private void OnGUI()
        {
            
            _levelNameText = EditorUtils.ShowLine("New Level Name", _levelNameText);
            if (GUILayout.Button("Create Level"))
            {
                CreateLevel();
            }
            
            _levelUnitPositions = EditorUtils.ShowLine("Level Asset", _levelUnitPositions);
            if (GUILayout.Button("Load Level"))
            {
                LoadLevel();
            }
            
           
            if (GUILayout.Button("Edit Current Level"))
            {
                EditLevel();
            }
        }

        private void EditLevel()
        {
            throw new System.NotImplementedException();
        }

        private void LoadLevel()
        {
            throw new System.NotImplementedException();
        }

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
                    Sid = unit.Sid,
                    PosX = unit.transform.position.x,
                    PosY = unit.transform.position.y,
                    PosZ = unit.transform.position.z,
                    RotX = unit.transform.rotation.x,
                    RotY = unit.transform.rotation.y,
                    RotZ = unit.transform.rotation.z
                });
            }

            AssetDatabase.CreateAsset(levelUnitPositions, EditorUtils.GetUniqueAssetName<LevelUnitPositions>($"{Utils.DataRoot}/levels/{_levelNameText}.asset"));
        }
    }
}