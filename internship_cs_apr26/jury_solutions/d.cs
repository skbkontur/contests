using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    public static void Main()
    {
        var reader = new StreamReader(Console.OpenStandardInput());
        var input = reader.ReadLine().Split();
        var totalDays = int.Parse(input[0]);
        var totalRooms = int.Parse(input[1]);
        var totalPeople = int.Parse(input[2]);

        var events = ReadEvents(totalPeople, reader);

        var freeRooms = new SortedSet<int>(Enumerable.Range(1, 2 * totalRooms - 1));
        var agentInfo = new Dictionary<int, (int EntryDay, int RoomId)>();
        var roomsInfo = new int[totalRooms];
        
        foreach (var @event in events)
        {
            var (day, isEntry, agentId) = @event;
            
            if (isEntry)
            {
                if (freeRooms.Count != 0)
                {
                    var minRoomId = freeRooms.Min;
                    freeRooms.Remove(minRoomId);
                    
                    agentInfo[agentId] = (day, minRoomId);
                }
            }
            else if (agentInfo.TryGetValue(agentId, out var info))
            {
                var (entryDay, entryRoomId) = info;
                freeRooms.Add(entryRoomId);
                
                if (entryRoomId >= totalRooms)
                {
                    roomsInfo[entryRoomId - totalRooms] += day - entryDay;
                }
            }
        }
        
        var (minDaysCount, minRoom) = roomsInfo.Select((daysCount, index) => (daysCount, index)).Min();
        Console.WriteLine($"{minDaysCount} {minRoom + 1}");
    }

    private static List<(int Day, bool IsEntry, int AgentId)> ReadEvents(int totalPeople, StreamReader reader)
    {
        var events = new List<(int Day, bool IsEntry, int AgentId)>(2 * totalPeople);
        
        for (var i = 0; i < totalPeople; i++)
        {
            var peopleInput = reader.ReadLine().Split();
            var entryDay = int.Parse(peopleInput[0]);
            var exitDay = int.Parse(peopleInput[1]);

            events.Add((entryDay, true, i));
            events.Add((exitDay, false, i));
        }
        
        events.Sort();
        
        return events;
    }
}