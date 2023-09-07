using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Services.AutoBet.Strategy_001_d
{
    public class Strategy_001_d_Model
    {
        public Strategy_001_d_Model()
        {
            StepOneId_a = Guid.Empty;
            StepOneId_b = Guid.Empty;

            StepTwoId_a = Guid.Empty;
            StepTwoId_b = Guid.Empty;
            StepTwoId_c = Guid.Empty;
        }

        public Guid StepOneId_a;
        public Guid StepOneId_b;

        public Guid StepTwoId_a;
        public Guid StepTwoId_b;
        public Guid StepTwoId_c;

    }
}
