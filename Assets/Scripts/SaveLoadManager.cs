
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

using System.IO;

public class SaveLoadManager : MonoBehaviour
{

    private static SaveLoadManager instance;

    public static SaveLoadManager GetInstance() {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    private List<SaveObject> bricksPlaced;

    public void SaveOnPlace(int id, Vector3 objPosition, Vector3 obScale, Quaternion objRotation)
    {


        SaveObject saveObject = new SaveObject
        {
            id = id,
            position = objPosition,
            rotation = objRotation,
            scale = obScale,
        };


        bricksPlaced.Add(saveObject);
        SaveListOnFile();

    }



    private void SaveListOnFile()
    {

        string json = JsonConvert.SerializeObject(bricksPlaced, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );

        var fileName = "bricks";

        File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".txt", json);

    }

}
