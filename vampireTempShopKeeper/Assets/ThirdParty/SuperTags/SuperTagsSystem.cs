using System.Collections.Generic;
using JetBrains.Annotations;
using SuperTags;
using UnityEngine;

namespace Assets.SuperTags
{
    public static class SuperTagsSystem
    {
        private static Dictionary<string,SuperTagData> _tagDb = new Dictionary<string, SuperTagData>();

        public static void AddTag([NotNull] string tagName, [NotNull] GameObject goRef)
        {
            if (_tagDb.ContainsKey(tagName))
            {
                if(_tagDb[tagName].References.Contains(goRef))
                    return;
                _tagDb[tagName].References.Add(goRef);
                return;
            }
            var tagData = new SuperTagData {TagName = tagName};
            if (tagData.References == null){tagData.References = new List<GameObject>();}
            tagData.References.Add(goRef);
            _tagDb.Add(tagName, tagData);
			SearchForSuperTagComponentOrAddItWithTheCorrectTags (goRef, tagName);
        }

        public static List<GameObject> GetObjectsWithTag(string tagName)
        {
            if (_tagDb.ContainsKey(tagName))
            {
                return _tagDb[tagName].References;
            }
            return null; //TODO replace this "null return" by something that makes sense
        }

        public static void ClearAllTags()
        {
            _tagDb = new Dictionary<string, SuperTagData>();
        }


        private static void SearchForSuperTagComponentOrAddItWithTheCorrectTags(GameObject go, string tag)
        {
			var superTag = go.GetComponent<SuperTag>() ?? go.AddComponent<SuperTag>();
            superTag.AddTag (tag);
        }

        public static void RemoveTag(string tagName, GameObject goRef)
        {
            if (!_tagDb.ContainsKey(tagName)) return;
            if (_tagDb[tagName].References.Contains(goRef))
                _tagDb[tagName].References.Remove(goRef);
        }

        public static string[] GetAllTags()
        {
            var tags = new List<string>();
            foreach(var entry in _tagDb)
            {
                tags.Add(entry.Value.TagName);
            }
            return tags.ToArray();
        }
        

    }

    internal struct SuperTagData
    {
        public string TagName;
        public List<GameObject> References;
    }
}
