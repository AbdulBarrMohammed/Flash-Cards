using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    public class FlashCard
    {
        public int Id { get; set; }

        public int StackId {get; set;} // foregin key of stack

        public string Question {get; set;}
        public string Answer {get; set;}
    }
}
