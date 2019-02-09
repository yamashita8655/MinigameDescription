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
	
	// game4
	[SerializeField] GameObject[] Game4OdaiObjects;
	[SerializeField] GameObject OkButton;
	[SerializeField] GameObject YesButton;
	[SerializeField] GameObject NoButton;
	[SerializeField] Text Game4WinText;
	
	[SerializeField] GameObject ReidaiObject;
	[SerializeField] GameObject OdaiObject;
	private List<int> Game4SeikaiList = new List<int>(){
		0,0,0,1,0,1,1,0,1,0,0,1,1,1,0
	};
	
	// game5
	[SerializeField] GameObject[] Game5MondaiObjects;
	private List<int> Game5SeikaiList = new List<int>(){
		10, 4, 3, 2, 9
	};
	
	// game6
	[SerializeField] GameObject[] Game6MondaiObjects;
	[SerializeField] Text Game6WinText;
	private List<int> Game6SeikaiList = new List<int>(){
		0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 1
	};
	
	// game7
	[SerializeField] GameObject[] Game7MondaiObjects;
	[SerializeField] GameObject Game7ClickFilter;
	[SerializeField] GameObject Game7Setumei;
	[SerializeField] GameObject Game7Setumei2;
	[SerializeField] GameObject Game7StartButton;
	[SerializeField] GameObject Game7EndButton;
	[SerializeField] Text Game7Setumei2Text;
	private List<int> Game7SeikaiList = new List<int>(){
		0, 4, 5, 9, 11, 13, 15
	};
	
	// game8
	[SerializeField] Text[] Game8KotaeTexts;
	[SerializeField] Text Game8MondaiText;
	[SerializeField] Text Game8SeikaiText;
	private List<int> Game8SeikaiList = new List<int>(){
		0, 2, 2, 0, 1
	};
	private List<string> Game8MondaiList = new List<string>(){
		"３ー１＝？",
		"３×３＝？",
		"９÷３＝？",
		"１５＋？＝２７",
		"？×？＝１６",
	};
	private List<List<string>> Game8KotaeList = new List<List<string>>();

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
	
	public void OnClickGame4StartButton() {
		Game4OdaiObjects[MondaiCounter].SetActive(false);
		MondaiCounter++;

		OkButton.SetActive(false);
		YesButton.SetActive(true);
		NoButton.SetActive(true);
		
		ReidaiObject.SetActive(false);
		OdaiObject.SetActive(true);
	}
	
	public void OnClickGame4Button(int index) {
		if (MondaiCounter > 14) {
			return;
		}

		int seikai = Game4SeikaiList[MondaiCounter];

		if (index == seikai) {
			WinNumber++;
		}
		
		Game4WinText.text = WinNumber + "/14";
		Game4OdaiObjects[MondaiCounter].SetActive(false);
		MondaiCounter++;
	}
	
	public void OnClickGame5Button(int index) {
		if (MondaiCounter > 4) {
			return;
		}
		int seikai = Game5SeikaiList[MondaiCounter];
		if (seikai == index) {
			Game5MondaiObjects[MondaiCounter].SetActive(false);
			MondaiCounter++;
		}
	}
	
	public void OnClickGame6Button(int index) {
		if (MondaiCounter > 14) {
			return;
		}
		int seikai = Game6SeikaiList[MondaiCounter];
		Game6MondaiObjects[MondaiCounter].SetActive(false);
		if (seikai == index) {
			WinNumber++;
		}
		
		MondaiCounter++;
		Game6WinText.text = WinNumber + "/15";
	}
	
	public void OnClickGame7StartButton() {
		for (int i = 0; i < Game7MondaiObjects.Length; i++) {
			Game7MondaiObjects[i].GetComponent<Toggle>().isOn = true;
		}

		Game7Setumei.SetActive(false);
		Game7Setumei2.SetActive(true);

		Game7StartButton.SetActive(false);
		Game7EndButton.SetActive(true);
		Game7ClickFilter.SetActive(false);
				
		Game7Setumei2Text.text = "光っていたところを押して";
	}
	
	public void OnClickGame7EndButton() {
		int findCounter = 0;
		int counter = 0;
		for (int i = 0; i < Game7MondaiObjects.Length; i++) {
			if (Game7MondaiObjects[i].GetComponent<Toggle>().isOn == false) {
				counter++;
				bool find = FindGame7Index(i);
				if (find) {
					findCounter++;
				}
			}
		}

		if (findCounter == Game7SeikaiList.Count) {
			if (counter == findCounter) {
				Game7Setumei2Text.text = "正解！";
			} else {
				Game7Setumei2Text.text = "失敗！";
			}
		} else {
			Game7Setumei2Text.text = "失敗！";
		}
		
		Game7EndButton.SetActive(false);
		Game7ClickFilter.SetActive(true);
	}

	private bool FindGame7Index(int index) {
		bool find = false;
		for (int i = 0; i < Game7SeikaiList.Count; i++) {
			if (Game7SeikaiList[i] == index) {
				find = true;
				break;
			}
		}
		return find;
	}
	
	public void OnClickGame8Button(int index) {
		if (MondaiCounter > 4) {
			return;
		}
		int seikai = Game8SeikaiList[MondaiCounter];
		if (index == seikai) {
			WinNumber++;
		}

		MondaiCounter++;

		Game8SeikaiText.text = WinNumber + "/5";

		if (MondaiCounter > 4) {
			return;
		}

		Game8MondaiText.text = Game8MondaiList[MondaiCounter];
		Game8KotaeTexts[0].text = Game8KotaeList[MondaiCounter][0];
		Game8KotaeTexts[1].text = Game8KotaeList[MondaiCounter][1];
		Game8KotaeTexts[2].text = Game8KotaeList[MondaiCounter][2];
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
		for (int i = 0; i < Game4OdaiObjects.Length; i++) {
			Game4OdaiObjects[i].SetActive(true);
		}
		OkButton.SetActive(true);
		YesButton.SetActive(false);
		NoButton.SetActive(false);
		Game4WinText.text = "0/14";

		ReidaiObject.SetActive(true);
		OdaiObject.SetActive(false);

		MondaiCounter = 0;
		WinNumber = 0;
	}
	private void InitGame5() {
		for (int i = 0; i < Game5MondaiObjects.Length; i++) {
			Game5MondaiObjects[i].SetActive(true);
		}

		MondaiCounter = 0;
	}
	private void InitGame6() {
		for (int i = 0; i < Game6MondaiObjects.Length; i++) {
			Game6MondaiObjects[i].SetActive(true);
		}

		MondaiCounter = 0;
		WinNumber= 0;
		Game6WinText.text = "0/15";
	}

	private void InitGame7() {
		for (int i = 0; i < Game7MondaiObjects.Length; i++) {
			Game7MondaiObjects[i].GetComponent<Toggle>().isOn = true;
		}

		for (int i = 0; i < Game7SeikaiList.Count; i++) {
			Game7MondaiObjects[Game7SeikaiList[i]].GetComponent<Toggle>().isOn = false;
		}

		Game7Setumei.SetActive(true);
		Game7Setumei2.SetActive(false);
		Game7StartButton.SetActive(true);
		Game7EndButton.SetActive(false);
		Game7ClickFilter.SetActive(true);
	}
	private void InitGame8() {

		Game8KotaeList.Clear();
		Game8KotaeList.Add(new List<string>(){
			"２",
			"１",
			"４"
		});
		Game8KotaeList.Add(new List<string>(){
			"１",
			"６",
			"９"
		});
		Game8KotaeList.Add(new List<string>(){
			"２７",
			"１２",
			"３"
		});
		Game8KotaeList.Add(new List<string>(){
			"１２",
			"１３",
			"１４"
		});
		Game8KotaeList.Add(new List<string>(){
			"８",
			"４",
			"１６"
		});

		WinNumber = 0;
		MondaiCounter = 0;
		Game8MondaiText.text = Game8MondaiList[MondaiCounter];

		Game8SeikaiText.text = "0/5";

		Game8KotaeTexts[0].text = Game8KotaeList[MondaiCounter][0];
		Game8KotaeTexts[1].text = Game8KotaeList[MondaiCounter][1];
		Game8KotaeTexts[2].text = Game8KotaeList[MondaiCounter][2];
	}
}
