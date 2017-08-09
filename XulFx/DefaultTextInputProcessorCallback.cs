using Gecko.Interfaces;
using System;


namespace Gecko
{
	public class DefaultTextInputProcessorCallback : nsITextInputProcessorCallback
	{
		public virtual bool OnNotify(nsITextInputProcessor aTextInputProcessor, nsITextInputProcessorNotification aNotification)
		{
			try
			{
				string type = nsString.Get(aNotification.GetTypeAttribute);
				switch (type)
				{
					case "request-to-commit":
						aTextInputProcessor.CommitComposition(null, 0, 0);
						break;
					case "request-to-cancel":
						aTextInputProcessor.CancelComposition(null, 0, 0);
						break;
				}
			}
			catch (AccessViolationException) { throw; }
			catch
			{
				return false;
			}
			return true;
		}
	}
}
