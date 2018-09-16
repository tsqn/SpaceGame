namespace Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Extensions;

    using Models;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// Базовое представление для отображения.
    /// </summary>
    /// <typeparam name="T">Тип модели.</typeparam>
    public class BaseView<T> where T : ScriptableObject
    {
        /// <summary>
        /// Коллекция моделей для отображения.
        /// </summary>
        private readonly List<T> _models;

        protected readonly GUIStyle BoxStyle;

        /// <summary>
        /// Флаг сортировки по имени.
        /// </summary>
        protected bool OrderByName;

        /// <summary>
        /// Флаг возможности изменения очередности.
        /// </summary>
        protected bool PositionChanging;


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
            BoxStyle = new GUIStyle
            {
                alignment = TextAnchor.MiddleCenter,
                margin = new RectOffset(10, 10, 10, 10),
                border = new RectOffset(5, 5, 5, 5)
            };

            _models = models;

            ViewModels = models.Select(stats => new ViewModel<T>
            {
                Model = stats
            }).ToList();
        }

        public void ModeDown(ViewModel<T> model)
        {
            var oldIndex = ViewModels.IndexOf(model);

            if (oldIndex == ViewModels.Count - 1)
                return;

            var newIndex = oldIndex++;
            ViewModels.RemoveAt(oldIndex);
            ViewModels.Insert(newIndex, model);
        }

        public void MoveUp(ViewModel<T> model)
        {
            var oldIndex = ViewModels.IndexOf(model);

            if (oldIndex == 0)
                return;

            var newIndex = oldIndex--;
            ViewModels.RemoveAt(oldIndex);
            ViewModels.Insert(newIndex, model);
        }

        /// <summary>
        /// Метод отображения представления на окне.
        /// </summary>
        public virtual void Show()
        {
            GUILayout.Box("Base view Title");
        }


        /// <summary>
        /// Возвращает новую вьюмодель с базовыми значениями.
        /// </summary>
        protected static ViewModel<T> CreateNewView(string path)
        {
            var unitBaseAttributes = ScriptableObject.CreateInstance<T>();
            const string defaultName = "New";
            var assetPath = $"{path}/{defaultName}.asset";

            AssetDatabase.CreateAsset(unitBaseAttributes,
                EditorUtils.GetUniqueAssetName<T>(assetPath));

            var viewModel = new ViewModel<T>
            {
                Model = unitBaseAttributes,
                ActionOnUpdate = ActionOnUpdate.Add
            };

            return viewModel;
        }

        /// <summary>
        /// Метод отображения модели.
        /// </summary>
        /// <param name="model">Модель.</param>
        protected virtual void ShowModel(T model)
        {
            EditorUtils.ShowLine(nameof(UnitAttributesMultipliers.Type), model.ToString());
        }

        /// <summary>
        /// Отрисовать вьюмодель.
        /// </summary>
        /// <param name="viewModel">Вьюмодель.</param>
        /// <param name="title">Заголовок вьюмодели.</param>
        protected void ShowViewModel(ViewModel<T> viewModel, string title = "")
        {
            if (viewModel.Model == null)
                return;

            EditorGUILayout.BeginHorizontal();

            var buttonText = string.IsNullOrEmpty(title) ? viewModel.Model.name : title;

            if (GUILayout.Button(buttonText))
                viewModel.IsExpanded = !viewModel.IsExpanded;

            if (PositionChanging)
            {
                if (GUILayout.Button(@"/\", GUILayout.Width(20)))
                    viewModel.ActionOnUpdate = ActionOnUpdate.Up;

                if (GUILayout.Button(@"\/", GUILayout.Width(20)))
                    viewModel.ActionOnUpdate = ActionOnUpdate.Down;
            }

            if (GUILayout.Button("DELETE", GUILayout.Width(60)))
                viewModel.ActionOnUpdate = ActionOnUpdate.Delete;

            EditorGUILayout.EndHorizontal();

            if (viewModel.IsExpanded)
                ShowModel(viewModel.Model);
        }


        protected void Update()
        {
            ApplyChanges();
        }

        /// <summary>
        /// Применение изменений.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void ApplyChanges()
        {
            var changed = false;

            foreach (var viewModel in ViewModels)
            {
                switch (viewModel.ActionOnUpdate)
                {
                    case ActionOnUpdate.None:
                        break;
                    case ActionOnUpdate.Add:
                        changed = true;
                        _models.Add(viewModel.Model);
                        break;
                    case ActionOnUpdate.Delete:

                        changed = true;
                        var model = viewModel.Model as ScriptableObject;
                        if (model != null)
                        {
                            var assetPath = AssetDatabase.GetAssetPath(model);
                            if (!string.IsNullOrEmpty(assetPath))
                                AssetDatabase.DeleteAsset(assetPath);
                        }

                        _models.Remove(viewModel.Model);
                        break;
                    case ActionOnUpdate.Up:
                        changed = true;
                        _models.MoveUp(viewModel.Model);
                        break;
                    case ActionOnUpdate.Down:
                        changed = true;
                        _models.MoveDown(viewModel.Model);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (changed)
                UpdateViewModels();
        }


        /// <summary>
        /// Сортировка моделей по имени.
        /// </summary>
        private void OrderModelsByName()
        {
            ViewModels = ViewModels.OrderBy(model => model.ToString()).ToList();
        }

        /// <summary>
        /// Обновление вьюмоделей.
        /// </summary>
        private void UpdateViewModels()
        {
            _models.RemoveAll(model => model == null);
            ViewModels.RemoveAll(viewModel => viewModel.Model == null);

            var isExpandedDict = new Dictionary<int, bool>();

            foreach (var viewModel in ViewModels)
            {
                isExpandedDict.Add(ViewModels.IndexOf(viewModel), viewModel.IsExpanded);
            }

            ViewModels = new List<ViewModel<T>>(
                _models.Select(stats => new ViewModel<T>
                {
                    Model = stats
                }));

            if (OrderByName)
                OrderModelsByName();

            foreach (var kvp in isExpandedDict)
            {
                if (ViewModels.Count > kvp.Key)
                    ViewModels[kvp.Key].IsExpanded = kvp.Value;
            }
        }
    }
}