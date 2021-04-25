
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheLenderRD.Domain.Dto;

namespace TheLenderRD.Domain.Entitys
{
	public class Log
	{
		[Key]
		public int QuryId { get; set; }

		[Required]
		public DateTime ConsultationDate { get; set; }

		[Required]
		public int Edad { get; set; }

		[Required]
		public decimal Amount { get; set; }

		[Required]
		public decimal AccountValue { get; set; }

		[Column(TypeName = "char(15)")]
		[Required]
		public string QueryIp { get; set; }

		[Required]
		public int MonthId { get; set; }

		[Required]
		[ForeignKey("MonthId")]
		public Month Month { get; set; }

	}
}
