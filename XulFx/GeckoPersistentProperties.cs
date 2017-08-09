using System;
using Gecko.Interfaces;

namespace Gecko
{
	public class GeckoPersistentProperties
		: GeckoProperties
	{
		internal static GeckoProperties Create(nsIPersistentProperties instance)
		{
			return new GeckoPersistentProperties(instance);
		}


		internal protected GeckoPersistentProperties(nsIPersistentProperties instance)
			: base(instance)
		{

		}
	}
}
