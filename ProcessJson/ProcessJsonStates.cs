using System;
using System.Collections.Generic;

namespace Covid19Bot.ProcessJson
{
    public class StatesList_Arjun
    {

        public string State { get; set; }

        public object Updated { get; set; }

        public int Cases { get; set; }

        public int TodayCases { get; set; }

        public int Deaths { get; set; }

        public int TodayDeaths { get; set; }

        public int Recovered { get; set; }

        public int Active { get; set; }

        public int CasesPerOneMillion { get; set; }

        public int DeathsPerOneMillion { get; set; }

        public int Tests { get; set; }

        public int TestsPerOneMillion { get; set; }

        public int Population { get; set; }

    }

    public class Countries_Arjun
    {
        public List<StatesList_Arjun> States_Arjun { get; set; }
    }
}
