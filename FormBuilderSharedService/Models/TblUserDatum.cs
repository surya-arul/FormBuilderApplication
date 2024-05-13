using System;
using System.Collections.Generic;

namespace FormBuilderSharedService.Models
{
    public partial class TblUserDatum
    {
        public int Id { get; set; }

        public int UserSubmitDetailsId { get; set; }

        public string Label { get; set; } = null!;

        public string Value { get; set; } = null!;

        public virtual TblUserSubmitDetail UserSubmitDetails { get; set; } = null!;
    }
}
