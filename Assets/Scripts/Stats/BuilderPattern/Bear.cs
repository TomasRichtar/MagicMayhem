using BuilderPattern;
using RichiGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RichiGames
{
    public class Bear : BasicEnemy
    {
        public override void CharacterSetUp()
        {
            CharacterDirector director = new CharacterDirector();
            _character = director.CreateBear();
        }
        public override void Attack()
        {

        }
    }
}
