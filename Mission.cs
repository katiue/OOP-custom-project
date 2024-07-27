using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_custom_project
{
    public class Mission : GameObject
    {
        private string _status;
        private List<GameObject> _reward;
        private Func<Game, bool> _condition;
        public Mission(string[] ids, string name, string desc, string status, List<GameObject> reward, Func<Game, bool> condition) : base(ids, name, desc)
        {
            _status = status;
            _reward = reward;
            _condition = condition;
        }
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }
        public List<GameObject> Reward
        {
            get
            {
                return _reward;
            }
        }
        public Func<Game, bool> Condition
        {
            get
            {
                return _condition;
            }
        }
        public bool CheckCondition(Game game, Func<Game, bool> condition)
        {
            return condition(game);
        }
    }
}
