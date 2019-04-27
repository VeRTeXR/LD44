using System;
using System.Collections.Generic;
using System.Diagnostics;
using SuperTags;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = System.Random;

namespace Assets.SuperTags.Editor
{
	
	
	
    [CustomEditor(typeof(SuperTag))]
    public class SuperTagEditor : UnityEditor.Editor
    {
        private GUISkin _superTagEditorSkin;
        private string _tagToAdd;
        private SuperTag _superTagsComponent;

        public override void OnInspectorGUI()
        {
//            Debug.Log("Rendering");
//            if (Application.isPlaying)
//                GUI.enabled = false;
//            else
//                Repaint();
            _superTagsComponent = (SuperTag) target; 
            var tags = _superTagsComponent.Tags;

            GUILayout.Space(10);

            if (_superTagEditorSkin == null)
            {
                _superTagEditorSkin = LoadGuiSkin();
                if (_superTagEditorSkin == null)
                {
                    Debug.LogError(
                        "The SuperTag Editor skin was not found. Did you delete or move it? Try to re-import the package from the Unity Asset Store to fix this issue.");
                    return;
                }
            }

            float currentLabelPos = 0;

            GUILayout.BeginHorizontal();

            for (var i = 0; i < tags.Count; i++)
            {
                Color backgroundColor, foregroundColor;
                GetColorTuple(tags[i], out backgroundColor, out foregroundColor);

                var tagsStyle = new GUIStyle(GUI.skin.box)
                {
                    normal =
                    {
                        background = Texture2D.whiteTexture,
                        textColor = foregroundColor
                    },
                    active =
                    {
                        background = Texture2D.whiteTexture,
                        textColor = foregroundColor
                    },
                    wordWrap = false,
                    stretchWidth = false
                };

                var tagString = string.Format("{0}        ", tags[i]);
                var labelSizeX = tagsStyle.CalcScreenSize(tagsStyle.CalcSize(new GUIContent(tagString))).x * 2;
                currentLabelPos += labelSizeX;
                
//              Debug.Log(tags[i] + " currentLabelPos" + currentLabelPos + " | CalcScreenSize" + tagsStyle.CalcScreenSize (tagsStyle.CalcSize( new GUIContent(tagString))).x + " | " + Screen.width);
                
                if (currentLabelPos > Screen.width)
                {
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    currentLabelPos = labelSizeX;
                }

                GUI.backgroundColor = backgroundColor;
                GUILayout.Box( tagString, tagsStyle );
         
                Rect labelRect = GUILayoutUtility.GetLastRect();

                GUI.backgroundColor = Color.black;
                labelRect = new Rect(labelRect.xMax - 18, labelRect.yMin + 2, 14, 14);
                if (GUI.Button(labelRect, "", _superTagEditorSkin.GetStyle("RemoveIcon")))
                {
                    _superTagsComponent.RemoveTag(tags[i]);
//                    Debug.Log("Remove!!!");
                }

            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            
            GUI.backgroundColor = Color.white;
            
            _tagToAdd = TextFieldAutoComplete(_tagToAdd, SuperTagsSystem.GetAllTags());
//            _tagToAdd = EditorGUILayout.TextField(_tagToAdd);
            if (GUILayout.Button("Add Tag", EditorStyles.miniButton)
		        || (Event.current.rawType == EventType.KeyUp && Event.current.keyCode == KeyCode.Return))
            {
                _superTagsComponent.AddTag(_tagToAdd);
                _tagToAdd = "";
            }
            
//            if (Event.current.rawType == EventType.KeyUp && Event.current.keyCode == KeyCode.Tab)
//            {
//                Debug.Log("AutoComplete!");
//            }

        }

        private static void GetColorTuple(string inString, out Color backgroundColor, out Color foregroundColor)
        {
            var sw = new Stopwatch();
            sw.Start();
            var hash = inString.GetHashCode();
            var rand = new System.Random(hash);
            backgroundColor = GetRandomColorFromString(rand);
            foregroundColor = Color.black;
            while (!IsContrastReadable(backgroundColor, foregroundColor))
            {
                backgroundColor = GetRandomColorFromString(rand);
                if (sw.ElapsedMilliseconds > 1000)
                {
                    Debug.LogWarning("BAD!!!");
                    return;
                }
            }
        }

        private static Color GetRandomColorFromString(System.Random rand)
        {
            var r = rand.Next(0, 256);
            var g = rand.Next(0, 256);
            var b = rand.Next(0, 256);
            return new Color(r/255f, g/255f, b/255f);
        }
        
        private static bool IsContrastReadable(Color color1, Color color2)
        {
            // Maximum contrast would be a value of "1.0f" which is the brightness
            // difference between "Color.Black" and "Color.White"
            const float minContrast = 0.5f;

            var brightness1 = GetBrightness(color1);
            var brightness2 = GetBrightness(color2);

            // Contrast readable?
            return (Math.Abs(brightness1 - brightness2) >= minContrast);
        }

        private static float GetBrightness(Color col)
        {
            return (col.r + col.g + col.b) / 3;
//            return (float) ((int) Math.Max(col.r, Math.Max(col.g, col.b)) + (int) Math.Min(col.r, Math.Min(col.g, col.b))) / 2f;
        }



        private GUISkin LoadGuiSkin()
        {
            return Resources.Load<GUISkin>("SuperTagSkin");
        }
        
        
        
        #region Text AutoComplete
		/// <summary>The internal struct used for AutoComplete (Editor)</summary>
		private struct EditorAutoCompleteParams
		{
			public const string FieldTag = "AutoCompleteField";
			public static readonly Color FancyColor = new Color(.6f, .6f, .7f);
			public static readonly float optionHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
			public const int fuzzyMatchBias = 3; // input length smaller then this letter, will not trigger fuzzy checking.
			public static List<string> CacheCheckList = null;
			public static string lastInput;
			public static string focusTag = "";
			public static string lastTag = ""; // Never null, optimize for length check.
			public static int selectedOption = -1; // record current selected option.
			public static Vector2 mouseDown;
			public static void Deselect()
			{
				selectedOption = -1;
				GUI.FocusControl("");
			}
		}
	
		private static string TextFieldAutoComplete(string input, string[] source, int maxShownCount = 5, float levenshteinDistance = 0.5f)
		{
			return TextFieldAutoComplete(EditorGUILayout.GetControlRect(), input, source, maxShownCount, levenshteinDistance);
		}

	    private static string TextFieldAutoComplete(Rect position, string input, string[] source, int maxShownCount = 5, float levenshteinDistance = 0.5f)
		{
			// Text field
			var controlId = GUIUtility.GetControlID(FocusType.Passive);
			var tag = string.Format("{0}{1}", EditorAutoCompleteParams.FieldTag, controlId);
			GUI.SetNextControlName(tag);
			var result = EditorGUI.TextField(position, input);
			// Matching with giving source
			if (!string.IsNullOrEmpty(input) &&
				(EditorAutoCompleteParams.lastTag.Length == 0 || EditorAutoCompleteParams.lastTag == tag) && 
				GUI.GetNameOfFocusedControl() == tag) 
			{
				// Matching
				if (EditorAutoCompleteParams.lastInput != input || 
					EditorAutoCompleteParams.focusTag != tag) // switch focus from another field.
				{
					// Update cache
					EditorAutoCompleteParams.focusTag = tag;
					EditorAutoCompleteParams.lastInput = input;
					List<string> uniqueSrc = new List<string>(new HashSet<string>(source)); // remove duplicate
					int srcCnt = uniqueSrc.Count;
					EditorAutoCompleteParams.CacheCheckList = new List<string>(System.Math.Min(maxShownCount, srcCnt));
					// Start with - slow
					for (int i = 0; i < srcCnt && EditorAutoCompleteParams.CacheCheckList.Count < maxShownCount; i++)
					{
						if (uniqueSrc[i].ToLower().StartsWith(input.ToLower()))
						{
							EditorAutoCompleteParams.CacheCheckList.Add(uniqueSrc[i]);
							uniqueSrc.RemoveAt(i);
							srcCnt--;
							i--;
						}
					}
					// Contains - very slow
					if (EditorAutoCompleteParams.CacheCheckList.Count == 0)
					{
						for (int i = 0; i < srcCnt && EditorAutoCompleteParams.CacheCheckList.Count < maxShownCount; i++)
						{
							if (uniqueSrc[i].ToLower().Contains(input.ToLower()))
							{
								EditorAutoCompleteParams.CacheCheckList.Add(uniqueSrc[i]);
								uniqueSrc.RemoveAt(i);
								srcCnt--;
								i--;
							}
						}
					}
	//				// Levenshtein Distance - very very slow.
	//				if (levenshteinDistance > 0f && // only developer request
	//					input.Length > EditorAutoCompleteParams.fuzzyMatchBias && // bias on input, hidden value to avoid doing it too early.
	//					EditorAutoCompleteParams.CacheCheckList.Count < maxShownCount) // have some empty space for matching.
	//				{
	//					levenshteinDistance = Mathf.Clamp01(levenshteinDistance);
	//					string keywords = input.ToLower();
	//					for (int i = 0; i < srcCnt && EditorAutoCompleteParams.CacheCheckList.Count < maxShownCount; i++)
	//					{
	//						int distance = Kit.Extend.StringExtend.LevenshteinDistance(uniqueSrc[i], keywords, caseSensitive: false);
	//						bool closeEnough = (int)(levenshteinDistance * uniqueSrc[i].Length) > distance;
	//						if (closeEnough)
	//						{
	//							EditorAutoCompleteParams.CacheCheckList.Add(uniqueSrc[i]);
	//							uniqueSrc.RemoveAt(i);
	//							srcCnt--;
	//							i--;
	//						}
	//					}
	//				}
				}
				// Draw recommend keyword(s)
				if (EditorAutoCompleteParams.CacheCheckList.Count > 0)
				{
					Event evt = Event.current;
					var cnt = EditorAutoCompleteParams.CacheCheckList.Count;
					var height = cnt * EditorAutoCompleteParams.optionHeight;
					var area = new Rect(position.x, position.y - height, position.width, height);
					
					// Fancy color UI
					EditorGUI.BeginDisabledGroup(true);
					EditorGUI.DrawRect(area, EditorAutoCompleteParams.FancyColor);
					GUI.Label(area, GUIContent.none, GUI.skin.button);
					EditorGUI.EndDisabledGroup();
					// Click event hack - part 1
					// cached data for click event hack.
					if (evt.type == EventType.Repaint)
					{
						// Draw option(s), if we have one.
						// in repaint cycle, we only handle display.
						var line = new Rect(area.x, area.y, area.width, EditorAutoCompleteParams.optionHeight);
						for (int i = 0; i < cnt; i++)
						{
							EditorGUI.LabelField(line, GUIContent.none);//, (input == EditorAutoCompleteParams.CacheCheckList[i]));
							var option = EditorGUI.IndentedRect(line);
							if (line.Contains(evt.mousePosition))
							{
								// hover style
								EditorGUI.LabelField(option, EditorAutoCompleteParams.CacheCheckList[i], GUI.skin.textArea);
								EditorAutoCompleteParams.selectedOption = i;
								GUIUtility.hotControl = controlId; // required for Cursor skin. (AddCursorRect)
								EditorGUIUtility.AddCursorRect(area, MouseCursor.ArrowPlus);
							}
							else if (EditorAutoCompleteParams.selectedOption == i)
							{
								// hover style
								EditorGUI.LabelField(option, EditorAutoCompleteParams.CacheCheckList[i], GUI.skin.textArea);
							}
							else
							{
								EditorGUI.LabelField(option, EditorAutoCompleteParams.CacheCheckList[i], EditorStyles.label);
							}
							line.y += line.height;
						}
						// when hover popup, record this as the last usein tag.
						if (area.Contains(evt.mousePosition) && EditorAutoCompleteParams.lastTag != tag)
						{
							// Debug.Log("->" + tag + " Enter popup: " + area);
							// used to trigger the clicked checking later.
							EditorAutoCompleteParams.lastTag = tag;
						}
					}
					else if (evt.type == EventType.MouseDown)
					{
						if (area.Contains(evt.mousePosition) || position.Contains(evt.mousePosition))
						{
							EditorAutoCompleteParams.mouseDown = evt.mousePosition;
						}
						else
						{
							// click outside popup area, deselecting.
							EditorAutoCompleteParams.Deselect();
						}
					}
					else if (evt.type == EventType.MouseUp)
					{
						if (position.Contains(evt.mousePosition))
						{
							// common case click on textfield.
							return result;
						}
						else if (area.Contains(evt.mousePosition))
						{
							if (Vector2.Distance(EditorAutoCompleteParams.mouseDown, evt.mousePosition) >= 3f)
							{
								// Debug.Log("Click and drag out the area.");
								return result;
							}
							else
							{
								// Click event hack - part 3
								// for some reason, this session only run when popup display on inspector empty space.
								// when any selectable field behind of the popup list, Unity can't reach this session.
								_AutoCompleteClickhandle(position, ref result);
								EditorAutoCompleteParams.focusTag = string.Empty; // Clean up
								EditorAutoCompleteParams.lastTag = string.Empty; // Clean up
							}
						}
						else
						{
							// click outside popup area, deselecting.
							EditorAutoCompleteParams.Deselect();
						}
						return result;
					}
					else if (evt.isKey && evt.rawType == EventType.KeyUp)
					{
						switch (evt.keyCode)
						{
							case KeyCode.PageUp:
							case KeyCode.UpArrow:
								EditorAutoCompleteParams.selectedOption--;
								if (EditorAutoCompleteParams.selectedOption < 0)
									EditorAutoCompleteParams.selectedOption = EditorAutoCompleteParams.CacheCheckList.Count - 1;
								break;
							case KeyCode.PageDown:
							case KeyCode.DownArrow:
								EditorAutoCompleteParams.selectedOption++;
								if (EditorAutoCompleteParams.selectedOption >= EditorAutoCompleteParams.CacheCheckList.Count)
									EditorAutoCompleteParams.selectedOption = 0;
								break;
							case KeyCode.KeypadEnter:
							case KeyCode.Return:
								if (EditorAutoCompleteParams.selectedOption != -1)
								{
									_AutoCompleteClickhandle(position, ref result);
									EditorAutoCompleteParams.focusTag = string.Empty;
									EditorAutoCompleteParams.lastTag = string.Empty; 
								}
								else
								{
									EditorAutoCompleteParams.Deselect();
								}
								break;
							case KeyCode.Escape:
								EditorAutoCompleteParams.Deselect();
								break;
							default:
								// hit any other key(s), assume typing, avoid override by Enter;
								EditorAutoCompleteParams.selectedOption = -1;
								break;
						}
//						Debug.Log(string.Format("Selected option: {0}", EditorAutoCompleteParams.selectedOption));
					}
				}
			}
			else if (EditorAutoCompleteParams.lastTag == tag &&
				GUI.GetNameOfFocusedControl() != tag)
			{
				// Click event hack - part 2
				// catching mouse click
				_AutoCompleteClickhandle(position, ref result);
				EditorAutoCompleteParams.lastTag = string.Empty;
			}
			return result;
		}
		/// <summary>calculate auto complete select option location, and select it.
		/// within area, and we display option in "Vertical" style.
		/// which line is what we care.
		/// </summary>
		private static void _AutoCompleteClickhandle(Rect position, ref string result)
		{
			var index = EditorAutoCompleteParams.selectedOption;
			var mousePos = EditorAutoCompleteParams.mouseDown; // hack: assume mouse stays in click position (1 frame behind).
//			Debug.Log("Result:" + EditorAutoCompleteParams.CacheCheckList[index]);
			if (0 <= index && index < EditorAutoCompleteParams.CacheCheckList.Count)
			{
				result = EditorAutoCompleteParams.CacheCheckList[index];
				GUI.changed = true;
				// Debug.Log("Selecting index (" + EditorAutoCompleteParams.selectedOption + ") "+ rst);
			}
			else
			{
				// Fail safe, when selectedOption failure
				
				var cnt = EditorAutoCompleteParams.CacheCheckList.Count;
				var height = cnt * EditorAutoCompleteParams.optionHeight;
				var area = new Rect(position.x, position.y - height, position.width, height);
				if (!area.Contains(mousePos))
					return;
				var lineY = area.y;
				for (var i = 0; i < cnt; i++)
				{
					if (lineY < mousePos.y && mousePos.y < lineY + EditorAutoCompleteParams.optionHeight)
					{
						result = EditorAutoCompleteParams.CacheCheckList[i];
						Debug.LogError("Fail to select on \"" + EditorAutoCompleteParams.lastTag + "\" selected = " + result + "\ncalculate by mouse position.");
						GUI.changed = true;
						break;
					}
					lineY += EditorAutoCompleteParams.optionHeight;
				}
			}
			EditorAutoCompleteParams.Deselect();
		}
		#endregion
       
    }
	
	
}
