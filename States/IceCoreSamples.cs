using System;
using System.Collections.Generic;
using System.Text;

namespace States
{
    public enum CoreState
    {
        OffloadingSample,
        RemovingExternalContaminates,
        WeighingSample,
        PreppingForStorage,
        StoringSample
    }

    public enum CoreTrigger
    {
        DrilledSample,
        InvalidSample,
        CleanedSample,
        InvalidWeight,
        ValidWeight,
        ContaminatedSampleDuringTransport,
        SamplePreparedSuccessfully,
        ContaminatedSampleDuringStorageEntry,
        SampleStoredSuccessfully
    }

    public class IceCoreSamples
    {
        private static Dictionary<CoreState, List<(CoreTrigger, CoreState)>> _coreSampleRules
            = new Dictionary<CoreState, List<(CoreTrigger, CoreState)>>
            {
                [CoreState.OffloadingSample] = new List<(CoreTrigger, CoreState)>
                {
                    (CoreTrigger.DrilledSample, CoreState.RemovingExternalContaminates)
                },
                [CoreState.RemovingExternalContaminates] = new List<(CoreTrigger, CoreState)>
                {
                    (CoreTrigger.InvalidSample, CoreState.OffloadingSample),
                    (CoreTrigger.CleanedSample, CoreState.WeighingSample)
                },
                [CoreState.WeighingSample] = new List<(CoreTrigger, CoreState)>
                {
                    (CoreTrigger.InvalidWeight, CoreState.OffloadingSample),
                    (CoreTrigger.ValidWeight, CoreState.PreppingForStorage)
                },
                [CoreState.PreppingForStorage] = new List<(CoreTrigger, CoreState)>
                {
                    (CoreTrigger.ContaminatedSampleDuringTransport, CoreState.OffloadingSample),
                    (CoreTrigger.SamplePreparedSuccessfully, CoreState.StoringSample)
                },
                [CoreState.StoringSample] = new List<(CoreTrigger, CoreState)>
                {
                    (CoreTrigger.ContaminatedSampleDuringStorageEntry, CoreState.OffloadingSample),
                    (CoreTrigger.SampleStoredSuccessfully, CoreState.OffloadingSample)
                }
            };

        // change to Main to run.
        public static void Main(string[] args)
        {
            var state = CoreState.OffloadingSample;

            while (true)
            {
                Console.WriteLine($"The coring machine is {state}");
                Console.WriteLine("Select a trigger:");

                for (int i = 0; i < _coreSampleRules[state].Count; i++)
                {
                    // _ = ignore the second parameter
                    var (t, _) = _coreSampleRules[state][i];
                    Console.WriteLine($"{i}. {t}");
                }

                int input = int.Parse(Console.ReadLine());

                var (_, s) = _coreSampleRules[state][input];
                state = s;
            }
        }
    }
}
