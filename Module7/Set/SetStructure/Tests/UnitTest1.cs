using System;
using System.Linq;
using SetStructure;
using Tests.Entities;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void ElementAddedWithIncrement()
        {
            var set = new Set<int>();
            set.Add(1);
            Assert.Equal(1 ,set.Count);
        }
        
        [Fact]
        public void SameElementsNotAdded()
        {
            var set = new Set<int>();
            set.Add(1);
            set.Add(1);
            Assert.Equal(1 ,set.Count);
        }
        
        [Fact]
        public void SetStrcutureUnderLoad()
        {
            int Min = 0;
            int Max = 100000000;

            int[] testData = new int[100000];
            Random randNum = new Random();
            for (int i = 0; i < testData.Length; i++)
            {
                testData[i] = randNum.Next(Min, Max);   
            }
            
            DateTime start = DateTime.Now;
            var set = new Set<int>();
            foreach (var number in testData)
            {
                set.Add(number);
            }

            foreach (var number in testData)
            {
                Assert.True(set.Contains(number));
            }

            DateTime end = DateTime.Now;
            TimeSpan ts = end - start;
        }
        
        [Fact]
        public void SearchForObjectTypeInSetWhenGetHashCodeIsOverrided()
        {
            var set = new Set<User>();
            set.Add(new User(){Name = "Petya"});
            set.Add(new User(){Name = "Vasya"});
            Assert.True(set.Contains(new User(){Name = "Vasya"}));
        }
        
        [Fact]
        public void SearchForObjectTypeInSetWithDifferentHashCode()
        {
            var set = new Set<UserWithDefaultHashCode>();
            set.Add(new UserWithDefaultHashCode(){Name = "Petya"});
            set.Add(new UserWithDefaultHashCode(){Name = "Vasya"});
            var expectedElement = new UserWithDefaultHashCode() {Name = "Vasya"};
            Assert.False(set.Contains(expectedElement));
        }

        [Fact]
        public void ElementIsRemovedWithDicrement()
        {
            var set = new Set<int>();
            set.Add(1);
            set.Add(2);
            Assert.Equal(2 ,set.Count);
            set.Remove(1);
            Assert.Equal(1 ,set.Count);
        }
        
        [Fact]
        public void SetIsSubsetOf()
        {
            var set1 = new Set<int>();
            var set2 = new Set<int>();
            set2.Add(1);
            set2.Add(2);
            set2.Add(3);
            
            set1.Add(2);
            set1.Add(1);
            Assert.True(set1.IsSubsetOf(set2));
        }
        
        [Fact]
        public void SetUnion()
        {
            var set1 = new Set<int>();
            var set2 = new Set<int>();
            set2.Add(1);
            set2.Add(2);
            set2.Add(3);
            
            set1.Add(2);
            set1.Add(1);
            set1.Add(4);
            
            var expectedSet = new Set<int>();
            expectedSet.Add(1);
            expectedSet.Add(2);
            expectedSet.Add(3);
            expectedSet.Add(4);
            Assert.True(set1.UnionWith(set2).All(element => expectedSet.Contains(element)));
        }
        
        [Fact]
        public void SetExcetWith()
        {
            var set1 = new Set<int>();
            var set2 = new Set<int>();
            set2.Add(1);
            set2.Add(2);
            set2.Add(3);
            
            set1.Add(2);
            set1.Add(1);
            set1.Add(4);
            
            var expectedSet = new Set<int>();
            expectedSet.Add(4);
            Assert.True(set1.ExceptWith(set2).All(element => expectedSet.Contains(element)));
            Assert.Equal(1,set1.ExceptWith(set2).Count);
        }
        
        [Fact]
        public void SetIntersectWith()
        {
            var set1 = new Set<int>();
            var set2 = new Set<int>();
            set2.Add(1);
            set2.Add(2);
            set2.Add(3);
            
            set1.Add(2);
            set1.Add(1);
            set1.Add(4);
            
            var expectedSet = new Set<int>();
            expectedSet.Add(1);
            expectedSet.Add(2);
            Assert.True(set1.IntersectWith(set2).All(element => expectedSet.Contains(element)));
            Assert.Equal(2,set1.IntersectWith(set2).Count);
        }
        
        [Fact]
        public void ThrowsExceptionIfSet2IsNull()
        {
            var set1 = new Set<int>();
            Set<int> set2 = null;
            set1.Add(2);
            
            Assert.Throws<ArgumentNullException>(() => set1.IntersectWith(set2));
        }
    }
}