using Gecko;
using System;

namespace UnitTests
{
	internal static class VariantExtensions
	{
		public static Variant ThrowIfIsNot(this Variant instance, params VariantDataType[] dataTypes)
		{
			foreach (VariantDataType dataType in dataTypes)
			{
				if (dataType == instance.DataType)
					return instance;
			}
			throw new InvalidCastException();
		}

		public static void ThrowIfIsNotAndDispose(this Variant instance, params VariantDataType[] dataTypes)
		{
			try
			{
				foreach (VariantDataType dataType in dataTypes)
				{
					if (dataType == instance.DataType)
						return;
				}
				throw new InvalidCastException();
			}
			finally
			{
				((IDisposable)instance).Dispose();
			}
		}

	}
}
