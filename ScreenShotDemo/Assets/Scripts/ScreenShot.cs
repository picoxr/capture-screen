using Pvr_UnitySDKAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public GameObject cube;
    public Eye eyeSide;
    void OnPostRender()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            cube.transform.Rotate(0, 0, 360 * Time.deltaTime);
            SaveImages.SaveImage(Pvr_UnitySDKManager.SDK.eyeTextures[Pvr_UnitySDKManager.SDK.currEyeTextureIdx + (int)eyeSide * 3]);
        }

    }
}
