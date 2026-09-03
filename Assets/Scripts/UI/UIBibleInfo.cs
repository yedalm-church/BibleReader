using TMPro;

public class UIBibleInfo : UIBase
{
    [UIInject("Text_Title")] private TMP_Text Text_Title;
    [UIInject("Text_Book")] private TMP_Text Text_Book;

    private string[] BookList => BibleManager.Instance.ReadingData.BibleType == BibleType.Old
        ? BibleBookSetting.OldTestament
        : BibleBookSetting.NewTestament;

    public override void UpdateContent()
    {
        base.UpdateContent();

        if (BookList.IsValidIndex(BibleManager.Instance.ReadingData.Book))
        {
            var index = BibleManager.Instance.ReadingData.Book - 1;
            Text_Title.SetText(BookList[index]);
            Text_Book.SetText($"{BibleManager.Instance.ReadingData.Book}¿Â");
        }
    }
}
