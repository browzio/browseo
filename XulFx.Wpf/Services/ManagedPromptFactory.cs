using Gecko.Interfaces;
using System;
using System.Runtime.InteropServices;

namespace Gecko.Services
{
	[Guid("F8E0CE4E-A164-4F08-8AEE-0806C9673BF8")]
	[ContractID(ManagedPromptFactoryFactory.ContractID)]
	sealed class ManagedPromptFactoryFactory
		: GenericOneClassNsFactory<ManagedPromptFactoryFactory, ManagedPromptFactory>
	{
		public const string ContractID = Contracts.Prompter;
	}

	[Guid("B4F9EE6B-9BB6-4CBB-84FA-A9C065AB98A5")]
	public sealed class ManagedPromptFactory
		: nsIPromptFactory
	{
		private static readonly ManagedPromptService _defaultService;

		static ManagedPromptFactory()
		{
			_defaultService = new ManagedPromptService();
			PromptServiceGetter = ManagedPromptFactory.GetDefaultManagedPromptService;
		}

		public static void Init()
		{
			ManagedPromptFactoryFactory.Register();
		}

		public static nsIPromptService2 GetDefaultManagedPromptService()
		{
			throw new NotImplementedException(); 
			//return _defaultService;
		}

		/// <summary>
		/// Allow injecting different PromptService implementations into PromptFactory.
		/// </summary>
		public static Func<nsIPromptService2> PromptServiceGetter { get; set; }

		public IntPtr GetPrompt(mozIDOMWindowProxy aParent, ref Guid iid)
		{
			IntPtr result = IntPtr.Zero;
			IntPtr iUnknownForObject = Marshal.GetIUnknownForObject(PromptServiceGetter());
			try
			{
				int errno = Marshal.QueryInterface(iUnknownForObject, ref iid, out result);
				if (errno != 0)
					throw Marshal.GetExceptionForHR(errno);
			}
			finally
			{
				Marshal.Release(iUnknownForObject);
			}
			return result;
		}
	}
}
