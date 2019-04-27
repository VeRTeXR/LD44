using System.Collections.Generic;
using UnityEngine;

namespace Assets.SuperTags
{
    public class SuperTag : MonoBehaviour 
    {
        [SerializeField]
        private List<string> _tags = new List<string>(); 
        
        public List<string> Tags
        {
            get { return _tags; }
        }

        public void AddTag(string newTag)
        {
            if (!_tags.Contains(newTag))
            {
                _tags.Add(newTag);    
            }
            SuperTagsSystem.AddTag(newTag, gameObject);
        }

        public void RemoveTag(string oldTag)
        {
            if (_tags.Contains(oldTag))
            {
                _tags.Remove(oldTag);    
            }
            SuperTagsSystem.RemoveTag(oldTag, gameObject);
        }

        public void SetTags(List<string> tags)
        {
            RemoveAllTagsFromSystem();
            _tags = tags;
            AddAllTagsToSystem();
        }

        #if UNITY_EDITOR
        //
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded() 
        {
            var tags = FindObjectsOfType<SuperTag>();
//            Debug.LogWarning("Compiled! Count: " + SuperTagsSystem.GetAllTags().Length.ToString() + " | tags amount found: " + tags.Length.ToString());
            foreach (var tag in tags)
            {
                tag.AddAllTagsToSystem();
            }
        }
        #endif

        private void AddAllTagsToSystem()
        {
            for (var i = 0; i < _tags.Count; i++)
            {
                SuperTagsSystem.AddTag(_tags[i], gameObject);
            }
        }

        private void OnEnable()
        {
//            Debug.Log("OnEnable");
            AddAllTagsToSystem();
        }

        private void OnDisable()
        {
//            Debug.Log("OnDisable");
            RemoveAllTagsFromSystem();           
        }

        private void RemoveAllTagsFromSystem()
        {
            for (var i = 0; i < _tags.Count; i++)
            {
                SuperTagsSystem.RemoveTag(_tags[i], gameObject);    
            }
        }
        
        
        

        

        public void OnDestroy()
        {
//            Debug.Log("OnDestroy");
            RemoveAllTagsFromSystem();
        }
    }
}
