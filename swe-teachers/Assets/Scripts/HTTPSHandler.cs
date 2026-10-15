using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// Source - https://stackoverflow.com/a/46008025
// Posted by Programmer, modified by community. See post 'Timeline' for change history
// Retrieved 2026-10-15, License - CC BY-SA 3.0
public HTTPSHandler{



    void Start()
    {
        StartCoroutine(getRequest("https://www.htl-salzburg.ac.at/lehrerinnen.html"));
    }

    
}

IEnumerator getRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }
