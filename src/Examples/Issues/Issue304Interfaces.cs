using System.ComponentModel;
using Xunit;
using ProtoBuf;

namespace Examples.Issues
{

    public class Issue304Interfaces
    {
        [Fact]
        public void ShouldDeepClone()
        {
            Serializer.DeepClone(new A(new B(new C(new[] { 1.0, 2.0, 3.0 }))));
        }

        [ProtoContract(SkipConstructor = true)]
        public class A
        {
            public A(IB b)
            {
                B = b;
            }

            [ProtoMember(1, DynamicType = true)]
            public IB B { get; }
        }


        public interface IB
        {
            IC C { get; }
        }

        [ProtoContract(SkipConstructor = true)]
        public class B : IB
        {
            public B(IC c)
            {
                C = c;
            }

            [ProtoMember(2, DynamicType = true)]
            public IC C { get; }
        }

        
        public interface IC
        {
            double[] Data { get; }
        }

        [ProtoContract(SkipConstructor = true)]
        public class C : IC
        {

            public C(double[] data)
            {
                Data = data;
            }

            [ProtoMember(3)]
            public double[] Data { get; }
        }
    }
}
