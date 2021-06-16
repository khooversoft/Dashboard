using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.sdk.Records;

namespace DashboardMgmt.Model
{
    public class StageModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Stage is too long")]
        public string Stage { get; set; } = null!;

        [Range(0, 100, ErrorMessage = "Must be between 0 and 100")]
        public int OrderNumber { get; set; }
    }

    public static class StageModelExtensions
    {
        public static StageModel? ConvertTo(this StageRecord? subject)
        {
            if (subject == null) return null;

            return new StageModel
            {
                Stage = subject.Stage,
                OrderNumber = subject.OrderNumber,
            };
        }
    }
}
