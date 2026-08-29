using TMPro;

public class UIBibleInfo : UIBase
{
    [UIInject("Text_Title")] private TMP_Text Text_Title;
    [UIInject("Text_Book")] private TMP_Text Text_Book;

    private string[] BookList => BibleManager.Instance.ReadingData.Type == BibleType.Old
        ? BibleBookSetting.OldTestament
        : BibleBookSetting.NewTestament;

    public override void UpdateContent()
    {
        base.UpdateContent();

        if (BookList.IsValidIndex(BibleManager.Instance.ReadingData.BookIndex))
        {
            Text_Title.SetText(BookList[BibleManager.Instance.ReadingData.BookIndex]);
            Text_Book.SetText($"{BibleManager.Instance.ReadingData.BookIndex}¿Â");
        }
    }
}
