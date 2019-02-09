using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameScene : MonoBehaviour {
	[SerializeField] GameObject GameSelectRoot;
	[SerializeField] GameObject[] GameRoots;
	[SerializeField] GameObject BackButton;

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
			if (i == index) {
				GameRoots[i].SetActive(true);
			} else {
				GameRoots[i].SetActive(false);
			}
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
}
