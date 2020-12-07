using System;

namespace Services
{
    public class ItemFoundArgs : EventArgs
    {
        public string ItemPath { get; set; }
        public bool EndSearch { get; set; }
        public bool RemoveItemFromResult { get; set; }

        public ItemFoundArgs(string path)
        {
            ItemPath = path;
        }
    }
}