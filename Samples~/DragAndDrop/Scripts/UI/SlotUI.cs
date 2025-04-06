using UnityEngine;
using UnityEngine.UI;

namespace DragAndDrop {

    public class SlotUI : MonoBehaviour, IDropable {

        public string DropableName => slotType.ToString();

        [SerializeField] private SlotType slotType;
        [SerializeField] private Image slotImage;

        public bool CanDrop(IDragable dragable) {
            if(dragable is ItemUI itemUI) {
                return itemUI.Type switch {
                    ItemUI.ItemType.Sword => slotType == SlotType.RightHand || slotType == SlotType.LeftHand,
                    ItemUI.ItemType.Shield => slotType == SlotType.RightHand || slotType == SlotType.LeftHand,
                    ItemUI.ItemType.Helmet => slotType == SlotType.Head,
                    _ => false
                };
            }
            return false;
        }

        public void Drop(IDragable dragable) {
            if(dragable is ItemUI itemUI) {
                slotImage.sprite = itemUI.Sprite;
                slotImage.enabled = true;
            }
        }

        public enum SlotType {
            RightHand,
            LeftHand,
            Head
        }
    }
}