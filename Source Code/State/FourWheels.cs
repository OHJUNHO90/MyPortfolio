
public class FourWheels : Behaviour
{
    protected Car target;

    public Behaviour(Car target) : base(target)
    {
        this.target = target;
    }

    //public override void StepOnTheAccelerator()
    //{
		//basic method 실행
    //}

    public override void stepOnTheBrake()
    {
		//Do Something
    }

    public override void ChangeGears()
    {
        //Hook method, 아무런 행동을 하지 않음.
    } 
}