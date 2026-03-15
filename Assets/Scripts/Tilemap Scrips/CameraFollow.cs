using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("跟随目标")]
    public Transform player; // 拖入玩家的Transform组件

    [Header("跟随偏移（摄像头和玩家的距离）")]
    public Vector3 offset = new Vector3(0, 2, -10); // 2D默认：x=0,y=2（高一点）,z=-10（摄像头在远处）

    [Header("平滑参数（可选）")]
    public float smoothSpeed = 5f; // 数值越小越平滑，0则无平滑

    void LateUpdate() // 用LateUpdate避免和玩家移动帧冲突
    {
        if (player == null) return; // 防止玩家为空报错

        // 计算目标位置：玩家位置 + 固定偏移
        Vector3 targetPosition = player.position + offset;

        // 平滑跟随（如果不需要平滑，直接写 transform.position = targetPosition; 即可）
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}