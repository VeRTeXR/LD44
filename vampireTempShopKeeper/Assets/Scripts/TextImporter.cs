using UnityEngine;

public class TextImporter : MonoBehaviour
{
	public TextAsset TextFile;
	public string[] TextLines;

	private void Awake()
	{
		if (TextFile != null) TextLines = TextFile.text.Split('\n');
	}

	public void Load(LevelConfiguration.Dialogue key)
	{
		if (key == LevelConfiguration.Dialogue.SleepInCoffin)
		{
			TextFile = Resources.Load<TextAsset>("Texts/D_Pub_01");
			if (TextFile != null) TextLines = TextFile.text.Split('\n');
		}
	}
}
