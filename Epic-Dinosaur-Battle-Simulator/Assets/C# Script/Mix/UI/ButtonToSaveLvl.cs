using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Globalization;

public class ButtonToSaveLvl : MonoBehaviour
{
    [SerializeField] private int maplvlid;
    [SerializeField] private int continent;
    [SerializeField] private int troopsLevel;

    public void SaveToFile()
    {
        string path = @"moj_plik.txt"; // ścieżka do pliku
        List<ObjectToDisplay> objectList = objectToDisplays();

        string content = $"else if (id == {maplvlid})\n {{\n_lobject=new List<ObjectToDisplay> {{";
        for(int i = 0; i < objectList.Count; i++)
        {
            if (i != objectList.Count - 1)
            {
                content = content + $"new ObjectToDisplay({objectList[i].XAxis.ToString(CultureInfo.InvariantCulture)}f, " +
                    $"{objectList[i].YAxis.ToString(CultureInfo.InvariantCulture)}f, {objectList[i].ZAxis.ToString(CultureInfo.InvariantCulture)}f, {objectList[i].PrefabId}, {objectList[i].Lvl}, {objectList[i].YRotation.ToString(CultureInfo.InvariantCulture)}f), ";
            }
            else 
            {
                content = content + $"new ObjectToDisplay({objectList[i].XAxis.ToString(CultureInfo.InvariantCulture)}f," +
                    $" {objectList[i].YAxis.ToString(CultureInfo.InvariantCulture)}f, {objectList[i].ZAxis.ToString(CultureInfo.InvariantCulture)}f, {objectList[i].PrefabId}, {objectList[i].Lvl}, {objectList[i].YRotation.ToString(CultureInfo.InvariantCulture)}f)}};\r\n        }} ";
            }
        }

        try
        {
            File.WriteAllText(path, content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }

    }

    private List<ObjectToDisplay> objectToDisplays() 
    {
        List<ObjectToDisplay> listOfObjectToSaveInFile = new List<ObjectToDisplay>();
        foreach (var display in GameManager.Instance.enemyGameObjects) 
        {
            Vector3 vector = display.transform.position;
            FighterPlacement f = display.gameObject.GetComponent<FighterPlacement>();
            ObjectToDisplay obj = new ObjectToDisplay(vector.x, vector.y, vector.z, f.index, troopsLevel,f.gameObject.transform.rotation.y);
            listOfObjectToSaveInFile.Add(obj);
        }
        return listOfObjectToSaveInFile;
    }

}

