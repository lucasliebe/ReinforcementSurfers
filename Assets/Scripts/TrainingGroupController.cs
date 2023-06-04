using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingGroupController : MonoBehaviour
{
	public int numTrainingAreas = 10;
	public int numAreasPerRow = 10;

	public int areaOffsetX = 10;
	public int areaOffsetY = 7;

	public GameObject trainingAreaPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numTrainingAreas; i++) {
			int column = i % numAreasPerRow;
			int row = i / numAreasPerRow;
			
			Instantiate(trainingAreaPrefab, transform.position + new Vector3(areaOffsetX * column, areaOffsetY * row, 0), Quaternion.identity, transform);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
