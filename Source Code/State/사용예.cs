
public class Car
{
	Behaviour myBehaviour;
	
	public void SetBehaviour<T>(T behaviour) where T : Behaviour
    {
        myBehaviour = behaviour;
    }
	
	public void DoSomething()
	{
		myBehaviour.DoSomething();
	}
}


public class Example
{
	public void OnEventOccurs()
	{
		/*이벤트 타입에 따른 런타임중 행동 변경*/
		ToWinterMode();     or
		ToFourWheelsMode(); or
		ToTwoWheelsMode();
		
		/*바뀐 상태에 따라 런타임중 다른 행동을 실행.*/
		DoSomething();
	}
	
	public ToWinterMode()
	{
		car.SetBehaviour<Winter>(new Winter());
	}
	
	public ToFourWheelsMode()
	{
		car.SetBehaviour<FourWheels>(new FourWheels());
	}
	
	public ToTwoWheelsMode()
	{
		car.SetBehaviour<TwoWheels>(new TwoWheels());
	}

	
}