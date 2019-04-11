using System.Runtime.CompilerServices;
using System.Text;
using FestivalManager.Entities;

namespace FestivalManager.Core.Controllers
{
    using Contracts;
    using Entities.Contracts;
    using FestivalManager.Entities.Factories.Contracts;
    using System;
    using System.Linq;

    public class FestivalController : IFestivalController
    {
        private readonly IStage stage;
        private readonly ISetFactory setFactory;
        private readonly IInstrumentFactory instrumentFactory;

        public FestivalController(
            IStage stage,
            ISetFactory setFactory,
            IInstrumentFactory instrumentFactory)
        {
            this.stage = stage;
            this.setFactory = setFactory;
            this.instrumentFactory = instrumentFactory;
        }

        //Done
        public string RegisterSet(string[] args)
        {
            string name = args[0];
            string setTypeName = args[1];

            ISet set = this.setFactory.CreateSet(name, setTypeName);
            this.stage.AddSet(set);
            string result = $"Registered {setTypeName} set";
            return result;
        }

        //Done
        public string SignUpPerformer(string[] args)
        {
            var name = args[0];
            var age = int.Parse(args[1]);

            var instruments = args
                .Skip(2)
                .ToArray();

            var instrumentsName = instruments
                .Select(i => this.instrumentFactory.CreateInstrument(i))
                .ToArray();

            IPerformer performer = new Performer(name, age);

            foreach (var instrument in instrumentsName)
            {
                performer.AddInstrument(instrument);
            }

            this.stage.AddPerformer(performer);

            string result = $"Registered performer {performer.Name}";
            return result;
        }

        //Done
        public string RegisterSong(string[] args)
        {
            string name = args[0];
            int[] time = args[1]
                .Split(':')
                .Select(x => int.Parse(x))
                .ToArray();

            int songDurationMinutes = time[0];
            int songDurationSeconds = time[1];

            TimeSpan timeSpan = new TimeSpan(0, songDurationMinutes, songDurationSeconds);
            ISong song = new Song(name, timeSpan);
            this.stage.AddSong(song);
            string result = $"Registered song {name} ({timeSpan:mm\\:ss})";
            return result;
        }

        //Done
        public string AddSongToSet(string[] args)
        {
            var songName = args[0];
            var setName = args[1];

            if (!this.stage.HasSet(setName))
            {
                throw new InvalidOperationException("Invalid set provided");
            }

            if (!this.stage.HasSong(songName))
            {
                throw new InvalidOperationException("Invalid song provided");
            }

            var set = this.stage.GetSet(setName);
            var song = this.stage.GetSong(songName);

            set.AddSong(song);

            string result = $"Added {songName} ({song.Duration:mm\\:ss}) to {setName}";
            return result;
        }

        //Done
        public string AddPerformerToSet(string[] args)
        {
            var performerName = args[0];
            var setName = args[1];

            if (!this.stage.HasPerformer(performerName))
            {
                throw new InvalidOperationException("Invalid performer provided");
            }

            if (!this.stage.HasSet(setName))
            {
                throw new InvalidOperationException("Invalid set provided");
            }

            var performer = this.stage.GetPerformer(performerName);
            var set = this.stage.GetSet(setName);

            set.AddPerformer(performer);

            string result = $"Added {performer.Name} to {set.Name}";
            return result;
        }
        //Done
        public string RepairInstruments(string[] args)
        {
            var instrumentsToRepair = this.stage.Performers
                .SelectMany(p => p.Instruments)
                .Where(i => i.Wear < 100)
                .ToArray();

            foreach (var instrument in instrumentsToRepair)
            {
                instrument.Repair();
            }

            string result = $"Repaired {instrumentsToRepair.Length} instruments";
            return result;
        }

        public string ProduceReport()
        {
            StringBuilder sb = new StringBuilder();

            TimeSpan totalFestivalLength = new TimeSpan(this.stage.Sets.Sum(s => s.ActualDuration.Ticks));

            sb.AppendLine($"Festival length: {getRightFormat(totalFestivalLength)}");

            foreach (var set in this.stage.Sets)
            {
                sb.AppendLine($"--{set.Name} ({getRightFormat(set.ActualDuration)}):");

                var performersOrderedDescendingByAge = set.Performers.OrderByDescending(p => p.Age);
                foreach (var performer in performersOrderedDescendingByAge)
                {
                    var instruments = string.Join(", ", performer.Instruments
                        .OrderByDescending(i => i.Wear));

                    sb.AppendLine($"---{performer.Name} ({instruments})");
                }

                if (!set.Songs.Any())
                    sb.AppendLine("--No songs played");
                else
                {
                    sb.AppendLine("--Songs played:");
                    foreach (var song in set.Songs)
                    {
                        sb.AppendLine($"----{song.Name} ({getRightFormat(song.Duration)})");
                    }
                }
            }

            return sb.ToString().Trim();
        }

        private string getRightFormat(TimeSpan timeSpan)
        {

            int minutes = timeSpan.Hours * 60 + timeSpan.Minutes;
            int seconds = timeSpan.Seconds;

            string result = $"{minutes:d2}:{seconds:d2}";
            return result;
        }
    }
}