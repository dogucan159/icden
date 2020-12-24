using DevExpress.Xpo;
using DX.Data.Xpo.Identity;
using DX.Data.Xpo.Identity.Persistent;
using System;
using System.Collections.Generic;
using System.Text;

namespace icden.NetCore.Entities {

	public class ApplicationRoleMapper : XPRoleMapper<string, ApplicationRole, XpoApplicationRole> {
		public override Func<XpoApplicationRole, ApplicationRole> CreateModel => base.CreateModel;

		public override XpoApplicationRole Assign( ApplicationRole source, XpoApplicationRole destination ) {
			XpoApplicationRole result = base.Assign( source, destination );
			return result;
		}

		public override string Map( string sourceField ) {
			return base.Map( sourceField );
		}
	}
	public class ApplicationRole : XPIdentityRole {
		public ApplicationRole() { }
	}

	[MapInheritance( MapInheritanceType.ParentTable )]
	public class XpoApplicationRole : XpoDxRole {
		public XpoApplicationRole( Session session ) : base( session ) {
		}
	}
}
