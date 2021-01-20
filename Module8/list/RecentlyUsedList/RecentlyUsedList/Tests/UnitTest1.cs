using System;
using RecentlyUsedList;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void AddElement()
        {
            var list = new RecentYUsedList();
            list.Add("first");
            Assert.Equal(1, list.Count);
        }
        
        [Fact]
        public void LastAddedElementShouldBeFirst()
        {
            var list = new RecentYUsedList();
            list.Add("first");
            list.Add("second");
            list.Add("third");
            Assert.Equal(3, list.Count);
            Assert.Equal("third", list[0]);
        }
        
        [Fact]
        public void DuplicatedItemShouldBeMoved()
        {
            var list = new RecentYUsedList();
            list.Add("first");
            list.Add("second");
            list.Add("first");
            Assert.Equal(2, list.Count);
            Assert.Equal("first", list[0]);
        }
        
        [Fact]
        public void NegativeIndexThrowsException()
        {
            var list = new RecentYUsedList();
            Assert.Throws<IndexOutOfRangeException>(() => list[-1]);
        }

        [Fact] 
        public void NullInsertionThrowsException()
        {
            var list = new RecentYUsedList();
            Assert.Throws<ArgumentNullException>(() => list.Add(null));
        }
        
        [Fact] 
        public void ListIsEmptyByDefault()
        {
            var list = new RecentYUsedList();
            Assert.Equal(0, list.Count);
        }
    }
}