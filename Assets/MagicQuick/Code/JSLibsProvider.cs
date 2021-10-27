using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class JSLibsProvider : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void TakeScreenshot();

    [DllImport("__Internal")]
    private static extern void FireRecordButtonClicked();

    public void TakeScreenShot () {
		
        StartCoroutine(GetScreenshot());
		
    }
	
    IEnumerator GetScreenshot(){

        yield return new WaitForEndOfFrame();
		
        TakeScreenshot();
		
    }
    

    public void RecordButtonClicked()
    {
	    FireRecordButtonClicked();
    }


}
