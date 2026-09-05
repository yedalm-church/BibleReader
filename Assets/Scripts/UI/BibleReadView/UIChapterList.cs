using System;
using System.Collections.Generic;
using UnityEngine;

public class UIChapterList : UIBase
{
    [UIInject("Contents")] private UIList UIList;

    public readonly List<ChapterListItem> items = new();

    public Action<int> OnClickChapterItem;

    public void CreateList()
    {
        OnClear();

        var count = TableDataManager.BibleData.GetVerseCount(BibleManager.Instance.ReadingData.Book, BibleManager.Instance.ReadingData.Chapter);

        for (int i = 0; i < count; ++i)
        {
            var item = UIList.AddItem<ChapterListItem>();
            item.SetData(i + 1, OnClickItem);
            items.Add(item);
        }

        BindEvent();
    }

    public void OnClear()
    {
        items.Clear();
        UIList.Clear();
    }

    public void OnClickItem(int InPrevChatper, int InChapter)
    {
        OnClickChapterItem?.Invoke(InChapter);

        var item = items.Find(x => x.Chapter == InPrevChatper);
        if (item != null)
        {
            item.ResetUI();
        }        
    }
}
