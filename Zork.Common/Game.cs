using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Zork
{
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler GameEnded;

        public World World { get; private set; }

        public string StartingLocation { get; set; }

        public string WelcomeMessage { get; set; }

        public string ExitMessage { get; set; }

        [JsonIgnore]
        public Player Player { get; private set; }

        [JsonIgnore]
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
                if (_isRunning == false)
                {
                    GameEnded?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public IInputService Input { get; set; }

        public IOutputService Output { get; set; }

        [JsonIgnore]
        public Dictionary<string, Command> Commands { get; private set; }

        public Game(World world, Player player)
        {
            World = world;
            Player = player;

            Commands = new Dictionary<string, Command>()
            {
                { "QUIT", new Command("QUIT", new string[] { "QUIT", "Q", "BYE" }, Quit) },
                { "LOOK", new Command("LOOK", new string[] { "LOOK", "L" }, Look) },
                { "NORTH", new Command("NORTH", new string[] { "NORTH", "N" }, game => Move(game, Directions.North)) },
                { "SOUTH", new Command("SOUTH", new string[] { "SOUTH", "S" }, game => Move(game, Directions.South)) },
                { "EAST", new Command("EAST", new string[] { "EAST", "E"}, game => Move(game, Directions.East)) },
                { "WEST", new Command("WEST", new string[] { "WEST", "W" }, game => Move(game, Directions.West)) },
                { "REWARD", new Command("REWARD", new string[] { "REWARD", "MONEYBAGS", "GIMME", "R" }, Reward) },
                { "SCORE", new Command("SCORE", new string[] { "SCORE" }, Score) }
            };
        }

        public void Start(IInputService input, IOutputService output)
        {
            Assert.IsNotNull(input);
            Input = input;
            Input.InputReceived += InputReceivedHandler;

            Assert.IsNotNull(output);
            Output = output;

            IsRunning = true;
        }

        private void InputReceivedHandler(object sender, string commandString)
        {
            Command foundCommand = null;
            foreach (Command command in Commands.Values)
            {
                if (command.Verbs.Contains(commandString))
                {
                    foundCommand = command;
                    break;
                }
            }

            if (foundCommand != null)
            {
                IncrementMoves();
                foundCommand.Action(this);
            }
            else
            {
                Output.WriteLine("Unknown command.");
            }
        }

        private void IncrementMoves() => Player.Moves++;

        private static void Move(Game game, Directions direction)
        {
            if (game.Player.Move(direction) == false)
            {
                game.Output.WriteLine("The way is shut!");
            }
        }

        private static void Reward(Game game)
        {
            game.Player.Score++;
            game.Output.WriteLine("Score increased!");
        }

        private static void Score(Game game) => game.Output.WriteLine($"Your score would be {game.Player.Score}, in {game.Player.Moves} move(s)");

        public static void Look(Game game) => game.Output.WriteLine(game.Player.Location.Description);

        private static void Quit(Game game) => game.IsRunning = false;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) => Player = new Player(World, StartingLocation);
    
        private bool _isRunning;
    }
}