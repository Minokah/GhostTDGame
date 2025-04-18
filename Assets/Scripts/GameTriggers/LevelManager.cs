using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
	public GameObject Map1, Map2, Map3, Map4;
    public Tilemap Grid1, Grid2, Grid3, Grid4;
    Game Game;
    public Transform[] waypoints1, waypoints2A, waypoints2B, waypoints3, waypoints4A, waypoints4B;

    [Header("Music Clips (one per map)")]
    [Tooltip("0 ⇒ Map1, 1 ⇒ Map2, etc.")]
    public AudioClip[] mapMusicClips;

    void Start()
    {
        Game = Game.Get();

        AudioManager.Instance.PlayMusic(mapMusicClips[0]);
    }
    public void loadMap(int mapId){
        if (mapId == 0)
        {
            Map1.SetActive(true);
            Map2.SetActive(false);
            Map3.SetActive(false);
            Map4.SetActive(false);

            Game.TowerPlacementManager.grassTilemap = Grid1;

            Game.SpellManager.grassTilemap = Grid1;
			
			Game.EnemySpawner.waypoints = waypoints1;
			Game.EnemySpawner.waypointsAlternate = waypoints1;
        }
        else if (mapId == 1)
        {
            Map1.SetActive(false);
            Map2.SetActive(true);
            Map3.SetActive(false);
            Map4.SetActive(false);
            
            Game.TowerPlacementManager.grassTilemap = Grid2;

            Game.SpellManager.grassTilemap = Grid2;

			Game.EnemySpawner.waypoints = waypoints2A;
			Game.EnemySpawner.waypointsAlternate = waypoints2B;
        }
        else if (mapId == 2)
        {
            Map1.SetActive(false);
            Map2.SetActive(false);
            Map3.SetActive(true);
            Map4.SetActive(false);
            
            Game.TowerPlacementManager.grassTilemap = Grid3;

            Game.SpellManager.grassTilemap = Grid3;
			
			Game.EnemySpawner.waypoints = waypoints3;
			Game.EnemySpawner.waypointsAlternate = waypoints3;
        }
        else if (mapId == 3)
        {
            Map1.SetActive(false);
            Map2.SetActive(false);
            Map3.SetActive(false);
            Map4.SetActive(true);
            
            Game.TowerPlacementManager.grassTilemap = Grid4;

            Game.SpellManager.grassTilemap = Grid4;
			
			Game.EnemySpawner.waypoints = waypoints4A;
			Game.EnemySpawner.waypointsAlternate = waypoints4B;
        }

        if (mapMusicClips != null &&
            mapMusicClips.Length > mapId &&
            mapMusicClips[mapId] != null)
        {
            AudioManager.Instance.PlayMusic(mapMusicClips[mapId]);
        }
        else
        {
            Debug.LogWarning($"[LevelManager] No music clip set for map {mapId}");
        }
    }	
}
