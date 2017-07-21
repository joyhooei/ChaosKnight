using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemControll : MonoBehaviour {
	public GameObject[] Items;

	public void DropItems(int[] IdItems,Vector3 Position){
		foreach (var item in Items) {
			foreach(var id in IdItems){
				if (id == item.GetComponent<ItemControl> ().IdItem) {
					Instantiate (item, Position, Quaternion.identity);
					break;
				}
			}
		}
	}
}
