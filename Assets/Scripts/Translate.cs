using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using UnityEngine;


public class Translate : MonoBehaviour
{
    void Start()
    {
        GetTranslationFromBaiduFanyi("These are available assignment operators. Since they all are very different, make sure to read the operator parameters description.", Language.EN, Language.ZH);
    }


    public static void GetTranslationFromBaiduFanyi(String source, Language from = Language.Auto, Language to = Language.Auto)
    {
        String jsonResult   = String.Empty;
        String languageFrom = from.ToString().ToLower();
        String languageTo   = to.ToString().ToLower();
        String appId        = "自己去注册";
        String appsecret    = "自己去注册";
        String randomNum    = System.DateTime.Now.Millisecond.ToString();
        String md5Sign      = GetMD5HashFromFile(appId + source + randomNum + appsecret);
        String url = String.Format("http://api.fanyi.baidu.com/api/trans/vip/translate?q={0}&from={1}&to={2}&appid={3}&salt={4}&sign={5}",
            source,
            languageFrom,
            languageTo,
            appId,
            randomNum,
            md5Sign
        );
        Console.WriteLine(url);
        WebClient wc = new WebClient();
        try
        {
            jsonResult = wc.DownloadString(url);
        }
        catch (Exception e)
        {
            jsonResult = string.Empty;
            Console.WriteLine(e.Message);
        }
        //Debug.Log(jsonResult);

        string                     strLine = jsonResult;
        Dictionary<string, object> dict    = MiniJSON.Json.Deserialize(strLine) as Dictionary<string, object>;
        //Debug.Log(dict["from"].ToString());
        //Debug.Log(dict["to"].ToString());
        List<object> provinceList = dict["trans_result"] as List<object>;
        foreach (var i in provinceList)
        {
            Dictionary<string, object> province = i as Dictionary<string, object>;
            Debug.Log(province["src"].ToString());
            Debug.Log(province["dst"].ToString());
        }
    }


    public static string GetMD5HashFromFile(string strToHash)
    {
        try
        {
            MD5    md5   = new MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(strToHash);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();
            String ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }

            return ret;
        }
        catch (Exception ex)
        {
            Debug.Log("Message " + System.DateTime.Now.ToLongTimeString() + " :" + ex.Message);
            return "Md5 Error";
        }
    }
}


public class Translation
{
    public string Src { get; set; }
    public string Dst { get; set; }
}


public class TranslationResult
{
    public string        Error_code   { get; set; }
    public string        Error_msg    { get; set; }
    public string        From         { get; set; }
    public string        To           { get; set; }
    public string        Query        { get; set; }
    public Translation[] Trans_result { get; set; }
}


public enum Language
{
    Auto = 0,
    ZH   = 1,
    JP   = 2,
    EN   = 3,
    KOR  = 4,
    SPA  = 5,
    FRA  = 6,
    TH   = 7,
    ARA  = 8,
    RU   = 9,
    PT   = 10,
    YUE  = 11,
    WYW  = 12,
    DE   = 13,
    NL   = 14,
    IT   = 15,
    EL   = 16
}