using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillDb{
	public int IdSkill;				//	~ id cua skill
	public int IdMC;				//	~ IdMc trong MCDb
	public string NameSkill;	//	~ ten hien thi cua skill
	[Range(0,1000)]
	public int MPCost; 			// ~ Mp cần de thuc hien skill. Min = 0
	[Range(0,10)]
	public float Duration;		// ~ thoi gian ton tai cua skill sau khi kich hoat
	[Range(0,50)]
	public float CoolDown;	//	~ thoi gian hoi skill
	[Range(0,1000)]
	public float DameSkill;	// ~ Sat thuong gay ra
	[Range(0,30)]
	public int Knock, Dash;	//	~ khoang cach day lui ve phia sau cua skill
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float Slow;			//	~ tyr le lam cham bot trung skill
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float SpeedUp;		//	~ ty le tang toc do di chuyen cua nhan vat
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float HpRegen;		//	~ tyr le hoi mau cho nhan vat
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float DodgeUp;	// ~ ty le tang ne don cua MC
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float AMUp;			// ~ ty le tang giap cho mc
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float MRUp;			// ~ ty le tang khang phep
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float DameUp;		// ~ ty le tang sat thuong
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float CristUp;		//	~ ty le tang sat thuong khi phep con tac dung
	[Range(0,1)]
	[Tooltip("0 : Không\n1 : Có")]
	public int Fear;				// ~ lam cho muc tieu quay nguoc mat lai : 0 =khong, 1 = co
	[Range(0,1)]
	[Tooltip("0 : Không\n1 : Có")]
	public int TakeDown;		// ~ lam muc tieu thuc hien animation Down
	[Range(0,1)]
	[Tooltip("0 : Không\n1 : Có")]
	public int Stun;				// ~ lam muc tien ko thuc hien dk hoat canh,
	[Range(0,1000)]
	public int UpLevel;			// ~ so level dk up cho skill
	[Range(0,100)]
	public int UpMax;			// ~ level max skill co the up
	[Range(0,10000)]
	public int CoinUp;			// ~ tien cho up tu level 0 - 1
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float CoinupInCrease;	// ~ ty le % them vao coinup
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float AblityUp;		// ~ ty le tang  them chi so cho ....
    private SkillDb skillDb;

    public SkillDb(SkillDb skillDb)
    {
        // TODO: Complete member initialization
        this.skillDb = skillDb;
    }

    public SkillDb() { }
}
