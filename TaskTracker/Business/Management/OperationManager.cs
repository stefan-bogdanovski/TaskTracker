using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Management
{
	public class OperationManager
	{
		//Singleton pattern, because we want only 1 instance of OperationManager class so we avoid conflicts with database.
		private static OperationManager manager = null;
		private static readonly object locker = new Object();



		private OperationManager() {}

		public static OperationManager GetManager
		{
			get
			{
				lock (locker)
				{
					if (manager == null)
					{
						manager = new OperationManager();
					}
					return manager;
				}
			}
		}
		public OperationResult ExecuteOperation(Operation operation)
		{
			try
			{
				return operation.Execute();
			}
			catch (Exception ex)
			{
				OperationResult result = new OperationResult();
				result.Errors.Add(ex.Message);
				return result;
			}
		}
	}
}
