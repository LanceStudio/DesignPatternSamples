
namespace DragAndDrop {

    public interface IDropable {
        string DropableName { get; }
        bool CanDrop(IDragable dragable);
        void Drop(IDragable dragable);
    }
}
