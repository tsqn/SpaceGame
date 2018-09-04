namespace Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;

    using UnityEditor;

    using UnityEngine;

    public class BaseView<T>
    {
        protected List<T> Models;

        protected List<WindowProperty<T>> Properties;

        public BaseView(List<T> models)
        {
            Models = models;

            Properties = models.Select(stats => new WindowProperty<T>
            {
                Model = stats
            }).ToList();
        }

        public virtual void Show()
        {
            GUILayout.Box("Base view Title");
        }

        protected virtual void Show(T model)
        {
            EditorCommon.ShowLine(nameof(ShipStatsMultipliers.Type), model.ToString());
        }

        protected void ShowProperty(WindowProperty<T> property)
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(property.Model.ToString()))
                property.IsExpanded = !property.IsExpanded;

            if (GUILayout.Button("DELETE", GUILayout.Width(60)))
                property.ActionOnUpdate = ActionOnUpdate.Delete;

            EditorGUILayout.EndHorizontal();

            if (property.IsExpanded)
                Show(property.Model);
        }


        protected void Update()
        {
            var changed = false;

            foreach (var property in Properties)
            {
                switch (property.ActionOnUpdate)
                {
                    case ActionOnUpdate.None:
                        break;
                    case ActionOnUpdate.Add:
                        Models.Add(property.Model);
                        changed = true;
                        break;
                    case ActionOnUpdate.Delete:
                        Models.Remove(property.Model);
                        changed = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (changed)
                UpdateProperties();
        }

        private void UpdateProperties()
        {
            Properties = new List<WindowProperty<T>>(
                Models
                    .Select(stats => new WindowProperty<T>
                    {
                        Model = stats
                    }).ToList());
        }
    }
}