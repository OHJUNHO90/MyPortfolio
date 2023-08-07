
public class Example
{
	public void Main()
	{
		Tag tag = new Tag();
		
		/*초기 설정시 설정된 타입에 따라 런타임중 객체의 행동이 달라짐*/
		tag.Initialize(typeof(BasicTagBehavior).Name);
		tag.PointToCoordinate(SomeThing);
		
		tag.Initialize(typeof(TypeA).Name);
		tag.PointToCoordinate(SomeThing);
		
		tag.Initialize(typeof(TypeB).Name);
		tag.PointToCoordinate(SomeThing);
		
		tag.Initialize(typeof(TypeC).Name);
		tag.PointToCoordinate(SomeThing);
	}	
}