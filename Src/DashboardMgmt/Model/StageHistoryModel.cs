using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.sdk.Records;
using Toolbox.Tools;

namespace DashboardMgmt.Model
{
    public class StageHistoryModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Provider is too long")]
        public string Provider { get; set; } = null!;

        [Required]
        [StringLength(100, ErrorMessage = "Stage is too long")]
        public string Stage { get; set; } = null!;

        public DateTime? StartDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public Guid Instance { get; } = Guid.NewGuid();
    }

    public static class StageHistoryModelExtensions
    {
        public static StageHistoryModel ConvertTo(this StageHistoryRecord subject)
        {
            subject.VerifyNotNull(nameof(subject));

            return new StageHistoryModel
            {
                Provider = subject.Provider,
                Stage = subject.Stage,
                StartDate = subject.StartDate,
                CompletedDate = subject.CompletedDate,
            };
        }

        public static void Reset(this StageHistoryModel subject)
        {
            subject.VerifyNotNull(nameof(subject));

            subject.Provider = string.Empty;
            subject.Stage = string.Empty;
            subject.StartDate = null;
            subject.CompletedDate = null;
        }
    }
}
