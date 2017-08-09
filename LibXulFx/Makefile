all: libxulfx.so

libxulfx.so: xulfx.o
	g++ -shared -L/usr/lib/xulrunner-24 `pkg-config --libs libxul` -Wl,--whole-archive -lmozglue -lmemory -Wl,--no-whole-archive -s -o libxulfx.so xulfx.o
	cp libxulfx.so ../bin/Debug/libxulfx.so
	#strip libxulfx.so

xulfx.o:
	g++ -Wall -fexceptions -DXP_UNIX -DXPCOM_GLUE -O2 -c xulfx.cpp -o xulfx.o

	
clean:
	rm -f libxulfx.so xulfx.o
	rm -f ../bin/Debug/libxulfx.so
