using BuilderPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RichiGames
{
    public class PlayerCharacter : Entity
    {
        public override void CharacterSetUp()
        {
            CharacterDirector director = new CharacterDirector();
            _character = director.CreatePlayer();

            //Debug.Log("Character: " + _character.Name);

            //foreach (var item in _character.Stats)
            //{
            //    Debug.Log(item.Value.Name + " " + item.Value.CurrentValue);
            //}
        }
    }
}
