using Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Management
{
	public class OperationResult
	{
		public IEnumerable<BaseDto> Data { get; set; }
		public List<string> Errors { get; set; } = new List<string>();
		public bool IsSuccessful => !Errors.Any();
	}
}
