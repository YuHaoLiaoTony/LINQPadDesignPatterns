<Query Kind="Program">
  <Reference>D:\GitHub\Chailease.Common\Chailease.Model\bin\Debug\Chailease.Model.dll</Reference>
  <Reference>D:\GitHub\Chailease.Common\Chailease.Utility\bin\Debug\Chailease.Utility.dll</Reference>
  <Namespace>Chailease.Utility.Helpers</Namespace>
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
		_LogService = logService;
		_MemberService = memberService;
		memberService.SendLogEvent += new MemberService.NumManipulationHandler(_LogService.SendLog);
	}
	public LogService _LogService { get; set; }
	public MemberService _MemberService { get; set; }

	public void Work<T>(T model)
	{
		_LogService.SendLog(model);
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