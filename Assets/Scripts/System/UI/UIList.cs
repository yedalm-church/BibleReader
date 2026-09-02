using System;
using System.Collections.Generic;
using UnityEngine;

public class UIList : MonoBehaviour
{
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _content;

    private readonly List<GameObject> _items = new();
    public int GetItemCount => _items.Count;

    public GameObject AddItem()
    {
        var item = Instantiate(_itemPrefab, _content);
        _items.Add(item);

        return item;
    }

    public T AddItem<T>() where T : Component
    {
        GameObject item = AddItem();
        return item.GetComponent<T>();
    }

    public T AddOrReUseItem<T>(int InIndex) where T : Component
    {
        var returnValue = GetItem<T>(InIndex);
        if (returnValue != null)
            return returnValue;

        return AddItem<T>();
    }

    public T GetItem<T>(int InIndex) where T : Component
    {
        if (_items == null || _items.IsValidIndex(InIndex) == false)
            return null;

        return _items[InIndex].GetComponent<T>();
    }

    public void SetActive(int InIndex, bool InActive)
    {
        if (_items == null || _items.IsValidIndex(InIndex) == false)
            return;

        if (_items[InIndex].activeInHierarchy == InActive)
            return;

        _items[InIndex].SetActive(InActive);
    }


    public void Clear()
    {
        foreach (GameObject item in _items)
        {
            if (item != null)
                Destroy(item);
        }

        _items.Clear();
    }
}