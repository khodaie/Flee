using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flee.PublicTypes;
using NUnit.Framework;

namespace Flee.Test
{
	[TestFixture]
	public class PayaTests
	{
		[Test]
		public void TestInheritednterfaceMembers()
		{
			var myContext = new MyContext();
			var context = new ExpressionContext(myContext);
			var expr = context.CompileGeneric<int>("Test.ChildProperty + Test.BaseProperty + Test.Property +  Test.BaseMethod(100) + Test.Method(200) + Test.ChildMethod(300)");

			Assert.AreEqual(3 + 7 + 11 + (100 + 1) + (200 + 2) + (300 + 3), expr.Evaluate());
		}

		[Test]
		public void TestInheritedIndexer()
		{
			var myContext = new MyContext();
			var context = new ExpressionContext(myContext);
			var expr = context.CompileGeneric<int>("Test[0]");

			Assert.AreEqual(3, expr.Evaluate());
		}

		public class MyContext
		{
			public ITestChild Test { get; } = new TestClass { BaseProperty = 3, Property = 7, ChildProperty = 11 };
		}

		public interface ITestBase
		{
			int BaseProperty { get; }

			int BaseMethod(int x);

			int this[int x] { get; }
		}

		public interface ITest : ITestBase
		{
			int Property { get; }

			int Method(int x);
		}

		public interface ITestChild : ITest
		{
			int ChildProperty { get; }

			int ChildMethod(int x);
		}

		public class TestClass : ITestChild
		{
			public int this[int x] { get => this.Method(x); }

			public int ChildProperty { get; set; }
			public int Property { get; set; }
			public int BaseProperty { get; set; }

			public int BaseMethod(int x) => x + 1;
			public int ChildMethod(int x) => x + 2;
			public int Method(int x) => x + 3;
		}
	}
}
