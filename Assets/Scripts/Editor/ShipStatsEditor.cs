namespace Editor
{
    using Data;

    using UnityEditor;

    [CustomEditor(typeof(ShipStats))]
    public class ShipStatsEditor : Editor
    {
        private ShipStats _shipStats;

        public void OnEnable()
        {
//            _shipStats = (ShipStats) target;
//            _shipStats.BaseShipStats = new List<BaseShipStats>
//            {
//                new BaseShipStats { Type = "1" },
//                new BaseShipStats { Type = "2" }
//            };

//            _shipStats.BaseShipStats = new BaseShipStats { Type = "1" };
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();

//            foreach (var VARIABLE in _shipStats.BaseShipStats)
//            {
//                EditorGUILayout.BeginHorizontal();
//                EditorGUILayout.LabelField("Type");
//                EditorGUILayout.TextField(VARIABLE.Type);
//                EditorGUILayout.EndHorizontal();
//            }

            EditorGUILayout.BeginVertical();
        }
    }
}