using RPG.Tools;
using RPG.GameElements;
using UnityEngine;

namespace RPG.GameSystems
{
    public class GameManager : SingletonMonoBase<GameManager>
    {
        public PlayerController mine
        {
            get
            {
                if (_mine == null)
                    _mine = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                return _mine;
            }
        }
        private PlayerController _mine;

        protected override void Init()
        {
            base.Init();
            DontDestroyOnLoad(gameObject);
        }

    }
}
