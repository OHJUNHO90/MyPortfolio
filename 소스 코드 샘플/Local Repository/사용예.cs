
public void Execute()
{
	//Lcoal DB에서 데이터를 받아와 타겟 생성.
	SOME_THING example = new SOME_THING();
	example = LocalRepository.Instance.GetContainer<CarTypeContainer>(typeof(CarTypeContainer).Name).rows.Find(t => t.CAR_CD.Equals(targetCd));

	if (example == null)
	{
		Debug.LogError("Not found the target in CarContainer: " + targetCd);
		return;
	}

	target.transform.localScale = Utility.StringToVector3(example.CAR_W, example.CAR_H, example.CAR_L);
}