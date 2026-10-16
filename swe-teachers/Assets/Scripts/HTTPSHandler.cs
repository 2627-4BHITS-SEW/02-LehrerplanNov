using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using System.Collections.Generic;
 

public class HTTPSHandler : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetRequestWithSelfSignedCert("https://www.htl-salzburg.ac.at/lehrerinnen.html"));
    }

    IEnumerator GetRequestWithSelfSignedCert(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Attach the custom certificate handler
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

            string pattern = @"href\s*=\s*(?:["']([^\"\']+)[\"\']|([^\s>]+))";
            
            MatchCollection matches = Regex.Matches(UnityWebRequest.text, pattern, RegexOptions.IgnoreCase);
            
            List<string> links = new List<string>();
            foreach (Match match in matches)
            {
                // Group 1 matches quoted URLs, Group 2 matches unquoted URLs
                string url = match.Groups[1].Success ? match.Groups[1].Value : match.Groups[2].Value;
                links.Add(url);
            }

            // Print the extracted links
            foreach (string link in links)
            {
                Debug.Log($"Found link: {link}");
            }
        }
    }
}