using System;
using Gecko.Interop;
using Gecko.Interfaces;
using Gecko.DOM;
using System.Runtime.InteropServices;

namespace Gecko
{
	public class GeckoNodeWeakReference : ComObject<nsIWeakReference>
	{
		private static nsIWeakReference GetWeakReferenceFromNode(GeckoNode node)
		{
			return node.QueryInterface<nsISupportsWeakReference>().GetWeakReference();
		}

		public GeckoNodeWeakReference(GeckoNode node)
			: base(GetWeakReferenceFromNode(node))
		{

		}

		public virtual GeckoNode Target
		{
			get
			{
				GeckoNode rv = null;
				Guid iid = typeof(nsIDOMNode).GUID;
				IntPtr pUnk = Instance.QueryReferent(ref iid);
				if (pUnk != IntPtr.Zero)
				{
					try
					{
						rv = ((nsIDOMNode)Marshal.GetObjectForIUnknown(pUnk)).Wrap(GeckoNode.Create);
					}
					finally
					{
						Marshal.Release(pUnk);
					}
				}
				return rv;
			}
		}

		public virtual GeckoNode GetAliveElement()
		{
			GeckoNode node = this.Target;
			if (node == null)
				throw new InvalidOperationException();

			GeckoDocument doc = node.OwnerDocument;
			if (doc == null || doc.DefaultView == null)
				throw new InvalidOperationException();

			return node;
		}

	}
}
