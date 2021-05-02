using System.Collections.Generic;
using VoxelTycoon;
using VoxelTycoon.Cities;
using VoxelTycoon.Modding;

namespace PauseAtNewDemands
{
    public class PauseAtNewDemands : Mod
    {
        private readonly HashSet<CityDemand> _checkedCityDemands = new HashSet<CityDemand>();

        protected override void OnGameStarting()
        {
            var demands = Demands();
            foreach (var demand in demands)
            {
                _checkedCityDemands.Add(demand);
            }
        }

        protected override void OnUpdate()
        {
            var demands = Demands();
            foreach (var demand in demands)
            {
                if (_checkedCityDemands.Add(demand))
                {
                    TimeManager.Current.TimeScale = 0.0f;
                }
            }
        }

        private static IEnumerable<CityDemand> Demands()
        {
            var list = new List<CityDemand>();
            
            var cities = CityManager.Current.Cities;
            for (var i = 0; i < cities.Count; i++)
            {
                var city = cities[i];
                if (city.Region.State != RegionState.Unlocked) continue;

                for (var j = 0; j < city.Demands.Count; j++)
                {
                    list.Add(city.Demands[j]);
                }
            }

            return list;
        }
    }
}
