using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemDb {
	public int IdItem;
	public string NameItem;
	[Range(0,4)]
	[Tooltip(@"0 : Vũ khí
1 : Áo giáp
2 : Kháng phép
3 : Tien
4 : Hỗ trợ")]
	public int TypeItem;
    public string Description;
	public int IdMC;
	[Range(0,1000)]
	public int DameAtk;
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float Vamp;
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float CristUp;
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float HpIncrease;
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float AMIncrease;
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float MRIncrease;
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float DodgeIncrease;
	[Range(0,1000)]
	public int UpCoin;
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float UpCoinIncrease;
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float UpAbility;
	[Range(0,100)]
	public int Uplevel;
	[Range(0,100)]
	public int UpMax;
	[Range(0,1)]
	[Tooltip("0 : Không sử dụng\n1 : Đang sử dụng")]
	public int Status;

	public ItemDb(){}

	public ItemDb(ItemDb item){
		this.AMIncrease = item.AMIncrease;
		this.CristUp = item.CristUp;
		this.DameAtk = item.DameAtk;
		this.DodgeIncrease = item.DodgeIncrease;
		this.HpIncrease = item.HpIncrease;
		this.IdItem = item.IdItem;
		this.IdMC = item.IdMC;
		this.MRIncrease = item.MRIncrease;
		this.NameItem = item.NameItem;
		this.Status = item.Status;
		this.TypeItem = item.TypeItem;
		this.UpAbility = item.UpAbility;
		this.UpCoin = item.UpCoin;
		this.UpCoinIncrease = item.UpCoinIncrease;
		this.Uplevel = item.Uplevel;
		this.UpMax = item.UpMax;
		this.Vamp = item.Vamp;
        this.Description = item.Description;
	}

	public void AddItem(ItemDb item){
		this.AMIncrease += item.AMIncrease;
		this.CristUp += item.CristUp;
		this.DameAtk += item.DameAtk;
		this.DodgeIncrease += item.DodgeIncrease;
		this.HpIncrease += item.HpIncrease;
		this.MRIncrease += item.MRIncrease;
		this.UpAbility += item.UpAbility;
		this.UpCoin += item.UpCoin;
		this.UpCoinIncrease += item.UpCoinIncrease;
		this.Uplevel += item.Uplevel;
		this.UpMax += item.UpMax;
		this.Vamp += item.Vamp;
	}
}

/* - IdItem: id phân biệt của các item trong table. 
- NameItem: đoạn text tên của item
- TypeItem: là 1 trường với mục đích tiện cho phân loại
- IdMC: giá trị trong trường IdMC thuộc MCdb mà tương ứng với 1 MC.
- DameAtk: sát thương của 1 đòn đánh thường
- Vamp: sẽ hồi lại 1 lượng Hp cho MC bằng với 1 số % / DameAtk đã được setup ở Vamp.
- CristUp: tỷ lệ % sẽ gây ra sát thương chí mạng khi MC tung đòn đánh thường.
- HpIncrease: 1 con số tăng thêm cho HpDefault của MC.
- AMIncrease: 1 con số tăng thêm cho AMDefault của MC.
- MRIncrease: 1 con số tăng thêm cho MRDefault của MC.
- DodgeIncrease: 1 con số tương ứng số % tăng thêm cho DodgeDefault của MC.
- UpCoin: số lượng Coin sẽ phải trả để thực hiện Upgrade từ lần upgrade +0 lên upgrade +1.
- UpCoinIncrease: 1 số % sẽ tăng thêm vào UpCoin sau mỗi lần upgrade.
- UpAbility: 1 số % sẽ tăng thêm sau upgrade cho tất cả các trường DameAtk, Vamp, CristUp, HpIncrease, AMIncrease, MRIncrease, DodgeIncrease.
- Uplevel: số lần đã upgrade của món item.
- UpMax: số lần có thể upgrade tối đa
- Status: trạng thái item đk sử dụng.
*/

