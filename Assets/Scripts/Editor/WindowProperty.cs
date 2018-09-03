namespace Editor
{
    public class WindowProperty<T>
    {
        public bool IsExpanded { get; set; }
            
        public T Model { get; set; }

        public ActionOnUpdate ActionOnUpdate { get; set; }
    }

    public enum ActionOnUpdate
    {
        None,
        Add,
        Delete
    }
}