using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common
{
    public class MT1_BaseDto
    {
		private int _id;

		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private DateTime _createDate;

		public DateTime CreateDate
        {
			get { return _createDate; }
			set { _createDate = value; }
		}

		private DateTime _updateDate;

		public DateTime UpdateDate
		{
			get { return _updateDate; }
			set { _updateDate = value; }
		}


	}
}
