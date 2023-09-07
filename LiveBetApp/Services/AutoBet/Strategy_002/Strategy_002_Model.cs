using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Services.AutoBet.Strategy_002
{
    public class Strategy_002_Model
    {
        public Strategy_002_Model()
        {
            StepOne_a = new Strategy_001_e_Model_Step();
            StepOne_b = new Strategy_001_e_Model_Step();
        }

        public Strategy_001_e_Model_Step StepOne_a; // tài H2 lần 1
        public Strategy_001_e_Model_Step StepOne_b; // tài H2 lần 2
    }

    public class Strategy_001_e_Model_Step
    {
        public Strategy_001_e_Model_Step()
        {
            BetId = Guid.Empty;
            Canceled = false;
            Betted = false;
        }

        public Guid BetId;
        public bool Betted;
        public bool Canceled;
    }
}
