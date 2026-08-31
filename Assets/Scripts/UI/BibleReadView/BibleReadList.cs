using System.Collections.Generic;
using UnityEngine;

public class BibleReadList : UIBase
{
    [UIInject("Contents")] private UIList UIList;

    private readonly List<VerseListItem> _verseListItem = new();

    public void CreateList()
    {
        OnClear();

        //var datas = BibleManager.Instance.GetChapter(BibleManager.Instance.ReadingData.Book,
        //    BibleManager.Instance.ReadingData.Chapter);

        var datas = BibleManager.Instance.GetChapter(1, 1);

        for (int i = 0, count = datas.Count; i < count; ++i)
        {
            var item = UIList.AddItem<VerseListItem>();
            item.SetData(datas[i].verse, datas[i].text);
            _verseListItem.Add(item);
        }
    }

    public void OnClear()
    {
        UIList.Clear();
        _verseListItem.Clear();
    }
}
