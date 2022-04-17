using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LoadParseJSON : MonoBehaviour
{
    public string configfile;
    public string pathToFile;

    private JObject config;
    private string configAsAString;

    public enum SOURCETYPE{
        STREAMING_ASSET,
        RESOURCES_FOLDER,
        FROM_ASSETS_ROOT,
        CUSTOM
    }

    public SOURCETYPE sourcetype = SOURCETYPE.STREAMING_ASSET;

    private void Awake(){
        LoadJson();
        Debug.Log(config);
    }

    public void LoadJson(){
        config = null;
        string sourcePath = GetSourcePath();
        pathToFile = Path.Combine(sourcePath, $"{configfile}.json");
        using (StreamReader stream = new StreamReader(pathToFile)) 
	    {
	        string json = stream.ReadToEnd();
	        config = JObject.Parse(json);
        }
    }

    public void LoadAsString(){
        configAsAString = null;
        string sourcePath = GetSourcePath();
        pathToFile = Path.Combine(sourcePath, $"{configfile}.json");
        using (StreamReader stream = new StreamReader(pathToFile)) 
	    {
	        configAsAString = stream.ReadToEnd();
        }
    }

    public JObject GetConfig(){
        if(config == null){
            LoadJson();
        }
        return config;
    }

    public string GetConfigAsString(){
        if(configAsAString == null){
            LoadAsString();
        }
        return configAsAString;
    }

    private string GetSourcePath(){
        switch(sourcetype){
            default:
            case SOURCETYPE.STREAMING_ASSET : return Application.streamingAssetsPath;
            case SOURCETYPE.RESOURCES_FOLDER : return Application.dataPath + "/Resources";
            case SOURCETYPE.FROM_ASSETS_ROOT : return Application.dataPath + "/" + pathToFile;
            case SOURCETYPE.CUSTOM : return pathToFile;
        }
    }

    public void SaveJSONConfig(JObject config){
        using (StreamWriter file = File.CreateText(pathToFile))
        using (JsonTextWriter writer = new JsonTextWriter(file))
        {
            config.WriteTo(writer);
        }
    }
}