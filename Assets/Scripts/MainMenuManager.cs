using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System;

public class MainMenuManager : MonoBehaviour
{
	[Header("Assignables")]
	[SerializeField] private List<TextMeshProUGUI> timerTexts = new List<TextMeshProUGUI>();
	[SerializeField] private TextMeshProUGUI versionText;
	public void loadScene(int index)
	{
		SceneManager.LoadScene(index);
	}

	private void Start()
	{
		string gameSavePath = Application.persistentDataPath + "/save.save";
		if (File.Exists(gameSavePath))
		{
			try
			{
				string[] lines = File.ReadAllLines(gameSavePath);

				timerTexts[0].text = lines[1];
				timerTexts[1].text = lines[2];
				timerTexts[2].text = lines[3];
			}
			catch (Exception e)
			{
				//Debug.LogError(e);
				Debug.LogError(String.Format("Error in {0} whilst saving game", nameof(MainMenuManager)));
			}
		}
	}

	private void Awake()
	{
		versionText.text = "Version " + Application.version; 
	}
}
