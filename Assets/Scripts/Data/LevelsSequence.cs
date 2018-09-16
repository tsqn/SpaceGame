namespace Data
{
    using System.Collections.Generic;

    using UnityEditor;

    using UnityEngine;

    public class LevelsSequence : ScriptableObject
    {
        private const string ASSET_NAME = "LevelsSequance.asset";
        private static LevelsSequence _instance;

        [SerializeField]
        public List<Level> Levels;

        public static LevelsSequence Instance
        {
            get
            {
                if (_instance != null && _instance.Levels != null)
                    return _instance;

                if (_instance != null)
                    if (_instance.Levels == null)
                        _instance.Levels = new List<Level>();
#if UNITY_EDITOR
                _instance = AssetDatabase.LoadAssetAtPath<LevelsSequence>(LevelsSequencePath);

                if (_instance != null)
                    return _instance;

                _instance = CreateInstance<LevelsSequence>();

                if (_instance.Levels == null)
                    _instance.Levels = new List<Level>();

                AssetDatabase.CreateAsset(_instance, LevelsSequencePath);
#else
                _instance = Resources.Load<LevelsSequence>(LevelsSequencePath);
#endif
                return _instance;
            }
        }

        private static string LevelsSequencePath => $"{Utils.LevelsDataPath}/{ASSET_NAME}";
    }
}