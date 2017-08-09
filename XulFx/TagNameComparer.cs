using System;
using System.Collections.Generic;

namespace Gecko
{
	public sealed class TagNameComparer : IEqualityComparer<string>
	{
		public bool Equals(string x, string y)
		{
			return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
		}

		public int GetHashCode(string obj)
		{
			if (obj == null)
				return 0;
			return obj.ToUpperInvariant().GetHashCode();
		}
	}
}
