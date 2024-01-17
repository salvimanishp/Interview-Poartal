using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterviewManagement.Models
{
	public class InterviewMaster
	{
	

		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string MobailNumber { get; set; }
		[Required]
		[DataType(DataType.Date)]
		public DateTime DOB { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Age { get; set; }
		[Required]
		public string Qualification { get; set; }
		[Required]
		public string Skill { get; set; }
		[Required]
		public string Experience { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string Country { get; set; }
		[Required]
		public string State { get; set; }
		[Required]
		public string City { get; set; }
		

		//Additional Property

		public long CompanyId { get; set; }
		public string CompanyName { get; set; }
		public long CandidateId { get; set; }

	}
}