// You can have the code break to debug the installed windows service.
//  When service is starting, it gives you a choice to attach a debugger.

public static void Main()
{
	#if DEBUG

	System.Diagnostics.Debugger.Break();

	#endif



	// The generated code goes here

}

// To avoid having to install and register the service before running, 
// the service can be run as a plain executable instead of a service.

#if DEBUG


{
	Service1 svc = new Service1();

	svc.OnStart(null);

	System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

}
#elif

// The generated code goes here

#endif

