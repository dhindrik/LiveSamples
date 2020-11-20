using System;

namespace Messages
{
    public class ResultMessage : Message
    {
        public ResultMessage(int homeGoals, int awayGoals)
        {
            HomeGoals = homeGoals;
            AwayGoals = awayGoals;
            TypeInfo = GetType().Name;
        }

        public ResultMessage()
        {

        }

        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
    }
}
