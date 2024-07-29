using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_custom_project
{
    public class Mission : GameObject
    {
        private readonly Func<Game, bool> _condition;
        public Mission(string[] ids, string name, string desc, string status, List<GameObject> reward, Func<Game, bool> condition) : base(ids, name, desc)
        {
            Status = status;
            Reward = reward;
            _condition = condition;
        }
        public string Status { get; set; }
        public List<GameObject> Reward { get; }
        public Func<Game, bool> Condition
        {
            get
            {
                return _condition;
            }
        }
        public static bool CheckCondition(Game game, Func<Game, bool> condition)
        {
            return condition(game);
        }
    }
}
