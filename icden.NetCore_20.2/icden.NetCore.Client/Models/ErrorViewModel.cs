using System;

namespace icden.NetCore.Client.Models {
    public class ErrorViewModel {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty( RequestId );
    }
}
