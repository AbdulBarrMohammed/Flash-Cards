using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCards.Models
{

    public class StudySession
    {
        public StudySession(){}
        public StudySession(int id, int stackId, string date, int score)
        {
            Id = id;
            StackId = stackId;
            Date = date;
            Score = score;
        }
        public int Id { get; set; }
        public int StackId {get; set;} // foregin key of stack
        public string Date { get; set; }
        public int Score { get; set; }
    }
}
