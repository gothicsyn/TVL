using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	public GameObject playerCharacter;
	public Camera mainCamera;
	public GameObject gameSettings;
	
	public float zOffset;
	public float yOffset;
	public float xRotOffset;
	
	private GameObject _pc;
	private PlayerCharacter _pcScript;
	
	public Vector3 _playerSpawnPointPos;			// The point in 3D space where the player will spawn

	// Use this for initialization
	void Start () {
		_playerSpawnPointPos  = new Vector3(420, 465, 752);
													// Default Spawn Point
		GameObject go = GameObject.Find(GameSettings.PLAYER_SPAWN_POINT);
		
		if(go == null){
			Debug.LogWarning("Cannot Find Player Spawn Point");
			
		go = new GameObject(GameSettings.PLAYER_SPAWN_POINT); 
			Debug.LogWarning("Created Player Spawn Point");
			
		go.transform.position = _playerSpawnPointPos;
			Debug.Log("Moved Player Spawn Point");
		}
		
		_pc = Instantiate(playerCharacter, go.transform.position, Quaternion.identity) as GameObject;
		_pc.name = "Character";
		
		_pcScript = _pc.GetComponent<PlayerCharacter>();
		
		zOffset = -2.5f;
		yOffset = 2.5f;
		xRotOffset = 22.5f;
		
		mainCamera.transform.position = new Vector3(_pc.transform.position.x, _pc.transform.position.y + yOffset, _pc.transform.position.z + zOffset);
		mainCamera.transform.Rotate(xRotOffset, 0, 0);
		
		LoadCharacter();
	}
	
	public void LoadCharacter(){
		GameObject gs = GameObject.Find("__GameSettings");
		
		if(gs == null){
			GameObject gs1 = Instantiate(gameSettings, Vector3.zero, Quaternion.identity) as GameObject;
			gs1.name = "__GameSettings";
		}
			GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();

			// Loading Character Data
			gsScript.LoadCharacterData();
	}	
}
