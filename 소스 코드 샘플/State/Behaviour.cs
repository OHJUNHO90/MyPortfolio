
public class Behaviour 
{
    protected Car target;

    public Behaviour(Car target)
    {
        this.target = target;
    }

    public virtual void StepOnTheAccelerator()
    {
        //basic method
    }

    public virtual void stepOnTheBrake()
    {
        //basic method
    }

    public virtual void ChangeGears()
    {
        //basic method
    } 
}