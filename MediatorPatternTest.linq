<Query Kind="Program">
  <Reference>D:\GitHub\Chailease.Common\Chailease.Model\bin\Debug\Chailease.Model.dll</Reference>
  <Reference>D:\GitHub\Chailease.Common\Chailease.Utility\bin\Debug\Chailease.Utility.dll</Reference>
  <Namespace>Chailease.Utility.Helpers</Namespace>
</Query>

void Main()
{
	ConcreteMediator mediator = new ConcreteMediator(); // 中介者

	MedicColleague medic = new MedicColleague("小護士", mediator); // 醫護兵
	InfantryColleague infantry = new InfantryColleague("小小強", mediator); // 步兵

	medic.Send(MsgTypeEnum.Normal, "吹東風了");
	infantry.Send(MsgTypeEnum.Normal, "左前方一隻小白兔走過去");
	medic.Send(MsgTypeEnum.Attack, "遭受敵人攻擊");
	infantry.Send(MsgTypeEnum.Hurt, "我中彈了");
}
// 中介者抽像類別
abstract class Mediator
{
	protected MedicColleague medic; // 醫護兵
	protected InfantryColleague infantry; // 步兵

	// 設定醫護兵
	public MedicColleague Medic
	{
		set { medic = value; }
	}

	// 設定步兵
	public InfantryColleague Infantry
	{
		set { infantry = value; }
	}
	public abstract void Work(MsgTypeEnum msgType, string msgCon, Colleague colleague);
}
public enum MsgTypeEnum
{
	Hurt,
	Attack,
	Normal
}
// 中介者
class ConcreteMediator : Mediator
{

	// 中介者處理接收到的訊息
	public override void Work(MsgTypeEnum msgType, string msgCon, Colleague colleague)
	{
		Console.WriteLine("中介者 接收到 {0} 訊息：{1} => 訊息處理", colleague.Name, msgCon);
		switch (msgType)
		{
			case MsgTypeEnum.Hurt:
				this.medic.Action(msgCon, colleague);
				break;
			case MsgTypeEnum.Attack:
				this.infantry.Action(msgCon, colleague);
				break;
			case MsgTypeEnum.Normal:
				if (colleague != this.medic) this.medic.Receive(msgCon, colleague);
				if (colleague != this.infantry) this.infantry.Receive(msgCon, colleague);
				break;
		}
	}
}

// 同事抽象類別
abstract class Colleague
{
	protected string name; // 姓名

	protected Mediator mediator; // 中介者

	// 設定姓名、中介者
	public Colleague(string name, Mediator mediator)
	{
		this.name = name;
		this.mediator = mediator;
	}

	public string Name
	{
		get { return name; }
		set { name = value; }
	}

	// 發送訊息給中介者
	public void Send(MsgTypeEnum msgType, string msgCon)
	{
		mediator.Work(msgType, msgCon, this);
	}

	// 接收一般訊息
	public void Receive(string msgCon, Colleague colleague)
	{
		Console.WriteLine("{0} 接收到 {1} 訊息：{2}", this.name, colleague.Name, msgCon);
	}
}

// 醫護兵
class MedicColleague : Colleague
{
	public MedicColleague(string name, Mediator mediator)
		: base(name, mediator)
	{
		mediator.Medic = this;
	}

	// 醫療行動
	public void Action(string msgCon, Colleague colleague)
	{
		Console.WriteLine("{0} 接收到 {1} 訊息：{2}。前往救護中", this.name, colleague.Name, msgCon);
	}
}

// 步兵
class InfantryColleague : Colleague
{
	public InfantryColleague(string name, Mediator mediator)
		: base(name, mediator)
	{
		mediator.Infantry = this;
	}

	// 攻擊行動
	public void Action(string msgCon, Colleague colleague)
	{
		Console.WriteLine("{0} 接收到 {1} 訊息：{2}。前往救助中", this.name, colleague.Name, msgCon);
	}
}