using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnityWeb : MonoBehaviour
{
    [SerializeField] private string _url;

    [ContextMenu("SendRequest")]
    private void SendRequest()
    {

    }

    private IEnumerator GetSimpleRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            //if(webRequest.isNetworkError || webRequest.isHttpError)
            //{
            //    Debug.LogError(webRequest.error);
            //}
            //else
            //{
            //}

        }
    } 
}
