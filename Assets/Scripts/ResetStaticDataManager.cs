using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        BaseCounter.ResectStaticData();
        CuttingCounter.ResectStaticData();
        TrashCounter.ResectStaticData();
    }
}
