using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    public class FlashCard
    {

        public FlashCard() {}
        public FlashCard(int id, int stackId, string question, string answer)
        {
            Id = id;
            StackId = stackId;
            Question = question;
            Answer = answer;
        }
        public int Id { get; set; }

        public int StackId {get; set;} // foregin key of stack

        public string Question {get; set;}
        public string Answer {get; set;}


    }
}
