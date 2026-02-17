using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modul_30_3_Refactored
{
    public class Inventory
    {
        private List<Item> _items;

        public int CurrentSize => _items.Sum(item => item.Count);
        public int MaxSize {  get; private set; }

        public Inventory(List<Item> items, int maxSize)
        {
            if (maxSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxSize), "Max inventory size can't be 0 or lower");
            
            MaxSize = maxSize;

            int total = items.Sum(item => item.Count);

            if (total > MaxSize)
                throw new ArgumentException("Initial items exceed inventory capacity");

            _items = new List<Item>(items);
        }

        public void Add(Item newItem)
        {
            if (newItem.Count <= 0) return;

            if (CurrentSize + newItem.Count > MaxSize)
                return;

            var existing = _items.FirstOrDefault(i => i.Name == newItem.Name);

            if (existing != null)
            {
                existing.Increase(newItem.Count);
            }
            else
            {
                _items.Add(newItem);
            }
        }

        public bool TryGetItemsByName(string name, int count, out List<Item> findedItems)
        {
            findedItems = new List<Item>();

            var availableItems = _items.Where(item => item.Name == name && item.Count > 0).ToList();

            if (availableItems.Count == 0)
                return false;

            int remaining = count;

            foreach (var item in availableItems)
            {
                if (remaining <= 0)
                    break;

                int available = item.Count;
                int take = Mathf.Min(available, remaining);

                item.TryDecrease(take);

                findedItems.Add(new Item(name, take));

                remaining -= take;
            }

            _items.RemoveAll(item => item.Count == 0);

            return remaining == 0;
        }

        public bool Contains(string name, int count = 1)
        {
            int total = _items.Where(i => i.Name == name).Sum(i => i.Count);
            return total >= count;
        }

        public bool TryShowAllItems(out List<string> items)
        {
            items = new();

            if (_items.Count > 0)
            {
                foreach (Item item in _items)
                    items.Add(item.ToString());

                return true;
            }

            return false;
        }
    }

    public class Item
    {
        private const string STRING_FORMAT = "Name: {0}, Count: {1}";

        public string Name { get; private set; }
        public int Count {  get; private set; }

        public Item(string name, int count)
        {
            Name = name;
            Count = count;
        }

        public void Increase(int value)
        {
            if (value <= 0)
                return;

            Count += value;
        }

        public bool TryDecrease(int value)
        {
            if (value <= 0)
                return false;

            if (Count < value)
                return false;

            Count -= value;
            return true;
        }

        public override string ToString()
        {
            return string.Format(STRING_FORMAT, Name, Count);
        }
    }
}