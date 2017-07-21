using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MCDb{
	public int IdMC;
	public string NameMC;
	[TextArea()]
	public string Description;
	public int CoinUnlock;
	public int IAPUnlock;
	public float HpDefault;
	public float MpDefault;
	[Range(0,100)]
	public float MpRegen,MpRegenTime;
	[Range(0,100)]
	public int AMDefault,MRDefault;
	[Range(0,1)]
	public float DodgeDefault;
	[Range(0,100)]
	public int MoveSpeed;
	public float AnimationSpeed;

	public MCDb(){
	}

	public MCDb(MCDb MC){
		this.AMDefault = MC.AMDefault;
		this.AnimationSpeed = MC.AnimationSpeed;
		this.CoinUnlock = MC.CoinUnlock;
		this.Description = MC.Description;
		this.DodgeDefault = MC.DodgeDefault;
		this.HpDefault = MC.HpDefault;
		this.IAPUnlock = MC.IAPUnlock;
		this.IdMC = MC.IdMC;
		this.MoveSpeed = MC.MoveSpeed;
		this.MpDefault = MC.MpDefault;
		this.MpRegen = MC.MpRegen;
		this.MpRegenTime = MC.MpRegenTime;
		this.MRDefault = MC.MRDefault;
		this.NameMC = MC.NameMC;
	}
}

/*
+ IdMC: id phân biệt của các Main Character trong table.
+ NameMC: đoạn text là tên của Main Character.
+ Description: đoạn text mô tả về Main Character.
+ CoinUnlock: số lượng Coin sẽ phải trả để unlock MC.
+ IAPUnlock: số lượng IAP sẽ phải trả để unlock MC
+ HpDefault: tổng lượng Hp cơ bản lúc đầu mà MC có.
+ MpDefault: tổng lượng Mp cơ bản lúc đầu mà MC có.
+ Tự động hồi lại sau 1 khoảng thời gian: giá trị của MpRegenTime.
+ MpRegen: 1 con số tương ứng với 1 lượng Mp sẽ tự động hồi phục lại lên MpDefault mỗi lần.
+ MpRegenTime: 1 khoảng thời gian được tính bằng giây, mà sau bao lâu đó thì sẽ thực hiện MpRegen lên MpDefault
+ MoveSpeed: tốc độ di chuyển, 1 con số nguyên tương ứng với 1 số pixel / 1 giây.
+ AMDefault: tổng lượng Giáp cơ bản lúc đầu mà MC có.
+ MRDefault: tổng lượng Kháng phép cơ bản lúc đầu mà MC có
+ DodgeDefault: tỷ lệ % mà MC né được đòn của Quái(không bị tính dame)
+ MpDefault: tổng lượng Mp cơ bản lúc đầu mà MC có
+ AnimationSpeed: 1 con số, tương ứng với 1 hệ số tăng thêm vào tốc độ thực hiện ở tất cả các animation của MC
*/
