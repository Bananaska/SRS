using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class GameConfigEditorWindow : EditorWindow
{
    private GameConfig targetConfig;
    private Vector2 scrollPosition;
    private Type enemyTypeEnum;

    [MenuItem("Tools/Game Config Editor")]
    public static void ShowWindow()
    {
        GetWindow<GameConfigEditorWindow>("Game Config Editor");
    }

    private void OnEnable()
    {
        enemyTypeEnum = Type.GetType("EnemyType") ?? typeof(EnemyType);
        if (!enemyTypeEnum.IsEnum)
            enemyTypeEnum = null;
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Game Config Editor", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        targetConfig = (GameConfig)EditorGUILayout.ObjectField("Game Config", targetConfig, typeof(GameConfig), false);

        if (targetConfig == null)
        {
            EditorGUILayout.HelpBox("Please assign a GameConfig asset.", MessageType.Info);
            return;
        }

        // Инициализация массива tenWave, если null
        if (targetConfig.tenWave == null)
            targetConfig.tenWave = new TenWaveData[0];

        // Кнопка добавления новой десятки волн
        if (GUILayout.Button("Add Ten Wave", GUILayout.Height(30)))
        {
            AddTenWave();
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        DrawTableHeader();

        for (int tenIdx = 0; tenIdx < targetConfig.tenWave.Length; tenIdx++)
        {
            TenWaveData tenWaveData = targetConfig.tenWave[tenIdx];

            // Инициализация внутренних массивов
            if (tenWaveData.waveDatas == null)
                tenWaveData.waveDatas = new WaveData[0];
            if (tenWaveData.enemyType == null)
                tenWaveData.enemyType = new EnemyType[0];

            // Приведение длин массивов к одному размеру
            int maxLength = Mathf.Max(tenWaveData.waveDatas.Length, tenWaveData.enemyType.Length);
            if (tenWaveData.waveDatas.Length != maxLength)
                Array.Resize(ref tenWaveData.waveDatas, maxLength);
            if (tenWaveData.enemyType.Length != maxLength)
                Array.Resize(ref tenWaveData.enemyType, maxLength);

            // Если нет волн в этой десятке, показываем кнопку для добавления первой волны
            if (maxLength == 0)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"TenWave {tenIdx}", GUILayout.Width(60));
                EditorGUILayout.LabelField("No waves", GUILayout.Width(50));
                if (GUILayout.Button("Add First Wave", GUILayout.Width(100)))
                {
                    AddWaveToTenWave(tenIdx, 0);
                }
                if (GUILayout.Button("Remove TenWave", GUILayout.Width(100)))
                {
                    RemoveTenWave(tenIdx);
                }
                EditorGUILayout.EndHorizontal();
                continue;
            }

            // Отображение всех волн в текущей десятке
            for (int waveIdx = 0; waveIdx < tenWaveData.waveDatas.Length; waveIdx++)
            {
                DrawWaveRow(tenIdx, waveIdx, tenWaveData);
            }
        }

        EditorGUILayout.EndScrollView();

        if (targetConfig.tenWave.Length == 0 || targetConfig.tenWave.All(t => t.waveDatas.Length == 0))
        {
            EditorGUILayout.HelpBox("No waves. Click 'Add Ten Wave' and then add waves inside.", MessageType.Info);
        }
    }

    private void DrawTableHeader()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("TenWave", GUILayout.Width(60));
        GUILayout.Label("Wave", GUILayout.Width(50));
        GUILayout.Label("Enemy Count", GUILayout.Width(90));
        GUILayout.Label("Enemy Type", GUILayout.Width(100));
        GUILayout.Label("Actions", GUILayout.Width(290));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
    }

    private void DrawWaveRow(int tenIdx, int waveIdx, TenWaveData tenWaveData)
    {
        EditorGUILayout.BeginHorizontal();

        // Индекс TenWave (только для первой волны, чтобы не повторять)
        GUI.enabled = false;
        if (waveIdx == 0)
            EditorGUILayout.IntField(tenIdx, GUILayout.Width(60));
        else
            GUILayout.Space(60); // отступ для выравнивания
        GUI.enabled = true;

        // Индекс волны
        GUI.enabled = false;
        EditorGUILayout.IntField(waveIdx, GUILayout.Width(50));
        GUI.enabled = true;

        // Enemy Count
        int newCount = EditorGUILayout.IntField(tenWaveData.waveDatas[waveIdx].EnemyCount, GUILayout.Width(90));
        if (newCount != tenWaveData.waveDatas[waveIdx].EnemyCount)
        {
            Undo.RecordObject(targetConfig, "Change Enemy Count");
            tenWaveData.waveDatas[waveIdx].EnemyCount = newCount;
            EditorUtility.SetDirty(targetConfig);
        }

        // Enemy Type
        if (enemyTypeEnum != null)
        {
            object currentEnum = tenWaveData.enemyType[waveIdx];
            object newEnum = EditorGUILayout.EnumPopup((Enum)currentEnum, GUILayout.Width(100));
            if (!newEnum.Equals(currentEnum))
            {
                Undo.RecordObject(targetConfig, "Change Enemy Type");
                tenWaveData.enemyType[waveIdx] = (EnemyType)newEnum;
                EditorUtility.SetDirty(targetConfig);
            }
        }
        else
        {
            string newType = EditorGUILayout.TextField(tenWaveData.enemyType[waveIdx].ToString(), GUILayout.Width(100));
            if (newType != tenWaveData.enemyType[waveIdx].ToString())
            {
                Undo.RecordObject(targetConfig, "Change Enemy Type");
                try
                {
                    tenWaveData.enemyType[waveIdx] = (EnemyType)Enum.Parse(typeof(EnemyType), newType);
                }
                catch { }
                EditorUtility.SetDirty(targetConfig);
            }
        }

        // Кнопки действий
        if (GUILayout.Button("Add Above", GUILayout.Width(80)))
        {
            AddWaveToTenWave(tenIdx, waveIdx);
        }
        if (GUILayout.Button("Add Below", GUILayout.Width(80)))
        {
            AddWaveToTenWave(tenIdx, waveIdx + 1);
        }
        if (GUILayout.Button("Remove Wave", GUILayout.Width(90)))
        {
            RemoveWaveFromTenWave(tenIdx, waveIdx);
        }
        if (waveIdx == 0 && GUILayout.Button("Remove TenWave", GUILayout.Width(100)))
        {
            RemoveTenWave(tenIdx);
        }

        EditorGUILayout.EndHorizontal();
    }

    private void AddTenWave()
    {
        Undo.RecordObject(targetConfig, "Add TenWave");
        var list = targetConfig.tenWave.ToList();
        var newTenWave = new TenWaveData
        {
            waveDatas = new WaveData[0],
            enemyType = new EnemyType[0]
        };
        list.Add(newTenWave);
        targetConfig.tenWave = list.ToArray();
        EditorUtility.SetDirty(targetConfig);
        Repaint(); // Принудительное обновление окна
    }

    private void RemoveTenWave(int tenIdx)
    {
        Undo.RecordObject(targetConfig, "Remove TenWave");
        var list = targetConfig.tenWave.ToList();
        list.RemoveAt(tenIdx);
        targetConfig.tenWave = list.ToArray();
        EditorUtility.SetDirty(targetConfig);
        Repaint();
    }

    private void AddWaveToTenWave(int tenIdx, int insertIndex)
    {
        Undo.RecordObject(targetConfig, "Add Wave");
        var tenWaveData = targetConfig.tenWave[tenIdx];
        var waveList = tenWaveData.waveDatas.ToList();
        var enemyList = tenWaveData.enemyType.ToList();

        WaveData newWave = new WaveData { EnemyCount = 1 };
        EnemyType defaultEnemy = enemyTypeEnum != null ? (EnemyType)Enum.GetValues(enemyTypeEnum).GetValue(0) : default;

        waveList.Insert(insertIndex, newWave);
        enemyList.Insert(insertIndex, defaultEnemy);

        tenWaveData.waveDatas = waveList.ToArray();
        tenWaveData.enemyType = enemyList.ToArray();
        EditorUtility.SetDirty(targetConfig);
        Repaint();
    }

    private void RemoveWaveFromTenWave(int tenIdx, int waveIdx)
    {
        Undo.RecordObject(targetConfig, "Remove Wave");
        var tenWaveData = targetConfig.tenWave[tenIdx];
        var waveList = tenWaveData.waveDatas.ToList();
        var enemyList = tenWaveData.enemyType.ToList();

        waveList.RemoveAt(waveIdx);
        enemyList.RemoveAt(waveIdx);

        tenWaveData.waveDatas = waveList.ToArray();
        tenWaveData.enemyType = enemyList.ToArray();
        EditorUtility.SetDirty(targetConfig);
        Repaint();
    }
}