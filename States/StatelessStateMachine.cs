using System;
using System.Collections.Generic;
using System.Text;
using Stateless;

namespace States
{
    public enum Health
    {
        NonReproductive,
        Pregnant,
        Reproductive
    }

    public enum Activity
    {
        GiveBirth,
        ReachPuberty,
        HaveAbortion,
        HaveUnprotectedSex,
        Hysterectomy
    }

    // real world use. 
    // Microsoft has WorkFlowFoundation (over-engineered)
    // this example uses stateless-4.0
    public class StatelessStateMachine
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var stateMachine = new StateMachine<Health, Activity>(Health.NonReproductive);
            stateMachine.Configure(Health.NonReproductive)
                .Permit(Activity.ReachPuberty, Health.Reproductive);
            stateMachine.Configure(Health.Reproductive)
                .Permit(Activity.Hysterectomy, Health.NonReproductive)
                .PermitIf(Activity.HaveUnprotectedSex, Health.Pregnant,
                    () => ParentsNotWatching);
            stateMachine.Configure(Health.Pregnant)
                .Permit(Activity.GiveBirth, Health.Reproductive)
                .Permit(Activity.HaveAbortion, Health.Reproductive);
        }

        public static bool ParentsNotWatching { get; set; }
    }
}
