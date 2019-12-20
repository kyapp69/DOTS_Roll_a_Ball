﻿using Unity.Entities;
using Unity.Jobs;

/// GameManagerが進行の管理を把握するための情報を収集する
[UpdateInGroup(typeof(InitializationSystemGroup))]
public class GameStatusSystem : JobComponentSystem
{
    EntityQuery query, timerQuery;

    protected override void OnCreate()
    {
        query = GetEntityQuery(typeof(Cube));
        timerQuery = GetEntityQuery(ComponentType.ReadOnly<Timer>());
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var count = query.CalculateEntityCount();
        var timer = timerQuery.GetSingleton<Timer>().Value;
        inputDeps = Entities
            .ForEach((ref GameState state)=>{
                state.ItemCount = count;
                state.timer = timer;
            }).Schedule(inputDeps);

        return inputDeps;
    }
}