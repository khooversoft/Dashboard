using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.sdk.Records;
using Toolbox.Tools;

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
        public static StageModel ConvertTo(this StageRecord subject)
        {
            subject.VerifyNotNull(nameof(subject));

            return new StageModel
            {
                Stage = subject.Stage,
                OrderNumber = subject.OrderNumber,
            };
        }

        public static void Reset(this StageModel subject)
        {
            subject.VerifyNotNull(nameof(subject));

            subject.Stage = string.Empty;
            subject.OrderNumber = 0;
        }
    }
}
