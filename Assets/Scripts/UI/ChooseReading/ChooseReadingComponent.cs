using UnityEngine;
using UnityEngine.UI;

public class ChooseReadingComponent : UIBase
{
    [UIInject("Image_Old_Title")] private Image Image_Old_Title;
    [UIInject("Image_New_Title")] private Image Image_New_Title;
    [UIInject("Image_Old_Title")] private UIBibleInfo UIBibleOldInfo;
    [UIInject("Image_New_Title")] private UIBibleInfo UIBibleNewInfo;
    [UIInject("Button_Alternate_Reading_Start")] private UIButton Button_Alternate_Reading_Start;
    [UIInject("Button_Al_Reading_Start")] private UIButton Button_Al_Reading_Start;
    [UIInject("Button_Record_Reading_Start")] private UIButton Button_Record_Reading_Start;

    public override void UpdateContent()
    {
        base.UpdateContent();

        Image_Old_Title.gameObject.SetActive(BibleManager.Instance.ReadingData.BibleType == BibleType.Old);
        Image_New_Title.gameObject.SetActive(BibleManager.Instance.ReadingData.BibleType == BibleType.New);

        if (BibleManager.Instance.ReadingData.BibleType == BibleType.Old)
            UIBibleOldInfo.UpdateContent();
        else 
            UIBibleNewInfo.UpdateContent();

        BindEvent();
    }

    protected override void OnDestroy()
    {
        UnBindEvent();
    }

    public override void BindEvent()
    {
        base.BindEvent();
        UIBindEvent.BindEvent(Button_Alternate_Reading_Start, OnClickAlternateReadingStart);
        UIBindEvent.BindEvent(Button_Al_Reading_Start, OnClickAlReadingStart);
        UIBindEvent.BindEvent(Button_Record_Reading_Start, OnClickRecordReadingStart);
    }

    public override void UnBindEvent()
    {
        base.UnBindEvent();
        UIBindEvent.UnBindEvent(Button_Alternate_Reading_Start, OnClickAlternateReadingStart);
        UIBindEvent.UnBindEvent(Button_Al_Reading_Start, OnClickAlReadingStart);
        UIBindEvent.UnBindEvent(Button_Record_Reading_Start, OnClickRecordReadingStart);
    }

    private void OnClickAlternateReadingStart()
    {
        SceneLoadManager.LoadScene("BibleReadView");
    }

    private void OnClickAlReadingStart()
    {
        //BibleManager.Instance.StartReading
        //    (BibleManager.Instance.ReadingData.Book,
        //     BibleManager.Instance.ReadingData.Chapter,
        //     BibleManager.Instance.ReadingData.Verse);

        SceneLoadManager.LoadScene("BibleReadView"); 
    }

    private void OnClickRecordReadingStart()
    { 
        SceneLoadManager.LoadScene("BibleReadView");
    }
}
