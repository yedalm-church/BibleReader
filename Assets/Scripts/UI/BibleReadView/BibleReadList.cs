using System.Collections.Generic;
using UnityEngine;

public class BibleReadList : UIBase
{
    [UIInject("Contents")] private UIList UIList;

    public readonly List<VerseListItem> VerseListItem = new();

    public void CreateList()
    {
        OnClear();

        var datas = TableDataManager.BibleData.GetChapter(BibleManager.Instance.ReadingData.Book,
            BibleManager.Instance.ReadingData.Chapter);

        for (int i = 0, count = datas.Count; i < count; ++i)
        {
            var item = UIList.AddOrReUseItem<VerseListItem>(i);
            item.SetData(datas[i].verse, datas[i].text);
            UIList.SetActive(i, true);
            VerseListItem.Add(item);
        }

        if (datas.Count < UIList.GetItemCount)
        {
            for (int i = datas.Count, count = UIList.GetItemCount; i < count; ++i)
            {
                UIList.SetActive(i, false);
            }
        }
    }

    public void OnClear()
    {
        VerseListItem.Clear();

        for (int i = 0, count = VerseListItem.Count; i < count; ++i)
        {
            VerseListItem[i].OnClear();
        }
    }
}
