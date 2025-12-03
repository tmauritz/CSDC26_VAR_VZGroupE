using System;
using System.IO;
using UnityEngine;

[Serializable]
internal class SaveData
{
    public int totalDoenerBuilt;
    public SaveData(int totalDoenerBuilt)
    {
        this.totalDoenerBuilt = totalDoenerBuilt;
    }
}

public class PersistenceManager
{
    private static String _filePath = Application.persistentDataPath + "/save.json";

    public static void addDoenerBuilt(int amount)
    {
        var doenerStat = loadDoenerStat();
        doenerStat += amount;
        saveTotalDoenerStat(doenerStat);
    }
    
    public static void saveTotalDoenerStat(int totalDoenerBuilt)
    {
        var json = JsonUtility.ToJson(new SaveData(totalDoenerBuilt));
        File.WriteAllText(_filePath, json);
    }

    public static int loadDoenerStat()
    {
        if (!File.Exists(_filePath)) return 0;
        
        var json = File.ReadAllText(_filePath);
        var data = JsonUtility.FromJson<SaveData>(json);
        return data.totalDoenerBuilt;
    }
    
}
