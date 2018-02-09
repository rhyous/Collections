namespace Rhyous.Collections.Tests.Collections
{
    class TestClass : ITreeable<TestClass>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TestClass Parent { get; set; }
        public ParentedList<TestClass, TestClass> Children
        {
            get { return _Children ?? (_Children = new ParentedList<TestClass, TestClass>(this)); }
            set { _Children = value; }
        }
        private ParentedList<TestClass, TestClass> _Children;
    }
}
