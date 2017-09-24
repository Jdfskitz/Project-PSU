//using MDF_EDITOR;
//using MDF_EDITOR.DATABASE;
//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System.Timers;
//
//public class ProgressBar : EditorWindow
//{
//	public static ProgressBar DISPLAY;
//	public static float progress;
//	public static int TimeToWait;
//	private float timer;
//	private bool IsTiming;
//
//	public void Init() 
//	{ 
//		DISPLAY = EditorWindow.GetWindow<ProgressBar>();
//		DISPLAY.Show();
//		TimeToWait = 20;
//		timer = 0;
//		IsTiming = true;
//	} 
//	
//	void OnGUI(){
//		if(IsTiming == true)
//		{
//			//+= is the same thing as adding to the current variable
//			//timer = timer + time.delatime is the same thing as time +=... its just faster to use +=
//
//			timer += Time.deltaTime;
//			progress = timer / TimeToWait;
//			EditorUtility.DisplayProgressBar("Generating Scripts...","This is generating the scripts for the database", progress);
//		}
//		
//		if (timer > TimeToWait)
//		{
//			EditorUtility.ClearProgressBar();
//			IsTiming = false;
//			DATABASE_GUI.Init();
//			this.Close();
//
//			//do something, like destroy;
//		}
//		
//	}
//
//}
