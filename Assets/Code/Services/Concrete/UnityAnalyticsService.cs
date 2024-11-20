using Code.Models.Abstraction;
using Code.Models.Concrete;
using Code.Services.Abstraction;
using Code.Utils;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using Event = Unity.Services.Analytics.Event;
using IAnalyticsService = Code.Services.Abstraction.IAnalyticsService;

namespace Code.Services.Concrete
{
    public class UnityAnalyticsService : IAnalyticsService
    {
        private const string AnalyticsConfigPath = "AnalyticsConfigs/UnityAnalyticsConfig";
        
        private readonly IAnalyticsConfig _analyticsConfig;
        
        public UnityAnalyticsService(ILevelFinishedService levelFinishedService, ILevelFailedService levelFailedService)
        {
            levelFinishedService.OnLevelFinished += OnLevelFinished;
            levelFailedService.OnLevelFailed += OnLevelFailed;
            
            UnityServices.InitializeAsync().Forget();

            _analyticsConfig = Resources.Load<UnityAnalyticsConfigSo>(AnalyticsConfigPath);
        }

        private void OnLevelFinished(int levelIndex)
        {
            var levelFinishedEvent = new UnityAnalyticsEvent(_analyticsConfig.LevelFinishedEventName,
                intParameters: new[] {new IntParameter(_analyticsConfig.LevelIndexParameterName, levelIndex)});
            
            AnalyticsService.Instance.RecordEvent(levelFinishedEvent);
            
            Debug.Log($"Recorded Unity Analytics event {_analyticsConfig.LevelFinishedEventName}" +
                      $" with user ID: {AnalyticsService.Instance.GetAnalyticsUserID()}");
        }

        private void OnLevelFailed(int levelIndex)
        {
            var levelFailedEvent = new UnityAnalyticsEvent(_analyticsConfig.LevelFailedEventName,
                intParameters: new[] {new IntParameter(_analyticsConfig.LevelIndexParameterName, levelIndex)});
            
            AnalyticsService.Instance.RecordEvent(levelFailedEvent);
            
            Debug.Log($"Recorded Unity Analytics event {_analyticsConfig.LevelFailedEventName}" +
                      $" with user ID: {AnalyticsService.Instance.GetAnalyticsUserID()}");
        }
    }

    public class UnityAnalyticsEvent : Event
    {
        public UnityAnalyticsEvent(string name) : base(name) { }

        public UnityAnalyticsEvent(string name, IntParameter[] intParameters = null, LongParameter[] longParameters = null,
            FloatParameter[] floatParameters = null, DoubleParameter[] doubleParameters = null,
            BoolParameter[] boolParameters = null, StringParameter[] stringParameters = null) : base(name)
        {
            if (intParameters != null)
                foreach (var intParameter in intParameters)
                    SetParameter(intParameter.Name, intParameter.Value);
            
            if (longParameters != null)
                foreach (var longParameter in longParameters)
                    SetParameter(longParameter.Name, longParameter.Value);

            if (floatParameters != null)
                foreach (var floatParameter in floatParameters)
                    SetParameter(floatParameter.Name, floatParameter.Value);

            if (doubleParameters != null)
                foreach (var doubleParameter in doubleParameters)
                    SetParameter(doubleParameter.Name, doubleParameter.Value);

            if (boolParameters != null)
                foreach (var boolParameter in boolParameters)
                    SetParameter(boolParameter.Name, boolParameter.Value);

            if (stringParameters != null)
                foreach (var stringParameter in stringParameters)
                    SetParameter(stringParameter.Name, stringParameter.Value);
        }
    }

    public readonly struct IntParameter
    {
        public readonly string Name;
        public readonly int Value;

        public IntParameter(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }

    public readonly struct LongParameter
    {
        public readonly string Name;
        public readonly long Value;

        public LongParameter(string name, long value)
        {
            Name = name;
            Value = value;
        }
    }

    public readonly struct FloatParameter
    {
        public readonly string Name;
        public readonly float Value;

        public FloatParameter(string name, float value)
        {
            Name = name;
            Value = value;
        }
    }
    
    public readonly struct DoubleParameter
    {
        public readonly string Name;
        public readonly double Value;

        public DoubleParameter(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }

    public readonly struct BoolParameter
    {
        public readonly string Name;
        public readonly bool Value;

        public BoolParameter(string name, bool value)
        {
            Name = name;
            Value = value;
        }
    }

    public readonly struct StringParameter
    {
        public readonly string Name;
        public readonly string Value;

        public StringParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}