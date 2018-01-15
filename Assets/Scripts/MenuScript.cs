using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MenuScript : NetworkBehaviour {

	static GameObject mainMenu;
	static GameObject playerLoadingMenu;
	static GameObject roomSelectMenu;

	private int[] prevSymbols;
	private int[] currentSymbols;

	static Transform roomCodeBtn1;
	static Transform roomSelect1;
	static Transform roomSelect2;
	static Transform roomSelect3;

	static Transform roomCodeBtn2;
	static Transform roomSelect4;
	static Transform roomSelect5;
	static Transform roomSelect6;

	static Transform codeSelectorsBtn;
	static Transform roomBig1;
	static Transform roomBig2;
	static Transform roomBig3;

    //public GameObject[] players;

	// Use this for initialization
	void Start () {
		mainMenu = GameObject.Find ("Main");
		playerLoadingMenu = GameObject.Find ("PlayerLoading");
		roomSelectMenu = GameObject.Find ("RoomSelect");

		System.Random rando = new System.Random();
        prevSymbols = new int[]{rando.Next(0,3), rando.Next(0,3), rando.Next(0,3)};
		currentSymbols = new int[]{prevSymbols[0], prevSymbols[1], prevSymbols[2]};

		roomCodeBtn1 = playerLoadingMenu.transform.Find ("RoomCodeBtn");
		roomSelect1 = roomCodeBtn1.GetChild(0);
		roomSelect2 = roomCodeBtn1.GetChild(1);
		roomSelect3 = roomCodeBtn1.GetChild(2);

		roomCodeBtn2 = roomSelectMenu.transform.Find ("RoomCodeBtn");
		roomSelect4 = roomCodeBtn2.GetChild (0);
		roomSelect5 = roomCodeBtn2.GetChild (1);
		roomSelect6 = roomCodeBtn2.GetChild (2);

		codeSelectorsBtn = roomSelectMenu.transform.Find ("CodeSelectors");
		roomBig1 = codeSelectorsBtn.GetChild (0);
		roomBig2 = codeSelectorsBtn.GetChild (1);
		roomBig3 = codeSelectorsBtn.GetChild (2);

		for (int i = 0; i < 3; i++) {
			roomSelect1.GetChild (i).gameObject.SetActive (false);
			roomSelect2.GetChild (i).gameObject.SetActive (false);
			roomSelect3.GetChild (i).gameObject.SetActive (false);
			roomSelect4.GetChild (i).gameObject.SetActive (false);
			roomSelect5.GetChild (i).gameObject.SetActive (false);
			roomSelect6.GetChild (i).gameObject.SetActive (false);
			roomBig1.GetChild (i).gameObject.SetActive (false);
			roomBig2.GetChild (i).gameObject.SetActive (false);
			roomBig3.GetChild (i).gameObject.SetActive (false);
		}

		roomSelect1.GetChild (currentSymbols [0]).gameObject.SetActive(true);
		roomSelect2.GetChild (currentSymbols [1]).gameObject.SetActive (true);
		roomSelect3.GetChild (currentSymbols [2]).gameObject.SetActive (true);
		roomSelect4.GetChild (currentSymbols [0]).gameObject.SetActive (true);
		roomSelect5.GetChild (currentSymbols [1]).gameObject.SetActive (true);
		roomSelect6.GetChild (currentSymbols [2]).gameObject.SetActive (true);
		roomBig1.GetChild (currentSymbols [0]).gameObject.SetActive (true);
		roomBig2.GetChild (currentSymbols [1]).gameObject.SetActive (true);
		roomBig3.GetChild (currentSymbols [2]).gameObject.SetActive (true);

		playerLoadingMenu.SetActive (false);
		roomSelectMenu.SetActive (false);
	}

	public void toPlayerLoading () {
		playerLoadingMenu.SetActive (true);
		mainMenu.SetActive (false);
		roomSelectMenu.SetActive (false);
		NetworkManager.singleton.StartHost ();
		//Or StartClient if joining a game
		Debug.Log (currentSymbols [0]);
	}

	public void toMainMenu () {
		mainMenu.SetActive (true);
		playerLoadingMenu.SetActive (false);
		roomSelectMenu.SetActive (false);
	}

	public void toRoomSelect () {
		roomSelectMenu.SetActive (true);
		playerLoadingMenu.SetActive (false);
		mainMenu.SetActive (false);
	}

    public void StartGame () {
        Application.LoadLevel("MainGame");
    }

	public void nextSymbol(int thisNumber) {
		codeSelectorsBtn.GetChild(thisNumber).GetChild (currentSymbols[thisNumber]).gameObject.SetActive (false);
		currentSymbols[thisNumber]++;
		if (currentSymbols [thisNumber] >= 3) {
			currentSymbols [thisNumber] = 0;
		}
		codeSelectorsBtn.GetChild(thisNumber).GetChild (currentSymbols[thisNumber]).gameObject.SetActive (true);
	}

	public void prevSymbol(int thisNumber) {
		codeSelectorsBtn.GetChild(thisNumber).GetChild (currentSymbols[thisNumber]).gameObject.SetActive (false);
		currentSymbols[thisNumber]--;
		if (currentSymbols [thisNumber] < 0) {
			currentSymbols [thisNumber] = 2;
		}
		codeSelectorsBtn.GetChild(thisNumber).GetChild (currentSymbols[thisNumber]).gameObject.SetActive (true);
	}

	public void changeSymbols() {
		roomSelect1.GetChild (prevSymbols [0]).gameObject.SetActive (false);
		roomSelect2.GetChild (prevSymbols [1]).gameObject.SetActive (false);
		roomSelect3.GetChild (prevSymbols [2]).gameObject.SetActive (false);
		roomSelect4.GetChild (prevSymbols [0]).gameObject.SetActive (false);
		roomSelect5.GetChild (prevSymbols [1]).gameObject.SetActive (false);
		roomSelect6.GetChild (prevSymbols [2]).gameObject.SetActive (false);

		for (int i = 0; i < 3; i++) {
			prevSymbols [i] = currentSymbols [i];
		}

		roomSelect1.GetChild (prevSymbols [0]).gameObject.SetActive (true);
		roomSelect2.GetChild (prevSymbols [1]).gameObject.SetActive (true);
		roomSelect3.GetChild (prevSymbols [2]).gameObject.SetActive (true);
		roomSelect4.GetChild (prevSymbols [0]).gameObject.SetActive (true);
		roomSelect5.GetChild (prevSymbols [1]).gameObject.SetActive (true);
		roomSelect6.GetChild (prevSymbols [2]).gameObject.SetActive (true);

		//When a client is started, networkAddress is the address to connect to, and networkPort is the port to connect to.
		//Prob need to change these for different lobbies.
	}

    public void joinLobby(int playerID){
        //players[playerID - 1].GetComponent<Image>().color = Color.red;
    }

	public void resetSymbols() {
		for (int i = 0; i < 3; i++) {
			currentSymbols [i] = prevSymbols [i];
		}
	}
}
