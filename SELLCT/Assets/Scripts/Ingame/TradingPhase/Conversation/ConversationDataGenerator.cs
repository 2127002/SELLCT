using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
class ConversationDataGenerator : AssetPostprocessor
{
    //static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    //{
    //    foreach (string str in importedAssets)
    //    {
    //        //�@IndexOf�̈�����"/(�ǂݍ��܂������t�@�C����)"�Ƃ���B
    //        if (str.IndexOf("/TR0_LOC1-4_SELL.csv") != -1)
    //        {
    //            Debug.Log("CSV�t�@�C����ǂݍ��݂܂�");
    //            //�@Asset��������ǂݍ��ށiResources�ł͂Ȃ��̂Œ��Ӂj
    //            TextAsset textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
    //            //�@������ScriptableObject�t�@�C����ǂݍ��ށB�Ȃ��ꍇ�͐V���ɍ��B
    //            string assetfile = str.Replace(".csv", ".asset");
    //            ConversationDataBase cdb = AssetDatabase.LoadAssetAtPath<ConversationDataBase>(assetfile);
    //            if (cdb == null)
    //            {
    //                cdb = ScriptableObject.CreateInstance<ConversationDataBase>();
    //                AssetDatabase.CreateAsset(cdb, assetfile);
    //            }

    //            cdb.datas = CSVSerializer.Deserialize<ConversationData>(textasset.text);
    //            EditorUtility.SetDirty(cdb);
    //            AssetDatabase.SaveAssets();
    //        }
    //    }
    //}
}
#endif