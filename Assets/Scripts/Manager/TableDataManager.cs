using UnityEngine;

public static class TableDataManager
{
    public static BibleDataLoader BibleDataLoader;

    public static void Initialize()
    {
        BibleDataLoader = new BibleDataLoader();

        if (!BibleDataLoader.Load())
        {
            Debug.LogError("성경 데이터 로드 실패");
            return;
        }
    }
}
