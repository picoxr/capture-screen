using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;

public class save : MonoBehaviour {

	// Use this for initialization
    public Text txt;
    public GameObject cube;
    public Camera uiCamera;
    void Start()
    {
        
        //Capture(); 
        //StartCoroutine(Capture2(new Rect(0, 0, Screen.width, Screen.height)));
        //Capture3(Camera.main, new Rect(0, 0, Screen.width, Screen.height));

       
       // StartCoroutine(.(new Rect(0,0,Screen.width, Screen.height)));
    }

   void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                cube.transform.Rotate(0, 0, 360 * Time.deltaTime);
              
               string destination = "/storage/emulated/0/Pictures/Screenshots/";
                if (!Directory.Exists(destination))
                {
                    Directory.CreateDirectory(destination);
                 
                }
                //string strpath = Application.dataPath;
                string nowtime = Time.time.ToString();
                ScreenCapture.CaptureScreenshot(destination + nowtime + "U.png");
                txt.text = destination;
           }
          else
            {
                Debug.Log("截图成功");
                cube.transform.Rotate(0, 0, 360 * Time.deltaTime);
                //string strpath = Application.dataPath;
                string strpath = "D:/zero/";
                string nowtime = Time.time.ToString();
                ScreenCapture.CaptureScreenshot(strpath+nowtime+".png");
                txt.text = strpath;
            }

           Capture3(uiCamera, new Rect(0, 0, Screen.width, Screen.height));
           Capture4();

        }
    }

 /*  public void button()
   {
       if (Application.platform == RuntimePlatform.Android)
       {
           cube.transform.Rotate(0, 0, 360 * Time.deltaTime);
           //string destination = "/storage/emulated/0/Android/com.picovr.data/";
           string destination = "/sdcard/Android/saveimage/";
           if (!Directory.Exists(destination))
           {
               Directory.CreateDirectory(destination);
           }
           //string strpath = Application.dataPath;
           string nowtime = Time.time.ToString();
           Application.CaptureScreenshot(destination + nowtime + ".png");
           txt.text = destination;
       }
       else
       {
           Debug.Log("截图成功");
           cube.transform.Rotate(0, 0, 360 * Time.deltaTime);
           //string strpath = Application.dataPath;
           string strpath = "D:/zero/";
           string nowtime = Time.time.ToString();
           Application.CaptureScreenshot(strpath + nowtime + ".png");
           txt.text = strpath;
       }
   }*/

    public void Capture()
    {
        //保存在工程的根目录下
        ScreenCapture.CaptureScreenshot("Screenshot.png");
        Debug.Log("Capture");
    }

    // 读取屏幕像素
    public IEnumerator Capture2(Rect rect)
    {
        // 先创建一个的空纹理，大小可根据实现需要来设置  
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);

        // 读取屏幕像素信息并存储为纹理数据
        yield return new WaitForEndOfFrame();
        screenShot.ReadPixels(rect, 0, 0, false);
        screenShot.Apply();

        // 然后将这些纹理数据，成一个png图片文件  
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = Application.dataPath + "/Screenshot2.png";
        System.IO.File.WriteAllBytes(filename, bytes);

       // UnityEditor.AssetDatabase.Refresh();
        Debug.Log(string.Format("截屏了一张图片: {0}", filename));
    }

    // 读取RenderTexture像素
    Texture2D Capture3(Camera camera, Rect rect)
    {
        // 创建一个RenderTexture对象  
        RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 0);
        // 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
        camera.targetTexture = rt;
        camera.Render();
        //ps: --- 如果这样加上第二个相机，可以实现只截图某几个指定的相机一起看到的图像。  
        //ps: camera2.targetTexture = rt;  
        //ps: camera2.Render();  
        //ps: -------------------------------------------------------------------  

        // 激活这个rt, 并从中中读取像素。  
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
        screenShot.Apply();

        // 重置相关参数，以使用camera继续在屏幕上显示  
        camera.targetTexture = null;
        //ps: camera2.targetTexture = null;  
        RenderTexture.active = null; // JC: added to avoid errors  
        GameObject.Destroy(rt);

        string nowtime = Time.time.ToString();
        // 最后将这些纹理数据，成一个png图片文件  
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = "/storage/emulated/0/Pictures/Screenshots/" + nowtime+".png";
        System.IO.File.WriteAllBytes(filename, bytes);
       // AssetDatabase.Refresh();//只能在编辑器模式下使用
        Debug.Log(string.Format("截屏了一张照片: {0}", filename));

        return screenShot;
    }

    void Capture4()
    {
        //获取系统时间并命名相片名 
        System.DateTime now = System.DateTime.Now;
        string times = now.ToString();
        times = times.Trim();
        times = times.Replace("/", "-");
        //文件名
        string filename = "Screenshot444" + times + ".png";
        //判断是否为Android平台 
        if (Application.platform == RuntimePlatform.Android)
        {

            //截取屏幕 
            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply();
            //转为字节数组 
            byte[] bytes = texture.EncodeToPNG();

            string destination = "/storage/emulated/0/Pictures/Screenshots/";
            //判断目录是否存在，不存在则会创建目录 
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }
            //文件路径
            string Path_save = destination  + filename;
            //存图片 
            System.IO.File.WriteAllBytes(Path_save, bytes);
        }
    }
}
