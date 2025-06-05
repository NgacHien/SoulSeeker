using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler 
{
    private string dataDirPatch = "";
    private string dataFileName = "";

    private bool encryptData = false;
    


   public FileDataHandler(string _dataDirPatch, string _dataFileName, bool _encryptData)
    {
        dataDirPatch = _dataDirPatch;
        dataFileName = _dataFileName;
        encryptData = _encryptData;
       
    }

    public void Save(GameData _data)
    {
        string fullPath = Path.Combine(dataDirPatch, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));


            string dataToStore = JsonUtility.ToJson(_data, true);
       

            if (encryptData)
            {
                dataToStore = EncryptDecrypt(dataToStore);
             
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }

        catch (Exception e)
        {
            Debug.Log("Error on trying to save to file: " + fullPath + "\n" + e);
        }

    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPatch, dataFileName);
        GameData loadData = null;

        if(File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath,FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }


                if(encryptData)
                    dataToLoad = EncryptDecrypt(dataToLoad);

                loadData = JsonUtility.FromJson<GameData>(dataToLoad);

            }

            catch (Exception e)
            {
                Debug.Log("Error while loading file: " + fullPath + "\n" + e);
            }
        }

         return loadData;
    }

    public void Delete()
    {
        string fullPath = Path.Combine(dataDirPatch, dataFileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    private string EncryptDecrypt(string _data)
    {
        string codeWord = "gamenotfun";
        string modifiedData = "";

        for (int i = 0; i < _data.Length; i++)
        {
            modifiedData += (char)(_data[i] ^ codeWord[i % codeWord.Length]);
        }

        return modifiedData;
    }

}
