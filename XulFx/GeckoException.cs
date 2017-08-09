using System;
using SpiderMonkey;
using Gecko.Interfaces;

namespace Gecko
{
	public class GeckoException
		: ApplicationException
	{
		public GeckoException()
			: base()
		{

		}

		public GeckoException(string message)
			: base(message)
		{

		}

		public GeckoException(string message, Exception innerException)
			: base(message, innerException)
		{

		}
	}

	public class GeckoJavaScriptException
		: GeckoException, nsIScriptError
	{
		private int _line;
		private int _column;
		private string _message;
		private string _filename;

		public GeckoJavaScriptException(string message, string filename, int line, int column)
			: base(string.Format("[JavaScript Error: \"{0}\" {{ file: \"{1}\" line: {2} column: {3} }}]", message, filename, line, column))
		{
			_line = line;
			_column = column;
			_message = message;
			_filename = filename;
		}


		public long GetTimeStampAttribute()
		{
			throw new NotImplementedException();
		}

		public string GetMessageAttribute()
		{
			return this.Message;
		}

		public void ToString(nsAUTF8StringBase result)
		{
			result.SetData(GetMessageAttribute());
		}

		public void GetErrorMessageAttribute(nsAStringBase result)
		{
			result.SetData(_message);
		}

		public void GetSourceNameAttribute(nsAStringBase result)
		{
			result.SetData(_filename);
		}

		public void GetSourceLineAttribute(nsAStringBase result)
		{
			throw new NotImplementedException();
		}

		public uint GetLineNumberAttribute()
		{
			return (uint)_line;
		}

		public uint GetColumnNumberAttribute()
		{
			return (uint)_column;
		}

		public uint GetFlagsAttribute()
		{
			throw new NotImplementedException();
		}

		public string GetCategoryAttribute()
		{
			throw new NotImplementedException();
		}

		public ulong GetOuterWindowIDAttribute()
		{
			throw new NotImplementedException();
		}

		public ulong GetInnerWindowIDAttribute()
		{
			throw new NotImplementedException();
		}

		public bool GetIsFromPrivateWindowAttribute()
		{
			throw new NotImplementedException();
		}

		public void Init(nsAStringBase message, nsAStringBase sourceName, nsAStringBase sourceLine, uint lineNumber, uint columnNumber, uint flags, string category)
		{
			throw new NotImplementedException();
		}

		public void InitWithWindowID(nsAStringBase message, nsAStringBase sourceName, nsAStringBase sourceLine, uint lineNumber, uint columnNumber, uint flags, nsACStringBase category, ulong innerWindowID)
		{
			throw new NotImplementedException();
		}

		public uint GetLogLevelAttribute()
		{
			throw new NotImplementedException();
		}

		public JSVal GetStackAttribute()
		{
			throw new NotImplementedException();
		}

		public void SetStackAttribute(ref JSVal value)
		{
			throw new NotImplementedException();
		}

		public void GetErrorMessageNameAttribute(nsAStringBase result)
		{
			throw new NotImplementedException();
		}

		public void SetErrorMessageNameAttribute(nsAStringBase value)
		{
			throw new NotImplementedException();
		}
	}
}