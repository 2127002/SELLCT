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
            //�@IndexOf�̈�����"/(�ǂݍ��܂������t�@�C����)"�Ƃ���B
            if (str.IndexOf(".csv") == -1) continue;

            Debug.Log("CSV�t�@�C����ǂݍ��݂܂�");
            //�@Asset��������ǂݍ��ށiResources�ł͂Ȃ��̂Œ��Ӂj
            TextAsset textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
            //�@������ScriptableObject�t�@�C����ǂݍ��ށB�Ȃ��ꍇ�͐V���ɍ��B
            string assetfile = str.Replace(".csv", ".asset");
            ConversationDataBase cdb = AssetDatabase.LoadAssetAtPath<ConversationDataBase>(assetfile);
            if (cdb == null)
            {
                cdb = ScriptableObject.CreateInstance<ConversationDataBase>();
                AssetDatabase.CreateAsset(cdb, assetfile);
            }

            //�f�[�^�̕s���l�`�F�b�N�i�����������j
            foreach (var d in cdb.datas)
            {
                for (int i = 0; i < d.Text.Length; i++)
                {
                    if (!string.IsNullOrEmpty(d.Name[i]))
                    {
                        if (!d.Text[i].Contains('�u'))
                        {
                            Debug.LogError("�u�����̂������������Ȃ�" + d.Text[i] + " ID" + d.ID);
                        }
                        if (!d.Text[i].Contains('�v'))
                        {
                            Debug.LogError("�v�����̂������������Ȃ�" + d.Text[i] + " ID" + d.ID);
                        }
                    }
                    else
                    {
                        if (d.Text[i].Contains('�u'))
                        {
                            Debug.LogError("�u�����̂���������������" + d.Text[i] + " ID" + d.ID);
                        }
                        if (d.Text[i].Contains('�v'))
                        {
                            Debug.LogError("�v�����̂���������������" + d.Text[i] + " ID" + d.ID);
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