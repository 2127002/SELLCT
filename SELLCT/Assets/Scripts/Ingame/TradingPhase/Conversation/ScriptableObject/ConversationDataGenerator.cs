using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
public class ConversationDataGenerator : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            //　IndexOfの引数は"/(読み込ませたいファイル名)"とする。
            if (str.IndexOf("/CSVTestData.csv") != -1)
            {
                Debug.Log("CSVファイルを読み込みます");
                //　Asset直下から読み込む（Resourcesではないので注意）
                TextAsset textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                //　同名のScriptableObjectファイルを読み込む。ない場合は新たに作る。
                string assetfile = str.Replace(".csv", ".asset");
                ConversationDataBase cdb = AssetDatabase.LoadAssetAtPath<ConversationDataBase>(assetfile);
                if (cdb == null)
                {
                    cdb = new();
                    AssetDatabase.CreateAsset(cdb, assetfile);
                }

                cdb.datas = CSVSerializer.Deserialize<ConversationData>(textasset.text);
                EditorUtility.SetDirty(cdb);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
#endif