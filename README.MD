# 中介者模式(Mediator Pattern)

※又稱「居間協調者模式」

## 優點

- 藉由將物件彼此鬆綁，可增加再利用性
- 藉由將控制邏輯中央集權化，可簡化系統維護
- 可以讓物件之間傳遞的訊息，變得簡單而且大幅減少

## 缺點

- 如果使用不當，會造成「中介者」本身過於複雜

## 範例程式碼參考

```csharp
void Main()
{
	MemberService service = new MemberService();
	LogService logService = new LogService();
	SendLogMediator mediator = new SendLogMediator(service, logService);
	service.Login(new LoginModel
	{
		Acc = "Tony",
		Pwd = "1234"
	});
	service.Login(new LoginModel
	{
		Acc = "May",
		Pwd = "5678"
	});
}

public class SendLogMediator
{
	public SendLogMediator(MemberService memberService, LogService logService)
	{
		memberService.SendLogEvent += new MemberService.NumManipulationHandler(logService.SendLog);
	}
}

public class MemberService
{
	public delegate void NumManipulationHandler(LoginModel login);

	public event NumManipulationHandler SendLogEvent;

	public void Login(LoginModel login)
	{
		SendLogEvent(login);
	}
}

public class LogService
{
	public void SendLog<T>(T model)
	{
		Console.WriteLine($"Log 寫入 {model}");
	}
}

public class LoginModel
{
	public string Acc { get; set; }
	public string Pwd { get; set; }
}
```

> 範例中 MemberService 與 LogService 不會互相知道對方，完全透過 SendLogMediator 去做事件訂閱
>
> 透過事件訂閱去完成紀錄 Log 的動作