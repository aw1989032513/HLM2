﻿using System.Collections.Generic;


namespace ET
{
	public static class RealmGateAddressHelper
	{
		public static StartSceneConfig GetGate(int zone,long accountID)
		{
			List<StartSceneConfig> zoneGates = StartSceneConfigCategory.Instance.Gates[zone];
			
			int n = accountID.GetHashCode() % zoneGates.Count;

			return zoneGates[n];
		}

		public static StartSceneConfig GetRealm(int zone)
        {
			StartSceneConfig zoneRealm = StartSceneConfigCategory.Instance.Realms[zone];
			return zoneRealm;
		}
	}
}