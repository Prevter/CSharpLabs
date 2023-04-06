using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Lab13
{
	public sealed class PhoneCharger : Device
	{
		public PhoneCharger() : base(30.0, 30.0)
		{
		}

		public override void Activate()
		{
			Active = true;
		}

		public override void Deactivate()
		{
			Active = false;
		}
	}
}
