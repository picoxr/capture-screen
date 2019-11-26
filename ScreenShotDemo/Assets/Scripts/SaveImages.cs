using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveImages : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void SaveImage(RenderTexture saveTexture)
    {
        if (saveTexture != null)
        {
            RenderTexture.active = saveTexture;

            Texture2D screenShot = new Texture2D((int)(saveTexture.width), (int)(saveTexture.height),
                                                 TextureFormat.RGB24, false);

            screenShot.ReadPixels(new Rect(0, 0, saveTexture.width, saveTexture.height), 0, 0);

            screenShot.Apply();

            byte[] bytes = screenShot.EncodeToPNG();
            string filepath = "/storage/emulated/0/Pictures/Screenshots/";

            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory(filepath);
            }
             string filename = filepath + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";

            System.IO.File.WriteAllBytes(filename, bytes);
          
            RenderTexture.active = null;
            Debug.Log("list111");
            AndroidJavaObject updateFileManager = new AndroidJavaObject("com.pico.updatefilestatus.UpdateFileClass");
            AndroidJavaObject ActivityContext = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            updateFileManager.Call("updateFileStatus", ActivityContext, filename);
            // PicoUnityActivity.CallObjectMethod("refresh", filepath);
        }
    }



}
