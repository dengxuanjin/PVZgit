using OfficeOpenXml;
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
[InitializeOnLoad]
public class Startup
{
    public static bool needRead = false;
    // ���������������ǰִ��
    static Startup()
    {
        if (!needRead)
        {
            return;
        }
        string path = Application.dataPath + "/Editor/�ؿ�����.xlsx";
        string assetName = "TablesObject/Level";
        FileInfo fileInfo = new FileInfo(path);
        //�������к���
        LevelData levelData = (LevelData)ScriptableObject.CreateInstance(typeof(LevelData));
        // ��Excel�ļ�,using��ʹ�� ��Ϻ��Զ��ر��ļ�
        using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
        {
            // ѡ�����ݱ�
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["��ʬ"];
            //����ÿһ��
            for(int i = worksheet.Dimension.Start.Row+2; i<= worksheet .Dimension.End.Row; i++)
            {

                //Debug.Log("----------");
                LevelItem levelItem = new LevelItem();
                Type type = typeof(LevelItem);
                for (int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
                {
                    //Debug.Log("��������" + worksheet.GetValue(i, j).ToString());
                    FieldInfo variable = type.GetField(worksheet.GetValue(2, j).ToString());
                    string tableValue = worksheet.GetValue(i, j).ToString();
                    variable.SetValue(levelItem, Convert.ChangeType(tableValue, variable.FieldType));
                }
                levelData.LevelDataList.Add(levelItem);

            }
        }
        //����ScripttableObjectΪ.asset�ļ�
        AssetDatabase.CreateAsset(levelData, "Assets/Resources/TablesObject/" + assetName + ".asset"); ;
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
