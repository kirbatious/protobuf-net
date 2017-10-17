using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;
using Xunit;

namespace Examples.Issues
{

    public class Issue304Interfaces2
    {
        [Fact]
        public void ShouldDeepClone()
        {
            var b = new B(new D(1));
            var c = new C(new D(2));

            var a = new A(b, c);

            var protoA = Serializer.GetProto<A>();
            var protoB = Serializer.GetProto<B>();
            var protoC = Serializer.GetProto<C>();

            var resB = Serializer.DeepClone(b);
            var resC = Serializer.DeepClone(c);

            // Fails here
            var resA = Serializer.DeepClone(a);
        }

        [ProtoContract(SkipConstructor = true)]
        [ProtoInclude(100, typeof(D))]
        public class A
        {
            [ProtoMember(1)]
            public B B { get; }


            [ProtoMember(2)]
            public C C { get; }


            public A(B b, C c)
            {
                B = b;
                C = c;
            }
        }

        [ProtoContract(SkipConstructor = true)]
        [ProtoInclude(100, typeof(D))]
        public class B
        {
            [ProtoMember(1, DynamicType = true)]
            public ID D2 { get; }

            public B(ID d)
            {
                D2 = d;
            }
        }

        [ProtoContract(SkipConstructor = true)]
        [ProtoInclude(100, typeof(D))]
        public class C
        {
            [ProtoMember(1, DynamicType = true)]
            public ID D2 { get; }

            public C(ID d)
            {
                D2 = d;
            }
        }

        //[ProtoContract]
        //[ProtoInclude(100, typeof(D))]
        public interface ID
        {
            int X { get; }
        }


        [ProtoContract(SkipConstructor = true)]
        public class D : ID
        {
            [ProtoMember(1)]
            public int X { get; }

            public D(int x)
            {
                X = x;
            }

        }

    }
}


