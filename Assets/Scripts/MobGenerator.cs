// <summary>
/// MobGenerator.cs
/// 08-11-13
/// M A Plant
/// 
/// This is responsible for controlling the spawning of mobs in open spawn points
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobGenerator : MonoBehaviour {
	public enum State {
		Idle,
		Initialize,
		Setup,
		SpawnMob,
	}
	
	public GameObject[] mobPrefabs;						// An array to hold all the mob prefabs we'll be using
	public GameObject[] spawnPoints;					// This array holds all information on spawn points through the scene
	
	public State state;									// This is a local variable that holds the current state
	
	
	// Called before the script is run 
	void Awake(){
		state = MobGenerator.State.Initialize;
	}
	
	
	// Use this for initialization
	IEnumerator Start () {
		while(true){
			switch(state){
			case State.Initialize:
				Initialize();
				break;
			case State.Setup:
				Setup();
				break;
			case State.SpawnMob:
				SpawnMob();
				break;
			}
			
			yield return 0;	
		}
	}
	
	
	
	// Make sure everything is initialized before continuing
	private void Initialize(){
		Debug.Log("***Standing By In Initialize Mode***");
		
		if(!CheckForMobPrefabs())
			return;
		
		if(!CheckForSpawnPoints())
			return;
		
		state = MobGenerator.State.Setup;
	}
	
	
	
	// Make sure everything is setup before continuing
	private void Setup() {
		Debug.Log("***Standing By In Setup Mode***");
		
		state = MobGenerator.State.SpawnMob;
	}
	
	
	
	// Spawn a mob if there is an empty Spawn Point
	private void SpawnMob() {
		Debug.Log("***Spawn Mob***");
		
		GameObject[] gos = AvailableSpawnPoints();
		
		for(int cnt = 0; cnt < gos.Length; cnt ++){
			GameObject go = Instantiate(mobPrefabs[Random.Range(0, mobPrefabs.Length)],
										gos[cnt].transform.position,
										Quaternion.identity
										) as GameObject;
			
			go.transform.parent = gos[cnt].transform;
		}
		
		state = MobGenerator.State.Idle;
	}
	
	
	// Check to see if an enemy prefab and Spawn Point are available
	private bool CheckForMobPrefabs(){
		if(mobPrefabs.Length > 0)
			return true;
		else
			return false;
	}
	
	
	// Check to see if at least one Spawn Point is available
		private bool CheckForSpawnPoints(){
		if(spawnPoints.Length > 0)
			return true;
		else
			return false;
	}
	
	
	
	// Check for any Spawn Points that do not have a child element to them already
	private GameObject[] AvailableSpawnPoints(){
		List<GameObject> gos = new List<GameObject>();
		
		for(int cnt = 0; cnt < spawnPoints.Length; cnt ++){
			if(spawnPoints[cnt].transform.childCount == 0){	
				Debug.Log("*** Spawn Point Available***");
				gos.Add(spawnPoints[cnt]);
			}
		}
		
		return gos.ToArray();
	}
	
}
