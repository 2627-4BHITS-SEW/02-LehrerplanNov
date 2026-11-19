using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using System.Linq; 
 

public class HTTPSHandler : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine(loadTeachInfo("https://www.htl-salzburg.ac.at/lehrerinnen.html"));
        StartCoroutine(GetRooms("https://www.htl-salzburg.ac.at/lehrerinnen.html")); 
    }

    IEnumerator loadTeachInfo(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.certificateHandler = new BypassCertificate();
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {webRequest.error}");
            }
            else
            {
                Debug.Log($"Received: {webRequest.downloadHandler.text}");
            }   


            string pattern = @"href\s*=\s*(?:""(?<url>[^""]*)""|'(?<url>[^']*)')";
            
            List<string> links = new List<string>();

            foreach (Match match in Regex.Matches(webRequest.downloadHandler.text, pattern, RegexOptions.IgnoreCase))
            {
                if (match.Groups["url"].Success && match.Groups["url"].Value.Contains("/lehrerinnen-details") )
                {
                    links.Add(match.Groups["url"].Value);
                    Debug.Log(match.Groups["url"].Value); 
                }
            }
        }
    }


    IEnumerator GetRooms(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.certificateHandler = new BypassCertificate();
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {webRequest.error}");
            }
            else
            {
                Debug.Log($"Received: {webRequest.downloadHandler.text}");
            }   

            string roomPattern = @"[a-z]-\d{3}";

            string pattern = @"href\s*=\s*(?:""(?<url>[^""]*)""|'(?<url>[^']*)')";
            
            List<string> links = new List<string>();
    
            foreach (Match match in Regex.Matches(webRequest.downloadHandler.text, pattern, RegexOptions.IgnoreCase))
            {
                if (match.Groups["url"].Success && match.Groups["url"].Value.Contains("/lehrerinnen-details") )
                {
                    links.Add(match.Groups["url"].Value);
                    Debug.Log(match.Groups["url"].Value); 
                }
            }

            List<string> rooms = links
            .Select(link => Regex.Match(link, roomPattern))
            .Where(match => match.Success)
            .Select(match => match.Value)
            .Distinct()
            .OrderBy(val => val)
            .ToList();

            foreach(string s in rooms)
            {
                Debug.Log(s);     
            }
            
        }
    }


}