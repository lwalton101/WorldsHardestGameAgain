using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
	[Header("Information")]
	public float distanceToGoal = 0.0f;
	public int fails = 0;

	[Header("Assignables")]
	[SerializeField] private Transform player;
	[SerializeField] private Tilemap tilemap;
	[SerializeField] private Transform goalMarker;
	[SerializeField] private TextMeshProUGUI failsText;
	private TimerManager timerManager;

	[Header("Lists")]
	[SerializeField] private List<TileBase> greenTiles = new List<TileBase>();
	[SerializeField] private List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();
	[SerializeField] private List<Coin> coins = new List<Coin>();
	private bool isComplete = false;

	public void removeCoin(Coin coin)
	{
		if (coins.Contains(coin))
		{
			coins.Remove(coin);
		}
	}

	public static GameManager instance;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Debug.LogError("2 GameManagers exist. Destroying one now");
			Destroy(gameObject);
		}

		if(timerManager == null)
		{
			timerManager = GetComponent<TimerManager>();
		}

		string gameSavePath = Application.persistentDataPath + "/save.save";
		if (File.Exists(gameSavePath))
		{
			try
			{
				string[] lines = File.ReadAllLines(gameSavePath);
				fails = Int32.Parse(lines[0]);
			}
			catch (Exception e)
			{
				//Debug.LogError(e);
				Debug.LogError(String.Format("Error in {0} whilst saving game", nameof(GameManager)));
			}
		}

		// ONLY ONE FAIL CAN HAPPEN BEFORE RESTART SO THIS CAN BE IN AWAKE TO HIDE IT
		failsText.text = "Fails: " + fails;
	}

	public void SaveGame(bool saveTimes)
	{
		string gameSavePath = Application.persistentDataPath + "/save.save";
		int fails = this.fails;
		string level1Time = "N/A";
		string level2Time = "N/A";
		string level3Time = "N/A";

		if (File.Exists(gameSavePath))
		{
			try
			{
				string[] lines = File.ReadAllLines(gameSavePath);
				level1Time = lines[1];
				level2Time = lines[2];
				level3Time = lines[3];
			}
			catch (Exception e)
			{
				//Debug.LogError(e);
				Debug.LogError(String.Format("Error in {0} whilst saving game", nameof(GameManager)));
			}

			File.Delete(gameSavePath);
		}
		if (saveTimes)
		{
			switch (SceneManager.GetActiveScene().buildIndex)
			{
				case 1:
					level1Time = timerManager.minutesText + ":" + timerManager.secondsText;
					break;
				case 2:
					level2Time = timerManager.minutesText + ":" + timerManager.secondsText;
					break;
				case 3:
					level3Time = timerManager.minutesText + ":" + timerManager.secondsText;
					break;
			} 
		}

		StreamWriter writer = new StreamWriter(gameSavePath, true);
		writer.WriteLine(fails.ToString());
		writer.WriteLine(level1Time);
		writer.WriteLine(level2Time);
		writer.WriteLine(level3Time);
		writer.Close();
	}
	//Saves game and sends to main menu
	//TODO: make SaveGame() function
	public void LevelComplete()
	{
		SaveGame(true);
		SceneManager.LoadScene(0);
	}

	public void QuitToMainMenu()
	{
		SaveGame(false);
		SceneManager.LoadScene(0);
	}

	private void Update()
	{
		//Once completed logic
		if (isComplete)
		{
			LevelComplete();
		}


		//changes distance to goal var dependant on player position and goalmarker gameobject
		distanceToGoal = Vector2.Distance(player.position, goalMarker.position);
	}

	private void FixedUpdate()
	{
		//Completion Logic
		//Needs to be on tile, not completed level already and no more coins
		TileBase onTile = tilemap.GetTile(Vector3Int.FloorToInt(player.transform.position));
		
		if (greenTiles.Contains(onTile) && !isComplete && coins.Count == 0)
		{
			Debug.Log("Level Complete on tile: " + onTile);
			isComplete = true;
		}
	}


	//Freezes all rigidbodies, increases fails variable and saves to file
	public void gameOver()
	{
		foreach (Rigidbody2D rb2D in rigidbodies)
		{
			if(rb2D == null)
			{
				continue;
			}
			rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
		}
		fails++;
		SaveGame(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
