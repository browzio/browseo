//#include "nsXPCOM.h"

extern "C"
{
	// This function is used to initialization
	// statically linked libmozglue and libmemory
	int XulFx_Init()
	{
		// Do nothing
		return 0;
	}
}
