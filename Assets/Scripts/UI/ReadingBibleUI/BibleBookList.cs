using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BibleType
{
    Old,
    New,
}

public class BibleBookList : UIBase
{
    [SerializeField] private BibleType type;
    [UIInject("Contents")] private UIList _list;

    private List<BibleItem> BibleItemList = new();

    public void CreateList()
    {
        _list.Clear();
        BibleItemList.Clear();

        switch (type)
        {
            case BibleType.Old:
                {
                    for (int i = 0, count = BibleBookSetting.OldTestament.Length; i < count; ++i)
                    {
                        var item = _list.AddItem<BibleItem>();
                        item.SetData(i, BibleBookSetting.OldTestament[i]);

                        BibleItemList.Add(item);
                    }
                }
                break;
            case BibleType.New:
                {
                    for (int i = 0, count = BibleBookSetting.NewTestament.Length; i < count; ++i)
                    {
                        var item = _list.AddItem<BibleItem>();
                        item.SetData(i, BibleBookSetting.NewTestament[i]);

                        BibleItemList.Add(item);
                    }
                }
                break;
        }
    }

    public void OnClear()
    {
        _list.Clear();
        BibleItemList.Clear();
    }

    public void ShowDefaultList()
    {
        switch (type)
        {
            case BibleType.Old:
                {
                    for (int i = 0, count = BibleBookSetting.OldTestament.Length; i < count; ++i)
                    {
                        _list.SetActive(i, true);
                    }
                }
                break;
            case BibleType.New:
                {
                }
                break;
        }
    }

    public void Search(string InText)
    {
        if (BibleItemList == null)
            return;

        var data = BibleItemList.Find(x => x.Name == InText);
        if (data is null)
            return;

        Search(data.BookIndex);
    }

    public void Search(int index)
    {
        if (BibleItemList == null)
            return;

        switch (type)
        {
            case BibleType.Old:
                {
                    var has = BibleItemList.Exists(x => x.BookIndex == index);
                    if (has is false)
                    {
                        for (int i = 0, count = BibleBookSetting.OldTestament.Length; i < count; ++i)
                        {
                            _list.SetActive(i, true);
                        }
                        return;
                    }

                    for (int i = 0, count = BibleBookSetting.OldTestament.Length; i < count; ++i)
                    {
                        var bookIndex = i + 1;
                        _list.SetActive(i, bookIndex == index);
                    }
                }
                break;
            case BibleType.New:
                {
                }
                break;
        }
    }
}