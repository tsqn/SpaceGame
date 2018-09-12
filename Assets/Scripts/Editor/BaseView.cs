namespace Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;

    using UnityEditor;

    using UnityEngine;

    using Object = UnityEngine.Object;

    /// <summary>
    /// Базовое представление для отображения.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseView<T> where T : Object
    {
        /// <summary>
        /// Коллекция моделей для отображения.
        /// </summary>
        protected List<T> Models;

        /// <summary>
        /// Коллекция вьюмоделей для отображения.
        /// </summary>
        protected List<ViewModel<T>> ViewModels;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="models">Список моделей.</param>
        protected BaseView(List<T> models)
        {
            Models = models;

            ViewModels = models.Select(stats => new ViewModel<T>
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
            EditorUtils.ShowLine(nameof(UnitAttributesMultipliers.Type), model.ToString());
        }

        /// <summary>
        /// Отрисовать вьюмодель.
        /// </summary>
        /// <param name="viewModel">Вьюмодель.</param>
        protected void ShowViewModel(ViewModel<T> viewModel)
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(viewModel.Model.ToString()))
                viewModel.IsExpanded = !viewModel.IsExpanded;

            if (GUILayout.Button("DELETE", GUILayout.Width(60)))
                viewModel.ActionOnUpdate = ActionOnUpdate.Delete;

            EditorGUILayout.EndHorizontal();

            if (viewModel.IsExpanded)
                ShowModel(viewModel.Model);
        }


        protected void Update()
        {
            var changed = false;

            foreach (var viewModel in ViewModels)
            {
                switch (viewModel.ActionOnUpdate)
                {
                    case ActionOnUpdate.None:
                        break;
                    case ActionOnUpdate.Add:
                        Models.Add(viewModel.Model);
                        changed = true;
                        break;
                    case ActionOnUpdate.Delete:

                        var assetPath = AssetDatabase.GetAssetPath(viewModel.Model);
                        if (!string.IsNullOrEmpty(assetPath))
                            AssetDatabase.DeleteAsset(assetPath);

                        Models.Remove(viewModel.Model);
                        changed = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (changed)
                UpdateViewModels();
        }

        private void UpdateViewModels()
        {
            ViewModels = new List<ViewModel<T>>(
                Models.OrderBy(stats => stats.name)
                    .Select(stats => new ViewModel<T>
                    {
                        Model = stats
                    }).ToList());
        }
    }
}