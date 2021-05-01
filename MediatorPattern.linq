<Query Kind="Program">
  <Reference>D:\GitHub\Chailease.Common\Chailease.Model\bin\Debug\Chailease.Model.dll</Reference>
  <Reference>D:\GitHub\Chailease.Common\Chailease.Utility\bin\Debug\Chailease.Utility.dll</Reference>
  <Namespace>Chailease.Utility.Helpers</Namespace>
</Query>

void Main()
{
	MemberService service = new MemberService();
	service.ChangeNum += new MemberService.NumManipulationHandler(new LogEvent().SendLog);
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
public class MemberService
{
	public delegate void NumManipulationHandler(LoginModel login);

	public event NumManipulationHandler ChangeNum;

	public void Login(LoginModel login)
	{
		ChangeNum(login);
	}
}

public class LogEvent
{
	public void SendLog(LoginModel login)
	{
		Console.WriteLine($"Log 寫入 Acc:{login.Acc} Pwd:{login.Pwd}");
	}
}

public class LoginModel
{
	public string Acc { get; set; }
	public string Pwd { get; set; }
}