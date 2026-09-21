using BridgeClubLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeClubLib.Repositories
{
	public class DatabaseNoRepository : IDatabaseNoRepository
	{
		private readonly List<string> databaseNos;

		public DatabaseNoRepository()
		{
			databaseNos = new List<string>();
		}

		public IEnumerable<string> GetDatabaseNos()
		{
			DirectoryInfo di = new DirectoryInfo("c:\\temp\\");
			DirectoryInfo[] tempDi = di.GetDirectories();

			foreach (DirectoryInfo dri in tempDi)
			{
				databaseNos.Add(dri.Name);
			}

			return databaseNos;
		}
	}
}
