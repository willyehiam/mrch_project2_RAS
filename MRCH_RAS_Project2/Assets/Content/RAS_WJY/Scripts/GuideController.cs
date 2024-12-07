using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPointGuide : MonoBehaviour
{
    public GameObject yellowPointPrefab; // 黄色光点的Prefab
    public Transform playerTransform;   // XR Origin的Transform
    public Transform targetMailbox;     // 邮筒的位置
    public float pointSpacing = 1.0f;   // 光点之间的间隔
    public int maxPoints = 20;          // 光点的最大数量
    private List<GameObject> points = new List<GameObject>(); // 存储生成的光点

    private bool guideActive = true;    // 是否激活引导

    void Start()
    {
        // 检查是否有必要的变量
        if (yellowPointPrefab == null || playerTransform == null || targetMailbox == null)
        {
            Debug.LogError("Missing necessary references! Please assign them in the Inspector.");
            return;
        }

        // 生成光点路径
        GeneratePath();
    }

    void GeneratePath()
    {
        if (!guideActive) return;

        // 清除现有光点
        ClearPoints();

        // 计算路径
        Vector3 start = playerTransform.position;
        Vector3 end = targetMailbox.position;
        Vector3 direction = (end - start).normalized;

        for (int i = 0; i < maxPoints; i++)
        {
            Vector3 pointPosition = start + direction * pointSpacing * i;

            // 如果超过目标点，则停止生成
            if (Vector3.Distance(pointPosition, end) < pointSpacing)
                break;

            // 创建光点
            GameObject point = Instantiate(yellowPointPrefab, pointPosition, Quaternion.identity);
            points.Add(point);
        }
    }

    void ClearPoints()
    {
        foreach (GameObject point in points)
        {
            Destroy(point);
        }
        points.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 如果玩家进入邮筒的Trigger Zone
        if (other.CompareTag("Player") && guideActive)
        {
            guideActive = false;
            ClearPoints(); // 移除光点
        }
    }
}
