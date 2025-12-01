using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyDataSO))]
public class EnemySOEditor : Editor
{
    EnemyDataSO enemyData;

    private void OnEnable()
    {
        enemyData = target as EnemyDataSO;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (enemyData != null)
        {
            if (enemyData.miniMapIcon == null) return;
            Texture2D texture = AssetPreview.GetAssetPreview(enemyData.miniMapIcon);
            GUILayout.Label("", GUILayout.Height(64), GUILayout.Width(64));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        }
    }
}
