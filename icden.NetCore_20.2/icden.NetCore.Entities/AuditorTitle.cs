using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icden.NetCore.Entities {
    public class AuditorTitle : XPObject {
        public AuditorTitle( Session session ) : base( session ) { }
        private string fCode;
        public string Code {
            get { return fCode; }
            set { SetPropertyValue( nameof( Code ), ref fCode, value ); }
        }

        private string fDescription;
        public string Description {
            get { return fDescription; }
            set { SetPropertyValue( nameof( Description ), ref fDescription, value ); }
        }
    }
}
