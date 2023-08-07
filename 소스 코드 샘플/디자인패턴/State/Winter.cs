
public class Winter : Behaviour
{
    protected Car target;

    public Behaviour(Car target) : base(target)
    {
        this.target = target;
    }

    public override void StepOnTheAccelerator()
    {
		//Hook method, 아무런 행동을 하지 않음.
    }

    public override void stepOnTheBrake()
    {
		//Do Something
    }

    //public override void ChangeGears()
    //{
        //basic method 실행
    //} 
}