using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
class ConversationDataGenerator : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            //　IndexOfの引数は"/(読み込ませたいファイル名)"とする。
            if (str.IndexOf(".csv") == -1) continue;

            Debug.Log("CSVファイルを読み込みます");
            //　Asset直下から読み込む（Resourcesではないので注意）
            TextAsset textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
            //　同名のScriptableObjectファイルを読み込む。ない場合は新たに作る。
            string assetfile = str.Replace(".csv", ".asset");
            ConversationDataBase cdb = AssetDatabase.LoadAssetAtPath<ConversationDataBase>(assetfile);
            if (cdb == null)
            {
                cdb = ScriptableObject.CreateInstance<ConversationDataBase>();
                AssetDatabase.CreateAsset(cdb, assetfile);
            }

            //データの不正値チェック（かぎかっこ）
            foreach (var d in cdb.datas)
            {
                for (int i = 0; i < d.Text.Length; i++)
                {
                    if (!string.IsNullOrEmpty(d.Name[i]))
                    {
                        if (!d.Text[i].Contains('「'))
                        {
                            Debug.LogError("「←このかぎかっこがない" + d.Text[i] + " ID" + d.ID);
                        }
                        if (!d.Text[i].Contains('」'))
                        {
                            Debug.LogError("」←このかぎかっこがない" + d.Text[i] + " ID" + d.ID);
                        }
                    }
                    else
                    {
                        if (d.Text[i].Contains('「'))
                        {
                            Debug.LogError("「←このかぎかっこがある" + d.Text[i] + " ID" + d.ID);
                        }
                        if (d.Text[i].Contains('」'))
                        {
                            Debug.LogError("」←このかぎかっこがある" + d.Text[i] + " ID" + d.ID);
                        }
                    }
                }
            }

            cdb.datas = CSVSerializer.Deserialize<ConversationData>(textasset.text);
            EditorUtility.SetDirty(cdb);
            AssetDatabase.SaveAssets();
        }
    }
}
#endif