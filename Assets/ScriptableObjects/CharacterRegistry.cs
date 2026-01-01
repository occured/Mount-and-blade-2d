using System.Collections.Generic;
using UnityEngine;
using MountAndBlade2D.Character;

namespace MountAndBlade2D.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CharacterRegistry", menuName = "MountAndBlade2D/CharacterRegistry")]
    public class CharacterRegistry : ScriptableObject
    {
        public List<CharacterStats> characters = new();

        public CharacterStats FindById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            foreach (var character in characters)
            {
                if (character != null && character.characterId == id)
                {
                    return character;
                }
            }

            return null;
        }
    }
}
