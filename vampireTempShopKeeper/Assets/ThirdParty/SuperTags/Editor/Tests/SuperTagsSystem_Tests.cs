using Assets.SuperTags;
using NUnit.Framework;
using UnityEngine;

namespace SuperTags.Editor.Tests
{
    public class SuperTagsSystemTests 
    {
        [Test]
        public void Add_and_Get_Tag_WorkProperly()
        {
            Assert.AreEqual(null, SuperTagsSystem.GetObjectsWithTag("aaa"));
            var go = new GameObject("testGo");
            SuperTagsSystem.AddTag("aaa", go);
            Assert.IsNotNull(SuperTagsSystem.GetObjectsWithTag("aaa"));
            Object.DestroyImmediate(go);
        }
        
        [Test]
        public void Add_Same_Tag_To_Two_GO_WorkProperly()
        {
            var go1 = new GameObject("testGo1");
            var go2 = new GameObject("testGo2");
            SuperTagsSystem.AddTag("aaa", go1);
            SuperTagsSystem.AddTag("aaa", go2);
            Assert.IsNotNull(SuperTagsSystem.GetObjectsWithTag("aaa"));
            Object.DestroyImmediate(go1);
            Object.DestroyImmediate(go2);
        }
        
        [Test]
        public void Add_Same_Tag_Twice_WorkProperly()
        {
            var go = new GameObject("testGo");
            SuperTagsSystem.AddTag("aaa", go);
            SuperTagsSystem.AddTag("aaa", go);
            var goRefs = SuperTagsSystem.GetObjectsWithTag("aaa");
            Assert.IsNotNull(goRefs);
            Assert.AreEqual(1, goRefs.Count);
            Object.DestroyImmediate(go);
        }
        
        [Test]
        public void Add_SuperTag_Component_And_Check_Tags_Are_Added_Correctly()
        {
            var go = new GameObject("testGo");
            var tag = go.AddComponent<SuperTag>();
            tag.AddTag("aaa");
            tag.AddTag("bbb");
            var goRefs = SuperTagsSystem.GetObjectsWithTag("aaa");
            Assert.IsNotNull(goRefs);
            Assert.AreEqual(1, goRefs.Count);
            Assert.AreEqual(go, goRefs[0]);
            
            goRefs = SuperTagsSystem.GetObjectsWithTag("bbb");
            Assert.IsNotNull(goRefs);
            Assert.AreEqual(1, goRefs.Count);
            Assert.AreEqual(go, goRefs[0]);
            
            Object.DestroyImmediate(go);
        }

        [Test]
        public void Removing_GameObject_Removes_Reference_To_Tag()
        {
            var go = new GameObject("testGo");
            var tag = go.AddComponent<SuperTag>();
            tag.AddTag("aaa");
            
            tag.OnDestroy();
            Object.DestroyImmediate(go);
            var goRefs = SuperTagsSystem.GetObjectsWithTag("aaa");
            Assert.AreEqual(0, goRefs.Count);
        }

        [TearDown]
        public void TearDown()
        {
            SuperTagsSystem.ClearAllTags();
        }
        
        

    }
}
