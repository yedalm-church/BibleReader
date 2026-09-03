using UnityEngine;

public static class TableDataManager
{
    public static BibleData BibleData;

    public static void Initialize()
    {
        BibleData = new BibleData();

        if (!BibleData.Load())
        {
            Debug.LogError("성경 데이터 로드 실패");
            return;
        }
    }
}
