

/*튜플 사용 예*/
public (int, string) GetClosePathPointInfo(Vector3 v3)
{
	PathPointInfo pointInfo = new PathPointInfo();

	int intX = (int)Math.Truncate(v3.x);
	float minDistance = 99999f;             

	Compare(v3, intX - 1, ref pointInfo, ref minDistance);
	Compare(v3, intX, ref pointInfo, ref minDistance);
	Compare(v3, intX + 1, ref pointInfo, ref minDistance);

	return (pointInfo.localIndex, pointInfo.lineCd);
}


public void PointToCoordinate(Vector3 v3)
{
	(int localindex, string linecd) PathInfo = GetClosePathPointInfo(v3);
	
	if (PathInfo == default)
	{
		Debug.Log("The segments could not be found.);
		return;
	}
	
	// DO Something
}