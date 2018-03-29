//
// LostItemGrouping.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/10/2016
//
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FoundIt.Models
{
    public class LostItemGrouping : ObservableCollection<LostItem>
    {
        public string Key { get; private set; }

        public LostItemGrouping(string key, IEnumerable<LostItem> items)
        {
            Key = key;

            foreach (var item in items)
                Items.Add(item);
        }
    }
}
