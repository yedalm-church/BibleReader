using System.Collections.Generic;
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

    private readonly List<BibleItem> _bibleItemList = new();

    private string[] BookList => type == BibleType.Old
            ? BibleBookSetting.OldTestament
            : BibleBookSetting.NewTestament;


    public void CreateList()
    {
        OnClear();

        for (int i = 0, count = BookList.Length; i < count; ++i)
        {
            var item = _list.AddItem<BibleItem>();

            item.SetData(type, i, BookList[i]);

            _bibleItemList.Add(item);
        }
    }

    public void OnClear()
    {
        _list.Clear();
        _bibleItemList.Clear();
    }

    public void ShowDefaultList()
    {
        SetAllActive(true);
    }

    public void Search(string inText)
    {
        var item = _bibleItemList.Find(x => x.Name == inText);

        if (item == null)
        {
            ShowDefaultList();
            return;
        }

        Search(item.BookIndex);
    }

    public void Search(int index)
    {
        var exists = _bibleItemList.Exists(x => x.BookIndex == index);

        if (!exists)
        {
            ShowDefaultList();
            return;
        }

        for (int i = 0; i < _bibleItemList.Count; ++i)
        {
            _list.SetActive(i, _bibleItemList[i].BookIndex == index);
        }
    }

    private void SetAllActive(bool isActive)
    {
        for (int i = 0; i < _bibleItemList.Count; ++i)
        {
            _list.SetActive(i, isActive);
        }
    }
}