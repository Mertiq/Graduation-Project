﻿using System.Collections.Generic;
using UnityEngine;

namespace MemoryMatchingGame
{
	public class GameManager : MonoBehaviour
	{
		//choosen Card gameObjects
		GameObject firstCardObject = null;
		GameObject secondCardObject = null;

		//choosen Cards
		Card firstCard = null;
		private Card secondCard = null;

		private int clickCounter;
		private bool canClick = true;

		readonly List<GameObject> cards = new List<GameObject>();

		[SerializeField] private bool isGameEnd;
		
		public delegate void GameEnd ();
		public static event GameEnd OnGameEnd;    
		
		public static int collectedCardsCount = 0;
		
		private void Start()
		{
			var cardsParent = GameObject.Find("Cards");

			for (var i = 0; i < cardsParent.transform.childCount; i++)
			{
				cards.Add(cardsParent.transform.GetChild(i).gameObject);
			}

		}

		private void FixedUpdate()
		{
			if (isGameEnd)
			{
				OnGameEnd?.Invoke();
				isGameEnd = !isGameEnd;
			}

			if (!canClick) return;
			if (!Input.GetMouseButtonDown(0)) return;
			if (Camera.main == null) return;
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (!Physics.Raycast(ray, out var hit, 100)) return;
			if (clickCounter == 0)
			{
				//assign card
				firstCardObject = hit.collider.gameObject;
				firstCard = firstCardObject.GetComponent<Card>();
				if (!firstCard.clickable) return;
				//turn card
				firstCardObject.GetComponent<Animator>().SetBool("turn", true);

				clickCounter++;
				firstCard.clickable = false;
			}
			else
			{
				//assign card
				secondCardObject = hit.collider.gameObject;
				secondCard = secondCardObject.GetComponent<Card>();

				if (!secondCard.clickable) return;
				//turn card
				secondCardObject.GetComponent<Animator>().SetBool("turn", true);

				canClick = false;

				//check card
				Invoke(nameof(Control), 1.8f);

				clickCounter = 0;
				secondCard.clickable = false;
			}
		}

		private void Control()
		{
			if (firstCard.cardType == secondCard.cardType)
			{
				firstCardObject.GetComponent<Animator>().SetBool("turn", false);
				secondCardObject.GetComponent<Animator>().SetBool("turn", false);
				firstCardObject.GetComponent<Animator>().SetBool("collect", true);
				secondCardObject.GetComponent<Animator>().SetBool("collect", true);

				firstCard.particleSystem.Play();
				secondCard.particleSystem.Play();
				collectedCardsCount += 2;

				if (collectedCardsCount >= 20)
				{
					isGameEnd = true;
					OnGameEnd?.Invoke();
				}
			}
			else
			{
				//turn card
				firstCardObject.GetComponent<Animator>().SetBool("turn", false);
				secondCardObject.GetComponent<Animator>().SetBool("turn", false);
				firstCard.clickable = true;
				secondCard.clickable = true;
			}
			canClick = true;
		}
	}
}

