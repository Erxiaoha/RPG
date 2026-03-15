using UnityEditor;
using UnityEngine;

public static class TogglePrefabVisibilityShortcut
{
    // 设置快捷键为空格键
    [MenuItem("Edit/Toggle Selected Prefab Active _SPACE", false, 10)]
    private static void ToggleSelectedPrefabActive()
    {
        // 获取当前在 Hierarchy 中选中的所有对象
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length > 0)
        {
            // 记录操作，以便支持撤销（Undo）
            Undo.RecordObjects(selectedObjects, "Toggle Prefab Active State");

            foreach (GameObject go in selectedObjects)
            {
                // 切换对象的激活状态
                go.SetActive(!go.activeSelf);
            }
        }
        else
        {
            Debug.Log("请先在 Hierarchy 中选中一个或多个对象。");
        }
    }
}