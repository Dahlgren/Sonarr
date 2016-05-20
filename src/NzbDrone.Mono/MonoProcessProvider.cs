using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Mono.Unix.Native;
using NLog;
using NzbDrone.Common.Processes;

namespace NzbDrone.Mono
{
	public class MonoProcessProvider : ProcessProvider
	{
		private readonly Logger _logger;

        public MonoProcessProvider(Logger logger)
            :base(logger)
        {
            _logger = logger;
        }
        
        public override Process Start(string path, string args = null, StringDictionary environmentVariables = null, System.Action<string> onOutputDataReceived = null, System.Action<string> onErrorDataReceived = null)
		{
			if (path.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase))
            {
                args = GetMonoArgs(path, args);
                path = "mono";
            }
            
			return base.Start(path, args, environmentVariables, onOutputDataReceived, onErrorDataReceived);
		}
		
		public override void RestartProcess(string path, string args = null, StringDictionary environmentVariables = null)
		{
			Syscall.execvp("mono", GetMonoArgs(path, args).Split(' '));
		}
		
		public override Process SpawnNewProcess(string path, string args = null, StringDictionary environmentVariables = null)
		{
			if (path.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase))
            {
                args = GetMonoArgs(path, args);
                path = "mono";
            }
            
			return base.SpawnNewProcess(path, args, environmentVariables);
		}
		
		private string GetMonoArgs(string path, string args)
        {
            return string.Format("--debug {0} {1}", path, args);
        }
	}
}

