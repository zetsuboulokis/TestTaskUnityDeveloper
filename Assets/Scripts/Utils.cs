using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Utils
{
    public static class Dowloader
    {
        public static IEnumerator LoadImage(string MediaUrl, RawImage rawImage)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log(request.error);
                    break;

                default:
                    rawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                    break;
            }
        }
    }
}
