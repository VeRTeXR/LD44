using UnityEngine;
using System.Collections;

public class TextImporter : MonoBehaviour {

		public TextAsset TextFile;
		public string[] TextLines;

	void Start () {
		if(TextFile!=null) 
		{
			TextLines = (TextFile.text.Split('\n'));
		}
	}

}
