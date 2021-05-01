<Query Kind="Program">
  <Reference>D:\GitHub\Chailease.Common\Chailease.Model\bin\Debug\Chailease.Model.dll</Reference>
  <Reference>D:\GitHub\Chailease.Common\Chailease.Utility\bin\Debug\Chailease.Utility.dll</Reference>
  <Namespace>Chailease.Utility.Helpers</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Encodings.Web</Namespace>
  <Namespace>System.Text.Unicode</Namespace>
</Query>

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
public static class ExtensionOfObject
{
	public static string ToJson<T>(this T model)
	{
		var options = new JsonSerializerOptions
		{
			Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
		};
		return JsonSerializer.Serialize(model, options);
	}
}

public class LogService
{
	public void SendLog<T>(T model)
	{
		Console.WriteLine($"Log 寫入 {model.ToJson()}");
	}
}

public class LoginModel
{
	public string Acc { get; set; }
	public string Pwd { get; set; }
}