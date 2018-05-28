using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using Qy_CSharp_NetWork.Tools.Json;
using Newtonsoft.Json.Linq;

namespace WJH
{
    public class EasyConfig : MonoBehaviour
    {
        public string ConfigFileName = "DeployXML/database.json"; // In StreamingAssets folder.
        public KeyCode ReloadKey = KeyCode.F5;

        private Dictionary<int, Config_Pos> configDict;

        public Dictionary<int, Config_Pos> ConfigDict
        {
            get
            {
                if (configDict == null)
                {
                    configDict = new Dictionary<int, Config_Pos>();

                }
                return configDict;
            }
        }

        public Config_Pos GetPosData(int status)
        {
            if (ConfigDict.Count==0)
            {
                SetConfig();
            }
            return ConfigDict[status];
        }


        public void SetConfig()
        {
            
            string ret = File.ReadAllText(Application.streamingAssetsPath + "/" + ConfigFileName);
            Debug.LogError(ret);
            JsonData data = JsonTools.GetJsonData(ret);
            JToken token = data["config_code"];

            foreach (var item in token)
            {
                Config_Pos pos = item.ToObject<Config_Pos>();
                ConfigDict.Add(pos.status, pos);
            }
        }
        [System.Serializable]
        public class Config_Pos
        {
            public int x, y, width, height,status;
            public override string ToString()
            {
                return string.Format("status:{4},x:{0},y:{1},width:{2},height:{3}", x, y, width, height,status);
            }
        }
    }
}
