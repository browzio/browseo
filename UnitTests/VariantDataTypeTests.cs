using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;
using Gecko;
using Gecko.Plugins;

namespace UnitTests
{
	[TestFixture]
	public sealed class VariantDataTypeTests : TestsBase
	{
		/// <summary>
		/// Rewrite Variant.GetIs* if this test fails
		/// </summary>
		[Test]
		public void AssertDataTypeEnumValues()
		{
			Assert.AreEqual((int)VariantDataType.SByte, 0);
			Assert.AreEqual((int)VariantDataType.Short, 1);
			Assert.AreEqual((int)VariantDataType.Integer, 2);
			Assert.AreEqual((int)VariantDataType.Long, 3);
			Assert.AreEqual((int)VariantDataType.Byte, 4);
			Assert.AreEqual((int)VariantDataType.UnsignedShort, 5);
			Assert.AreEqual((int)VariantDataType.UnsignedInteger, 6);
			Assert.AreEqual((int)VariantDataType.UnsignedLong, 7);
			Assert.AreEqual((int)VariantDataType.Float, 8);
			Assert.AreEqual((int)VariantDataType.Double, 9);
		}
	}
}
