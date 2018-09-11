namespace Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// Базовое представление для отображения.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseView<T>
    {
        /// <summary>
        /// Коллекция моделей для отображения.
        /// </summary>
        protected List<T> Models;

        /// <summary>
        /// Коллекция вьюмоделей для отображения.
        /// </summary>
        protected List<ViewModel<T>> Properties;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="models">Список моделей.</param>
        protected BaseView(List<T> models)
        {
            Models = models;

            Properties = models.Select(stats => new ViewModel<T>
            {
                Model = stats
            }).ToList();
        }

        /// <summary>
        /// Метод отображения представления на окне.
        /// </summary>
        public virtual void Show()
        {
            GUILayout.Box("Base view Title");
        }

        /// <summary>
        /// Метод отображения модели.
        /// </summary>
        /// <param name="model"></param>
        protected virtual void ShowModel(T model)
        {
            EditorUtils.ShowLine(nameof(ShipStatsMultipliers.Type), model.ToString());
        }

        /// <summary>
        /// Метод отображения вьюмодели.
        /// </summary>
        /// <param name="property"></param>
        protected void ShowViewModel(ViewModel<T> property)
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(property.Model.ToString()))
                property.IsExpanded = !property.IsExpanded;

            if (GUILayout.Button("DELETE", GUILayout.Width(60)))
                property.ActionOnUpdate = ActionOnUpdate.Delete;

            EditorGUILayout.EndHorizontal();

            if (property.IsExpanded)
                ShowModel(property.Model);
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
            Properties = new List<ViewModel<T>>(
                Models
                    .Select(stats => new ViewModel<T>
                    {
                        Model = stats
                    }).ToList());
        }
    }
}