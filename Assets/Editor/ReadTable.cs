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
    // 这个方法会在运行前执行
    static Startup()
    {
        if (!needRead)
        {
            return;
        }
        string path = Application.dataPath + "/Editor/关卡管理.xlsx";
        string assetName = "TablesObject/Level";
        FileInfo fileInfo = new FileInfo(path);
        //创建序列号类
        LevelData levelData = (LevelData)ScriptableObject.CreateInstance(typeof(LevelData));
        // 打开Excel文件,using会使用 完毕后自动关闭文件
        using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
        {
            // 选择数据表单
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["僵尸"];
            //遍历每一行
            for(int i = worksheet.Dimension.Start.Row+2; i<= worksheet .Dimension.End.Row; i++)
            {

                //Debug.Log("----------");
                LevelItem levelItem = new LevelItem();
                Type type = typeof(LevelItem);
                for (int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
                {
                    //Debug.Log("数据内容" + worksheet.GetValue(i, j).ToString());
                    FieldInfo variable = type.GetField(worksheet.GetValue(2, j).ToString());
                    string tableValue = worksheet.GetValue(i, j).ToString();
                    variable.SetValue(levelItem, Convert.ChangeType(tableValue, variable.FieldType));
                }
                levelData.LevelDataList.Add(levelItem);

            }
        }
        //保存ScripttableObject为.asset文件
        AssetDatabase.CreateAsset(levelData, "Assets/Resources/TablesObject/" + assetName + ".asset"); ;
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
