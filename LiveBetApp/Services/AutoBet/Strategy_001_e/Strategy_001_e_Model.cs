using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Services.AutoBet.Strategy_001_e
{
    public class Strategy_001_e_Model
    {
        public Strategy_001_e_Model()
        {
            StepOne_a = new Strategy_001_e_Model_Step();
            StepOne_b = new Strategy_001_e_Model_Step();
            StepTwo_a = new Strategy_001_e_Model_Step();
        }

        public Strategy_001_e_Model_Step StepOne_a; // tài H1 lần 1
        public Strategy_001_e_Model_Step StepOne_b; // tài cả trận lần 1
        public Strategy_001_e_Model_Step StepTwo_a; // tài H1 lần 2

        public int BeginHdp;
        public int TargetPriceStepOne;
        public int TargetPriceStepTwo;

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
