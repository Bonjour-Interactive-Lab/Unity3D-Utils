using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LoadParseJSON : MonoBehaviour
{
    public string configfile;
    private string pathToFile;

    private JObject config;

    private void Awake(){
    }

    public void LoadJson(){
        config = null;
        pathToFile = Path.Combine(Application.streamingAssetsPath, configfile);
        using (StreamReader stream = new StreamReader(pathToFile)) 
	    {
	        string json = stream.ReadToEnd();
	        config = JObject.Parse(json);
        }
    }

    public JObject GetConfig(){
        if(config == null){
            LoadJson();
        }
        return config;
    }

    public void SaveJSONConfig(JObject config){
        using (StreamWriter file = File.CreateText(pathToFile))
        using (JsonTextWriter writer = new JsonTextWriter(file))
        {
            config.WriteTo(writer);
        }
    }
}