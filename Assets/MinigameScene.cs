using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameScene : MonoBehaviour {
	[SerializeField] GameObject GameSelectRoot;
	[SerializeField] GameObject[] GameRoots;
	[SerializeField] GameObject BackButton;
	
	// game1
	[SerializeField] GameObject[] PanelButtonObjects;
	[SerializeField] Text[] PanelButtonTexts;
	private List<int> TouchIndex = new List<int>() {
		5,2,4,3,1,7,8,6,9,10,14,12,15,13,11,20,19,18,16,17,23,25,21,24,22
	};
	private int NowNumber = 1;
	
	// game2
	[SerializeField] GameObject[] OdaiPanelObjects;
	[SerializeField] Text MondaiText;
	[SerializeField] Text WinText;
	private List<string> MondaiTexts = new List<string>() {
		"負けて", "勝って", "あいこ", "勝って", "勝って", "終わり"
	};
	// お題は、グー、チョキ、グー、パー、グー
	private List<int> SeikaiIndex = new List<int>() {
		1, 0, 0, 1, 2
	};
	private int MondaiCounter = 0;
	private int WinNumber = 0;

	// game3
	[SerializeField] GameObject[] LowNumberButtonObjects;
	[SerializeField] Text[] LowNumberButtonTexts;
	[SerializeField] Text LowWinText;
	private List<List<int>> MondaiList = new List<List<int>>();
	private List<List<int>> SeikaiList = new List<List<int>>();
	private int ButtonCounter = 0;
	private int LowNumber = 0;

	void Start() {
		Initialize();
	}

	private void Initialize() {
		BackButton.SetActive(false);
		SetGameSelectRootActive(true);
		for (int i = 0; i < GameRoots.Length; i++) {
			GameRoots[i].SetActive(false);
		}
	}
	
	public void OnClickGameButton(int index) {
		for (int i = 0; i < GameRoots.Length; i++) {
			GameRoots[i].SetActive(false);
		}

		GameRoots[index].SetActive(true);
		if (index == 0) {
			InitGame1();
		} else if (index == 1) {
			InitGame2();
		} else if (index == 2) {
			InitGame3();
		} else if (index == 3) {
			InitGame4();
		} else if (index == 4) {
			InitGame5();
		} else if (index == 5) {
			InitGame6();
		} else if (index == 6) {
			InitGame7();
		} else if (index == 7) {
			InitGame8();
		}
		
		SetGameSelectRootActive(false);
		BackButton.SetActive(true);
	}

	private void SetGameSelectRootActive(bool active) {
		GameSelectRoot.SetActive(active);
	}
	
	public void OnClickBackButton() {
		BackButton.SetActive(false);
		for (int i = 0; i < GameRoots.Length; i++) {
			GameRoots[i].SetActive(false);
		}
		SetGameSelectRootActive(true);
	}
	
	public void OnClick25PanelButton(int index) {
		int num = TouchIndex[index];
		if (num == NowNumber) {
			PanelButtonObjects[index].SetActive(false);
			NowNumber++;
		}
	}
	
	public void OnClickRPSButton(int index) {
		if (MondaiCounter >= 5) {
			return;
		}

		int seikai = SeikaiIndex[MondaiCounter];
		if (seikai == index) {
			WinNumber++;
		}

		OdaiPanelObjects[MondaiCounter].SetActive(false);
		WinText.text = WinNumber + "/5";
		MondaiCounter++;
		MondaiText.text = MondaiTexts[MondaiCounter];
	}
	
	public void OnClickLowButton(int index) {
		if (MondaiCounter >= 5) {
			return;
		}

		int seikai = SeikaiList[MondaiCounter][ButtonCounter];

		if (index == seikai) {
			ButtonCounter++;
			LowNumberButtonObjects[index].SetActive(false);
		} else {
			MondaiCounter++;
			ButtonCounter = 0;
			if (MondaiCounter < 5) {
				InitGame3Mondai();
			} else {
				for (int i = 0; i < LowNumberButtonObjects.Length; i++) {
					LowNumberButtonObjects[i].SetActive(false);
				}
			}
		}

		if (ButtonCounter == 5) {
			WinNumber++;
			MondaiCounter++;
			ButtonCounter = 0;
			if (MondaiCounter < 5) {
				InitGame3Mondai();
			}
		}
		LowWinText.text = WinNumber + "/5";
	}

	private void InitGame3Mondai() {
		for (int i = 0; i < LowNumberButtonObjects.Length; i++) {
			LowNumberButtonObjects[i].SetActive(true);
		}

		for (int i = 0; i < MondaiList[MondaiCounter].Count; i++) {
			LowNumberButtonTexts[i].text = MondaiList[MondaiCounter][i].ToString();
		}
	}
	
	private void InitGame1() {
		NowNumber = 1;
		for (int i = 0; i < PanelButtonObjects.Length; i++) {
			PanelButtonObjects[i].SetActive(true);
			PanelButtonTexts[i].text = TouchIndex[i].ToString();
		}
	}
	private void InitGame2() {
		for (int i = 0; i < OdaiPanelObjects.Length; i++) {
			OdaiPanelObjects[i].SetActive(true);
		}
		MondaiText.text = MondaiTexts[0];
		WinText.text = "0/5";
		MondaiCounter = 0;
	}

	private void InitGame3() {
		for (int i = 0; i < LowNumberButtonObjects.Length; i++) {
			LowNumberButtonObjects[i].SetActive(true);
		}
		
		SeikaiList.Add(new List<int>(){0,1,2,4,3});
		SeikaiList.Add(new List<int>(){3,4,1,2,0});
		SeikaiList.Add(new List<int>(){1,0,2,3,4});
		SeikaiList.Add(new List<int>(){4,1,0,2,3});
		SeikaiList.Add(new List<int>(){0,1,2,3,4});

		MondaiList.Add(new List<int>(){0,5,8,13,10});
		MondaiList.Add(new List<int>(){31,13,30,3,10});
		MondaiList.Add(new List<int>(){-5,-10,0,2,10});
		MondaiList.Add(new List<int>(){18,9,25,99,5});
		MondaiList.Add(new List<int>(){-30,-28,-9,0,2});
	
		MondaiCounter = 0;
		WinNumber = 0;
		ButtonCounter = 0;

		for (int i = 0; i < MondaiList[MondaiCounter].Count; i++) {
			LowNumberButtonTexts[i].text = MondaiList[MondaiCounter][i].ToString();
		}
		LowWinText.text = "0/5";
	}
	private void InitGame4() {
	
	}
	private void InitGame5() {
	
	}
	private void InitGame6() {
	
	}
	private void InitGame7() {
	
	}
	private void InitGame8() {
	
	}
}
