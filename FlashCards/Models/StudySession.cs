using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    public class StudySession
    {
        public int Id { get; set; }
        public int StackId {get; set;} // foregin key of stack
        public string Date { get; set; }
        public int Score { get; set; }
    }
}
