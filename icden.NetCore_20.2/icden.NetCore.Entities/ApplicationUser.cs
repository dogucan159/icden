using DevExpress.Xpo;
using DX.Data.Xpo.Identity;
using DX.Data.Xpo.Identity.Persistent;
using System;

namespace icden.NetCore.Entities {

	public class ApplicationUserMapper : XPUserMapper<ApplicationUser, XpoApplicationUser> {
		public override XpoApplicationUser Assign( ApplicationUser source, XpoApplicationUser destination ) {
			var result = base.Assign( source, destination );
			return result;
		}

		public override string Map( string sourceField ) {
			return base.Map( sourceField );
		}

		public override Func<XpoApplicationUser, ApplicationUser> CreateModel => base.CreateModel;
	}

	public class ApplicationUser  : XPIdentityUser {
		public ApplicationUser() {

		}
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string IdentityNumber { get; set; }

		public AuditorTitle AuditorTitle { get; set; }
		
	}

	public class XpoApplicationUserMapper : XPUserMapper<ApplicationUser, XpoApplicationUser> {
		public override Func<XpoApplicationUser, ApplicationUser> CreateModel => base.CreateModel;
		public override XpoApplicationUser Assign( ApplicationUser source, XpoApplicationUser destination ) {
			XpoApplicationUser result = base.Assign( source, destination );

			return result;
		}

		public override string Map( string sourceField ) {
			return base.Map( sourceField );
		}
	}

	// This class will be persisted in the database by XPO
	// It should have the same properties as the ApplicationUser
	[MapInheritance( MapInheritanceType.ParentTable )]
    public class XpoApplicationUser : XpoDxUser {
        public XpoApplicationUser( Session session ) : base( session ) {
        }
    }
}
