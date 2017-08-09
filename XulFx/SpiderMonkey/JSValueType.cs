using System;
using System.Collections.Generic;
using System.Text;

namespace SpiderMonkey
{
	enum JSValueType: byte
	{
		Double = 0x00,
		Int32 = 0x01,
		Undefined = 0x02,
		Boolean = 0x03,
		Magic = 0x04,
		String = 0x05,
		Null = 0x06,
		Object = 0x07,
		Unknown = 0x20,

		///* The below types never appear in a jsval; they are only used in tracing. */
		//NonFunObj = 0x57, 
		//FunObj = 0x67,
		//StrOrNull = 0x97,
		//ObjOrNull = 0x98,
		//Boxed = 0x99
	}
}
