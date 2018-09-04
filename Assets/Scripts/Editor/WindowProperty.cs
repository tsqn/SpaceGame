namespace Editor
{
    public class WindowProperty<T>
    {
        public ActionOnUpdate ActionOnUpdate { get; set; }
        public bool IsExpanded { get; set; }

        public T Model { get; set; }
    }

    public enum ActionOnUpdate
    {
        None,
        Add,
        Delete
    }
}