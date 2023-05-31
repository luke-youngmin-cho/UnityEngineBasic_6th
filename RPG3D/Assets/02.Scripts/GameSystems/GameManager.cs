using RPG.Tools;
using RPG.GameElements;
using UnityEngine;

namespace RPG.GameSystems
{
    public class GameManager : SingletonMonoBase<GameManager>
    {
        public Player mine
        {
            get
            {
                if (_mine == null)
                    _mine = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                return _mine;
            }
        }
        private Player _mine;

        protected override void Init()
        {
            base.Init();
            DontDestroyOnLoad(gameObject);
        }

    }
}
