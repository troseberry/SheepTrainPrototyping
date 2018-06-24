using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class OverriddenNetworkLobbyManager : NetworkLobbyManager
{
    public NetworkDiscovery discovery;
    public Transform spawn_01;
    public Transform spawn_02;
    public Transform spawn_03;
    public Transform spawn_04;
    public List<Transform> startPositions;

    public void Start()
    {
        startPositions = base.startPositions;
        // I feel like finding might be better but I just wish I can add to list originally
//        startPositions.Add(GameObject.Find("Spawn_01").transform);
//        startPositions.Add(GameObject.Find("Spawn_02").transform);
//        startPositions.Add(GameObject.Find("Spawn_03").transform);
//        startPositions.Add(GameObject.Find("Spawn_04").transform);
        startPositions.Add(spawn_01);
        startPositions.Add(spawn_02);
        startPositions.Add(spawn_03);
        startPositions.Add(spawn_04);
    }



}