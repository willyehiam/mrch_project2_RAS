using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TriggerZoneAssetDisplay : MonoBehaviour
{
    [Header("Asset Settings")]
    public List<GameObject> assetsToDisplay; // 需要显示的资产列表
    public Transform spawnParent; // 父级对象，用于组织生成的资产
    public Vector3 spawnOffset; // 每个资产之间的偏移量
    public int maxAssetsToDisplay = 5; // 最大显示的资产数量

    [Header("UI Settings")]
    public Slider assetCountSlider; // 滑块，用于控制显示的资产数量
    public Text assetCountText; // 文本，用于显示当前数量

    private List<GameObject> spawnedAssets = new List<GameObject>(); // 存储生成的资产实例

    private void Start()
    {
        // 初始化UI
        if (assetCountSlider != null)
        {
            assetCountSlider.maxValue = maxAssetsToDisplay;
            assetCountSlider.value = maxAssetsToDisplay;
            assetCountSlider.onValueChanged.AddListener(UpdateDisplayedAssets);
        }

        if (assetCountText != null)
        {
            assetCountText.text = $"Assets: {maxAssetsToDisplay}";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 确保是玩家触发
        {
            DisplayAssets((int)assetCountSlider.value);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 确保是玩家触发
        {
            ClearAssets();
        }
    }

    private void DisplayAssets(int count)
    {
        ClearAssets(); // 清空之前的资产

        for (int i = 0; i < count; i++)
        {
            if (i < assetsToDisplay.Count) // 确保不超过列表中的资产数量
            {
                GameObject asset = Instantiate(assetsToDisplay[i], spawnParent);
                asset.transform.localPosition = i * spawnOffset;
                spawnedAssets.Add(asset);
            }
        }
    }

    private void ClearAssets()
    {
        foreach (var asset in spawnedAssets)
        {
            Destroy(asset);
        }
        spawnedAssets.Clear();
    }

    private void UpdateDisplayedAssets(float value)
    {
        if (assetCountText != null)
        {
            assetCountText.text = $"Assets: {(int)value}";
        }
    }
}
